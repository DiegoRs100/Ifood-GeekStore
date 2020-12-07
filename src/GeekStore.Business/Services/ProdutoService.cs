﻿using GeekStore.Core.Base;
using GeekStore.Core.Interfaces.BuildingBlocks;
using System;
using GeekStore.Business.Dto_s;
using GeekStore.Business.Interfaces.Services;
using GeekStore.Business.Dto_s.Validations;
using GeekStore.Business.Interfaces.Repositories;
using System.Threading.Tasks;
using GeekStore.Core.Interfaces.Services;
using System.Collections.Generic;

namespace GeekStore.Business.Services
{
    public class ProdutoService : ServiceBase<Produto, ProdutoValidation>, IProdutoService
    {
        #region Injection

        private readonly IProdutoRepository _produtoRepository;
        private readonly IGeekStoreDbContextService _geekStoreDbContextService;
        private readonly IFileServerService _fileServerService;

        public ProdutoService (IProdutoRepository produtoRepository,
                               IGeekStoreDbContextService geekStoreDbContextService,
                               IFileServerService fileServerService,
                               INotificationService notificador) : base(notificador, produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _geekStoreDbContextService = geekStoreDbContextService;
            _fileServerService = fileServerService;
        }

        #endregion

        public override async Task<Produto> Inserir(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto))
                return null;

            // Não permitimos que dois produtos sejam cadastrados com o mesmo nome
            if (await _produtoRepository.VerificarExistencia(produto.Nome))
            {
                Notificar($"Já existe um produto cadastrado com o nome '{produto.Nome}'");
                return null;
            }

            var transaction = await _geekStoreDbContextService.CreateTransaction();

            #region Persistindo imagem no FileServer

            if (produto.Imagem != null)
                await _fileServerService.SalvarImagem(produto.Imagem);

            #endregion

            await _produtoRepository.InsertAndSave(produto);
            _geekStoreDbContextService.Commit(transaction);

            return produto;
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodos();

            foreach (var item in produtos)
                item.Imagem?.DefinirUrl(_fileServerService.ObterUrlFileServer());

            return produtos;
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);
            produto?.Imagem?.DefinirUrl(_fileServerService.ObterUrlFileServer());

            return produto;
        }

        //public override async Task<Produto> Atualizar(Produto entity)
        //{
        //    if (!ExecutarValidacao(new ProdutoValidation(), entity))
        //        return null;

        //    if (_produtoRepository.ObterPorId(entity.Id).Result?.Ativo != true)
        //    {
        //        Notificar("Registro inexistente.");
        //        return null;
        //    }

        //    await _entityRepository.UpdateAndSave(entity);
        //    return entity;
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _produtoRepository?.Dispose();
            _geekStoreDbContextService?.Dispose();
            _fileServerService?.Dispose();
        }
    }
}