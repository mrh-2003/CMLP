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
        public List<EConcepto> Listar()
        {
            List<EConcepto> lista = new List<EConcepto>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = "SELECT * FROM conceptos";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    EConcepto concepto = new EConcepto();
                                    concepto.Codigo = reader.GetInt32(0);
                                    concepto.Concepto = reader.GetString(1);
                                    concepto.Importe = reader.GetDecimal(2);

                                    lista.Add(concepto);
                                }
                            }
                        }
                        trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return lista;
        }


        //public DataTable Listar()
        //{
        //    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM conceptos", connection))
        //        {
        //            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
        //            DataTable dt = new DataTable();
        //            adapter.Fill(dt);

        //            return dt;
        //        }
        //    }
        //}
        public DataTable BuscarPorCodigo(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM conceptos WHERE LOWER(codigo) LIKE LOWER(@valor_buscado)", connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable BuscarPorConcepto(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM boletas WHERE concepto_codigo=@valor_buscado", connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", valorBuscado);
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

    }
}
