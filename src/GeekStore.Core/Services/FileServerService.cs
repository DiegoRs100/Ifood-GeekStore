using GeekStore.Core.Base;
using GeekStore.Core.Dto_s;
using GeekStore.Core.Interfaces;
using GeekStore.Core.Interfaces.BuildingBlocks;
using GeekStore.Core.Interfaces.Repositories;
using GeekStore.Core.Interfaces.Services;
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
        public readonly IContextServiceBase _contextService;

        public FileServerService (IImagemRepository imagemRepository,
                                  IContextServiceBase contextService,
                                  INotificationService notification) : base(notification)
        {
            _imagemRepository = imagemRepository;
            _contextService = contextService;
        }

        #endregion

        public async Task<Imagem> SalvarImagem(Imagem imagem)
        {
            #region Formatando dados da imagem

            if (imagem?.Nome == null)
            {
                Notificar("Imagem inválida.");
                return null;
            }

            var extensao = Path.GetExtension(imagem.Nome);

            if (string.IsNullOrEmpty(extensao))
            {
                Notificar("A extensão da imagem é inválida.");
                return null;
            }

            var imgBytes = imagem.ObterArray();

            if (imgBytes?.Any() != true)
            {
                Notificar("Imagem inválida.");
                return null;
            }

            imagem.DefinirNome(Path.GetFileNameWithoutExtension(imagem.Nome) ?? imagem.Id.ToString());
            imagem.DefinirExtensao(extensao);
            imagem.DefinirPath($"{ imagem.Id }.{ imagem.Extensao }");

            #endregion

            await _contextService.CreateTransaction();
            await _imagemRepository.InsertAndSave(imagem);

            #region Salvando imagem

            try
            {
                File.WriteAllBytes("Foo.txt", imgBytes);
            }
            catch
            {
                Notificar("Não foi possível salvar a imagem.", "Erro ao tentar acessar o FileServer.");
                return null;
            }

            #endregion

            _contextService.Commit();
            return imagem;
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