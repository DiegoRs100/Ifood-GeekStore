using GeekStore.Core.Dto_s;

namespace GeekStore.Core.Interfaces.BuildingBlocks
{
    public interface IAuthenticationService
    {
        LoginResponse GerarJwt(UsuarioLogin usuarioLogin);
    }
}