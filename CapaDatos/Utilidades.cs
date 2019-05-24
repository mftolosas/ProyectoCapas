using CapaEntidades;
using Factories;

namespace CapaDatos
{
    public class Utilidades
    {
        //Static hace que un elemento no sea instanciable(New).
        //Se puede acceder al metodo mediante la clase (Utilidades.ObtenerConexion();).
        public static DataBase ObtenerConexionPorTipo(DbKind dbKind)
        {
            DataBase dataBase;
            switch (dbKind)
            {
                case DbKind.SqlServer:

                    dataBase = new SqlServerDataBase
                    {
                        DatabaseName = "BASE DE DATOS",
                        Host = "AZDESAR9\\SQLEXPRESS",
                        User = "usrchicharrones",
                        Password = "U$r#Ch1ch@rr0N3s"
                    };
                    dataBase.CrearConexion();
                    break;

                case DbKind.PostgreSql:
                    dataBase = new PostgreSqlDataBase
                    {
                        DatabaseName = "dvdrental",
                        Host = "127.0.0.1",
                        User = "postgres",
                        Password = "master"
                    };
                    dataBase.CrearConexion();
                    break;
                default:
                    dataBase = new SqlServerDataBase
                    {
                        DatabaseName = "BASE DE DATOS",
                        Host = "AZDESAR9\\SQLEXPRESS",
                        User = "usrchicharrones",
                        Password = "U$r#Ch1ch@rr0N3s"
                    };
                    dataBase.CrearConexion();
                    break;
            }

            return dataBase;
        }          
    }
}
