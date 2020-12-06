using GeekStore.Api.BuildingBlocks.ViewModels;
using System;

namespace GeekStore.Api.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public double Preco { get; set; }

        public Guid? IdImagem { get; set; }
        public ImagemViewModel Imagem { get; set; }
    }
}