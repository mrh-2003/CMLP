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
                                    EPago pago = new EPago();
                                    pago.Id = reader.GetInt32(0);
                                    pago.Descripcion = reader.GetString(1);
                                    pago.Monto = reader.GetDecimal(2);
                                    pago.Vencimiento = reader.GetDateTime(3);
                                    pago.ConceptoCodigo = reader.GetInt32(4);
                                    lista.Add(pago);
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
                    string mensaje;
                    EPago ePago = null;
                    if (opcion != "insert")
                        ePago = getPago(pago.Id);
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (opcion == "insert")
                        {
                            query = "INSERT INTO pagos (descripcion, monto, vencimiento, concepto_codigo) VALUES (@descripcion, @monto, @vencimiento, @concepto_codigo)";
                            mensaje = "Se inserto correctamente el Pago con el código " + pago.Id+ " cuyo monto es " + pago.Monto + " y vence el " + pago.Vencimiento.ToString();
                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE pagos SET descripcion = @descripcion, monto = @monto, vencimiento = @vencimiento, concepto_codigo = @concepto_codigo WHERE id = @id";
                            mensaje = "Se actualizo correctamente el Pago con Código " + pago.Id + " ANTES: " + ePago.Monto + " - " + ePago.Vencimiento.ToString() + " AHORA: " + pago.Monto + " - " + pago.Vencimiento.ToString();
                        }
                        else
                        {
                            query = "DELETE FROM pagos WHERE id = @id";
                            mensaje = "Se elimno correctamente el Pago con código " + pago.Id + " cuyos datos son: " + pago.Monto + " - " + pago.Vencimiento.ToString();
                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", pago.Id);
                            cmd.Parameters.AddWithValue("@descripcion", pago.Descripcion);
                            cmd.Parameters.AddWithValue("@monto", pago.Monto);
                            cmd.Parameters.AddWithValue("@vencimiento", pago.Vencimiento);
                            cmd.Parameters.AddWithValue("@concepto_codigo", pago.ConceptoCodigo);
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    return mensaje;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public EPago getPago(int id)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM pagos WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            EPago pago = new EPago();
                            pago.Id = reader.GetInt32(0);
                            pago.Descripcion = reader.GetString(1);
                            pago.Monto = reader.GetDecimal(2);
                            pago.Vencimiento = reader.GetDateTime(3);
                            pago.ConceptoCodigo = reader.GetInt32(4);
                            return pago;
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
