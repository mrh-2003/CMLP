using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EAlumno
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string ApellidosNombres { get; set; }
        public int Grado { get; set; }
        public char Seccion { get; set; }
        public string Email { get; set; }
        public string EmailApoderado { get; set; }
        public int Celular { get; set; }
        public int CelularApoderado { get; set; }
        public string Descuento { get; set; }
        public DateTime FinDescuento { get; set; }
        public int AnioRegistro { get; set; }

        public EAlumno() { }

    }
}
