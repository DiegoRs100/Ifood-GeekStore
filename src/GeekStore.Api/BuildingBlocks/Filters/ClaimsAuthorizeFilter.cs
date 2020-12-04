using GeekStore.Core.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace GeekStore.Api.BuildingBlocks.Filters
{
    public class ClaimsAuthorizeFilter : IAuthorizationFilter
    {
        #region Injection

        private readonly Claim _claim;

        public ClaimsAuthorizeFilter(Claim claim)
        {
            _claim = claim;
        }

        #endregion

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(HttpStatusCode.Unauthorized.ToInt());
                return;
            }

            var validClaims = context.HttpContext.User.Identity.IsAuthenticated &&
               context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value.Contains(_claim.Value));

            if (!validClaims)
                context.Result = new StatusCodeResult(HttpStatusCode.MethodNotAllowed.ToInt());
        }
    }
}