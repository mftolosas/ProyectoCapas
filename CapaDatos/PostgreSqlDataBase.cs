using System;
using System.Data;
using Npgsql;//Referencia Nuget.
using Factories;
using System.Data.Common;

namespace CapaDatos
{
    //Implementacion de la fabrica. 
    public class PostgreSqlDataBase : DataBase, IDisposable
    {
        //Conexion a PostgreSql.
        private NpgsqlConnection _conexion;
        //Usamos Override para modificar el metodo de fabrica abstracto.
        public override void CrearConexion()
        {
            _conexion = new NpgsqlConnection($"Host={Host};Username={User};Password={Password};Database={DatabaseName}");
        }

        public void Dispose()
        {
            if (_conexion.State == ConnectionState.Open)
                _conexion.Close();
        }

        public override DbConnection AbrirConexion()
        {
            if (_conexion.State == ConnectionState.Closed)
                _conexion.Open();
            return _conexion;
        }
        //Propideades de fabrica.
        public override string Host { get; set; }
        public override string User { get; set; }
        public override string Password { get; set; }
        public override string DatabaseName { get; set; }
    }
}
