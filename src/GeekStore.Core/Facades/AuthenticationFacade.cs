using GeekStore.Core.Base;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Dto_s.Mappings;
using GeekStore.Core.Extentions;
using GeekStore.Core.Interfaces.BuildingBlocks;
using GeekStore.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using LoginResponse = GeekStore.Core.Facades.Dto_s.LoginResponse;

namespace GeekStore.Core.Facades
{
    public class AuthenticationFacade : FacadeBase, IAuthenticationFacade
    {
        #region Injection

        private readonly HttpClient _httpClient;

        public AuthenticationFacade (HttpClient httpClient,
                                     IOptions<ApiAuthenticationSettings> settings,
                                     INotificationService notificador) : base(notificador)
        {
            httpClient.BaseAddress = new Uri(settings.Value.UrlBase);
            _httpClient = httpClient;
        }

        #endregion

        public async Task<bool> Login(UsuarioLogin usuarioLogin)
        {
            if (!ExecutarValidacao(new UsuarioLoginValidation(), usuarioLogin))
                return false;

            _httpClient.SetBasicAuthentication(usuarioLogin.Login, usuarioLogin.Senha);

            var response = await _httpClient.PostAsync("/api/login", null);

            if (!response.IsValid())
                Notificar("Ocorreu um erro desconhecido durante a tentativa de Login.");

            var loginResponse = await response.ReadAsync<LoginResponse>();

            if (!loginResponse.Success)
            {
                Notificar(loginResponse.Error);
                return false;
            }

            return true;
        }
    }
}