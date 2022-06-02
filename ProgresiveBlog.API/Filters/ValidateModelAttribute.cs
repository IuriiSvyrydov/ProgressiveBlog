namespace ProgresiveBlog.API.Filters
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorResponse();

                apiError.StatusCode = 404;
                apiError.StatusParse = "Bad request";
                apiError.TimeStamp = DateTime.Now;
                var errors = context.ModelState.AsEnumerable();
                foreach (var error in errors)
                {
                    apiError.Errrors.Add(error.Value.ToString());
                }
                context.Result =  new ObjectResult(apiError);
                return;

            }
        }
    }
}
