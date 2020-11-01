using AutoMapper;
using Framework.Dtos;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplieController : ControllerBase
    {

        #region Atributos

        private readonly ISupplierRepository supplieRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public SupplieController(ISupplierRepository supplieRepository, IMapper mapper)
        {
            this.supplieRepository = supplieRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Acciones

        /// <summary>
        /// Método que retorna una lista de insumos
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await supplieRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<SupplieDto>>(result));
        }

        /// <summary>
        /// Método que retorna un insumo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}", Name = "GetSupplie")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await supplieRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<SupplieDto>(result));
        }

        /// <summary>
        /// Método que elimina un insumo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await supplieRepository.FindByKey(id);

            if (result == null)
            {
                return NotFound();
            }

            await supplieRepository.Delete(result);

            return Ok();
        }

        /// <summary>
        /// Método para crear un insumo
        /// </summary>
        /// <param name="supplie"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] SupplieDto supplie)
        {
            if (supplie == null)
            {
                return BadRequest("No existe un insumo para crear.");
            }

            var newSupplie = mapper.Map<Supplie>(supplie);

            newSupplie.Id = Guid.NewGuid();
            newSupplie.FechaCreacion = DateTime.Now;
            newSupplie.FechaModificacion = DateTime.Now;

            await supplieRepository.Add(newSupplie);

            return Created(Url.Link("GetSupplie", new { id = newSupplie.Id }), newSupplie);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] SupplieDto supplie)
        {
            var supplieDB = await supplieRepository.FindByKey(supplie.Id);

            if (supplieDB == null)
            {
                return BadRequest("El insumo no existe");
            }

            mapper.Map(supplie, supplieDB);

            supplieDB.FechaModificacion = DateTime.Now;

            await supplieRepository.Edit(supplieDB);

            return Ok();

        }

        #endregion
    }
}
