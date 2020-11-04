using AutoMapper;
using Framework.Dtos;
using Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VaselinaWeb.API.Controllers;
using VaselinaWeb.API.Profiles;
using VaselinaWeb.DataModel.Repositories;

namespace FrameworkTest
{
    public class ProductControllerTest
    {
        [Test]
        public void GetListTest()
        {
            //Arrange - Configuracion
            Mock<IProductRepository> productRespository = new Mock<IProductRepository>();

            IList<Product> registrosEnBD = new List<Product>();
            IList<ProductDto> esperado = new List<ProductDto>();
            Random aleatorio = new Random();

            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                var fechaActual = DateTime.Now;
                var peso = aleatorio.Next(30, 150);

                registrosEnBD.Add(new Product
                {
                    Descripcion = $"Descripcion test {i}",
                    Nombre = $"Nombre prueba {i}",
                    Id = id,
                    Peso = peso,
                    Imagen = null,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                });

                esperado.Add(new ProductDto
                {
                    Descripcion = $"Descripcion test {i}",
                    Nombre = $"Nombre prueba {i}",
                    Id = id,
                    Peso = peso,
                    Imagen = null
                });
            }

            productRespository.Setup(x => x.GetAll()).Returns(Task.FromResult(registrosEnBD));

            var config = new MapperConfiguration(opts =>
            {
                // Add your mapper profile configs or mappings here
                opts.AddProfile<ProductProfile>();
            });
            var mapper = config.CreateMapper(); // Use this mapper to instantiate your class            

            var controller = new ProductController(productRespository.Object, mapper);

            //Act - Ejecución
            IActionResult resultado = controller.List().Result;
            var respuesta = (OkObjectResult)resultado;

            var objRespuesta = respuesta.Value as List<ProductDto>;

            //Assert - Prueba
            respuesta.ShouldNotBeNull();
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(objRespuesta[i].Descripcion, esperado[i].Descripcion);
                Assert.AreEqual(objRespuesta[i].Id, esperado[i].Id);
                Assert.AreEqual(objRespuesta[i].Peso, esperado[i].Peso);
                Assert.AreEqual(objRespuesta[i].Nombre, esperado[i].Nombre);
                Assert.AreEqual(objRespuesta[i].Imagen, esperado[i].Imagen);
            }
        }

        [Test]
        public void GetListNullTest()
        {
            //Arrange - Configuracion
            Mock<IProductRepository> productRespository = new Mock<IProductRepository>();

            var config = new MapperConfiguration(opts =>
            {
                // Add your mapper profile configs or mappings here
                opts.AddProfile<ProductProfile>();
            });
            var mapper = config.CreateMapper(); // Use this mapper to instantiate your class            

            var controller = new ProductController(productRespository.Object, mapper);

            //Act - Ejecución
            IActionResult resultado = controller.List().Result;
            var respuesta = (NotFoundResult)resultado;

            //Assert - Prueba
            respuesta.ShouldNotBeNull();

        }

        [Test]
        public void GetIdTest()
        {
            //Arrange - Configuracion
            Mock<IProductRepository> productRespository = new Mock<IProductRepository>();

            Random aleatorio = new Random();

            var id = Guid.NewGuid();
            var peso = aleatorio.Next(30, 150);

            Product registrosEnBD = new Product
            {
                Id = id,
                Descripcion = "Descripción prueba Id 1",
                Nombre = "Nombre prueba Id 1",
                Imagen = null,
                Peso = peso,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            ProductDto esperado = new ProductDto
            {
                Id = id,
                Descripcion = "Descripción prueba Id 1",
                Nombre = "Nombre prueba Id 1",
                Imagen = null,
                Peso = peso
            };

            productRespository.Setup(x => x.Find(x => x.Id == id)).Returns(Task.FromResult(registrosEnBD));
            //productRespository.Setup(x => x.Find(x => x.Id == It.IsAny<Guid>())).Returns(Task.FromResult(registrosEnBD));

            var config = new MapperConfiguration(opts =>
            {
                // Add your mapper profile configs or mappings here
                opts.AddProfile<ProductProfile>();
            });
            var mapper = config.CreateMapper(); // Use this mapper to instantiate your class            

            var controller = new ProductController(productRespository.Object, mapper);

            //Act - Ejecución
            IActionResult resultado = controller.Get(id).Result;
            var respuesta = (OkObjectResult)resultado;

            var objRespuesta = respuesta.Value as ProductDto;

            //Assert - Prueba

            //Assert.AreEqual(objRespuesta., esperado); Fallo pero devuelve lo mismo
            Assert.AreEqual(objRespuesta.Descripcion, esperado.Descripcion);
            Assert.AreEqual(objRespuesta.Id, esperado.Id);
            Assert.AreEqual(objRespuesta.Peso, esperado.Peso);
            Assert.AreEqual(objRespuesta.Nombre, esperado.Nombre);
            Assert.AreEqual(objRespuesta.Imagen, esperado.Imagen);

        }

        [Test]
        public void GetIdNullTest()
        {
            //Arrange - Configuracion
            Mock<IProductRepository> productRespository = new Mock<IProductRepository>();

            var id = Guid.NewGuid();

            var config = new MapperConfiguration(opts =>
            {
                // Add your mapper profile configs or mappings here
                opts.AddProfile<ProductProfile>();
            });
            var mapper = config.CreateMapper(); // Use this mapper to instantiate your class            

            var controller = new ProductController(productRespository.Object, mapper);

            //Act - Ejecución
            IActionResult resultado = controller.Get(id).Result;
            var respuesta = (NotFoundResult)resultado;

            //Assert - Prueba
            respuesta.ShouldNotBeNull();

        }
    }
}
