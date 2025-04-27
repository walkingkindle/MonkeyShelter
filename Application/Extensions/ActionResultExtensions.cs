using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Extensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestObjectResult(result.Error);
            }

        }
    }
}
