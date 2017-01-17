using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VueJsDemo.Api.Infrastructure;

namespace VueJsDemo.Api.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
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
                        errors.Add(errorDto);
                    }
                }
                context.Result = new BadRequestObjectResult(new { errors} );
            }
        }
    }
}