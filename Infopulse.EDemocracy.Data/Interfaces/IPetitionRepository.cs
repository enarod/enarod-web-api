﻿using Infopulse.EDemocracy.Model;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IPetitionRepository
	{
		PetitionWithVote Get(int petitionID);
		IEnumerable<PetitionWithVote> Get(bool showPreliminaryPetitions = false);
		Petition AddNewPetition(Petition newPetition);
        IEnumerable<PetitionWithVote> Search(string text, bool showPreliminaryPetitions = false);
		IEnumerable<PetitionWithVote> CategorySearch(string text, bool showPreliminaryPetitions = false);
		IEnumerable<PetitionWithVote> KeyWordSearch(string tag, bool showPreliminaryPetitions = false);
	}
}