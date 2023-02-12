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
    public class DConcepto
    {
        private readonly string connectionString = Utilidades.cadena();

        public string Mantenimiento(EConcepto concepto, string opcion)
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
                            query = "INSERT INTO conceptos (codigo, concepto, importe) VALUES (@codigo, @concepto, @importe)";
                        else if (opcion == "update")
                            query = "UPDATE conceptos SET concepto = @concepto, importe = @importe WHERE codigo = @codigo";
                        else
                            query = "DELETE FROM conceptos WHERE codigo = @codigo";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@codigo", concepto.Codigo);
                            cmd.Parameters.AddWithValue("@concepto", concepto.Concepto);
                            cmd.Parameters.AddWithValue("@importe", concepto.Importe);
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


        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM conceptos", connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
    }
}
