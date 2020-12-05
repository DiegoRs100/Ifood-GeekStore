using System.Collections.Generic;

namespace GeekStore.Core.Dto_s
{
    public class ApiResponse<TClass> where TClass : class
    {
        #region Properties

        public bool Success { get; set; }

        public TClass Data { get; set; }
        public IEnumerable<Notificacao> Errors { get; set; }

        #endregion

        public ApiResponse()
        { }

        public ApiResponse(Notificacao notificacao)
        {
            Success = false;

            Errors = new List<Notificacao>()
            {
                notificacao
            };
        }
    }
}