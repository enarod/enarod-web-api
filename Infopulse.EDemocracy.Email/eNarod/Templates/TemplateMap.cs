using System.Collections.Generic;

namespace Infopulse.EDemocracy.Email.eNarod.Templates
{
	public class TemplateMap
	{
		public static Dictionary<Action, Template> Map { get; set; }


		static TemplateMap()
		{
			Map = new Dictionary<Action, Template>()
			      {
				      { Action.PetitionCreation, new PetitionCreatedTemaplte() },
					  { Action.PetitionEmailVote, new PetitionVoteTemplate() }
			      };
		}
	}
}