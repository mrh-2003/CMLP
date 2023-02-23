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

                using (NpgsqlCommand command = new NpgsqlCommand(@"SELECT c.id, c.descripcion, a.dni, c.monto_pagado, c.monto_total, c.vencimiento 
FROM calendarios c inner join alumnos a on a.id = c.alumno_id", connection))
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
                    EAlumno eAlumno = (new DAlumno()).getAlumnoById(calendario.AlumnoId);
                    if (opcion != "insert")
                        eCalendario = getCalendario(calendario.Id);
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (opcion == "insert")
                        {
                            query = "INSERT INTO calendarios (descripcion, monto_total, monto_pagado, vencimiento, alumno_id) VALUES (@descripcion, @monto_total, @monto_pagado, @vencimiento, @alumno_id)";
                            mensaje = "Se insertó correctamente el Calendario de Pago del alumno " + eAlumno.Dni + " cuyo monto es " + calendario.MontoTotal + " y vence el " + calendario.Vencimiento.ToString();
                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE calendarios SET descripcion = @descripcion, monto_total = @monto_total, monto_pagado = @monto_pagado, vencimiento = @vencimiento, alumno_id = @alumno_id WHERE id = @id";
                            mensaje = "Se actualizó correctamente el Calendario de Pago del alumno " + eAlumno.Dni + " ANTES: " + eCalendario.MontoTotal + " - " + eCalendario.Vencimiento.ToString() + " AHORA: " + calendario.MontoTotal + " - " + calendario.Vencimiento.ToString();
                        }
                        else
                        {
                            query = "DELETE FROM calendarios WHERE id = @id";
                            mensaje = "Se eliminó correctamente el Calendario con datos: " + eAlumno.Dni + "-" + calendario.MontoTotal + " - " + calendario.Vencimiento.ToString();
                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", calendario.Id);
                            cmd.Parameters.AddWithValue("@descripcion", calendario.Descripcion);
                            cmd.Parameters.AddWithValue("@monto_total", calendario.MontoTotal);
                            cmd.Parameters.AddWithValue("@monto_pagado", calendario.MontoPagado);
                            cmd.Parameters.AddWithValue("@vencimiento", calendario.Vencimiento);
                            cmd.Parameters.AddWithValue("@alumno_id", calendario.AlumnoId);
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

                using (NpgsqlCommand command = new NpgsqlCommand(@"SELECT c.id, c.descripcion, a.dni, c.monto_pagado, c.monto_total, c.vencimiento 
                FROM calendarios c inner join alumnos a on a.id = c.alumno_id
                WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado)", connection))
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
                                MontoTotal = reader.GetDecimal(2),
                                MontoPagado = reader.GetDecimal(3),
                                Vencimiento = reader.GetDateTime(4),
                                AlumnoId = reader.GetInt32(5),
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
