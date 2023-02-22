using System;
using System.Collections.Generic;
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
            //string line;
            //try
            //{
            //    StreamReader sr = new StreamReader("1.txt");
            //    line = sr.ReadLine();
            //    sr.Close();
            //    return line;
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
            return "Host=localhost:5432;Username=postgres;Password=74143981;Database=CMLP";
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
        
    }
}
