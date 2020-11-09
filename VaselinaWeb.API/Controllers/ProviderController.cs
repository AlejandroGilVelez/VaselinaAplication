using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Dtos;
using Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {

        #region Atributos

        private readonly IProviderRepository providerRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public ProviderController(IProviderRepository providerRepository, IMapper mapper)
        {
            this.providerRepository = providerRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Acciones

        /// <summary>
        /// Método que retorna una lista de proveedores
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await providerRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ProviderDto>>(result));
        }

        /// <summary>
        /// Método que retorna un proveedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}", Name = "GetProvider")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await providerRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProviderDto>(result));
        }

        /// <summary>
        /// Método que elimina un proveedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await providerRepository.FindByKey(id);

            if (result == null)
            {
                return NotFound();
            }

            await providerRepository.Delete(result);

            return Ok();
        }

        /// <summary>
        /// Método para crear un proveedor
        /// </summary>
        /// <param name="supplie"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProviderDto provider)
        {
            var result = await providerRepository.Find(x => x.Nit == provider.Nit);

            if (result != null)
            {
                return BadRequest("El proveedor ya existe.");
            }

            var newProvider = mapper.Map<Provider>(provider);

            newProvider.Id = Guid.NewGuid();
            newProvider.FechaCreacion = DateTime.Now;
            newProvider.FechaModificacion = DateTime.Now;

            await providerRepository.Add(newProvider);

            return Created(Url.Link("GetProvider", new { id = newProvider.Id }), newProvider);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProviderDto provider)
        {
            var providerDB = await providerRepository.FindByKey(provider.Id);

            if (providerDB == null)
            {
                return BadRequest("El insumo no existe");
            }

            mapper.Map(provider, providerDB);

            providerDB.FechaModificacion = DateTime.Now;

            await providerRepository.Edit(providerDB);

            return Ok();

        }

        #endregion
    }
}
