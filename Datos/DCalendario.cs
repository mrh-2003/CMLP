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
    public class DCalendario
    {
        private readonly string connectionString = Utilidades.cadena();
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM calendarios", connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        public string Mantenimiento(ECalendario calendario, string opcion)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string mensaje;
                    ECalendario eCalendario = null;
                    if (opcion != "insert")
                        eCalendario = getCalendario(calendario.Id);
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (opcion == "insert")
                        {
                            query = "INSERT INTO calendarios (descripcion, monto, vencimiento, alumno_dni) VALUES (@descripcion, @monto, @vencimiento, @alumno_dni)";
                            mensaje = "Se inserto correctamente el Calendario de Pago " + calendario.Id + "del alumno "+calendario.AlumnoDNI+" cuyo monto es " +calendario.Monto + " y vence el " + calendario.Vencimiento.ToString();

                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE calendarios SET descripcion = @descripcion, monto = @monto, vencimiento = @vencimiento, alumno_dni = @alumno_dni WHERE id = @id";
                            mensaje = "Se actualizo correctamente el Calendario de Pago " + "del alumno " +calendario.AlumnoDNI+ "ANTES: "+eCalendario.Monto + " - " + eCalendario.Vencimiento.ToString() + "AHORA: " + calendario.Monto + " - " + calendario.Vencimiento.ToString();

                        }
                        else
                        {
                            query = "DELETE FROM calendarios WHERE id = @id";
                            mensaje = "Se elimno correctamente el Calendario " + calendario.Id + " con datos: " +calendario.AlumnoDNI +"-"+ calendario.Monto + " - " + calendario.Vencimiento.ToString();

                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", calendario.Id);
                            cmd.Parameters.AddWithValue("@descripcion", calendario.Descripcion);
                            cmd.Parameters.AddWithValue("@monto", calendario.Monto);
                            cmd.Parameters.AddWithValue("@vencimiento", calendario.Vencimiento);
                            cmd.Parameters.AddWithValue("@alumno_dni", calendario.AlumnoDNI);
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
        public DataTable BuscarPorDniODescripcion(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM calendarios WHERE LOWER(alumno_dni) LIKE LOWER(@valor_buscado) OR LOWER(descripcion) LIKE LOWER(@valor_buscado)", connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public ECalendario getCalendario(int id)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM calendarios WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            ECalendario eCalendario = new ECalendario()
                            {
                                Id = reader.GetInt32(0),
                                Descripcion = reader.GetString(1),
                                Monto = reader.GetInt32(2),
                                Vencimiento = reader.GetDateTime(3),
                                AlumnoDNI = reader.GetString(4),
                            };
                            return eCalendario;
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
