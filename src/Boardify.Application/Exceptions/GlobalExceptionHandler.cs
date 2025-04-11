using Boardify.Application.Exceptions.Custom;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace Boardify.Application.Exceptions
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandler(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            string responseMessage = "Internal Server Error! Please try again later.";
            var responseModel = new ExceptionResponseModel();

            switch (exception)
            {
                case ValidationException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseModel.ValidationErrors = ex.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
                    responseMessage = string.Join("; ", ex.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                    break;


                case TokenCreationException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseMessage = ex.Message;
                    break;

                case LoginProcessingException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseMessage = ex.Message;
                    break;

                case InvalidCredentialsException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseMessage = ex.Message;
                    break;

                case UnauthorizedAccessException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseMessage = ex.Message;
                    break;

                case AuthenticationException ex:
                    statusCode = HttpStatusCode.Unauthorized;
                    responseMessage = ex.Message;
                    break;

                case ApplicationException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseMessage = ex.Message;
                    break;

                case FileNotFoundException ex:
                    statusCode = HttpStatusCode.NotFound;
                    responseMessage = ex.Message;
                    break;

                case NotFoundException ex:
                    statusCode = HttpStatusCode.NotFound;
                    responseMessage = ex.Message;
                    break;

                case AuthorizationExcepction ex:
                    statusCode = HttpStatusCode.Unauthorized;
                    responseMessage = ex.Message;
                    break;

                case ForbiddenException ex:
                    statusCode = HttpStatusCode.Forbidden;
                    responseMessage = ex.Message;
                    break;

                case BadRequestException ex:
                    statusCode = HttpStatusCode.BadRequest;
                    responseMessage = ex.Message;
                    break;

                case BussinessException ex:
                    statusCode = HttpStatusCode.Conflict;
                    responseMessage = ex.Message;
                    break;

                default:
                    Log.Error(exception, "An unexpected error occurred");
                    break;
            }

            if (_env.IsDevelopment())
            {
                responseModel.StackTrace = exception.StackTrace;
                responseMessage = exception.Message;
            }

            responseModel.ResponseCode = (int)statusCode;
            responseModel.ResponseMessage = responseMessage;

            Log.Error(exception, responseMessage);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseModel));
        }
    }
}