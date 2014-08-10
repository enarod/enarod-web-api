using Infopulse.EDemocracy.Data.Interfaces;

namespace Infopulse.EDemocracy.Data.Repositories
{
	public class ParticipantRepository : BaseRepository, IParticipantRepository
	{
        //public Entities.Participant GetParticipant(string certificateThumbPrint)
        //{
        //    using (DemocracyEntities db = new DemocracyEntities())
        //    {
        //        var result = from p in db.AgreementVotes
        //                     where p.CertificateThumbPrint == certificateThumbPrint
        //                     select p.Participant;

        //        var first = result.FirstOrDefault();
        //        if (first != default(Participant))
        //        {
        //            return new Entities.Participant()
        //            {
        //                ID = first.ID,
        //                FirstName = first.FirstName,
        //                LastName = first.LastName,
        //                MiddleName = first.MiddleName,
        //                TaxID = first.TaxID,
        //                Passport = first.Passport,
        //                DOB = first.DOB
        //            };

        //        }

        //        return null;
        //    }
        //}
	}
}