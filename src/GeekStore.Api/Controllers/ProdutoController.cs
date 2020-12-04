using Application.Api.ViewModels;
using AutoMapper;
using GeekStore.Api.BuildingBlocks.Controllers;
using GeekStore.Business.Dto_s;
using GeekStore.Business.Interfaces.Repositories;
using GeekStore.Business.Interfaces.Services;
using GeekStore.Core.Interfaces.BuildingBlocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Api.Controllers
{
    [Route("api/produto")]
    public class ProdutoController : MainController
    {
        #region Injection

        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(ISessionApp appSession,
                                INotificationService notificador,
                                IMapper mapper,
                                IProdutoService produtoService,
                                IProdutoRepository produtoRepository) : base(notificador, appSession)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        #endregion

        [HttpPost("cadastrar")]
        public async Task<ActionResult<ProdutoViewModel>> Inserir(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var produtos = await _produtoService.Inserir(_mapper.Map<Produto>(produtoViewModel));
            return CustomResponse(_mapper.Map<ProdutoViewModel>(produtos));
        }

        [HttpPut("atualizar/{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            if (produtoViewModel.Id != id)
                return BadRequest();

            var produto = await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));
            return CustomResponse(_mapper.Map<Produto>(produto));
        }

        [HttpDelete("remover/{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Remover(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto?.Ativo != true)
                return NotFound();

            await _produtoService.Remover(produto);
            return CustomResponse(_mapper.Map<ProdutoViewModel>(produto));
        }

        [HttpGet("obter-todos")]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodos();
            return CustomResponse(_mapper.Map<IEnumerable<ProdutoViewModel>>(produtos));
        }
    }
}