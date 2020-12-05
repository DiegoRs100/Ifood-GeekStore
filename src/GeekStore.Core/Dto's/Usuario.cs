using System.Collections.Generic;

namespace GeekStore.Core.Dto_s
{
    public class UsuarioLogin
    {
        public string Login { get; private set; }
        public string Senha { get; private set; }
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserToken
    {
        public string Login { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }

    public class Claim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}