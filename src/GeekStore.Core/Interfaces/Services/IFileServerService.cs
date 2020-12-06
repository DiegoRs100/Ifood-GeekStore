using GeekStore.Core.Dto_s;
using System;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.Services
{
    public interface IFileServerService : IDisposable
    {
        public Task<Imagem> SalvarImagem(Imagem imagem);
    }
}