using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entidades;
using Npgsql;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace Datos
{
    public class DBoleta
    {
        private readonly string anio = Utilidades.anio;
        private readonly string connectionString = Utilidades.cadena();
        public string Mantenimiento(EBoleta boleta, string opcion)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    string mensaje;
                    EBoleta eBoleta = null;
                    EAlumno eAlumno = (new DAlumno()).getAlumnoById(boleta.AlumnoId);
                    if (opcion != "insert")
                        eBoleta =getBoleta(boleta.Codigo);
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        string query;
                        if (opcion == "insert")
                        {
                            query = "INSERT INTO boletas (codigo, alumno_id, monto, fecha, concepto_codigo) VALUES (@codigo, @alumno_id, @monto, @fecha, @concepto_codigo)";
                            mensaje = "Se inserto correctamente la Boleta" + boleta.Codigo + " del alumno "+eAlumno.Dni+ " cuyo monto es " + boleta.Monto + " - Fecha: " + boleta.Fecha.ToString();
                        }
                        else if (opcion == "update")
                        {
                            query = "UPDATE boletas SET monto = @monto, fecha = @fecha, concepto_codigo = @concepto_codigo WHERE codigo = @codigo";
                            mensaje = "Se actualizo correctamente la Boleta " + boleta.Codigo + " ANTES: " + eAlumno.Dni + " - " + eBoleta.Monto + " - " + eBoleta.Fecha.ToString() + " AHORA: " + boleta.Monto + " - " + boleta.Fecha.ToString();
                        }
                        else
                        {
                            query = "DELETE FROM boletas WHERE codigo = @codigo";
                            mensaje = "Se elimno correctamente la Boleta " + boleta.Codigo + " con datos: " + eAlumno.Dni + " - " + boleta.Monto + " - "+boleta.Fecha.ToString();
                        }
                        using (var cmd = new NpgsqlCommand(query, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@codigo", boleta.Codigo);
                            cmd.Parameters.AddWithValue("@alumno_id", boleta.AlumnoId);
                            cmd.Parameters.AddWithValue("@monto", boleta.Monto);
                            cmd.Parameters.AddWithValue("@fecha", boleta.Fecha);
                            cmd.Parameters.AddWithValue("@concepto_codigo", boleta.ConceptoCodigo);
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
                string query = @"SELECT b.codigo as ""CODIGO"", a.dni as ""DNI"", b.monto as ""MONTO"", b.fecha as ""FECHA"", b.concepto_codigo as ""CP"" FROM boletas b INNER JOIN alumnos a ON a.id = b.alumno_id";
                if(anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM b.fecha) = @anio";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    if(anio != "TODOS")
                        command.Parameters.AddWithValue("anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable ListarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(@"SELECT b.codigo as ""CODIGO"", a.dni as ""DNI"", b.monto as ""MONTO"", b.fecha as ""FECHA"", b.concepto_codigo as ""CP"" FROM boletas b INNER JOIN 
                    alumnos a ON a.id = b.alumno_id WHERE fecha BETWEEN @fechaInicio AND @fechaFin", connection))
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
        public DataTable ListarPorConceptos(DateTime fechaInicio, DateTime fechaFin, int concepto)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(@"SELECT b.codigo as ""CODIGO"", a.dni as ""DNI"", a.apellidos_nombres as ""APELLIDOS Y NOMBRES"", b.monto as ""MONTO"", b.fecha as ""FECHA"", b.concepto_codigo as ""CPO"" FROM boletas b INNER JOIN 
                    alumnos a ON a.id = b.alumno_id WHERE b.fecha BETWEEN @fechaInicio AND @fechaFin and b.concepto_codigo = @concepto", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);
                    command.Parameters.AddWithValue("concepto", concepto);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }
        public DataTable BuscarPorCodigoOdni(string valorBuscado)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT b.codigo as ""CODIGO"", a.dni as ""DNI"", b.monto as ""MONTO"", b.fecha as ""FECHA"", b.concepto_codigo as ""CP""FROM boletas b INNER JOIN 
                    alumnos a ON a.id = b.alumno_id";
                if (anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM b.fecha) = @anio and (LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(b.codigo) LIKE LOWER(@valor_buscado))";
                else
                    query += " WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(b.codigo) LIKE LOWER(@valor_buscado)";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand( query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
                    if (anio != "TODOS")
                        command.Parameters.AddWithValue("anio", Convert.ToInt32(anio));
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
        }

        public EBoleta getBoleta(string codigo)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM boletas WHERE codigo=@codigo", conn))
                    {
                        cmd.Parameters.AddWithValue("@codigo", codigo);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            EBoleta eBoleta = new EBoleta()
                            {
                                Codigo = reader.GetString(0),
                                AlumnoId = reader.GetInt32(1),
                                Monto = reader.GetDecimal(2),
                                Fecha = reader.GetDateTime(3),
                                ConceptoCodigo = reader.GetInt32(4),
                            };
                            return eBoleta;
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
        public decimal TotalPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(monto) FROM boletas WHERE fecha BETWEEN @fechaInicio AND @fechaFin;", connection))
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
        public decimal TotalPorConceptos(DateTime fechaInicio, DateTime fechaFin, int concepto)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand("SELECT SUM(monto) FROM boletas WHERE fecha BETWEEN @fechaInicio AND @fechaFin and concepto_codigo = @concepto;", connection))
                {
                    command.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("fechaFin", fechaFin);
                    command.Parameters.AddWithValue("concepto", concepto);

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
                string query = "SELECT SUM(monto) FROM boletas";
                if(anio != "TODOS")
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
        public decimal TotalBuscarPorCodigoOdni(string valorBuscado)
        {
            decimal total = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT  SUM(b.monto) FROM boletas b INNER JOIN 
                    alumnos a ON a.id = b.alumno_id";
                if (anio != "TODOS")
                    query += " WHERE EXTRACT(YEAR FROM b.fecha) = @anio and (LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(b.codigo) LIKE LOWER(@valor_buscado))";
                else
                    query += " WHERE LOWER(a.dni) LIKE LOWER(@valor_buscado) OR LOWER(b.codigo) LIKE LOWER(@valor_buscado)";
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@valor_buscado", "%" + valorBuscado.ToLower() + "%");
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
    }
}
