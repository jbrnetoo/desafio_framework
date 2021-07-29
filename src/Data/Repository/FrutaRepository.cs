using Data.Context;
using Domain.Entidades;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class FrutaRepository : Repository<Fruta>, IFrutaRepository
    {
        public FrutaRepository(ComercioContext context) : base(context) { }

        public async Task AtualizarEstoque(Guid id, int quantidade)
        {
            var fruta = await ObterPorId(id);
            fruta.Quantidade += quantidade;
            await Atualizar(fruta);
        }
    }
}
