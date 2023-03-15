using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ECalendario
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
	    public decimal MontoTotal { get; set; }
	    public decimal MontoPagado { get; set; }
        public DateTime Vencimiento { get; set; }
        public int AlumnoId { get; set; }
        public int ConceptoCodigo { get; set; }
        public decimal Mora { get; set; }
        public DateTime Cancelacion { get; set; }
        public DateTime Emision { get; set; }
        public bool Txt { get; set; }
    }
}
