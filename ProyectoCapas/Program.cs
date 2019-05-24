using CapaLogica;
using System;

namespace ProyectoCapas
{
    class Program
    {
		//Inicia programa
        static void Main(string[] args)
        {
            var dbKind = Logica.SeleccionarDB();
            var opcion = Logica.SeleccionarOpcion();
            Logica.EjecutarOpcion(opcion, dbKind);
            Console.ReadKey();
        }
    }
}
