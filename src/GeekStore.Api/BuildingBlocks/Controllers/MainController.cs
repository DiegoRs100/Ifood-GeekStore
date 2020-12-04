using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace GeekStore.Api.BuildingBlocks.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        #region Properties

        protected Guid UsuarioId { get; private set; }
        protected bool UsuarioAutenticado { get; }

        #endregion Properties

        #region Injection

        private readonly INotificationService _notificador;
        public readonly ISessionApp _appSession;

        protected MainController(INotificationService notificador,
                                 ISessionApp appSession)
        {
            _notificador = notificador;
            _appSession = appSession;

            if (appSession.IsAuthenticated())
            {
                UsuarioId = appSession.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        #endregion

        protected bool OperacaoValida()
        {
            return !_notificador.ExisteNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes()
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotificarErroModelInvalida(modelState);

            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            foreach (var erro in modelState.Values.SelectMany(e => e.Errors))
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }
    }
}