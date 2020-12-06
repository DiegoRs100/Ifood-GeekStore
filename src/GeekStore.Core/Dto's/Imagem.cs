using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GeekStore.Core.Dto_s
{
    public class Imagem : EntityBase
    {
        #region Properties

        public string Nome { get; private set; }
        public string Extensao { get; private set; }
        public string Path { get; private set; }

        [NotMapped]
        public string Base64 { get; private set; }

        [NotMapped]
        private byte[] Content { get; set; }

        #endregion

        public void DefinirNome(string nome)
        {
            Nome = nome;
        }

        public void DefinirExtensao(string extensao)
        {
            Extensao = extensao;
        }

        public void DefinirPath(string path)
        {
            Path = path;
        }

        public byte[] ObterArray()
        {
            if (Content?.Any() == true)
                return Content;

            if (!string.IsNullOrEmpty(Base64))
            {
                try
                {
                    return Convert.FromBase64String(Base64);
                }
                catch
                {
                    return default;
                }
            }

            return default;
        }
    }
}