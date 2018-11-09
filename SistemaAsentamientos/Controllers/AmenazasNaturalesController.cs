using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaAsentamientos.Data;
using SistemaAsentamientos.Models;
using SistemaAsentamientos.ModelsClass;

namespace SistemaAsentamientos.Controllers
{
    public class AmenazasNaturalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private AmenazasModels amenazasModels;

        public AmenazasNaturalesController(ApplicationDbContext context)
        {
            _context = context;
            amenazasModels = new AmenazasModels(_context);
        }

        // GET: AmenazasNaturales
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public List<object[]> filtrarAmenazas(int numPagina, string valor, string order)
        {
            return amenazasModels.filtrarAmenazas(numPagina, valor, order);
        }

        public List<AmenazaNatural> getAmenazas(int id)
        {
            return amenazasModels.getAmenazas(id);
        }

        public List<IdentityError> guardarAmenaza(string descripcion, string estado)
        {
            return amenazasModels.guardarAmenaza(descripcion, estado);
        }

        public List<IdentityError> editarAmenaza(int id, string descripcion, Boolean estado, int funcion)
        {
            return amenazasModels.editarAmenaza(id, descripcion, estado, funcion);
        }

    }
}
