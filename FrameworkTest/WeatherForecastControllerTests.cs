using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using VaselinaWeb.API.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using VaselinaWeb.API;

namespace FrameworkTest
{
    public class WeatherForecastControllerTests 
    {
        //[Test]
        //public void GetSumariesTest()
        //{
        //    Arrange - Configuracion
        //    Mock<ILogger<WeatherForecastController>> logger = new Mock<ILogger<WeatherForecastController>>();

        //    var controller = new WeatherForecastController(logger.Object);

        //    Act - Ejecución
        //    var resultado = controller.GetSumaries();

        //    Assert - Prueba
        //    Assert.AreEqual(resultado.Length, 10);
        //}

        //[Test]
        //public void GetTest()
        //{
        //    Arrange - Configuracion
        //    Mock<ILogger<WeatherForecastController>> logger = new Mock<ILogger<WeatherForecastController>>();

        //    var controller = new WeatherForecastController(logger.Object);

        //    Act - Ejecución
        //    var resultado = controller.Get();

        //    Assert - Prueba
        //    Assert.AreEqual(resultado.Count(), 5);
        //    Assert.AreEqual(typeof(WeatherForecast), resultado.FirstOrDefault().GetType());
        //}
    }
}
