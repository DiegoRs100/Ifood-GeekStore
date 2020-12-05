using FluentValidation;
using FluentValidation.Results;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System.Threading.Tasks;

namespace GeekStore.Core.Base
{
    public abstract class ServiceBase<TEntity, TValidation> : IServiceBase<TEntity>
        where TEntity : EntityBase
        where TValidation : AbstractValidator<TEntity>, new()
    {
        #region Injection

        protected readonly INotificationService _notificador;
        private readonly IRepositoryBase<TEntity> _entityRepository;

        protected ServiceBase(INotificationService notificador, IRepositoryBase<TEntity> entityRepository)
        {
            _notificador = notificador;
            _entityRepository = entityRepository;
        }

        #endregion

        public virtual async Task<TEntity> Atualizar(TEntity entity)
        {
            if (!ExecutarValidacao(new TValidation(), entity))
                return null;

            if (_entityRepository.ObterPorId(entity.Id).Result?.Ativo != true)
            {
                Notificar("Registro inexistente.");
                return null;
            }

            await _entityRepository.UpdateAndSave(entity);
            return entity;
        }

        public virtual async Task<TEntity> Inserir(TEntity entity)
        {
            if (!ExecutarValidacao(new TValidation(), entity))
                return null;

            await _entityRepository.InsertAndSave(entity);
            return entity;
        }

        public virtual async Task<bool> Remover(TEntity entity)
        {
            await _entityRepository.DeleteAndSave(entity);
            return true;
        }

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