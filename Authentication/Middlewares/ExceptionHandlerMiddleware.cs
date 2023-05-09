using Authentication.Base;
using Authentication.Base.Helpers;
using System.Net;
using System.Text.Json;

namespace Authentication.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(
            HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.OK;
                var resultBody = new BaseResponse<object>();
                switch (error)
                {
                    case AuthenticationException:
                        var ex = (AuthenticationException)error;
                        resultBody.ResponseCode = (int)ex.Code;
                        resultBody.ResponseMessage = GetExceptionMessage((ResponseCode)resultBody.ResponseCode);
                        break;
                    default:
                        resultBody.ResponseCode = (int)ResponseCode.InternalServerError;
                        resultBody.ResponseMessage = GetExceptionMessage(ResponseCode.InternalServerError);
                        break;
                }

                var result = JsonSerializer.Serialize(resultBody);
                await response.WriteAsync(result);
            }
        }

        private string? GetExceptionMessage(ResponseCode responseCode)
        {
            var messages = StringHelper.GetMessages();
            messages.TryGetValue(responseCode, out var message);
            return message;
        }

    }
}
