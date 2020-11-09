using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using VaselinaWeb.API.Controllers;

namespace FrameworkTest
{
    public class RolesControllerTest
    {
        [Test]
        public void GetRolesTest()
        {
            //Arrange - Configuracion


            var controller = new RolesController();
            string[] esperado = new string[2];
            esperado[0] = "Administrador";
            esperado[1] = "Vendedores";

            //Act - Ejecución
            IActionResult resultado = controller.List().Result;

            var respuesta = (OkObjectResult)resultado;

            //Assert - Prueba           

            respuesta.ShouldNotBeNull();

            respuesta.Value.ShouldBe(esperado);
        }
    }
}
