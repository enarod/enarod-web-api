using System.Collections.Generic;
using Infopulse.EDemocracy.Common.Operations;
using clientEntity = Infopulse.EDemocracy.Model.BusinessEntities;


namespace Infopulse.EDemocracy.Data.Interfaces
{
    /// <summary>
    /// Interface for Agreement repository
    /// </summary>
    public interface IAgreementRepository
    {
		clientEntity.Agreement GetAgreement(int ID);
		List<clientEntity.Agreement> GetAgreements(bool Votes = false);
		OperationResult Vote(Model.AgreementVote vote);
	    OperationResult ClearVotes();
	    OperationResult ClearVote(int agreementID);
    }
}