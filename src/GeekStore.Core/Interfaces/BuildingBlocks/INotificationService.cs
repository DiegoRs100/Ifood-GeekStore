using GeekStore.Core.Dto_s;
using System.Collections.Generic;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface INotificationService
    {
        bool ExisteNotificacao();

        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}