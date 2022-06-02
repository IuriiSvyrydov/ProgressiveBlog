namespace ProgresiveBlog.API.Controllers.v1
{
    public class BaseController : ControllerBase
    {
         protected IActionResult HandleErrorResponse(List<Error>errors)
        {
            var apiError = new ErrorResponse();
            if (errors.Any(e => e.Code == ErrorCode.NotFound))
            {
                var err = errors.FirstOrDefault(e => e.Code == ErrorCode.NotFound);
                apiError.StatusCode = 404;
                apiError.StatusParse = "Not found";
                apiError.TimeStamp = DateTime.Now;
                apiError.Errrors.Add(err.Message);
                return NotFound(apiError);
            }
           // var error = errors.FirstOrDefault(e => e.Code == ErrorCode.ServerError);
            apiError.StatusCode = 400;
                apiError.StatusParse = "Bad request";
                apiError.TimeStamp = DateTime.Now;
            errors.ForEach(e => apiError.Errrors.Add(e.Message));
                //return NotFound(apiError);
                return StatusCode(400,apiError);
            
        }
    }
}
