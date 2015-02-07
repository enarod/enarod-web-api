using System;
using System.Data.Entity.Migrations.Model;

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
		/// Debug message. Should not be displayed to user.
		/// </summary>
		public string DebugMessage { get; set; }


		/// <summary>
		/// Indicates whether operation was successful or not.
		/// </summary>
		public bool IsSuccess
		{
			get { return this.ResultCode > 0; }
		}


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


		public static OperationResult CopyFrom(OperationResult otherResult)
		{
			return new OperationResult
				   {
					   ResultCode = otherResult.ResultCode,
					   Message = otherResult.Message
				   };
		}


		/// <summary>
		/// Sets failed operation result caused by unhandled exception.
		/// </summary>
		/// <param name="exc">Exception that causued operation failure.</param>
		/// <param name="resultCode">Optional result code.</param>
		/// <returns>failed operation result.</returns>
		public static OperationResult ExceptionResult(Exception exc, int resultCode = -1)
		{
			var innerExceptionMessage = OperationResult.GetInnerException(exc).Message;
			return new OperationResult
				   {
					   ResultCode = resultCode,
					   Message = "Unhandled exception has occued. Please contact the administrator.",
					   DebugMessage = innerExceptionMessage
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