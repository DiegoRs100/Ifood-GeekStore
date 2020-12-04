using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System.Collections.Generic;

namespace GeekStore.Core.Services
{
    public class NotificationService : INotificationService
    {
        #region Properties

        private readonly List<Notificacao> _notificacoes;

        #endregion

        public NotificationService()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public bool ExisteNotificacao()
        {
            return _notificacoes.Count > 0;
        }
    }
}