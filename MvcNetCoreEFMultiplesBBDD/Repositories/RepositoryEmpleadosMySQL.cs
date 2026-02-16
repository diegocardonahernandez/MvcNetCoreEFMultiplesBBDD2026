using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{

    #region STORES PROCEDURES AND VIEWS

    //    create or replace view V_EMPLEADOS
    //as
    //    select
    //      EMP.EMP_NO, EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO,
    //       DEPT.DEPT_NO as IDDEPARTAMENTO,
    //       DEPT.DNOMBRE as DEPARTAMENTO, DEPT.LOC as LOCALIDAD
    //       from EMP
    //       inner join DEPT on EMP.DEPT_NO= DEPT.DEPT_NO;

    //------------
    //CREATE PROCEDURE SP_ALL_VEMPLEADOS()

    //SELECT* FROM V_EMPLEADOS;

    #endregion

    public class RepositoryEmpleadosMySQL : IRepositoryEmpleados
    {

        private HospitalContext context;

        public RepositoryEmpleadosMySQL(HospitalContext context)
        {
            this.context = context;
        }
        public async Task<List<EmpleadoView>> GetEmpleadosVistaAsync()
        {
            var sql = "CALL SP_ALL_VEMPLEADOS";
            var consulta = this.context.EmpleadosView.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
        public async Task<EmpleadoView> GetDetallesEmpleadoAsync(int id)
        {
            var consulta = from datos in this.context.EmpleadosView
                           where datos.IdEmpleado == id
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<List<Empleado>> GetEmpleados()
        {
            var consulta = from datos in this.context.Empleados select datos;
            return await consulta.ToListAsync();
        }

        public Task CreateEmpleado(string apellido, string oficio, int dir, int salario, int comision, string dept)
        {
            throw new NotImplementedException();
        }
    }
}
