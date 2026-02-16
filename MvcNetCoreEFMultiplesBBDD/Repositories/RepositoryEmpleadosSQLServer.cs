using Google.Protobuf.WellKnownTypes;
using Humanizer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Mysqlx.Crud;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using System.Diagnostics.Metrics;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosSQLServer : IRepositoryEmpleados
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

        //------------


//        SELECT* FROM EMP
//SELECT* FROM DEPT

//ALTER PROCEDURE SP_INSERT_EMPLEADO_LUNES
//        (@apellido NVARCHAR(50),@oficio NVARCHAR(50),
//		@dir INT, @salario INT, @comision INT, @departamento NVARCHAR(50), @empno INT OUT)

//AS
//    declare @newempno int
//    declare @fechahoy DATETIME

//    declare @iddept int
//    select @newempno = CAST(MAX(EMP_NO) AS INT) + 1  FROM EMP

//    select @empno = @newempno

//    select @fechahoy = GETDATE()

//    select @iddept = (SELECT DEPT_NO FROM DEPT WHERE DNOMBRE = @departamento)

//	INSERT INTO EMP VALUES(@newempno, @apellido, @oficio, @dir, @fechahoy, @salario, @comision, @iddept)
//GO

//EXEC SP_INSERT_EMPLEADO_LUNES
//    @apellido = 'GARCIA',
//    @oficio = 'ANALISTA',
//    @dir = 7839,
//    @salario = 2500,
//    @comision = 300,
//    @departamento = 'VENTAS';

//        DELETE FROM EMP WHERE APELLIDO ='Cardona'

//	SELECT DISTINCT DNOMBRE FROM DEPT

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

        public async Task<List<Empleado>> GetEmpleados()
        {
            var consulta = from datos in this.context.Empleados select datos;
            return await consulta.ToListAsync();
        }

        public async Task CreateEmpleado(string apellido, string oficio, int dir, int salario, int comision, string dept)
        {
            var consulta = "SP_INSERT_EMPLEADO_LUNES @apellido, @oficio, @dir, @salario, @comision, @departamento";
            SqlParameter pamApe = new SqlParameter("@apellido", apellido);
            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            SqlParameter pamDir = new SqlParameter("@dir", dir);
            SqlParameter pamSalario = new SqlParameter("@salario", salario);
            SqlParameter pamComision = new SqlParameter("@comision", comision);
            SqlParameter pamDept = new SqlParameter("@departamento", dept);
            await this.context.Database.ExecuteSqlRawAsync(consulta, pamApe, pamOficio, pamDir, pamDir, pamSalario, pamComision, pamDept);

        }
   

    }
}
