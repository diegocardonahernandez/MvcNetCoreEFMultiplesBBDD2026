using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region STORED PROCEDURES AND VIEWS

//ORACLE
/*
create or replace view V_EMPLEADOS
as
    select EMP.EMP_NO, EMP.APELLIDO, EMP.OFICIO
    , EMP.SALARIO, DEPT.DEPT_NO, DEPT.DNOMBRE, DEPT.LOC 
    from EMP
    inner join DEPT
    on EMP.DEPT_NO = DEPT.DEPT_NO;
create or replace procedure SP_ALL_VEMPLEADOS
(p_cursor_empleados out SYS_REFCURSOR)
as
begin
    open p_cursor_empleados for
    select * from V_EMPLEADOS;
end;
*/


#endregion

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosVistaAsync()
        {
            //begin
            //    sp_procedure(:param1, :param2);
            //end;
            string sql = "begin ";
            sql += " SP_ALL_VEMPLEADOS (:p_cursor_empleados); ";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            //INDICAMOS EL TIPO DE ORACLE
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql, pamCursor);
            return await consulta.ToListAsync();
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
