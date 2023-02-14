using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Npgsql;
using System.Data;

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
                            query = "INSERT INTO usuarios (usuario, contrasenia, rol) VALUES (@usuario, @contrasenia, @rol)";
                        else if (opcion == "update")
                            query = "UPDATE usuarios SET usuario = @usuario, contrasenia = @contrasenia, rol = @rol WHERE id = @id";
                        else
                            query = "DELETE FROM usuarios WHERE id = @id";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", usuario.Id);
                            cmd.Parameters.AddWithValue("@usuario", usuario.Usuario);
                            cmd.Parameters.AddWithValue("@contrasenia", usuario.Contrasenia);
                            cmd.Parameters.AddWithValue("@rol", usuario.Rol);
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
        public int Login(string usuario, string contrasenia)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT id, contrasenia FROM usuarios WHERE usuario = @usuario", conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                if (Utilidades.GetSHA256(contrasenia) == reader["contrasenia"].ToString())
                                    return Convert.ToInt32(reader["id"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return 0;
        }
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM usuarios", connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public EUsuario getUsuario(int id)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM usuarios WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            EUsuario eUsuario = new EUsuario()
                            {
                                Id = reader.GetInt32(0),
                                Usuario = reader.GetString(1),
                                Contrasenia = reader.GetString(2),
                                Rol = reader.GetString(3)
                            };
                            return eUsuario;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

    }
}
