using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;
using Npgsql;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Windows.Media;

namespace Datos
{
    public class DCalendario
    {
        private readonly string anio = Utilidades.anio;
        private readonly string connectionString = Utilidades.cadena();
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT c.id as ""ID"", c.descripcion as ""DESCRIPCION"", c.concepto_codigo as ""CP"", a.dni as ""DNI"", c.monto_pagado as ""CANCELADO"", c.monto_total as ""IMPORTE"" , c.vencimiento as ""VENCE EL"", c.emision as ""EMISION"", c.cancelacion as ""CANCELACION"", c.mora as ""MORA"" FROM calendarios c inner join alumnos a on a.id = c.alumno_id";
                if(anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio ORDER BY c.vencimiento";
                else
                    query += " ORDER BY c.vencimiento";
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
        public List<ECalendario> ListarLista()
        {
            List<ECalendario> lista = new List<ECalendario>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = "select * from calendarios";
                        if (anio != "TODOS")
                            query += " WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio ORDER BY c.vencimiento";
                        else
                            query += " ORDER BY c.vencimiento";
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            if (anio != "TODOS")
                                cmd.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ECalendario calendario = new ECalendario();
                                    calendario.Id = reader.GetInt32(0);
                                    calendario.Descripcion = reader.GetString(1);
                                    calendario.MontoTotal = reader.GetDecimal(2);
                                    calendario.MontoPagado = reader.GetDecimal(3);
                                    calendario.Vencimiento = reader.GetDateTime(4);
                                    calendario.AlumnoId = reader.GetInt32(5);
                                    calendario.ConceptoCodigo = reader.GetInt32(6);
                                    calendario.Mora = reader.GetDecimal(7);
                                    calendario.Cancelacion = reader.IsDBNull(8) ? default(DateTime) : reader.GetDateTime(8);
                                    calendario.Emision = reader.GetDateTime(9);
                                    lista.Add(calendario);
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
        public List<ECalendarioDTO> ListarListaDTO()
        {
            List<ECalendarioDTO> lista = new List<ECalendarioDTO>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = @"SELECT c.id, c.descripcion, a.dni, c.monto_pagado, c.monto_total, c.vencimiento, c.concepto_codigo
                            FROM calendarios c inner join alumnos a on a.id = c.alumno_id";
                        if (anio != "TODOS")
                            query += " WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio";


                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            if (anio != "TODOS")
                                cmd.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ECalendarioDTO calendario = new ECalendarioDTO();
                                    calendario.Id = reader.GetInt32(0);
                                    calendario.Descripcion = reader.GetString(1);
                                    calendario.Dni = reader.GetString(2);
                                    calendario.MontoPagado = reader.GetDecimal(3);
                                    calendario.MontoTotal = reader.GetDecimal(4);
                                    calendario.Vencimiento = reader.GetDateTime(5);
                                    calendario.ConceptoCodigo = reader.GetInt32(6);
                                    lista.Add(calendario);
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
                            query = "INSERT INTO calendarios (descripcion, monto_total, monto_pagado, vencimiento, alumno_id, concepto_codigo, mora, cancelacion, emision) VALUES (@descripcion, @monto_total, @monto_pagado, @vencimiento, @alumno_id, @concepto_codigo, @mora, @cancelacion, @emision)";
                            mensaje = "Se insertó correctamente el Calendario de Pago del alumno " + eAlumno.Dni + " cuyo monto es " + calendario.MontoTotal + " y vence el " + calendario.Vencimiento.ToString();
                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE calendarios SET descripcion = @descripcion, monto_total = @monto_total, monto_pagado = @monto_pagado, vencimiento = @vencimiento, alumno_id = @alumno_id, concepto_codigo = @concepto_codigo, mora = @mora, cancelacion=@cancelacion, emision = @emision WHERE id = @id";
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
                            cmd.Parameters.AddWithValue("@concepto_codigo", calendario.ConceptoCodigo);
                            cmd.Parameters.AddWithValue("@mora", calendario.Mora);
                            cmd.Parameters.AddWithValue("@cancelacion", calendario.Cancelacion);
                            cmd.Parameters.AddWithValue("@emision", calendario.Emision);
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
                string query = @"SELECT c.id as ""ID"", c.descripcion as ""DESCRIPCION"", c.concepto_codigo as ""CP"", a.dni as ""DNI"", c.monto_pagado as ""CANCELADO"", c.monto_total as ""IMPORTE"" , c.vencimiento as ""VENCE EL"", c.emision as ""EMISION"", c.cancelacion as ""CANCELACION"", c.mora as ""MORA"" 
                                FROM calendarios c inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio AND (LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado)) ORDER BY c.vencimiento";
                else
                    query += " WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) ORDER BY c.vencimiento";
                
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
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
                            ECalendario eCalendario = new ECalendario();
                            eCalendario.Id = reader.GetInt32(0);
                            eCalendario.Descripcion = reader.GetString(1);
                            eCalendario.MontoTotal = reader.GetDecimal(2);
                            eCalendario.MontoPagado = reader.GetDecimal(3);
                            eCalendario.Vencimiento = reader.GetDateTime(4);
                            eCalendario.AlumnoId = reader.GetInt32(5);
                            eCalendario.ConceptoCodigo = reader.GetInt32(6);
                            eCalendario.Mora = reader.GetDecimal(7);
                            eCalendario.Cancelacion = reader.GetDateTime(8);
                            eCalendario.Emision = reader.GetDateTime(9);         
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
        public List<ECalendarioDTO> PagosPendientes()
        {
            List<ECalendarioDTO> lista = new List<ECalendarioDTO>();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query = @"SELECT c.id, c.descripcion, a.dni, c.monto_pagado, c.monto_total, c.vencimiento, c.concepto_codigo
                            FROM calendarios c inner join alumnos a on a.id = c.alumno_id";
                        if (anio != "TODOS")
                            query += " WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio and c.monto_total::numeric <> 0 AND c.monto_pagado < c.monto_total";
                        else
                            query += " WHERE c.monto_total::numeric <> 0 AND c.monto_pagado < c.monto_total";


                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            if (anio != "TODOS")
                                cmd.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ECalendarioDTO calendario = new ECalendarioDTO();
                                    calendario.Id = reader.GetInt32(0);
                                    calendario.Descripcion = reader.GetString(1);
                                    calendario.Dni = reader.GetString(2);
                                    calendario.MontoPagado = reader.GetDecimal(3);
                                    calendario.MontoTotal = reader.GetDecimal(4);
                                    calendario.Vencimiento = reader.GetDateTime(5);
                                    calendario.ConceptoCodigo = reader.GetInt32(6);
                                    lista.Add(calendario);
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
        public DataTable ListarDeudoresXFecha(DateTime date)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"select a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", a.email as ""EMAIL"", c.concepto_codigo as ""CP"", c.descripcion as ""MOTIVO DE PAGO"",
                        (c.monto_total- c.monto_pagado) as ""MONTO"",c.emision as ""EMISION"" , c.vencimiento as ""VENCE EL"" from calendarios c
                        inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " where EXTRACT(YEAR FROM c.emision) = @anio and c.emision <= @date and c.monto_total <> c.monto_pagado order by a.apellidos_nombres";
                else 
                    query += " where c.emision <= @date and c.monto_total <> c.monto_pagado order by a.apellidos_nombres";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand( query, connection))
                {
                    command.Parameters.AddWithValue("date", date);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public decimal DeudaPorFecha(DateTime date)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"select sum(c.monto_total- c.monto_pagado)  from calendarios c";
                if (anio != "TODOS")
                    query += " where EXTRACT(YEAR FROM c.emision) = @anio and c.emision <= @date and c.monto_total <> c.monto_pagado ";
                else
                    query += " where c.emision <= @date and c.monto_total <> c.monto_pagado";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("date", date);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public DataTable ListarDeudoresXFechaXGrado(DateTime date, int grado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"select a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", a.email as ""EMAIL"", c.concepto_codigo as ""CP"", c.descripcion as ""MOTIVO DE PAGO"",
                        (c.monto_total- c.monto_pagado) as ""MONTO"", c.emision as ""EMISION"" , c.vencimiento as ""VENCE EL"" from calendarios c
                        inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " where EXTRACT(YEAR FROM c.emision) = @anio and c.emision <= @date and a.grado = @grado and c.monto_total <> c.monto_pagado order by a.apellidos_nombres";
                else
                    query += " where c.emision <= @date and a.grado = @grado and c.monto_total <> c.monto_pagado  order by a.apellidos_nombres";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("date", date);
                    command.Parameters.AddWithValue("grado", grado);
                    if(anio != "TODOS")
                        command.Parameters.AddWithValue("anio", Convert.ToInt32(anio));

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public decimal DeudaPorFechaXGrado(DateTime date, int grado)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"select sum(c.monto_total- c.monto_pagado) from calendarios c
                        inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " where EXTRACT(YEAR FROM c.emision) = @anio and c.emision <= @date and a.grado = @grado and c.monto_total <> c.monto_pagado";
                else
                    query += " where c.emision <= @date and a.grado = @grado and c.monto_total <> c.monto_pagado";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("date", date);
                    command.Parameters.AddWithValue("grado", grado);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal TotalPorGrado(int mes, int grado)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total) from calendarios c inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " where a.grado = @grado and EXTRACT(MONTH FROM c.vencimiento) = @mes and EXTRACT(YEAR FROM c.vencimiento) = @anio";
                else 
                    query += " where a.grado = @grado and EXTRACT(MONTH FROM c.vencimiento) = @mes";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("mes", mes);
                    command.Parameters.AddWithValue("grado", grado);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public DataTable ListarPencionesXMesXGrado(int mes, int grado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"select  a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", a.email as ""EMAIL"", c.concepto_codigo as ""CP"", c.descripcion as ""MOTIVO DE PAGO"",
                    c.monto_total as ""IMPORTE"", c.vencimiento as ""VENCE EL"" from calendarios c inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " where a.grado = @grado and EXTRACT(MONTH FROM c.vencimiento) = @mes and EXTRACT(YEAR FROM c.vencimiento) = @anio";
                else
                    query += " where a.grado = @grado and EXTRACT(MONTH FROM c.vencimiento) = @mes";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("mes", mes);
                    command.Parameters.AddWithValue("grado", grado);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable KardexGeneral()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", c.emision AS ""EMISION"", c.vencimiento AS ""VENCE EL"", c.concepto_codigo AS ""CP"", c.cancelacion AS ""CANCELADO"", 
                        c.descripcion AS ""MOTIVO DE PAGO"", c.monto_total AS ""IMPORTE"", c.mora AS ""IMP/MORA"" FROM calendarios c INNER JOIN 
                        alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio ORDER BY c.vencimiento";
                else
                    query += " ORDER BY c.vencimiento";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable KardexXAlumno(string dni)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT c.emision AS ""EMISION"", c.vencimiento AS ""VENCE EL"", c.concepto_codigo AS ""CP"", c.cancelacion AS ""CANCELADO"", 
                        c.descripcion AS ""MOTIVO DE PAGO"", c.monto_total AS ""IMPORTE"", c.mora AS ""IMP/MORA"" FROM calendarios c INNER JOIN 
                        alumnos a on a.id = c.alumno_id WHERE a.dni = @dni";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio ORDER BY c.vencimiento";
                else
                    query += " ORDER BY c.vencimiento";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@dni", dni);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable KardexXGrado(int grado, int seccion)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", c.emision AS ""EMISION"", c.vencimiento AS ""VENCE EL"", c.concepto_codigo AS ""CP"", c.cancelacion AS ""CANCELADO"", 
                        c.descripcion AS ""MOTIVO DE PAGO"", c.monto_total AS ""IMPORTE"", c.mora AS ""IMP/MORA"" FROM calendarios c INNER JOIN 
                        alumnos a on a.id = c.alumno_id WHERE a.grado = @grado and a.seccion = @seccion";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio ORDER BY c.vencimiento";
                else
                    query += " ORDER BY c.vencimiento";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@grado", grado);
                    command.Parameters.AddWithValue("@seccion", seccion);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public decimal PagadoPorAlumno(string dni)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total + c.mora) from calendarios c inner join alumnos a on a.id = c.alumno_id  where c.monto_total <= c.monto_pagado and a.dni = @dni";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("dni", dni);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal DeudaPorAlumno(string dni)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total - c.monto_pagado) from calendarios c inner join alumnos a on a.id = c.alumno_id where c.monto_total > c.monto_pagado and a.dni = @dni";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("dni", dni);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal PagadoGeneral()
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total + c.mora) from calendarios c inner join alumnos a on a.id = c.alumno_id  where c.monto_total <= c.monto_pagado";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal DeudaGeneral()
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total - c.monto_pagado) from calendarios c inner join alumnos a on a.id = c.alumno_id where c.monto_total > c.monto_pagado";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal PagadoGradoSeccion(int grado, int seccion)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total + c.mora) from calendarios c inner join alumnos a on a.id = c.alumno_id  where c.monto_total <= c.monto_pagado and a.grado = @grado and a.seccion = @seccion";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("grado", grado);
                    command.Parameters.AddWithValue("seccion", seccion);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal DeudaGradoSeccion(int grado, int seccion)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total - c.monto_pagado) from calendarios c inner join alumnos a on a.id = c.alumno_id where c.monto_total > c.monto_pagado and a.grado = @grado and a.seccion = @seccion";
                if (anio != "TODOS")
                    query += " and EXTRACT(YEAR FROM c.vencimiento) = @anio";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("grado", grado);
                    command.Parameters.AddWithValue("seccion", seccion);
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public DataTable BuscarKardexGeneral(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", c.emision AS ""EMISION"", c.vencimiento AS ""VENCE EL"", c.concepto_codigo AS ""CP"", c.cancelacion AS ""CANCELADO"", 
                        c.descripcion AS ""MOTIVO DE PAGO"", c.monto_total AS ""IMPORTE"", c.mora AS ""IMP/MORA"" FROM calendarios c INNER JOIN 
                        alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += @" WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio AND (LOWER(a.dni) LIKE LOWER(@valor_buscado) 
                     OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) OR LOWER(a.apellidos_nombres) LIKE LOWER(@valor_buscado))";
                else
                    query += @" WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) 
                     OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) OR LOWER(a.apellidos_nombres) LIKE LOWER(@valor_buscado)";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public decimal PagadoBusquedaGeneral(string valorBuscado)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total + c.mora) from calendarios c inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += @" WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio AND (LOWER(a.dni) LIKE LOWER(@valor_buscado) 
                     OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) OR LOWER(a.apellidos_nombres) LIKE LOWER(@valor_buscado))";
                else
                    query += @" WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) 
                     OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) OR LOWER(a.apellidos_nombres) LIKE LOWER(@valor_buscado)";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        public decimal DeudaBusquedaGeneral(string valorBuscado)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = "select sum(c.monto_total - c.monto_pagado) from calendarios c inner join alumnos a on a.id = c.alumno_id";
                if (anio != "TODOS")
                    query += @" WHERE EXTRACT(YEAR FROM c.vencimiento) = @anio AND (LOWER(a.dni) LIKE LOWER(@valor_buscado) 
                     OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) OR LOWER(a.apellidos_nombres) LIKE LOWER(@valor_buscado))";
                else
                    query += @" WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) 
                     OR LOWER(c.descripcion) LIKE LOWER(@valor_buscado) OR LOWER(a.apellidos_nombres) LIKE LOWER(@valor_buscado)";

                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("@anio", Convert.ToInt32(anio));

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
        
    }
}
