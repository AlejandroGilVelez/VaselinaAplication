using AutoMapper;
using Framework.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaselinaWeb.DataModel.Repositories;

namespace VaselinaWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {

        #region Atributos

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        #endregion


        #region Constructor

        public ProductListController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        #endregion


        #region Acciones

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

            var productList = mapper.Map<List<ProductDto>>(result);

            return Ok(productList);
        }

        /// <summary>
        /// Método que retorna un producto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await productRepository.Find(x => x.Id == id);

            if (result == null)
            {
                return NotFound();
            }           

            var productList = mapper.Map<ProductDto>(result);

            return Ok(productList);
        }

        #endregion

    }
}