using Resulty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Redirect.Application.Errors
{
    public static class ApplicationError
    {
        public static Error ResourceNotFoundError(string title = "Resource Not Found", string? detail = null)
        {
            return new()
            {
                Type = Type.ResourceNotFound,
                Title = title,
                Detail = detail
            };
        }

        public static class Type
        {
            public const string ResourceNotFound = "Resource Not Found";
        }
    }
}
