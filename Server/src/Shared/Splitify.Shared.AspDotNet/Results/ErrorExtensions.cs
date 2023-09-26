using Microsoft.AspNetCore.Mvc;
using Resulty;
using Splitify.BuildingBlocks.Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Shared.AspDotNet.Results
{
    public static class ErrorExtensions
    {
        public static ProblemDetails ToProblemDetails(this Error error)
        {
            return new()
            {
                Title = error.Title,
                Detail = error.Detail,
                Type = error.Type,
                Status = MapErrorTypeToStatusCode(error.Type)
            };
        }

        private static int MapErrorTypeToStatusCode(string type)
        {
            return type switch
            {
                ErrorType.ResourceNotFound => 404,
                ErrorType.ValidationError => 400,
                ErrorType.InvalidOperationError => 500,
                _ => 500
            }; ;
        }
    }
}
