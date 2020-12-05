using GeekStore.Api.BuildingBlocks.Extentions;
using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace GeekStore.Api.BuildingBlocks
{
    public class AppSession : ISessionApp
    {
        #region Properties

        public string Name => _accessor.HttpContext.User.Identity.Name;

        #endregion

        #region Injection

        private readonly IHttpContextAccessor _accessor;

        public AppSession(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        #endregion

        public Guid GetUserId()
        {
            // Caso o usuário não seja identificado, assume-se o ID do usuário Default do sistema.
            return IsAuthenticated()
                ? Guid.Parse(_accessor.HttpContext.User.GetUserId())
                : new Guid("551ed650-c290-4daa-bba5-eed3ba221645");
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }
}