﻿using System.ComponentModel.DataAnnotations;

namespace Infopulse.EDemocracy.Web.Models
{
	public class UserModel
	{
		[Required]
		[Display(Name = "User email")]
		public string UserEmail { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}