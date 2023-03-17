using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Npgsql;

namespace Datos
{
    public class DColegio
    {
        private readonly string connectionString = Utilidades.cadena();
        public string Mantenimineto(EColegio colegio)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (getColegio() == null)
                            query = "INSERT INTO colegio (email, contrasenia, numero, txtsalida, mora) VALUES (@email, @contrasenia, @numero, @txtsalida, @mora)";
                        else
                            query = "UPDATE colegio SET email = @email, contrasenia = @contrasenia, numero = @numero, txtsalida = @txtsalida, mora = @mora WHERE id = @id";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            if(getColegio() != null)
                                cmd.Parameters.AddWithValue("@id", getColegio().Id);
                            cmd.Parameters.AddWithValue("@email", colegio.Email);
                            cmd.Parameters.AddWithValue("@contrasenia", colegio.Contrasenia);
                            cmd.Parameters.AddWithValue("@numero", colegio.Numero);
                            cmd.Parameters.AddWithValue("@txtsalida", colegio.Txtsalida);
                            cmd.Parameters.AddWithValue("@mora", colegio.Mora);
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    return "Tarea realizada correctamente";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
        public EColegio getColegio()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM colegio", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            EColegio eColegio = new EColegio();
                            eColegio.Id = reader.GetInt32(0);
                            eColegio.Email = reader.GetString(1);
                            eColegio.Numero = reader.GetString(2);
                            eColegio.Txtsalida = reader.GetString(3);
                            eColegio.Mora = reader.GetDecimal(4);
                            eColegio.Contrasenia = reader.GetString(5);
                            return eColegio;
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
