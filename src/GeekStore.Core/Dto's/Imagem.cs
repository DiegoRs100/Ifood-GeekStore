using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStore.Core.Dto_s
{
    public class Imagem : EntityBase
    {
        public string Path { get; }

        [NotMapped]
        public byte[] Content { get; }
    }
}