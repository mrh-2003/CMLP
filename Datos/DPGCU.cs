using Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DPGCU
    {
        private readonly string connectionString = Utilidades.cadena();
        private readonly string anio = Utilidades.anio;
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT id as ""ID"", codigo as ""CODIGO"", nombres as ""APELLIDOS Y NOMBRES"", importe as ""IMPORTE"", mora as ""MORA"", fecha as ""FECHA/PAGO"" FROM public.pgcu";
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
        public string Mantenimiento(EPGCU epgcu, string opcion)
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
                        {
                            query = "INSERT INTO pgcu (codigo, nombre, importe, mora, fecha) VALUES (@codigo, @nombre, @importe, @mora, @fecha)";
                        }
                        else
                        {
                            query = "DELETE FROM pgcu WHERE id = @id";
                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", epgcu.id);
                            if(opcion == "insert")
                            {
                                cmd.Parameters.AddWithValue("@codigo", epgcu.codigo);
                                cmd.Parameters.AddWithValue("@nombre", epgcu.nombres);
                                cmd.Parameters.AddWithValue("@importe", epgcu.importe);
                                cmd.Parameters.AddWithValue("@mora", epgcu.mora);
                                cmd.Parameters.AddWithValue("@fecha", epgcu.fecha);
                            }
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    return "Accion realizada correctamente";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public DataTable ListarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(@"SELECT id as ""ID"", codigo as ""CODIGO"", nombres as ""APELLIDOS Y NOMBRES"", importe as ""IMPORTE"", mora as ""MORA"", fecha as ""FECHA/PAGO"" FROM public.pgcu WHERE fecha BETWEEN @fechaInicio AND @fechaFin", connection))
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
        public decimal TotalPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(importe) FROM public.pgcu WHERE fecha BETWEEN @fechaInicio AND @fechaFin;", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                total = reader.GetDecimal(0);
                            }
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
                string query = "SELECT SUM(importe) FROM public.pgcu";
                if (anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM fecha) = @anio";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("anio", Convert.ToInt32(anio));
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                total = reader.GetDecimal(0);
                            }
                        }
                    }
                }
            }

            return total;
        }
        public DataTable BuscarPorNombreODNI(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT id as ""ID"", codigo as ""CODIGO"", nombres as ""APELLIDOS Y NOMBRES"", importe as ""IMPORTE"", mora as ""MORA"", fecha as ""FECHA/PAGO"" FROM public.pgcu WHERE LOWER(codigo) LIKE LOWER(@valor_buscado) OR LOWER(nombres) LIKE LOWER(@valor_buscado)";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
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
