using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EPGCU
    {
        private int id { get; set; }
        private string codigo { get; set; }
        private string nombres { get; set; }
        private decimal importe { get; set; }
        private decimal mora { get; set; }
        private DateTime fecha { get; set; }
    }
}
