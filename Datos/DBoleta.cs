using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Npgsql;

namespace Datos
{
    public class DBoleta
    {
        private readonly string connectionString = Utilidades.cadena();
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

    }
}
