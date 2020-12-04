using System.Collections.Generic;

namespace GeekStore.Core.Dto_s
{
    public class ApiResponse<TClass> where TClass : class
    {
        public bool Success { get; set; }

        public TClass Data { get; set; }
        public IEnumerable<Notificacao> Errors { get; set; }
    }
}