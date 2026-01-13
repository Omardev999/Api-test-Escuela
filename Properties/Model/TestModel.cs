using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Db_Escuela.Properties.Model;

public class TestModel
{
    private DbConnectionStringBuilder builder;
    private readonly IConfiguration _configuration;

    public TestModel(IConfiguration configuration)
    {
        _configuration = configuration;
        builder = new DbConnectionStringBuilder();
    }

    public DbConnectionStringBuilder GetBuilder()
    {
        return builder;
    }

    public IConfiguration GetConfiguration()
    {
        return _configuration;
    }
    
    public List<String> Matricula()
    {
       
        // Configuración de la cadena de conexión
        DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
        builder.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
        using (SqlConnection conn = new SqlConnection(builder.ConnectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("✅ Conexión exitosa");
                
                List<String> matriculas = new List<String>();
                
                // Query simple
                string query = "SELECT * FROM core.Alumno";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Obtener todas las columnas disponibles
                            var columns = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                columns.Add(reader.GetName(i));
                            }
                            Console.WriteLine($"Columnas disponibles: {string.Join(", ", columns)}");
                            
                            string matricula = reader["Matricula"]?.ToString() ?? "";
                            Console.WriteLine($"Matrícula: {matricula}");
                            matriculas.Add(matricula);
                        }
                    }
                }
                return  matriculas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return new List<String>();
            }
        }
       
    }
}