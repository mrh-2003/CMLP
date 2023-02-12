using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EAlumno
    {
        public string Dni { get; set; }
        public string ApellidosNombres { get; set; }
        public int Grado { get; set; }
        public char Seccion { get; set; }
        public string Email { get; set; }
        public int Celular { get; set; }
        public int CelularMama { get; set; }
        public int CelularPapa { get; set; }
        public string Descuento { get; set; }
        public DateTime FinDescuento { get; set; }

        public EAlumno() { }

        public EAlumno(string dni, string nombres, int grado, char seccion, string email, int celular, int celularMama, int celularPapa, string descuento, DateTime finDescuento)
        {
            Dni = dni;
            ApellidosNombres = nombres;
            Grado = grado;
            Seccion = seccion;
            Email = email;
            Celular = celular;
            CelularMama = celularMama;
            CelularPapa = celularPapa;
            Descuento = descuento;
            FinDescuento = finDescuento;
        }
    }
}
