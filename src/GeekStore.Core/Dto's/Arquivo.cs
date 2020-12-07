using GeekStore.Core.Extentions.Exceptions;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GeekStore.Core.Dto_s.Validations
{
    public abstract class Arquivo : EntityBase
    {
        #region Properties

        public string Nome { get; private set; }
        public string Extensao { get; private set; }

        public string NomeArquivoFileServer => $"{ Id }.{ Extensao }";

        [NotMapped]
        public string Url { get; private set; }

        [NotMapped]
        public string Base64 { get; private set; }

        [NotMapped]
        private byte[] Content { get; set; }

        #endregion

        public void DefinirExtensao(string extensao)
        {
            Extensao = extensao?.RemoveText(".").ToLower();
        }

        public void DefinirUrl(string urlFileServer)
        {
            Url = $"{urlFileServer}{Id}.{Extensao}";
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