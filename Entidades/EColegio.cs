using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EColegio
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public string Numero { get; set; }
        public string Txtsalida { get; set; }
        public decimal Mora { get; set; }
    }
}
