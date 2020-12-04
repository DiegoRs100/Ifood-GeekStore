namespace GeekStore.Core.Dto_s
{
    public class Notificacao
    {
        #region Properties

        public string Mensagem { get; }
        public string Detalhe { get; }

        #endregion

        public Notificacao(string mensagem, string detalhe = null)
        {
            Mensagem = mensagem;
            Detalhe = detalhe;
        }
    }
}