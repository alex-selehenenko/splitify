using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;

namespace Splitify.Redirect.Application.Errors
{
    public static class ApplicationError
    {
        public static Error ResourceNotFoundError(string title = "Resource Not Found", string? detail = null)
        {
            return new()
            {
                Type = ErrorType.ResourceNotFound,
                Title = title,
                Detail = detail
            };
        }
    }
}
