using AutoMapper;
using Framework.Dtos;
using Framework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaselinaWeb.API.Utilidades;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Atributos

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        #endregion

        #region Constructor

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
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
        [AllowAnonymous]
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await productRepository.GetAll();

            if (result == null)
            {
                return NotFound();
            }
            throw new Exception("Vali verga");
            //List<ProductDto> productList = new List<ProductDto>();           


            //foreach (var item in result)
            //{

            //    productList.Add(new ProductDto
            //    {
            //        Id = item.Id,
            //        Nombre = item.Nombre,
            //        Descripcion = item.Descripcion,
            //        Peso = item.Peso,
            //        //Imagen = Convert.ToBase64String(item.Imagen)
            //        Imagen = ConvertImagen.ImagenToString(item.Imagen)
            //    });
            //}

            var productList = mapper.Map<List<ProductDto>>(result);

            return Ok(productList);
        }

        /// <summary>
        /// Método que retorna un producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await productRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            //List<ProductDto> productList = new List<ProductDto>();           


            //foreach (var item in result)
            //{

            //    productList.Add(new ProductDto
            //    {
            //        Id = item.Id,
            //        Nombre = item.Nombre,
            //        Descripcion = item.Descripcion,
            //        Peso = item.Peso,
            //        //Imagen = Convert.ToBase64String(item.Imagen)
            //        Imagen = ConvertImagen.ImagenToString(item.Imagen)
            //    });
            //}

            var productList = mapper.Map<ProductDto>(result);

            return Ok(productList);
        }

        /// <summary>
        /// Método que elimina un producto.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            await productRepository.Delete(result);

            return Ok();
        }

        /// <summary>
        /// Método que crea un producto
        /// </summary>
        /// <param name="imagenes"></param>
        /// <param name="product"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método que edita un producto.
        /// </summary>
        /// <param name="imagenes"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] List<IFormFile> imagenes, [FromForm] ProductDto product)
        {
            var producto = await productRepository.Find(x => x.Id == product.Id);

            if (producto == null)
            {
                return BadRequest("El producto no existe");
            }

            byte[] imagen = null;

            if (imagenes.FirstOrDefault() != null)
            {
                imagen = ConvertImagen.ImagenToArray(imagenes.FirstOrDefault());                
            }

            producto.Nombre = product.Nombre;
            producto.Descripcion = product.Descripcion;
            producto.Peso = product.Peso;
            producto.Imagen = imagen;
            producto.FechaModificacion = DateTime.Now;

            await productRepository.Edit(producto);

            return Ok();
        }        

        #endregion
    }
}