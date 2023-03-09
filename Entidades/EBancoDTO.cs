using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EBancoDTO
    {
        public string NCredito { get; set; }
        public int NCuota { get; set; }
        public DateTime FVncmto { get; set; }
        public DateTime FPago { get; set; }
        public decimal SImporte { get; set; }
        public string ACliente { get; set; }
        public decimal SInterMora { get; set; }
        public decimal SPagado { get; set; }
    }
}
