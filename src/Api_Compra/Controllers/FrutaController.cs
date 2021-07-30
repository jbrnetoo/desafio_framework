using Api_Compra.Models;
using AutoMapper;
using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Compra.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [ApiController]
    public class FrutaController : ControllerBase
    {
        private readonly IFrutaRepository _frutaRepository;
        private readonly IMapper _mapper;

        public FrutaController(IFrutaRepository frutaRepository, IMapper mapper)
        {
            _frutaRepository = frutaRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obter uma lista de frutas
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<ActionResult> ObterFrutas()
        {
            try
            {
                var frutas = await _frutaRepository.ObterTodos();

                var listaDtoFrutas = _mapper.ProjectTo<DtoFruta>(frutas.AsQueryable());

                return Ok(listaDtoFrutas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inserir uma nova fruta no estoque
        /// </summary>
        /// <param name="dtoFruta">Uma fruta deve ser informada.</param> 
        /// <returns></returns>
        [HttpPost("")]
        public async Task<ActionResult> InserirFruta(DtoFruta dtoFruta)
        {
            try
            {
                var fruta = _mapper.Map<DtoFruta, Fruta>(dtoFruta);

                await _frutaRepository.Adicionar(fruta);

                return Ok("Fruta inserida!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar Estoque
        /// </summary>
        /// <param name="dtoAtualizarEstoque">Id e Quantidade devem ser informados</param> 
        /// <returns></returns>
        [HttpPut("Estoque")]
        public async Task<ActionResult> AtualizarEstoque(DtoAtualizarEstoque dtoAtualizarEstoque)
        {
            try
            {
                await _frutaRepository.AtualizarEstoque(dtoAtualizarEstoque.Id, dtoAtualizarEstoque.Quantidade);

                return Ok("Estoque Atualizado!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Remover fruta do estoque
        /// </summary>
        /// <param name="id">Código Guid da fruta.</param> 
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoverFruta(Guid id)
        {
            try
            {
                await _frutaRepository.Remover(id);

                return Ok("Fruta removida!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
