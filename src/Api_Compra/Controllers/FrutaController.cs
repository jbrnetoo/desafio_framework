using Api_Compra.Models;
using AutoMapper;
using Domain.Entidades;
using Domain.Interfaces;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
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
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public FrutaController(IFrutaRepository frutaRepository, ILogger logger, IMapper mapper)
        {
            _frutaRepository = frutaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        #region ObterFrutas
        /// <summary>
        /// Obter uma lista de frutas
        /// </summary>
        /// <returns>
        /// 200 - Lista de frutas obtida com sucesso
        /// </returns>
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
                _logger.Error(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region ObterFrutaPorId
        /// <summary>
        /// Obtém uma fruta
        /// </summary>
        /// <param name="id">Código Guid da fruta.</param> 
        /// <returns>
        /// 200 - Fruta obtida com sucesso
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> ObterFrutaPorId(Guid id)
        {
            try
            {
                var fruta = await _frutaRepository.ObterPorId(id);

                var dtoFruta = _mapper.Map<Fruta, DtoFruta>(fruta);

                return Ok(dtoFruta);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region InserirFruta
        /// <summary>
        /// Inserir uma nova fruta no estoque
        /// </summary>
        /// <param name="dtoFruta">Uma fruta deve ser informada.</param> 
        /// <returns>
        /// 200 - Fruta inserida com sucesso
        /// </returns>
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
                _logger.Error(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region AtualizarFruta
        /// <summary>
        /// Atualizar Fruta
        /// </summary>
        /// <param name="dtoFruta">Uma fruta deve ser informada.</param> 
        /// <returns>
        /// 200 - Fruta atualizada com sucesso
        /// </returns>
        [HttpPut("")]
        public async Task<ActionResult> AtualizarFruta(DtoFruta dtoFruta)
        {
            try
            {
                var fruta = _mapper.Map<DtoFruta, Fruta>(dtoFruta);

                await _frutaRepository.Atualizar(fruta);

                return Ok("Fruta Atualizada!");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region AtualizarEstoque
        /// <summary>
        /// Atualizar Estoque
        /// </summary>
        /// <param name="dtoAtualizarEstoque">Id e Quantidade devem ser informados</param> 
        /// <returns>
        /// 200 - Estoque atualizado com sucesso
        /// </returns>
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
                _logger.Error(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region RemoverFruta
        /// <summary>
        /// Remover fruta do estoque
        /// </summary>
        /// <param name="id">Código Guid da fruta.</param> 
        /// <returns>
        /// 200 - Fruta removida com sucesso
        /// </returns>
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
                _logger.Error(ex.Message);

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion

    }
}
