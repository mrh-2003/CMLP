using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Vencimiento { get; set; }
        public int ConceptoCodigo { get; set; }
        public DateTime Emision { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
