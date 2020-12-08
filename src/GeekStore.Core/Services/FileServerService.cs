using GeekStore.Core.Base;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Dto_s.Validations;
using GeekStore.Core.Interfaces.BuildingBlocks;
using GeekStore.Core.Interfaces.Repositories;
using GeekStore.Core.Interfaces.Services;
using GeekStore.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Core.Services
{
    public class FileServerService : ServiceBase, IFileServerService
    {
        #region Injection

        public readonly IImagemRepository _imagemRepository;
        public readonly IGeekStoreDbContextService _geekStoreDbContextService;
        public readonly FileServerSettings _fileServerSettings;

        public FileServerService (IImagemRepository imagemRepository,
                                  IGeekStoreDbContextService geekStoreDbContextService,
                                  INotificationService notification,
                                  IOptions<FileServerSettings> fileServerSettings) : base(notification)
        {
            _imagemRepository = imagemRepository;
            _fileServerSettings = fileServerSettings.Value;
            _geekStoreDbContextService = geekStoreDbContextService;
        }

        #endregion

        public async Task<string> ObterBase64(Arquivo arquivo)
        {
            try
            {
                var array = await File.ReadAllBytesAsync(Path.Combine(_fileServerSettings.Path, arquivo.NomeArquivoFileServer));
                return Convert.ToBase64String(array);
            }
            catch
            {
                return null;
            }
        }

        public string ObterUrlFileServer()
        {
            return _fileServerSettings.Url;
        }

        public async Task<Imagem> SalvarImagem(Imagem imagem)
        {
            imagem = FormatarArquivo(imagem);

            if (imagem == null)
                return null;

            var transaction = await _geekStoreDbContextService.CreateTransaction();
            await _imagemRepository.InsertAndSave(imagem);

            var result = await SalvarArquivo(imagem);

            if (!result)
                return null;

            _geekStoreDbContextService.Commit(transaction);
            return imagem;
        }

        private TClass FormatarArquivo<TClass>(TClass arquivo) where TClass : Arquivo
        {
            if (arquivo?.Nome == null)
            {
                Notificar("Arquivo inválido.");
                return null;
            }

            arquivo.DefinirExtensao(Path.GetExtension(arquivo.Nome));

            if (string.IsNullOrEmpty(arquivo.Extensao))
            {
                Notificar("A extensão do arquivo é inválida.");
                return null;
            }

            return arquivo;
        }

        private async Task<bool> SalvarArquivo<TClass>(TClass arquivo) where TClass : Arquivo
        {
            var imgBytes = arquivo.ObterArray();

            if (imgBytes?.Any() != true)
            {
                Notificar("Arquivo inválido.");
                return false;
            }

            try
            {
                await File.WriteAllBytesAsync(Path.Combine(_fileServerSettings.Path, arquivo.NomeArquivoFileServer), imgBytes);
            }
            catch
            {
                Notificar($"Não foi possível salvar o arquivo { arquivo.Nome }.",
                    "Erro ao tentar acessar o FileServer.");

                return false;
            }

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _imagemRepository?.Dispose();
        }
    }
}