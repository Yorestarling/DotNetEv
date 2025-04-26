using Microsoft.AspNetCore.Mvc;

namespace Common.Responses
{
    public class HttpStatusResponseUtils
    {
        public static ActionResult<ResponseDto<T>> HttpResponse<T>(ResponseDto<T> eventResult)
        => new ObjectResult(eventResult) { StatusCode = (int?)eventResult.StatusCode };

    }

}

