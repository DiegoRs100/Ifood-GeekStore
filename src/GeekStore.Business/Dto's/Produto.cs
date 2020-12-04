using GeekStore.Core.Dto_s;
using System;

namespace GeekStore.Business.Dto_s
{
    public class Produto : EntityBase
    {
        public string Descricao { get; }
        public double Preco { get; }

        public Guid? IdImagem { get; }
        public Imagem Imagem { get; }
    }
}