using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAsentamientos.Data;
using SistemaAsentamientos.Models;
using SistemaAsentamientos.ModelsClass;

namespace SistemaAsentamientos.Controllers
{
    public class ProvinciasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ProvinciasModels provinciasModels;

        public ProvinciasController(ApplicationDbContext context)
        {
            _context = context;
            provinciasModels = new ProvinciasModels(_context);
        }

        // GET: Provincias
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public List<object[]> filtrarDatos(int numPagina, string valor, string order)
        {
            return provinciasModels.filtrarDatos(numPagina, valor, order);
        }

        public List<IdentityError> guardarProvincia(string nombre, string estado)
        {
            return provinciasModels.guardarProvincia(nombre, estado);
        }

        public List<Provincia> getProvincias(int id)
        {
            return provinciasModels.getProvincias(id);
        }

        public List<IdentityError> editarProvincia(int id, string nombre, Boolean estado, int funcion)
        {
            return provinciasModels.editarProvincia(id, nombre, estado, funcion);
        }

             
    }
}
