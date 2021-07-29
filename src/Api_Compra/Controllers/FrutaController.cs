using Api_Compra.Models;
using AutoMapper;
using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Compra.Controllers
{
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

        [HttpPut("Estoque")]
        public async Task<ActionResult> AtualizarFruta(DtoAtualizarEstoque dtoAtualizarEstoque)
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
    }
}
