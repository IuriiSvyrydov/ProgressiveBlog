using ProgresiveBlog.Application.Enums;

namespace ProgresiveBlog.Application.Models
{
    public class OperationResult<T>
    {
        public T Payload { get; set; }
        public bool IsError { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();

        public void AddError(ErrorCode errorCode,string message)
        {
            Errors.Add(new Error {Code = errorCode, Message = message});
            IsError = true;

        }

        public void ResetIsErrorFlag()
        {
            IsError = false;
        }

        public void HandleError(ErrorCode code,string message)
        {
            Errors.Add(new Error{Code = code,Message = message});
        }

        public void AddUnKnownError(string message)
        {
             HandleError(ErrorCode.UnknownError,message);
        }
    }
}
