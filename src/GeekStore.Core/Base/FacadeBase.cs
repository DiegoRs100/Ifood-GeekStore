using FluentValidation;
using FluentValidation.Results;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;

namespace GeekStore.Core.Base
{
    public abstract class FacadeBase
    {
        #region Injection

        private readonly INotificationService _notificador;

        protected FacadeBase(INotificationService notificador)
        {
            _notificador = notificador;
        }

        #endregion

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notificar(error.ErrorMessage);
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TValidacao, TEntidade>(TValidacao validacao, TEntidade entidade)
            where TValidacao : AbstractValidator<TEntidade>
            where TEntidade : EntityBase
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid)
                return true;

            Notificar(validator);

            return false;
        }
    }
}