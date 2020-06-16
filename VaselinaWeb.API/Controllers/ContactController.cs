﻿using Framework.Dtos;
using Framework.Models;
using Framework.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        #region Atributos

        private readonly IContactRepository contactRepository;

        #endregion

        #region Constructor

        public ContactController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        #endregion

        #region Acciones


        /// <summary>
        /// Metodo que retorna una lista de contactos.
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await contactRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            // El modelo y el dto son los mismos datos ud es una gva, no habia necesidad de hacer esto.
            //List<ContactDto> contacts = new List<ContactDto>();

            //foreach (var item in result)
            //{
            //    contacts.Add(new ContactDto
            //    {
            //        Id = item.Id,
            //        Nombres = item.Nombres,
            //        Empresa = item.Empresa,
            //        Correo = item.Correo,
            //        Telefono = item.Telefono,
            //        Mensaje = item.Mensaje
            //    });
            //}


            return Ok();
        }

        /// <summary>
        /// Metodo que retorna un contacto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id) 
        {
            var result = await contactRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Metodo que elimina un contacto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id) 
        {
            var result = await contactRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            await contactRepository.Delete(result);

            return Ok();
        }

        /// <summary>
        /// Metodo que crea un contacto.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ContactDto contact)
        {
            var result = contact;

            if (result == null)
            {
                return BadRequest("No existe un contacto para guardar");
            }

            var contactClient = new Contact
            {
                Id = Guid.NewGuid(),
                Nombres = contact.Nombres,
                Empresa = contact.Empresa,
                Correo = contact.Correo,
                Telefono = contact.Telefono,
                Mensaje = contact.Mensaje,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            contactClient = await contactRepository.Add(contactClient);

            
            UtilsSendEmail.SendEmail("gelu170@gmail.com", contactClient.Nombres, contactClient.Empresa, contactClient.Correo, contactClient.Telefono, contactClient.Mensaje);

            return Ok();

        }

        /// <summary>
        /// Metodo que actualiza un contacto.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ContactDto contact)
        {
            var result = await contactRepository.Find(x => x.Correo == contact.Correo);

            if (result == null)
            {
                return NotFound();
            }

            result.Nombres = contact.Nombres;
            result.Empresa = contact.Empresa;
            result.Correo = contact.Correo;
            result.Telefono = contact.Telefono;
            result.Mensaje = contact.Mensaje;
            result.FechaModificacion = DateTime.Now;

            await contactRepository.Edit(result);

            return Ok();
        }

        #endregion
    }
}