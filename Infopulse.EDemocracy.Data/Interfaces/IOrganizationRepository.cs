using Infopulse.EDemocracy.Model;
using System.Collections.Generic;

namespace Infopulse.EDemocracy.Data.Interfaces
{
	public interface IOrganizationRepository
	{
		IEnumerable<Organization> GetAll();
		Organization Get(int organizationID);
		Organization Add(Organization organizationToAdd);
		Organization Update(Organization organizationToUpdate);
		bool Delete(int organizationID);
	}
}