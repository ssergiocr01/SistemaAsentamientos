using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAsentamientos.Models
{
    public class Canton
    {
        public int CantonID { get; set; }

        public string Nombre { get; set; }

        public Boolean Estado { get; set; }

        public int ProvinciaID { get; set; }

        public Provincia Provincia { get; set; }
    }
}
