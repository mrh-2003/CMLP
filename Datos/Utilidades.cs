using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Utilidades
    {
        public static string cadena()
        {
            string line;
            try
            {
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                StreamReader sr = new StreamReader(downloadsFolder + "\\1.txt");
                line = sr.ReadLine();
                sr.Close();
                return line;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static bool escribirTxt(string nombre, string contenido)
        {
            try
            {
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                StreamWriter sw = new StreamWriter(downloadsFolder + "\\" + nombre + ".txt");
                string contenidoConSaltos = contenido.Replace("\n", "\r\n"); // Agregar saltos de línea para cada '\n'
                sw.Write(contenidoConSaltos);
                sw.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
    }
}
