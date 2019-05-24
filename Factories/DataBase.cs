using System.Data.Common;
//Implmentando el patron Factory.
//Definicion: Es un patron de diseño que resume la creacion de objetos y permite que estos se terminen de crear mientras se ejecutan.
//Este patron nos facilita la creacion de familias de objetos.
namespace Factories
{
    public abstract class DataBase
        //Fabrica donde creamos los objetos sin especificar su clase.
        //Define un metodo para que una clase difiera la creacion de instancias.
    {
        public abstract DbConnection AbrirConexion();
        public abstract void CrearConexion();
        //Propiedades.
        public abstract string DatabaseName { get; set; }
        public abstract string Host { get; set; }
        public abstract string User { get; set; }
        public abstract string Password { get; set; }
    }
}
