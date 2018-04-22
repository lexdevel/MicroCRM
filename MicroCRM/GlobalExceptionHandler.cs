using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MicroCRM.Extensions;
using MicroCRM.Models.Errors;

namespace MicroCRM
{
    public sealed class GlobalExceptionHandler : IExceptionFilter
    {
        #region Private members and constants

        private const string DefaultErrorMessage = "Unexpected error occurred";
        private readonly Environment _environment;

        #endregion

        public GlobalExceptionHandler(Environment environment)
        {
            _environment = environment;
        }

        public void OnException(ExceptionContext exceptionContext)
        {
            var vm = new ErrorViewModel
            {
                ErrorMessage = _environment.IsDevelopment() ? exceptionContext.Exception.Message : DefaultErrorMessage,
                StackTrace = _environment.IsDevelopment() ? exceptionContext.Exception.StackTrace : null
            };

            exceptionContext.Result = new ObjectResult(vm)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                DeclaredType = typeof(ErrorViewModel)
            };
        }
    }
}
