using Microsoft.AspNetCore.Mvc;

using AppCrud.Data;
using AppCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCrud.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly AppDbContext appDbContext;

        public EmpleadoController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Empleado> lista = await appDbContext.Empleados.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Empleado empleado)
        {
            try
            {
                await appDbContext.Empleados.AddAsync(empleado);
                await appDbContext.SaveChangesAsync();
                TempData["Mensaje"] = "Empleado agregado exitosamente";
                TempData["Tipo"] = "success";
                return RedirectToAction(nameof(Lista));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al agregar el empleado: " + ex.ToString();
                TempData["Tipo"] = "error";
                return View(empleado);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var empleado = await appDbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
                if (empleado == null)
                {
                    TempData["Mensaje"] = "Empleado no encontrado";
                    TempData["Tipo"] = "error";
                    return RedirectToAction(nameof(Lista));
                }
                return View(empleado);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al cargar el empleado: " + ex.ToString();
                TempData["Tipo"] = "error";
                return RedirectToAction(nameof(Lista));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            try
            {
                appDbContext.Empleados.Update(empleado);
                await appDbContext.SaveChangesAsync();
                TempData["Mensaje"] = "Empleado actualizado exitosamente";
                TempData["Tipo"] = "success";
                return RedirectToAction(nameof(Lista));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al actualizar el empleado: " + ex.ToString();
                TempData["Tipo"] = "error";
                return View(empleado);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var empleado = await appDbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
                if (empleado == null)
                {
                    TempData["Mensaje"] = "Empleado no encontrado";
                    TempData["Tipo"] = "error";
                    return RedirectToAction(nameof(Lista));
                }

                appDbContext.Empleados.Remove(empleado);
                await appDbContext.SaveChangesAsync();
                TempData["Mensaje"] = "Empleado eliminado exitosamente";
                TempData["Tipo"] = "success";
                return RedirectToAction(nameof(Lista));
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al eliminar el empleado: " + ex.ToString();
                TempData["Tipo"] = "error";
                return RedirectToAction(nameof(Lista));
            }
        }

    }
}
