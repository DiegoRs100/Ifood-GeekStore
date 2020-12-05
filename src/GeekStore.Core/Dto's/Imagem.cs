using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStore.Core.Dto_s
{
    public class Imagem : EntityBase
    {
        public string Path { get; private set; }
        public string Extensao { get; private set; }

        [NotMapped]
        public byte[] Content { get; private set; }
    }
}