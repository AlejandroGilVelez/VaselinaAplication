using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Framework.Dtos;
using Framework.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaselinaWeb.API.Utilidades;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Atributos

        private readonly IProductRepository productRepository;

        #endregion

        #region Constructor

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;            
        }

        #endregion
      

        #region Acciones

        //[HttpPost("Imagen")]
        //public async Task<IActionResult> Imagen([FromForm] List<IFormFile> imagenes , [FromForm] ProductDto producto)
        //{
        //    if (imagenes.FirstOrDefault() != null)
        //    {

        //        using (var ms = new MemoryStream())
        //        {
        //            imagenes.FirstOrDefault().CopyTo(ms);
        //            var fileBytes = ms.ToArray();
        //            //string s = Convert.ToBase64String(fileBytes);
        //            producto.Imagen = fileBytes;
        //            // act on the Base64 data
        //        }
        //    }
            

        //    return Ok(producto);
        //}

        /// <summary>
        /// Método que retorna una lista de productos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await productRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            List<ProductDto> productList = new List<ProductDto>();           
           

            foreach (var item in result)
            {
                
                productList.Add(new ProductDto
                {
                    Id = item.Id,
                    Nombre = item.Nombre,
                    Descripcion = item.Descripcion,
                    Peso = item.Peso,
                    //Imagen = Convert.ToBase64String(item.Imagen)
                    Imagen = ConvertImagen.ImagenToString(item.Imagen)
                });
            }

            return Ok(productList);
        }       

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] List<IFormFile> imagenes, [FromForm] ProductDto product)
        {
            var result = await productRepository.Find(x => x.Id == product.Id);

            if (result != null)
            {
                return BadRequest("Ese producto ya existe");
            }

            byte[] imagen = null;

            if (imagenes.FirstOrDefault() != null)
            {
                imagen = ConvertImagen.ImagenToArray(imagenes.FirstOrDefault());
                //using (var ms = new MemoryStream())
                //{
                //    imagenes.FirstOrDefault().CopyTo(ms);
                //    var fileBytes = ms.ToArray();
                //    //string s = Convert.ToBase64String(fileBytes);
                //    //product.Imagen = fileBytes;
                //    imagen = fileBytes;
                //    // act on the Base64 data
                //}
            }

            var producto = new Product
            {
                Id = Guid.NewGuid(),
                Nombre = product.Nombre,
                Descripcion = product.Descripcion,
                Peso = product.Peso,
                Imagen = imagen,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            await productRepository.Add(producto);

            return Ok();
        }

        #endregion
    }
}