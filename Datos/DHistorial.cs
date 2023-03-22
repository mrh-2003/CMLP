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
        private readonly string anio = Utilidades.anio;
        private readonly string connectionString = Utilidades.cadena();
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT id as ""ID"", descripcion as ""DESCRIPCIÓN"", usuario as ""USUARIO"", fecha as ""FECHA"" FROM historial";
                if(anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM fecha) = @anio ORDER BY fecha";
                else
                    query += " ORDER BY fecha";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if(anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
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
                string query = "SELECT * FROM historial";
                if (anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM fecha) = @anio and (LOWER(usuario_id) LIKE LOWER(@valor_buscado) OR LOWER(descripcion) LIKE LOWER(@valor_buscado))";
                else
                    query += " WHERE LOWER(usuario_id) LIKE LOWER(@valor_buscado) OR LOWER(descripcion) LIKE LOWER(@valor_buscado)";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

    }
}
