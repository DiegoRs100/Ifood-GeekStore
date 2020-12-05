using Application.Api.BuildingBlocks.ViewModels;
using AutoMapper;
using GeekStore.Api.BuildingBlocks.Controllers;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Api.BuildingBlocks.Controllers
{
    [Route("api/v{version:apiVersion}/Authentication")]
    public class AuthenticationController : MainController
    {
        #region Injection

        private readonly IMapper _mapper;
        private readonly IAuthenticationFacade _authenticationFacade;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController (ISessionApp appSession,
                                         INotificationService notificador,
                                         IMapper mapper,
                                         IAuthenticationFacade authenticationFacade,
                                         IAuthenticationService authenticationService) : base(notificador, appSession)
        {
            _mapper = mapper;
            _authenticationFacade = authenticationFacade;
            _authenticationService = authenticationService;
        }

        #endregion

        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLoginViewModel usuarioLoginViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var resultadologin = await _authenticationFacade.Login(_mapper.Map<UsuarioLogin>(usuarioLoginViewModel));

            if (!resultadologin)
                return CustomResponse();

            return CustomResponse(_authenticationService.GerarJwt(_mapper.Map<UsuarioLogin>(usuarioLoginViewModel)));
        }
    }
}