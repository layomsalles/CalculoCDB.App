using CalculoCDBApp.Application.Request;
using CalculoCDBApp.Application.Response;
using CalculoCDBApp.Application.Interface;

namespace CalculoCDBApp.Application.Service
{
    public class CdbService : ICdbService
    {
        private const decimal CDI = 0.009m;// o m significa que é um decimal
        private const decimal TB = 1.08m;// o m significa que é um decimal

        public CalcularCdbResponse Calcular(CalcularCdbRequest request)
        {
            Validar(request);

            decimal valorBruto = CalcularValorBruto(request.ValorInicial, request.Meses);

            //Descobre o rendimento
            decimal rendimento = valorBruto - request.ValorInicial;

            //Descobre a aliquota
            decimal aliquota = ObterAliquota(request.Meses);

            //Calcula imposto
            decimal imposto = rendimento * aliquota;

            //Descobrir o valor liquido
            decimal valorLiquido = valorBruto - imposto;

            return new CalcularCdbResponse
            {
                ValorBruto = valorBruto,
                ValorLiquido = valorLiquido
            };
        }

        private decimal CalcularValorBruto(decimal valorInicial, int meses)
        {
            decimal taxaMensal = CDI * TB;
            decimal valorBruto = valorInicial;

            for (int mes = 0; mes < meses; mes++)
            {
                valorBruto = valorBruto * (1 + taxaMensal);
            }

            return valorBruto;
        }

        private decimal ObterAliquota(int meses)
        {
            if (meses <= 6)
            {
                return 0.225m;
            }
            
            if (meses <= 12)
            {
                return 0.20m;
            }
            
            if (meses <= 24)
            {
                return 0.175m;
            }
            
            return 0.15m;
        }

        private void Validar(CalcularCdbRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.ValorInicial <= 0)
            {
                throw new ArgumentException(
                    "O valor inicial deve ser maior que zero.");
            }

            if (request.Meses <= 1)
            {
                throw new ArgumentException(
                    "O prazo deve ser maior que 1 mês.");
            }
        }
    }
}
