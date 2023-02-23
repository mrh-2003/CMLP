using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace Entidades
{
    public class EBoleta
    {
        public string Codigo { get; set; }
        public int AlumnoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int ConceptoCodigo { get; set; }
    }
}
