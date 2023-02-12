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
        public string AlumnoDNI { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int ConceptoCodigo { get; set; }

        public EBoleta()
        {
        }

        public EBoleta(string codigo, string alumnoDNI, decimal monto, DateTime fecha, int conceptoCodigo)
        {
            Codigo = codigo;
            AlumnoDNI = alumnoDNI;
            Monto = monto;
            Fecha = fecha;
            ConceptoCodigo = conceptoCodigo;
        }
    }
}
