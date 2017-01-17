using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VueJsDemo.Api.Infrastructure;

namespace VueJsDemo.Api.Filters
{
    public class ValidatesModelAttribute : TypeFilterAttribute
    {
        public ValidatesModelAttribute() : base(typeof(ValidatesModelFilterImpl))
        {
        }

        private class ValidatesModelFilterImpl : ActionFilterAttribute
        {
            private readonly IHostingEnvironment _env;

            public ValidatesModelFilterImpl(IHostingEnvironment env)
            {
                _env = env;
            }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    var errors = new List<object>();
                    foreach (var state in context.ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            var errorDto = new ErrorDto
                            {
                                Code = 500,
                                Message = error.ErrorMessage
                            };
                            if (error.ErrorMessage.IsNullOrWhiteSpace())
                            {
                                errorDto.Message = error.Exception.Message;
                            }
                            if (_env.IsDevelopment())
                            {
                                errorDto.StackTrace = error.Exception.StackTrace;
                            }
                            errors.Add(errorDto);
                        }
                    }
                    context.Result = new BadRequestObjectResult(new { errors });
                }
            }
        }
    }
}