using System;

namespace Infopulse.EDemocracy.Model.Common
{
	/// <summary>
	/// Represents operation result upon specific data type.
	/// </summary>
	/// <typeparam name="T">Type of data being processed.</typeparam>
	public class OperationResult<T> : OperationResult
	{
		/// <summary>
		/// Returned data.
		/// </summary>
		public T Data { get; set; }


		/// <summary>
		/// Sets successful operation result with specific data.
		/// </summary>
		/// <param name="data">Data that has been returned as operation result.</param>
		/// <returns>Successful operation result with data.</returns>
		public static OperationResult<T> Success(T data)
		{
			return OperationResult<T>.Success(1, "Success", data);
		}


		/// <summary>
		/// Sets successful operation result with specific code, message and data.
		/// </summary>
		/// <param name="resultCode">Result code.</param>
		/// <param name="message">Result message.</param>
		/// <param name="data">Data that has been returned as operation result.</param>
		/// <returns>Successful operation result with specific code, messsage and data.</returns>
		public static OperationResult<T> Success(int resultCode, string message, T data)
		{
			return new OperationResult<T>
			{
				ResultCode = resultCode,
				Message = message,
				Data = data
			}; 
		}


		/// <summary>
		/// Sets general failed operation result.
		/// </summary>
		/// <returns>General failure result.</returns>
		public static new OperationResult<T> Fail()
		{
			return OperationResult<T>.Fail(-1, "General error");
		}


		/// <summary>
		/// Sets failed operation result with specific code and message.
		/// </summary>
		/// <param name="resultCode">Failed operation code.</param>
		/// <param name="message">Failed operation message.</param>
		/// <returns>Failed operation result.</returns>
		public static new OperationResult<T> Fail(int resultCode, string message)
		{
			return new OperationResult<T>
				{
					ResultCode = resultCode,
					Message = message,
					Data = default(T)
				};
		}

		
		public static OperationResult<T> CopyFrom(OperationResult originalResult, T data = default(T))
		{
			return new OperationResult<T>
			       {
				       ResultCode = originalResult.ResultCode,
				       Message = originalResult.Message,
				       Data = data
			       };
		}
		

		/// <summary>
		/// Sets failed operation result casued by unhandled exception.
		/// </summary>
		/// <param name="exc">Exception that caused failure.</param>
		/// <returns>Failed operation result.</returns>
		public static new OperationResult<T> ExceptionResult(Exception exc)
		{
			var innerExceptionMessage = OperationResult.GetInnerException(exc).Message;
			return new OperationResult<T>
			{
				ResultCode = -1,
				Message = string.Format("Unhandled exception has occued. Please contact the administrator.{0}Message:{0}{1}", Environment.NewLine, innerExceptionMessage),
				DebugMessage = innerExceptionMessage,
				Data = default(T)
			};
		}
	}
}