using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using clientEntities = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class AgreementRepository : BaseRepository, IAgreementRepository
	{
		public clientEntities.Agreement GetAgreement(int agreementID)
		{
			//return clientEntities.Agreement.GetAgreement(agreementID);
			using (var db = new EDEntities())
			{
				var result = from p in db.Agreements
							 where p.ID == agreementID
							 select p;

				var first = result.FirstOrDefault();
				if (first == default(Agreement))
				{
					throw new ApplicationException("Agreement not found {ID = " + agreementID.ToString() + "}");
				}

				var votes = (from p in db.AgreementVotes
							 where p.AgreementID == first.ID
							 select p);

				clientEntities.Agreement agreement = new clientEntities.Agreement(first) { NumberOfVotes = votes.Count() };
				return agreement;
			}
		}


		public List<clientEntities.Agreement> GetAgreements(bool withVotes = false)
		{
			if (withVotes)
				return this.GetAgreementsWithVotes();
			else
				return this.GetAgreements();
		}


		private List<clientEntities.Agreement> GetAgreements()
		{
			var list = new List<clientEntities.Agreement>();

			using (var db = new EDEntities())
			{
				var result = from p in db.Agreements
							 select p;

				foreach (var item in result)
				{
					list.Add(new clientEntities.Agreement(item) { NumberOfVotes = db.AgreementVotes.Count(v => v.AgreementID == item.ID) });
				}
			}

			return list;
		}


		private List<clientEntities.Agreement> GetAgreementsWithVotes()
		{
			List<clientEntities.Agreement> list = new List<clientEntities.Agreement>();
			//using (Infopulse.EDemocracy.Data.DemocracyEntities db = new DemocracyEntities())
			//{
			//    var result = from p in db.vwAgreements
			//                 select p;

			//    foreach (var item in result)
			//        list.Add(new Entities.Agreement()
			//        {
			//            ID = item.ID,
			//            Name = item.Name,
			//            ShortDesc = item.ShortDesc,
			//            Text = item.Text,
			//            NumberOfVotes = item.NumberOfVotes
			//        });
			//}

			return list;
		}


		//public AgreementVoteResponse Vote(Entities.Agreement agreement, Entities.Participant participant, string agreementText, string agreementHash, string signatureHash, string certificateThumbPrint, int issuer)
		//{
		//using (var db = new DemocracyEntities())
		//{
		//    var result = from p in db.AgreementVotes
		//                 where (p.AgreementID == agreement.ID || p.AgreementHash == agreementHash)
		//                    && (p.ParticipantID == participant.ID || p.SignatureHash == signatureHash || p.CertificateThumbPrint == certificateThumbPrint)
		//                 select p;

		//    if (result.Any())
		//    {
		//        AgreementVoteResponse avr = new AgreementVoteResponse();
		//        avr.ParticipantHasAlreadyVoted();
		//        return avr;
		//    }

		//    using (TransactionScope scope = new TransactionScope())
		//    {
		//        var participantID = SaveParticipant(participant, db);
		//        participant.ID = participantID;

		//        AgreementVote av = new AgreementVote()
		//                                {
		//                                    AgreementID = agreement.ID,
		//                                    ParticipantID = participant.ID,
		//                                    AgreementText = agreementText,
		//                                    AgreementHash = agreementHash,
		//                                    SignatureHash = signatureHash,
		//                                    CertificateThumbPrint = certificateThumbPrint,
		//                                    Issuer = issuer,
		//                                    CreatedDate = DateTime.UtcNow
		//                                };

		//        db.AgreementVotes.Add(av);
		//        db.SaveChanges();
		//        scope.Complete();

		//        AgreementVoteResponse avr = new AgreementVoteResponse();
		//        avr.SuccessVote();
		//        return avr;
		//    }
		//}
		//}




		//private Entities.Participant GetUnknownUser()
		//{
		//    return new Entities.Participant
		//           {
		//               FirstName = "Unknown",
		//               MiddleName = "Unknown",
		//               LastName = "Unknown",
		//               DOB = new DateTime(1991, 8, 24),
		//               Passport = "Unknown",
		//               TaxID = -1,
		//               adress = new Entities.Adress { City = "Kyiv", Country = "Ukraine", Street = "Khreshchatyk", ZIPCode = "03001" },
		//               contact = new Entities.Contact { Phone = "+380441111111", email = "email@example.com" }
		//           };
		//}

		///// <summary>Get users by TAX ID or passport.</summary>
		///// <remarks>This method has been created to workaround Roslyn bug</remarks>
		//private IEnumerable<Participant> GetParticipants(DemocracyEntities db, long? taxID, string passport)
		//{
		//    var participants = new List<Participant>();

		//    foreach (var participant in db.Participants)
		//    {
		//        if (participant.TaxID == taxID || participant.Passport.ToUpper() == passport.ToUpper())
		//        {
		//            participants.Add(participant);
		//        }
		//    }

		//    return participants;
		//}


		public OperationResult Vote(AgreementVote vote)
		{
			OperationResult result;

			using (var db = new EDEntities())
			{
				var certificate = db.Certificates.SingleOrDefault(c => c.SerialNumber == vote.Certificate.SerialNumber);
				if (certificate == null)
				{
					result = OperationResult.Fail(-4, "Certificate is not registered.");
					return result;
				}

				vote.CertificateID = certificate.ID;
				vote.PersonID = certificate.PersonID;
				vote.CreatedDate = DateTime.Now;

				vote.Agreement = null;
				vote.Certificate = null;
				vote.Person = null;

				//var votes = (from p in db.AgreementVotes
				//			 where p.AgreementID == vote.AgreementID
				//				&& (p.PersonID == vote.PersonID || p.CertificateID == vote.CertificateID || p.Certificate.SerialNumber == vote.Certificate.SerialNumber)
				//			 select p);

				//if (votes.Any())
				//	throw new ApplicationException("The agreement was voted");

				db.AgreementVotes.Add(vote);
				db.SaveChanges();

				result = OperationResult.Success(1, "Agreement vote has been registered.");
			}

			return result;
		}


		public OperationResult ClearVotes()
		{
			OperationResult result;

			try
			{
				using (var db = new EDEntities())
				{
					db.AgreementVotes.RemoveRange(db.AgreementVotes);
					db.SaveChanges();
					result = OperationResult.Success(1, "All agreement votes has beed deleted.");
				}
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}


			return result;
		}


		public OperationResult ClearVote(int agreementID)
		{
			OperationResult result;

			try
			{
				using (var db = new EDEntities())
				{
					db.AgreementVotes.RemoveRange(db.AgreementVotes.Where(v => v.AgreementID == agreementID));
					db.SaveChanges();
					result = OperationResult.Success(1, string.Format("Agreement votes for #{0} has beed deleted.", agreementID));
				}
			}
			catch (Exception exception)
			{
				result = OperationResult.ExceptionResult(exception);
			}


			return result;
		}
	}
}
