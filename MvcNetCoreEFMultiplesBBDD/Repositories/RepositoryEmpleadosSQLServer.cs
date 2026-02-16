using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using System;
using System.Diagnostics.Metrics;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosSQLServer:IRepositoryEmpleados
    {
        #region STORED PROCEDURES AND VIEWS
        /*
        SQL Server
        ALTER VIEW V_EMPLEADOS
        AS
        SELECT CAST(ISNULL(ROW_NUMBER() OVER (ORDER BY E.EMP_NO),0) AS INT)  AS IDEMPLEADO,
                E.APELLIDO, E.OFICIO, E.SALARIO, E.DEPT_NO, D.DNOMBRE AS DEPARTAMENTO, D.LOC AS LOCALIDAD
        FROM EMP E INNER JOIN DEPT D ON E.DEPT_NO = D.DEPT_NO
        GO
        create procedure SP_ALL_VEMPLEADOS
        as
	        select * from V_EMPLEADOS
        go
        */

        #endregion

        private HospitalContext context;

        public RepositoryEmpleadosSQLServer(HospitalContext context)
        {
            this.context = context;
        }

        public async Task <List<EmpleadoView>> GetEmpleadosVistaAsync()
        {
            string sql = "SP_ALL_VEMPLEADOS";
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql);
            List<EmpleadoView> data = await
                consulta.ToListAsync();
            return data;
            //var consulta = from datos in this.context.EmpleadosView select datos;
            //return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> GetDetallesEmpleadoAsync(int id)
        {
            var consulta = from datos in this.context.EmpleadosView
                           where datos.IdEmpleado == id
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }

    }
}
