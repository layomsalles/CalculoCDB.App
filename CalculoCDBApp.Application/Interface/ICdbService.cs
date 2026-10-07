using CalculoCDBApp.Application.Request;
using CalculoCDBApp.Application.Response;
namespace CalculoCDBApp.Application.Interface
{
    public interface ICdbService
    {
        CalcularCdbResponse Calcular(CalcularCdbRequest request);
    }
}
