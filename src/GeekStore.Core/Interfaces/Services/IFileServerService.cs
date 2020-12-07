using GeekStore.Core.Dto_s;
using GeekStore.Core.Dto_s.Validations;
using System;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.Services
{
    public interface IFileServerService : IDisposable
    {
        Task<Imagem> SalvarImagem(Imagem imagem);
        string ObterUrlFileServer();
        Task<string> ObterBase64(Arquivo arquivo);
    }
}