using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VueJsDemo.Api.Repository;

namespace VueJsDemo.Api.Filters
{
    public class ValidateContactExistsAttribute : TypeFilterAttribute
    {
        public ValidateContactExistsAttribute() : base(typeof(ValidateContactExistsAttribute))
        {
        }

        private class ValidateAuthorExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IContactsRepository _contactsRepository;

            public ValidateAuthorExistsFilterImpl(IContactsRepository contactsRepository)
            {
                _contactsRepository = contactsRepository;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as string;
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        if ((await _contactsRepository.ListAsync()).All(a => a.MobilePhone != id))
                        {
                            context.Result = new NotFoundObjectResult(id);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}