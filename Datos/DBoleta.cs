using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entidades;
using Npgsql;

namespace Datos
{
    public class DBoleta
    {
        private readonly string connectionString = Utilidades.cadena();
        public string Mantenimiento(EBoleta boleta, string opcion)
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
                            query = "INSERT INTO boletas (codigo, alumno_dni, monto, fecha, concepto_codigo) VALUES (@codigo, @alumno_dni, @monto, @fecha, @concepto_codigo)";
                        else if (opcion == "update")
                            query = "UPDATE boletas SET alumno_dni =@alumno_dni, monto = @monto, fecha = @fecha, concepto_codigo = @concepto_codigo WHERE codigo = @codigo";
                        else
                            query = "DELETE FROM boletas WHERE codigo = @codigo";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@codigo", boleta.Codigo);
                            cmd.Parameters.AddWithValue("@alumno_dni", boleta.AlumnoDNI);
                            cmd.Parameters.AddWithValue("@monto", boleta.Monto);
                            cmd.Parameters.AddWithValue("@fecha", boleta.Fecha);
                            cmd.Parameters.AddWithValue("@concepto_codigo", boleta.ConceptoCodigo);
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

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM boletas", connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable ListarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM boletas WHERE fecha BETWEEN @fechaInicio AND @fechaFin", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        public DataTable ListarPorConceptos(DateTime fechaInicio, DateTime fechaFin, int concepto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM boletas WHERE fecha BETWEEN @fechaInicio AND @fechaFin and concepto_codigo = @concepto", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);
                    command.Parameters.AddWithValue("concepto", concepto);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public decimal TotalPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(monto) FROM boletas WHERE fecha BETWEEN @fechaInicio AND @fechaFin;", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = reader.GetDecimal(0);
                        }
                    }
                }
            }

            return total;
        }
        public decimal TotalPorConceptos(DateTime fechaInicio, DateTime fechaFin, int concepto)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(monto) FROM boletas WHERE fecha BETWEEN @fechaInicio AND @fechaFin and concepto_codigo = @concepto;", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);
                    command.Parameters.AddWithValue("concepto", concepto);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = reader.GetDecimal(0);
                        }
                    }
                }
            }

            return total;
        }
        public decimal Total()
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(monto) FROM boletas", connection))
                {

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            total = reader.GetDecimal(0);
                        }
                    }
                }
            }

            return total;
        }
    }
}
