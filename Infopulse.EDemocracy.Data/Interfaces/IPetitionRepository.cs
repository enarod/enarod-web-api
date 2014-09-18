using Infopulse.EDemocracy.Model;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionRepository
	{
		PetitionWithVote Get(int petitionID);
		IEnumerable<PetitionWithVote> Get();
		Petition AddNewPetition(Petition newPetition);
        IEnumerable<PetitionWithVote> Search(string text);
		IEnumerable<PetitionWithVote> KeyWordSearch(string tag);
	}
}