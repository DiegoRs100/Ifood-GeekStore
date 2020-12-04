﻿using Application.Api.BuildingBlocks.ViewModels;
using System;

namespace Application.Api.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }

        public string Descricao { get; set; }
        public double Preco { get; set; }

        public Guid? IdImagem { get; set; }
        public ImagemViewModel Imagem { get; set; }
    }
}