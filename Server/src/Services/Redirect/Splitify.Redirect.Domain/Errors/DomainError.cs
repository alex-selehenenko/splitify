using Resulty;

namespace Splitify.Redirect.Domain.Errors
{
    public static class DomainError
    {
        public static Error ValidationError(string title = "Validation Error", string? detail = null)
        {
            return new Error()
            {
                Type = Type.ValidationError,
                Title = title,
                Detail = detail
            };
        }

        public static class Type
        {
            public const string ValidationError = "Validation Error";
        }
    }
}
