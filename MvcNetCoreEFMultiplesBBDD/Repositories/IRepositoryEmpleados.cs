using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<EmpleadoView>> GetEmpleadosVistaAsync();
        Task<EmpleadoView> GetDetallesEmpleadoAsync(int id);
    }
}
