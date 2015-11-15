using System.Linq;
using Infopulse.EDemocracy.Data.Repositories;
using Infopulse.EDemocracy.Web.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using Infopulse.EDemocracy.Common.Operations;
using Infopulse.EDemocracy.Web.CORS;
using Infopulse.EDemocracy.Web.Resources;

namespace Infopulse.EDemocracy.Web.Controllers.API
{
	[CorsPolicyProvider]
	[RoutePrefix("api/Account")]
	public class AccountController : ApiController
	{
		private AuthRepository authRepository = null;

		public AccountController()
		{
			authRepository = new AuthRepository();
		}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(UserModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await authRepository.RegisterUser(userModel);

			IHttpActionResult errorResult = GetErrorResult(result);

			if (errorResult != null)
			{
				return errorResult;
			}

			return Ok(OperationResult.Success(1, "Ви зареєстровані"));
		}

		/// <summary>
		/// Changes current user password.
		/// </summary>
		/// <returns>Operation result.</returns>
		[HttpPost]
		public OperationResult ChangePassword([FromUri]string currentPassword, [FromUri]string newPassword)
		{
			var result = OperationExecuter.Execute(() =>
			{
				var changePasswordResult = authRepository.ChangePassword(1, currentPassword, newPassword);

				if (changePasswordResult.Succeeded)
				{
					return OperationResult.Success(1, UserMessages.PasswordChanged_Success);
				}
				else
				{
					var errorMessage = changePasswordResult.Errors.Any()
						? string.Join(". ", changePasswordResult.Errors)
						: UserMessages.PasswordChanged_Fail_WrongCurrentPassword;
					return OperationResult.Fail(-2, errorMessage);
				}
			});

			return result;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				authRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}
