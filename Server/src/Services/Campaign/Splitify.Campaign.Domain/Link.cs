using Resulty;
using Splitify.BuildingBlocks.Domain;
using Splitify.BuildingBlocks.Domain.Errors;
using Splitify.Shared.Services.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Campaign.Domain
{
    public class Link : Entity
    {
        public string Url { get; }

        internal Link(string id, string url, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Url = url;
        }
    }
}
