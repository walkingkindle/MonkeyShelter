using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Extensions
{
    public static class ResultExtensions
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

        public static Result<K> OnSuccess<T,K>(this Result<T> result, Func<T,K> func)
        {
            if (result.IsFailure)
            {
                return Result.Failure<K>(result.Error);
            }

            return Result.Success(func(result.Value));
        }
    }
}
