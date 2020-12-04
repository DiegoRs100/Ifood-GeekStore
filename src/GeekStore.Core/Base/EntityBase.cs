using System;

namespace GeekStore.Core.Dto_s
{
    public abstract class EntityBase
    {
        #region Properties   

        public Guid Id { get; private set; }

        public DateTime DataInclusao { get; }
        public Guid IdUsuarioInclusao { get; private set; }

        public DateTime? DataAlteracao { get; private set; }
        public Guid? IdUsuarioAlteracao { get; private set; }

        public bool Ativo { get; private set; }

        #endregion Properties

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public void DefnirId(Guid id)
        {
            Id = id;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }
    }
}