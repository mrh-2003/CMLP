using System;
using System.Collections.Generic;
using Npgsql;
using Entidades;
using System.Data;
using System.Reflection;

namespace Datos
{
    public class DAlumno
    {
        private readonly string anio = Utilidades.anio;
        private readonly string connectionString = Utilidades.cadena();
        public string Mantenimiento(EAlumno alumno, string opcion)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string mensaje;
                    EAlumno eAlumno = null;
                    if (opcion != "insert")
                        eAlumno = getAlumno(alumno.Dni);
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (opcion == "insert")
                        {
                            query = "INSERT INTO alumnos (dni, apellidos_nombres, grado, seccion, email, email_apoderado, celular, celular_apoderado, descuento, fin_descuento, anio_registro) VALUES (@dni, @apellidos_nombres, @grado, @seccion, @email, @email_apoderado, @celular, @celular_apoderado, @descuento, @fin_descuento, @anio_registro)";
                            mensaje = "Se insertó correctamente el Alumno " + alumno.Dni + " y se le asignó " + alumno.Descuento + " y vence el " + alumno.FinDescuento.ToString();
                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE alumnos SET dni = @dni, apellidos_nombres = @apellidos_nombres, grado = @grado, seccion = @seccion, email = @email, email_apoderado = @email_apoderado, celular = @celular, celular_apoderado = @celular_apoderado, descuento = @descuento, fin_descuento = @fin_descuento, anio_registro = @anio_registro WHERE id = @id";
                            mensaje = "Se actualizó correctamente el Alumno " + alumno.Dni + " ANTES: " + eAlumno.Descuento + " - " + eAlumno.FinDescuento.ToString() + " AHORA: " + alumno.Descuento + " - " + alumno.FinDescuento.ToString();
                        }
                        else
                        {
                            query = "DELETE FROM alumnos WHERE id = @id";
                            mensaje = "Se eliminó correctamente el Alumno " + alumno.Dni + " con datos: " + alumno.Descuento + " - " + alumno.FinDescuento.ToString();
                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@id", alumno.Id);
                            cmd.Parameters.AddWithValue("@dni", alumno.Dni);
                            cmd.Parameters.AddWithValue("@apellidos_nombres", alumno.ApellidosNombres);
                            cmd.Parameters.AddWithValue("@grado", alumno.Grado);
                            cmd.Parameters.AddWithValue("@seccion", alumno.Seccion);
                            cmd.Parameters.AddWithValue("@email", alumno.Email);
                            cmd.Parameters.AddWithValue("@email_apoderado", alumno.EmailApoderado);
                            cmd.Parameters.AddWithValue("@celular", alumno.Celular);
                            cmd.Parameters.AddWithValue("@celular_apoderado", alumno.CelularApoderado);
                            cmd.Parameters.AddWithValue("@descuento", alumno.Descuento);
                            cmd.Parameters.AddWithValue("@fin_descuento", alumno.FinDescuento);
                            cmd.Parameters.AddWithValue("@anio_registro", alumno.AnioRegistro);
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
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM alumnos";
                if(anio != "TODOS")
                    query += " WHERE anio_registro = @anio";                
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if(anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        //public List<EAlumno> Listar()
        //{
        //    List<EAlumno> lista = new List<EAlumno>();

        //    using (var conn = new NpgsqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            using (var trans = conn.BeginTransaction())
        //            {
        //                string query = "SELECT dni, apellidos_nombres, grado, seccion, email, celular, celular_mama, celular_papa, descuento, fin_descuento FROM alumnos";
        //                using (var cmd = new NpgsqlCommand(query, conn, trans))
        //                {
        //                    using (var reader = cmd.ExecuteReader())
        //                    {
        //                        while (reader.Read())
        //                        {
        //                            EAlumno alumno = new EAlumno();
        //                            alumno.Dni = reader.GetString(0);
        //                            alumno.ApellidosNombres = reader.GetString(1);
        //                            alumno.Grado = reader.GetInt32(2);
        //                            alumno.Seccion = reader.GetChar(3);
        //                            alumno.Email = reader.GetString(4);
        //                            alumno.Celular = reader.GetInt32(5);
        //                            alumno.CelularApoderado = reader.GetInt32(6);
        //                            alumno.CelularPapa = reader.GetInt32(7);
        //                            alumno.Descuento = reader.GetString(8);
        //                            alumno.FinDescuento = reader.GetDateTime(9);

        //                            lista.Add(alumno);
        //                        }
        //                    }
        //                }
        //                trans.Commit();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //    return lista;
        //}
        public EAlumno getAlumno(string dni)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM alumnos WHERE dni = @dni and anio_registro = @anio_registro", conn))
                    {
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@anio_registro", Convert.ToInt32(anio));
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            EAlumno eAlumno = new EAlumno()
                            {
                                Id = reader.GetInt32(0),
                                Dni = reader.GetString(1),
                                ApellidosNombres = reader.GetString(2),
                                Grado = reader.GetInt32(3),
                                Seccion = reader.GetChar(4),
                                Email = reader.GetString(5),
                                EmailApoderado = reader.GetString(6),
                                Celular = reader.GetInt32(7),
                                CelularApoderado = reader.GetInt32(8),
                                Descuento = reader.GetString(9),
                                FinDescuento = reader.GetDateTime(10),
                                AnioRegistro = reader.GetInt32(11)
                            };
                            return eAlumno;
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
        public EAlumno getAlumnoById(int id)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM alumnos WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            EAlumno eAlumno = new EAlumno()
                            {
                                Id = reader.GetInt32(0),
                                Dni = reader.GetString(1),
                                ApellidosNombres = reader.GetString(2),
                                Grado = reader.GetInt32(3),
                                Seccion = reader.GetChar(4),
                                Email = reader.GetString(5),
                                EmailApoderado = reader.GetString(6),
                                Celular = reader.GetInt32(7),
                                CelularApoderado = reader.GetInt32(8),
                                Descuento = reader.GetString(9),
                                FinDescuento = reader.GetDateTime(10),
                                AnioRegistro = reader.GetInt32(11)
                            };
                            return eAlumno;
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
        public DataTable listObjectToDataTable(List<EAlumno> list)
        {
            DataTable dataTable = new DataTable();

            if (list.Count > 0)
            {
                // Obtener información de la clase del primer objeto en la lista
                Type objectType = list[0].GetType();
                PropertyInfo[] properties = objectType.GetProperties();

                // Agregar las columnas al DataTable
                foreach (PropertyInfo property in properties)
                {
                    dataTable.Columns.Add(property.Name, property.PropertyType);
                }
                foreach (object obj in list)
                {
                    DataRow dataRow = dataTable.NewRow();

                    // Asignar los valores de cada propiedad al DataRow
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        object value = obj.GetType().GetProperty(column.ColumnName).GetValue(obj, null);
                        dataRow[column] = value;
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
        public DataTable BuscarPorNombreODNI(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "SELECT * FROM alumnos WHERE LOWER(dni) LIKE LOWER(@valor_buscado) OR LOWER(apellidos_nombres) LIKE LOWER(@valor_buscado)";
                if(anio != "TODOS")
                    query = "SELECT * FROM alumnos WHERE anio_registro = @anio_registro and (LOWER(dni) LIKE LOWER(@valor_buscado) OR LOWER(apellidos_nombres) LIKE LOWER(@valor_buscado))";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if(anio != "TODOS")
                        command.Parameters.AddWithValue("@anio_registro", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable FiltrarGradoSeccion(int grado, string seccion)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM alumnos";
                if (grado != 0 && seccion != "")
                {
                    query += "WHERE grado = @grado AND seccion =@seccion";
                    if (anio != "TODOS")
                        query += " AND anio_registro = @anio_registro";
                }
                else
                {
                    if (anio != "TODOS")
                        query += " WHERE anio_registro = @anio_registro and (grado = @grado OR seccion =@seccion)";
                    else
                        query += " WHERE grado = @grado OR seccion =@seccion";
                }
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@grado", grado);
                    command.Parameters.AddWithValue("@seccion", seccion.ToUpper());
                    if(anio != "TODOS")
                        command.Parameters.AddWithValue("@anio_registro", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

    }
}
