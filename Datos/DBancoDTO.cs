using Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datos
{
    public class DBancoDTO
    {
        private readonly string connectionString = Utilidades.cadena();
        public List<EBancoDTO> ListarSinCalendario()
        {
            List<EBancoDTO> lista = new List<EBancoDTO>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = "select * from banco where calendario = false";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    EBancoDTO banco = new EBancoDTO();
                                    banco.Id = reader.GetInt32(0);
                                    banco.NCredito = reader.GetString(1);
                                    banco.NCuota = reader.GetInt32(2);
                                    banco.FVncmto = reader.GetDateTime(3);
                                    banco.FPago = reader.GetDateTime(4);
                                    banco.SImporte = reader.GetDecimal(5);
                                    banco.ACliente = reader.GetString(6);
                                    banco.SInterMora = reader.GetDecimal(7);
                                    banco.SPagado = reader.GetDecimal(8);
                                    banco.Calendario = reader.GetBoolean(9);
                                    lista.Add(banco);
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
        public List<EBancoDTO> ListarConCalendario()
        {
            List<EBancoDTO> lista = new List<EBancoDTO>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = "select * from banco where calendario = true";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    EBancoDTO banco = new EBancoDTO();
                                    banco.Id = reader.GetInt32(0);
                                    banco.NCredito = reader.GetString(1);
                                    banco.NCuota = reader.GetInt32(2);
                                    banco.FVncmto = reader.GetDateTime(3);
                                    banco.FPago = reader.GetDateTime(4);
                                    banco.SImporte = reader.GetDecimal(5);
                                    banco.ACliente = reader.GetString(6);
                                    banco.SInterMora = reader.GetDecimal(7);
                                    banco.SPagado = reader.GetDecimal(8);
                                    banco.Calendario = reader.GetBoolean(9);
                                    lista.Add(banco);
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
        public string EliminarTodo()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM banco";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    return "Accion realizada con exito";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public string ActualizarCalendario()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE banco SET calendario = @calendario";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@calendario", true);
                        cmd.ExecuteNonQuery();
                    }
                    return "Accion realizada con exito";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public string Mantenimiento(EBancoDTO banco, string opcion)
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
                            query = "INSERT INTO banco (ncredito, ncuota, fvncmto, fpago, simporte, acliente, sintermora, spagado) VALUES (@ncredito, @ncuota, @fvncmto, @fpago, @simporte, @acliente, @sintermora, @spagado)";
                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE banco SET ncredito = @ncredito, ncuota = @ncuota, fvncmto = @fvncmto, fpago = @fpago, simporte = @simporte, acliente = @acliente, sintermora = @sintermora, spagado = @spagado, calendario = @calendario WHERE id = @id";
                        }
                        else
                        {
                            query = "DELETE FROM banco WHERE id = @id";
                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", banco.Id);
                            cmd.Parameters.AddWithValue("@ncredito", banco.NCredito);
                            cmd.Parameters.AddWithValue("@ncuota", banco.NCuota);
                            cmd.Parameters.AddWithValue("@fvncmto", banco.FVncmto);
                            cmd.Parameters.AddWithValue("@fpago", banco.FPago);
                            cmd.Parameters.AddWithValue("@simporte", banco.SImporte);
                            cmd.Parameters.AddWithValue("@acliente", banco.ACliente);
                            cmd.Parameters.AddWithValue("@sintermora", banco.SInterMora);
                            cmd.Parameters.AddWithValue("@spagado", banco.SPagado);
                            cmd.Parameters.AddWithValue("@calendario", banco.Calendario);
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    return "Accion realizada con exito";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public bool InsertarArchivos(string nombre)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        var query = "INSERT INTO archivos (nombre) VALUES (@nombre)";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
