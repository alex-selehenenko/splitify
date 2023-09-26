using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;

namespace Splitify.Redirect.Domain.Errors
{
    public static class DomainError
    {
        public static Error ValidationError(string title = "Validation Error", string? detail = null)
        {
            return new Error()
            {
                Type = ErrorType.ValidationError,
                Title = title,
                Detail = detail
            };
        }

        public static Error InvalidOperationError(string title = "Invalid Operation Error", string? detail = null)
        {
            return new Error()
            {
                Type = ErrorType.InvalidOperationError,
                Title = title,
                Detail = detail
            };
        }
    }
}
