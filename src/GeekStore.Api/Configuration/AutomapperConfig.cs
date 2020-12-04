using Application.Api.BuildingBlocks.ViewModels;
using Application.Api.ViewModels;
using AutoMapper;
using GeekStore.Business.Dto_s;
using GeekStore.Core.Dto_s;

namespace GeekStore.Api.Configuration
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
            CreateMap<Imagem, ImagemViewModel>().ReverseMap();
        }
    }
}