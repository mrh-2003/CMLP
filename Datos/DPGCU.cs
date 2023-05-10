using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DPGCU
    {
        private readonly string connectionString = Utilidades.cadena();
        public DataTable Listar()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                string query = @"SELECT id as ""ID"", codigo as ""CODIGO"", nombres as ""APELLIDOS Y NOMBRES"", importe as ""IMPORTE"", mora as ""MORA"", fecha as ""FECHA/PAGO"" FROM public.pgcu";
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
