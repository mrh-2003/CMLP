﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

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
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\SistemaCMLP";
                StreamReader sr = new StreamReader(downloadsFolder + "\\cmlp.txt");
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
        public static string eliminarCaracteresEspeciales(string cadena)
        {
            // Normaliza la cadena para eliminar los diacríticos
            string normalizedString = cadena.Normalize(NormalizationForm.FormD);

            // Crea una expresión regular para eliminar los caracteres que no sean letras ni números
            Regex regex = new Regex("[^a-zA-Z ]");

            // Elimina los caracteres no deseados de la cadena normalizada
            string cleanString = regex.Replace(normalizedString, "");
            return cleanString;

        }
        public static bool escribirTxt(string nombre, string contenido)
        {
            try
            {
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\SistemaCMLP\\PGCU";
                if (!Directory.Exists(downloadsFolder))
                    Directory.CreateDirectory(downloadsFolder);
                StreamWriter sw = new StreamWriter(downloadsFolder + "\\" + nombre + ".ING");
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
            remitente = "no.responder.cmlp@gmail.com";
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
        public static string getHead()
        {
            return @"                    <html>
                    <head>
                        <title>Pago de boleta electrónica</title>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                font-size: 16px;
                                line-height: 1.5;
                                color: #333;
                                background-color: #f5f5f5;
                                margin: 0;
                                padding: 0;
                            }
                            header {
                                text-align: center;
                                color: black;
                                padding: 15px;
                            }
                            main {
                                padding: 20px;
                                background-color: #fff;
                            }
                            table {
                                border-collapse: collapse;
                                margin-bottom: 20px;
                                width: 100%;
                            }

                            table td, table th {
                                border: 1px solid #ccc;
                                padding: 10px;
                                text-align: left;
                            }

                            table th {
                                background-color: #f7f7f7;
                                font-weight: bold;
                            }
                            footer {
                                font-weight: bold;
                                color: #000;
                                padding: 20px;
                                display: flex;
                                align-items: center;
            
                            }
                            .footer-text {
                                margin: 0;
                                margin: auto;
                                font-size: 20px;
                            }
                            .footer-text span {
                                font-size: 40px;
                                color: #007bff;
                            }
        
                        </style>
                    </head>
                    <body>";
        }
        public static string getFoot()
        {
            return @"                            <p>Para cualquier consulta, por favor no dude en contactarnos a través de nuestro correo electrónico [Correo] o en nuestro número de atención al cliente [Número de atención al cliente].</p>
                            <p>Atentamente,</p>
                            <p>El area de administración del Colegio Militar Leoncio Prado</p>
                        </main>";
        }

    }
}
