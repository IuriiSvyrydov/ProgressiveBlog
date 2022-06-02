namespace ProgresiveBlog.API.Filters
{
    public class BlogExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var apiError = new ErrorResponse();
            apiError.StatusCode = 500;
            apiError.StatusParse = "Internal server";
            apiError.TimeStamp = DateTime.Now;
            apiError.Errrors.Add(context.Exception.Message);
            context.Result = new JsonResult(apiError) { StatusCode = 500};
        }
    }
}
