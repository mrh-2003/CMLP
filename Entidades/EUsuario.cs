﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EUsuario
    {
        public string Usuario { get; set; }
        public string Contrasenia { get; set; }
        public EUsuario()
        {

        }
        public EUsuario(string usuario, string contrasenia)
        {
            Usuario = usuario;
            Contrasenia = contrasenia;
        }
    }
}
