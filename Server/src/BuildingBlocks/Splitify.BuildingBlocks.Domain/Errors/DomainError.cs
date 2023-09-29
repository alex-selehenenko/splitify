using Resulty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.BuildingBlocks.Domain.Errors
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

        public static Error ResourceNotFound(string title = "Resource Not Found", string? detail = null) => new()
        {
            Type = ErrorType.ResourceNotFound,
            Title = title,
            Detail = detail
        };
    }
}
