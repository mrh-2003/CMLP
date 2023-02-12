using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Npgsql;

namespace Datos
{
    public class DUsuario
    {
        private readonly string connectionString = Utilidades.cadena();
        public string Mantenimiento(EUsuario usuario, string opcion)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (opcion == "insert")
                            query = "INSERT INTO usuarios (usuario, contrasenia) VALUES (@usuario, @contrasenia)";
                        else if (opcion == "update")
                            query = "UPDATE usuarios SET contrasenia = @contrasenia WHERE usuario = @usuario";
                        else
                            query = "DELETE FROM usuarios WHERE usuario = @usuario";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@usuario", usuario.Usuario);
                            cmd.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    return "Tarea realizada exitosamente";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public bool Login(string usuario, string contrasenia)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT contrasenia FROM usuarios WHERE usuario = @usuario", conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                if (Utilidades.GetSHA256(contrasenia) == reader["contrasenia"].ToString())
                                    return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }

    }
}
