using GeekStore.Core.Dto_s;
using System;

namespace GeekStore.Business.Dto_s
{
    public class Produto : EntityBase
    {
        public string Nome { get; private set; }
        public double Preco { get; private set; }

        public Guid? IdImagem { get; private set; }
        public Imagem Imagem { get; private set; }
    }
}