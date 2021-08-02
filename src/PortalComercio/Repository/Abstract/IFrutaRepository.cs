using PortalComercio.Models;
using System;
using System.Linq;

namespace PortalComercio.Repository.Abstract
{
    public interface IFrutaRepository
    {
        IQueryable<DtoFruta> ObterTodos();
        DtoFruta ObterPorId(Guid id);
        bool InserirFruta(DtoFruta fruta);
        bool AtualizarFruta(DtoFruta fruta);
        bool AtualizarEstoque(DtoAtualizarEstoque dtoAtualizarEstoque);
        bool RemoverFruta(Guid id);
    }
}
