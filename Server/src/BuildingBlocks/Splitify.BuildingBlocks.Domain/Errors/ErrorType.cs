using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.BuildingBlocks.Domain.Errors
{
    public class ErrorType
    {
        public const string ValidationError = "Validation.Error";

        public const string ResourceNotFound = "ResourceNotFound.Error";

        public const string InvalidOperationError = "InvalidOperation.Error";
    }
}
