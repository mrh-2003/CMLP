using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EPGCU
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombres { get; set; }
        public decimal importe { get; set; }
        public decimal mora { get; set; }
        public DateTime fecha { get; set; }
    }
}
