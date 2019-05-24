//Using: 'Directiva' Resuelve referencias.
using System.Collections.Generic;//Contiene interfaces y clases que definen colecciones genericas.
//Namespace: Organizaxcion del codigo.
namespace CapaDatos
{
    //Interface: contiene definiciones para un grupo de funcionalidades relacionadas que una clase o estructura puede implementar.
    //Public: sin restricciones.(Clases, metodos, variables, estructuras.)
    //where : class representa una restriccion, T sera solamente una clase.

        //Implementando el patron Repository.
        //Es un patron de diseño intermediario entre nuestra logica de negocio y nustra logica de acceso a las bases de datos.
        //Nos ayuda a gestionar grandes cantidades de consultas sin afectar la logica de negocio.
    public interface IRepository<T> where T : class
    {
        //Ienumerable: Expone un enumerador que se puede iterar.
        //T: Generico para tipo de dato especifico.
        //Definiciones que la interfaz implementara.
        //Void: no retorna nada.
        IEnumerable<T> ObtenerTodos();
        void Insertar(T entity);
        void Actualizar(T entity);
        void Eliminar(int id);
    }
}

