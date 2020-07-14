using Framework.Utilidades;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkTest
{
    public class CalculadoraTest
    {
        private Calculadora objCalculadora;

        [SetUp]
        public void Setup()
        {
            objCalculadora = new Calculadora();
        }

        [Test]
        public void SumarTest()
        {
            //Setup - Configuracion
            int numero1 = 5;
            int numero2 = 10;
            int resultadoEsperado = 15;

            //Calculadora objCalculadora = new Calculadora();

            //Arrange - Ejecución
            var resultado = objCalculadora.Sumar(numero1, numero2);

            //Assert - Prueba
            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [Test]
        public void RestarTest()
        {
            //Setup - Configuracion
            int numero1 = 15;
            int numero2 = 10;
            int resultadoEsperado = 5;

            //Calculadora objCalculadora = new Calculadora();

            //Arrange - Ejecución
            var resultado = objCalculadora.Restar(numero1, numero2);

            //Assert - Prueba
            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [Test]
        public void DividirTest()
        {
            //Setup - Configuracion
            int numero1 = 9;
            int numero2 = 3;
            int resultadoEsperado = 3;

            //Calculadora objCalculadora = new Calculadora();

            //Arrange - Ejecución
            var resultado = objCalculadora.Dividir(numero1, numero2);

            //Assert - Prueba
            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [Test]
        public void DividirZeroTest()
        {
            //Setup - Configuracion
            int numero1 = 9;
            int numero2 = 0;
            int resultadoEsperado = 0;

            //Calculadora objCalculadora = new Calculadora();

            //Arrange - Ejecución
            var resultado = objCalculadora.Dividir(numero1, numero2);

            //Assert - Prueba
            //Assert.Throws<DivideByZeroException>(() => objCalculadora.Dividir(numero1, numero2));
            Assert.AreEqual(resultado, resultadoEsperado);
        }

    }
}
