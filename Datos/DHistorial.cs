using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using Npgsql;

namespace Datos
{
    public class DHistorial
    {
        private readonly string connectionString = Utilidades.cadena();
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM historial ORDER BY fecha", connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public string Insertar(EHistorial historial)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        var query = "INSERT INTO historial (descripcion, usuario, fecha) VALUES (@descripcion, @usuario, @fecha)";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@descripcion", historial.Descripcion);
                            cmd.Parameters.AddWithValue("@usuario", historial.Usuario);
                            cmd.Parameters.AddWithValue("@fecha", historial.Fecha);
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
        public DataTable BuscarPorUsuarioOdescripcion(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM historial WHERE LOWER(usuario_id) LIKE LOWER(@valor_buscado) OR LOWER(descripcion) LIKE LOWER(@valor_buscado)", connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

    }
}
