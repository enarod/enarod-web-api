using System.Collections.Generic;
using clientEntity = Infopulse.EDemocracy.Model.BusinessEntities;
using Infopulse.EDemocracy.Model.Common;


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