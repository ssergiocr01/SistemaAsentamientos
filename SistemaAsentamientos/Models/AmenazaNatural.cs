using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAsentamientos.Models
{
    public class AmenazaNatural
    {
        public int AmenazaNaturalID { get; set; }

        public string Descripcion { get; set; }

        public Boolean Estado { get; set; }

        public ICollection<CatalogoAmenaza> CatalogoAmenazas { get; set; }
    }
}
