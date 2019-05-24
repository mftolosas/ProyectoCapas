using System;
using System.Data;
using Factories;
using System.Data.Common;
using System.Data.SqlClient;//Referencia conexion Sql.

namespace CapaDatos
{
    //Implmentacion de la fabrica.
    public class SqlServerDataBase : DataBase, IDisposable
    {
        //Conexion a SqlServer.
        private SqlConnection _conexion;

        public override void CrearConexion()
        {
            _conexion = new SqlConnection($"Data Source={Host};Initial Catalog={DatabaseName};User Id={User};Password={Password}");
        }

        public override DbConnection AbrirConexion()
        {
            if (_conexion.State == ConnectionState.Closed)
                _conexion.Open();
            return _conexion;
        }

        public void Dispose()
        {
            if (_conexion.State == ConnectionState.Open)
                _conexion.Close();
        }

        public override string Host { get; set; }
        public override string User { get; set; }
        public override string Password { get; set; }
        public override string DatabaseName { get; set; }
    }
}

