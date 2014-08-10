using System;

namespace Infopulse.EDemocracy.Model.Common
{
	/// <summary>
	/// Represents result of operation that does not returns any data (returns void).
	/// </summary>
	public class OperationResult
	{
		/// <summary>
		/// Operation result code.
		/// </summary>
		public int ResultCode { get; set; }


		/// <summary>
		/// Operation result message.
		/// </summary>
		public string Message { get; set; }


		/// <summary>
		/// Sets general successful operation result.
		/// </summary>
		/// <returns>Successful operation result.</returns>
		public static OperationResult Success()
		{
			return new OperationResult
				   {
					   ResultCode = 1,
					   Message = "Success"
				   };
		}


		/// <summary>
		/// Sets successful operation result with specific code and message.
		/// </summary>
		/// <param name="resultCode">Operation result code to set.</param>
		/// <param name="message">Operation result message to set.</param>
		/// <returns>Successful operation result.</returns>
		public static OperationResult Success(int resultCode, string message)
		{
			return new OperationResult
			{
				ResultCode = resultCode,
				Message = message
			};
		}


		/// <summary>
		/// Sets general failed operation result.
		/// </summary>
		/// <returns>Failed operation result.</returns>
		public static OperationResult Fail()
		{
			return OperationResult.Fail(-1, "General error");
		}


		/// <summary>
		/// Sets failed operation result specified by code and message.
		/// </summary>
		/// <param name="resultCode">Operation result code.</param>
		/// <param name="message">Operation result message.</param>
		/// <returns>Failed operation result.</returns>
		public static OperationResult Fail(int resultCode, string message)
		{
			return new OperationResult
			{
				ResultCode = resultCode,
				Message = message
			};
		}


		/// <summary>
		/// Sets failed operation result caused by unhandled exception.
		/// </summary>
		/// <param name="exc">Exception that causued operation failure.</param>
		/// <returns>failed operation result.</returns>
		public static OperationResult ExceptionResult(Exception exc)
		{
			return new OperationResult
				   {
					   ResultCode = -1,
					   Message = string.Format(
							"Unhandled exception has occued.{0}{1}",
							Environment.NewLine,
							OperationResult.GetInnerException(exc).Message)
				   };
		}


		/// <summary>
		/// Gets last inner exception.
		/// </summary>
		/// <param name="exception">Parent exception.</param>
		/// <returns>Last child exception.</returns>
		protected static Exception GetInnerException(Exception exception)
		{
			var innerException = exception;

			while (innerException.InnerException != null)
			{
				innerException = innerException.InnerException;
			}

			return innerException;
		}
	}
}