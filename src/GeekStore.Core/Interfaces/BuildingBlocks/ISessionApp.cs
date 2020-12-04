using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface ISessionApp
    {
        string Name { get; }

        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}