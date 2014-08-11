using BE = Infopulse.EDemocracy.Model.BusinessEntities;

namespace Infopulse.EDemocracy.Model.ClientEntities
{
	public class EmailVote
	{
		 public BE.Petition petition { get; set; }
		 public string Email { get; set; }
	}
}