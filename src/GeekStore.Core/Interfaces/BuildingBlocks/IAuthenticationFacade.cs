using GeekStore.Core.Dto_s;
using System.Threading.Tasks;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface IAuthenticationFacade
    {
        Task<bool> Login(UsuarioLogin usuarioLogin);
    }
}