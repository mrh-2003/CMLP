using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ECalendarioDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Dni { get; set; }
        public decimal MontoPagado { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime Vencimiento { get; set; }
        public int ConceptoCodigo { get; set; }
    }
}
