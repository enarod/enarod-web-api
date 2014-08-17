using Infopulse.EDemocracy.Model.Common;
using System.Collections.Generic;
using E = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionRepository
	{
		OperationResult<E.Petition> Get(int petitionID);
		OperationResult<IEnumerable<E.Petition>> Get();
		OperationResult AddNewPetition(E.Petition newPetition);
        OperationResult<IEnumerable<E.Petition>> Search(string text);
		OperationResult<IEnumerable<E.Petition>> KeyWordSearch(string tag);
	}
}