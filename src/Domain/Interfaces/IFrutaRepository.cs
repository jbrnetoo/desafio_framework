using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFrutaRepository : IRepository<Fruta>
    {
        Task AtualizarEstoque(Guid id, int quantidade);
    }
}
