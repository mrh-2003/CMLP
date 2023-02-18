using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EHistorial
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
