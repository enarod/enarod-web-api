﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace Infopulse.EDemocracy.Web.Auth.Models
{
	public class ApplicationRole : IdentityRole<int, ApplicationUserRole>
	{
		public ApplicationRole() { }
		public ApplicationRole(string name) { Name = name; }
	}
}