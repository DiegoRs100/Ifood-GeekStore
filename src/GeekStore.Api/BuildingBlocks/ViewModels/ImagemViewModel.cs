﻿using System;

namespace GeekStore.Api.BuildingBlocks.ViewModels
{
    public class ImagemViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public string Base64 { private get; set; }
        public string Url { get; private set; }
    }
}