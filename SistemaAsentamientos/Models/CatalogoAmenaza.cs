using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAsentamientos.Models
{
    public class CatalogoAmenaza
    {
        public int CatalogoAmenazaID { get; set; }

        public string Descripcion { get; set; }

        public Boolean Estado { get; set; }

        public int AmenazaNaturalID { get; set; }

        public AmenazaNatural AmenazaNatural { get; set; }
    }
}
