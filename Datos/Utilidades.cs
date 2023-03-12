using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Utilidades
    {
        public static string anio;
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
        public static async Task<string> EnviarCorreo(string remitente, string password, string destinatario, string asunto, string cuerpo)
        {
            if (string.IsNullOrEmpty(remitente))
                throw new ArgumentException("El remitente no puede ser nulo ni vacío.", nameof(remitente));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("La contraseña no puede ser nula ni vacía.", nameof(password));
            if (string.IsNullOrEmpty(destinatario))
                throw new ArgumentException("El destinatario no puede ser nulo ni vacío.", nameof(destinatario));
            using (MailMessage message = new MailMessage())
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    message.From = new MailAddress(remitente);
                    message.To.Add(destinatario);
                    message.Subject = asunto;
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.Body = cuerpo;
                    message.BodyEncoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = true;

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(remitente, password);
                    smtp.EnableSsl = true;

                    try
                    {
                        await smtp.SendMailAsync(message);
                        return "El correo electrónico se envió correctamente.";
                    }
                    catch (Exception ex)
                    {
                        return string.Format("Error al enviar el correo electrónico: {0}", ex.Message);
                    }
                }
            }
        }

        public static bool VerificarConexionInternet()
        {
            try
            {
                using (var cliente = new WebClient())
                using (var stream = cliente.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
