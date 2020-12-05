using FluentValidation;

namespace GeekStore.Core.Dto_s.Mappings
{
    public class UsuarioLoginValidation : AbstractValidator<UsuarioLogin>
    {
        public UsuarioLoginValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser preenchido.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                    .WithMessage("O campo {PropertyName} precisa ser preenchido.");
        }
    }
}