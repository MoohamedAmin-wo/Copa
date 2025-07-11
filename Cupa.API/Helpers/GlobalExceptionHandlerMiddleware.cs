using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;


namespace Cupa.API.Helpers
{
    public class GlobalExceptionHandlerMiddleware : IExceptionHandler
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(IEmailSender emailSender, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"un handled exception \n Details {exception.Message}");

            var request = httpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var jsonBody = await reader.ReadToEndAsync();
                var jObject = JObject.Parse(jsonBody);

                // Access JSON properties
                var value = (string)jObject["Property1"]!;
                // Do something with the value

                var currentUser = httpContext.User.Identity?.Name ?? "Anonymous";

                var exceptionDetails = new
                {
                    RequestMethod = request.Method,
                    RequestPath = request.Path,
                    ExceptionMessage = exception.Message,
                    StackTrace = exception.StackTrace,
                    CurrentUser = currentUser,
                    Timestamp = DateTime.UtcNow,
                    Headers = request.GetTypedHeaders(),
                    Body = value

                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = new
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    error = "un Handled exception occurred !",
                    message = exception.Message
                };

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);
                await _emailSender.SendEmailAsync("spark_work@outlook.sa", "Exception Occurred with copa applicaion !",
                    $"<h3>Request :</h3> \n <p>{exceptionDetails.RequestMethod}</p> \n" +
                    $"\n <h3>RequestPath : </h3> \n <p> {exceptionDetails.RequestPath}</p>\n" +
                    $"\n <h3>Exception Message :</h3> \n <p>{exceptionDetails.ExceptionMessage}</p>\n" +
                    $"\n <h3>user : </h3> \n <p>{exceptionDetails.CurrentUser}</p> \n" +
                    $"\n <h3>Request Body : </h3> \n <p>{exceptionDetails.Body}</p>\n" +
                    $"\n <h3>Error occurred Time :</h3>\n <p>{exceptionDetails.Timestamp}</p>\n" +
                    $"\n <h3>Request Headers :</h3>\n <p>{exceptionDetails.Headers}</p>\n" +
                    $"\n <h3>Acutal Body :</h3>\n <p>{exceptionDetails.Body}</p>\n" +
                    $"\n <h3>Stack Trace :</h3>\n <p>{exceptionDetails.StackTrace}</p>");


                //await _whatsAppClient.SendMessage("+201099069607" , WhatsAppLanguageCode.English_US , "hello_world");

                return true;
            }
        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Position = 0;
            return body;
        }
    }
}
