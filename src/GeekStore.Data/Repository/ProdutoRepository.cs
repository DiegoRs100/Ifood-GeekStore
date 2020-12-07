using GeekStore.Core.Base;
using GeekStore.Data.Contexts;
using GeekStore.Business.Dto_s;
using GeekStore.Business.Interfaces.Repositories;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GeekStore.Data.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto, GeekStoreDbContext>, IProdutoRepository
    {
        public ProdutoRepository(GeekStoreDbContext context) : base(context)
        { }

        public override async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await Context.Produtos.AsNoTracking()
                .Include(x => x.Imagem)
                .Where(x => x.Ativo).ToListAsync();
        }

        public override async Task<Produto> ObterPorId(Guid id, bool tracking = false)
        {
            return await Context.Produtos.AsNoTracking()
                .Include(x => x.Imagem)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<bool> VerificarExistencia(string nomeProduto)
        {
            return await Context.Produtos.AsNoTracking()
                .AnyAsync(x => x.Nome.Trim().ToUpper() == nomeProduto.Trim().ToUpper()
                    && x.Ativo);
        }
    }
}