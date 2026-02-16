using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<Empleado>> GetEmpleados();
        Task CreateEmpleado(string apellido, string oficio, int dir, int salario, int comision, string dept);
        Task<List<EmpleadoView>> GetEmpleadosVistaAsync();
        Task<EmpleadoView> GetDetallesEmpleadoAsync(int id);
    }
}
