using Humanizer.Localisation.DateToOrdinalWords;
using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEFMultiplesBBDD.Models;
using MvcNetCoreEFMultiplesBBDD.Repositories;

namespace MvcNetCoreEFMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private IRepositoryEmpleados repo;

        public EmpleadosController(IRepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<EmpleadoView> empleados = await
                this.repo.GetEmpleadosVistaAsync();
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            EmpleadoView empleado = await
                this.repo.GetDetallesEmpleadoAsync(id);
            return View(empleado);
        }

        public async Task<IActionResult> EmpleadosEMP()
        {
            List<Empleado> empleados = await this.repo.GetEmpleados();
            return View(empleados);
        }

        public IActionResult InsertEmpleado()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> InsertEmpleado(string apellido, string oficio, int dir, int salario,int comision, string departamento)
        {
            await this.repo.CreateEmpleado(apellido, oficio, dir, salario, comision, departamento);
            return RedirectToAction("EmpleadosEMP");
        }

    }
}
