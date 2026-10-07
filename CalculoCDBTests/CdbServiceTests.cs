using CalculoCDBApp.Application.Request;
using CalculoCDBApp.Application.Service;

namespace CalculoCDBTests
{
    public class CdbServiceTests
    {
        private readonly CdbService _service;

        public CdbServiceTests()
        {
            _service = new CdbService();
        }

        [Fact]
        public void Calcular_DeveLancarExcecao_QuandoValorInicialForZero()
        {
            #region Arrange
            var request = new CalcularCdbRequest
            {
                ValorInicial = 0,
                Meses = 6
            };
            #endregion


            #region Action
            Action act = () => _service.Calcular(request);
            #endregion


            #region Assert
            Assert.Throws<ArgumentException>(act);
            #endregion
        }

        [Fact]
        public void Calcular_DeveLancarExcecao_QuandoMesesForMenorOuIgualAUm()
        {
            #region Arrange
            var request = new CalcularCdbRequest
            {
                ValorInicial = 1000,
                Meses = 1
            };
            #endregion


            #region Action
            Action act = () => _service.Calcular(request);
            #endregion


            #region Assert
            Assert.Throws<ArgumentException>(act);
            #endregion
        }

        [Fact]
        public void Calcular_DeveLancarExcecao_QuandoRequestForNulo()
        {
            // Act
            Action act = () => _service.Calcular(null!);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Calcular_DeveCalcularValorBruto_ParaSeisMeses()
        {
            #region Arrange
            var request = new CalcularCdbRequest
            {
                ValorInicial = 1000,
                Meses = 6
            };
            #endregion


            #region Action
            var resultado = _service.Calcular(request);
            #endregion


            #region Assert
            Assert.Equal(1059.76m, resultado.ValorBruto);
            Assert.Equal(1046.31m, resultado.ValorLiquido);
            #endregion
        }

        [Theory]
        [InlineData(6, 1046.31)]
        [InlineData(7, 1056.05)]
        [InlineData(12, 1098.47)]
        [InlineData(13, 1110.55)]
        [InlineData(24, 1215.58)]
        [InlineData(25, 1232.54)]
        public void Calcular_DeveAplicarAliquotaCorreta(int meses,double valorLiquidoEsperado)
        {
            // Arrange
            var request = new CalcularCdbRequest
            {
                ValorInicial = 1000m,
                Meses = meses
            };

            // Act
            var resultado = _service.Calcular(request);

            // Assert
            //decimal rendimento =
            //    resultado.ValorBruto - request.ValorInicial;

            //decimal impostoEsperado =
            //    rendimento * (decimal)aliquotaEsperada;

            //decimal valorLiquidoEsperado =
            //    resultado.ValorBruto - impostoEsperado;

            Assert.Equal((decimal)valorLiquidoEsperado,resultado.ValorLiquido);
        }
    }
}
