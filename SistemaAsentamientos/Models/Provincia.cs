using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAsentamientos.Models
{
    public class Provincia
    {
        public int ProvinciaID { get; set; }

        public string Nombre { get; set; }

        public Boolean Estado { get; set; }

        public ICollection<Canton> Cantones { get; set; }
    }
}
