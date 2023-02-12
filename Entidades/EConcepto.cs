using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EConcepto
    {
        public int Codigo { get; set; }
        public string Concepto { get; set; }
        public decimal Importe { get; set; }

        public EConcepto() { }

        public EConcepto(int codigo, string concepto, decimal importe)
        {
            Codigo = codigo;
            Concepto = concepto;
            Importe = importe;
        }
    }
}
