using System.Collections.Generic;

namespace GeekStore.Core.Dto_s
{
    public class UsuarioLogin : EntityBase
    {
        public string Login { get; set;  }
        public string Senha { get; set; }
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