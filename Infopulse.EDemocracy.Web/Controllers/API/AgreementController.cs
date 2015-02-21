using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Data.Interfaces;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Model.ClientEntities;
using Infopulse.EDemocracy.Model.Common;
using Infopulse.EDemocracy.Web.CORS;
using Agreement = Infopulse.EDemocracy.Model.BusinessEntities.Agreement;
using AgreementVote = Infopulse.EDemocracy.Model.AgreementVote;
using Certificate = Infopulse.EDemocracy.Model.Certificate;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	[CorsPolicyProvider]
	public class AgreementController : BaseApiController
	{
		private readonly IAgreementRepository agreementRepository;

		public AgreementController()
		{
			// should be refactored after DI functionality added
			this.agreementRepository = new AgreementRepository();
		}

		public AgreementController(IAgreementRepository testAgreementRepository)
		{
			this.agreementRepository = testAgreementRepository;
		}


		[HttpGet]
		public OperationResult<IEnumerable<Agreement>> Get()
		{
			OperationResult<IEnumerable<Agreement>> result;

			try
			{
				var agreements = this.agreementRepository.GetAgreements();// (votes.Value);
				result = OperationResult<IEnumerable<Agreement>>.Success(agreements);
			}
			catch (Exception exception)
			{
				result = OperationResult<IEnumerable<Agreement>>.ExceptionResult(exception);
			}

			return result;
		}


		[HttpGet]
		public OperationResult<Agreement> Get(int id)
		{
			OperationResult<Agreement> result;

			try
			{
				var agreement = this.agreementRepository.GetAgreement(id);
				result = OperationResult<Model.BusinessEntities.Agreement>.Success(1, "Success", agreement);
			}
			catch (Exception exc)
			{
				result = OperationResult<Model.BusinessEntities.Agreement>.ExceptionResult(exc);
			}

			return result;
		}


		[HttpPost]
		public OperationResult Post(ClientAgreementVote vote)
		{
			OperationResult result;

			try
			{
				if (vote.AgreementID == 0)
				{
					result = OperationResult.Fail(-2, "AgreementID was not provided.");
					return result;
				}

				// verify:
				IVerificationRepository verificationRepository;
				switch (vote.CertificateType)
				{
					case EntityDictionary.Certificate.Type.DPA:
						{
							verificationRepository =
								// BUG: DPA verification server doesn't work properly
								//new DpaVerificationRepository();
								new UaCryptoVerificationRepository();
							break;
						}
					case EntityDictionary.Certificate.Type.UACrypto:
						{
							verificationRepository = new UaCryptoVerificationRepository();
							break;
						}
					default:
						{
							verificationRepository = new UaCryptoVerificationRepository();
							break;
						}
				}

				var verificationResult = verificationRepository.Verify(vote.SignedData);
				var isVerficationSuccessfull =
					verificationResult.Descendants("Result").SingleOrDefault() != null &&
					verificationResult.Descendants("Result").SingleOrDefault().Value == "Success" &&
					verificationResult.Descendants("Serial").SingleOrDefault() != null &&
					verificationResult.Descendants("Serial").SingleOrDefault().Value.Length > 0;

				if (!isVerficationSuccessfull)
				{
					result = OperationResult.Fail(-3, "Certificate verification failed.");
					return result;
				}

				var agreementVote = new AgreementVote
									{
										AgreementID = vote.AgreementID,
										Certificate = new Certificate() { SerialNumber = verificationResult.Descendants("Serial").SingleOrDefault().Value },
										SignedData = vote.SignedData,
										SignedHash = vote.Signature
									};
				result = this.agreementRepository.Vote(agreementVote);
			}
			catch (Exception exc)
			{
				result = OperationResult.ExceptionResult(exc);
			}

			return result;
		}


		[HttpDelete]
		public OperationResult Delete()
		{
			var result = this.agreementRepository.ClearVotes();
			return result;
		}


		[HttpDelete]
		public OperationResult Delete([FromUri] int id)
		{
			var result = this.agreementRepository.ClearVote(id);
			return result;
		}
	}
}