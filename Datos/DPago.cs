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
    public class DPago
    {
        private readonly string connectionString = Utilidades.cadena();
        public List<EPago> Listar()
        {
            List<EPago> lista = new List<EPago>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = "SELECT * FROM pagos";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    EPago concepto = new EPago();
                                    concepto.Id = reader.GetInt32(0);
                                    concepto.Descripcion = reader.GetString(1);
                                    concepto.Monto = reader.GetDecimal(2);
                                    concepto.Vencimiento = reader.GetDateTime(3);

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
        public string Mantenimiento(EPago pago, string opcion)
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
                            query = "INSERT INTO pagos (descripcion, monto, vencimiento) VALUES (@descripcion, @monto, @vencimiento)";
                        else if (opcion == "update")
                            query = "UPDATE pagos SET descripcion = @descripcion, monto = @monto, vencimiento = @vencimiento WHERE id = @id";
                        else
                            query = "DELETE FROM pagos WHERE id = @id";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", pago.Id);
                            cmd.Parameters.AddWithValue("@descripcion", pago.Descripcion);
                            cmd.Parameters.AddWithValue("@monto", pago.Monto);
                            cmd.Parameters.AddWithValue("@vencimiento", pago.Vencimiento);
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
    }
}
