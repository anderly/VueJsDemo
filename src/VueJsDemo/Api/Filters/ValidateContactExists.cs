using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VueJsDemo.Api.Repository;

namespace VueJsDemo.Api.Filters
{
    /// <summary>
    /// Validates the contact record exists first prior to proceeding.
    /// Used on GET, PUT and DELETE requests to make sure the record exists when we try to read, update or delete it.
    /// </summary>
    public class ValidateContactExistsAttribute : TypeFilterAttribute
    {
        public ValidateContactExistsAttribute() : base(typeof(ValidateContactExistsFilterImpl))
        {
        }

        private class ValidateContactExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IContactsRepository _contactsRepository;

            public ValidateContactExistsFilterImpl(IContactsRepository contactsRepository)
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