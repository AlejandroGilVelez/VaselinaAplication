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
    public class ClientController : ControllerBase
    {

        #region Atributos

        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        #endregion

        #region Acciones        

        /// <summary>
        /// Retorna una lista de clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await clientRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ClientDto>>(result));
        }

        /// <summary>
        /// Método que retorna un cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}", Name = "Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await clientRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ClientDto>(result));
        }

        /// <summary>
        /// Método que elimina un cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await clientRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            await clientRepository.Delete(result);

            return Ok();
        }

        /// <summary>
        /// Método para crear un cliente.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ClientDto client)
        {
            var result = await clientRepository.Find(x => x.Nit == client.Nit);

            if (result != null)
            {
                return BadRequest("El cliente ya existe");
            }

            var cliente = mapper.Map<Client>(client);

            cliente.Id = Guid.NewGuid();
            cliente.FechaCreacion = DateTime.Now;
            cliente.FechaModificacion = DateTime.Now;

            await clientRepository.Add(cliente);

            return Created(Url.Link("Get", new { id = cliente.Id }), cliente);

            //return CreatedAtRoute("Get", new { id = cliente.Id }, cliente);
            //return Ok();
        }

        /// <summary>
        /// Método para editar un cliente.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ClientDto client)
        {
            var clienteDB = await clientRepository.FindByKey(client.Id);

            if (clienteDB == null)
            {
                return BadRequest("El cliente no existe");
            }

            mapper.Map(client, clienteDB);

            clienteDB.FechaModificacion = DateTime.Now;

            await clientRepository.Edit(clienteDB);

            return Ok();
        }

        #endregion
    }
}