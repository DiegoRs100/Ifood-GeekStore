using GeekStore.Api.BuildingBlocks.ViewModels;
using GeekStore.Api.ViewModels;
using AutoMapper;
using GeekStore.Business.Dto_s;
using GeekStore.Core.Dto_s;

namespace GeekStore.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            AddBuildingBlocksMappings();
            AddBusinessMappings();
        }

        private void AddBusinessMappings()
        {
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }

        private void AddBuildingBlocksMappings()
        {
            CreateMap<UsuarioLogin, UsuarioLoginViewModel>().ReverseMap();
            CreateMap<UserToken, UserTokenViewModel>().ReverseMap();
            CreateMap<Claim, ClaimViewModel>().ReverseMap();
            CreateMap<LoginResponse, LoginResponseViewModel>().ReverseMap();

            CreateMap<Imagem, ImagemViewModel>().ReverseMap();
        }
    }
}