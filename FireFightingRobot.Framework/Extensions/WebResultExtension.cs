using Microsoft.AspNetCore.Mvc;

namespace FireFightingRobot.Framework.Extensions
{
    /// <summary>
    /// Extends the result class with methods that are only related to Web API
    /// </summary>
    public static class WebResultExtensions
    {
        public static IActionResult ToActionResult(this Result result) =>
            result.Success
                ? new OkResult() as IActionResult
                : new BadRequestObjectResult(result.Error);

        public static IActionResult ToActionResult<T>(this Result<T> result) =>
            result.Success
                ? new OkObjectResult(result.Value) as IActionResult
                : new BadRequestObjectResult(result.Error);

        public static IActionResult ToActionResult<T>(this Task<Result<T>> result) =>
            result.Result.Success
                ? new OkObjectResult(result.Result.Value) as IActionResult
                : new BadRequestObjectResult(result.Result.Error);

        public static IActionResult ToActionResult(this Task<Result> result) =>
            result.Result.Success
                ? new OkObjectResult(null) as IActionResult
                : new BadRequestObjectResult(result.Result.Error);

    }
}
