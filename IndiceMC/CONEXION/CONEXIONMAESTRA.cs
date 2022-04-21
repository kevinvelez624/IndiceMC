using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

 
namespace IndiceMC.CONEXION
{
    class CONEXIONMAESTRA
    {
        public static string conexion = ("Data Source = localhost:1433; Initial Catalog = DBXAMARIN;Integrated Security = false;User ID = bd1; Password = 12345");

        public static SqlConnection conectar = new SqlConnection(conexion);

        public static void Abrir()
        {
            if (conectar.State == ConnectionState.Closed)
            {
                conectar.Open();
            }
        }

        public static void Cerrar()
        {
            if (conectar.State == ConnectionState.Open)
            {
                conectar.Close();
            }
        }
    }
}
