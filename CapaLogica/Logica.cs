using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using CapaEntidades;
using CapaDatos;

namespace CapaLogica
{
    public class Logica
    {
        public static DbKind SeleccionarDB()
        {
            var dbKind = DbKind.PostgreSql;
            Console.WriteLine("Elija una base de datos:");
            Console.WriteLine("1 => SqlServer");
            Console.WriteLine("2 => PosgreSql");
            Console.WriteLine("3 => Salir del programa");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine();
                    Console.WriteLine("Usted selecciono la base de datos DbKind.SqlServer");
                    dbKind = DbKind.SqlServer;
                    break;
                case "2":
                    Console.WriteLine("Usted selecciono la base de datos DbKind.PostgreSql");
                    dbKind = DbKind.PostgreSql;
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("No selecciono una opcion correcta, por defecto se conectara a la base de datos DbKind.SqlServer");
                    dbKind = DbKind.SqlServer;
                    break;
            }
            return dbKind;
        }


        public static void InsertarCliente(DbKind dbKind)
        {
            while (true)
            {
                Cliente cliente = new Cliente();
                List<string> errors = new List<string>();

                Console.Write("Ingrese Nombre: ");
                cliente.Nombre = Console.ReadLine();
                if (string.IsNullOrEmpty(cliente.Nombre) || string.IsNullOrWhiteSpace(cliente.Nombre))
                    errors.Add("=>El nombre no puede quedar vacio.");
                if (cliente.Nombre.Length < 5)
                    errors.Add("=>El nombre debe tener al menos 5 caracteres.");
                bool result = Regex.IsMatch(cliente.Nombre, @"^[a-zA-Z\sa-zA-ZZñÑáéíóúÁÉÍÓÚ]+$");
                if (!result)
                    errors.Add("=>El nombre solo debe contener letras del alfabeto.");

                Console.Write("Ingrese Telefono:");
                bool EsTelefono = decimal.TryParse(Console.ReadLine(), out decimal telefono);
                if (!EsTelefono)
                    errors.Add("=>El telefono no debe tener caracteres diferentes a los numericos.");
                cliente.Telefono = telefono;

                Console.Write("Ingrese Email: ");
                cliente.Email = Console.ReadLine();
                if (string.IsNullOrEmpty(cliente.Email) || string.IsNullOrWhiteSpace(cliente.Email))
                    errors.Add("=>El formato de email es incorrecto.");
                if (!EsEmail(cliente.Email))
                    errors.Add("=>Ingrese un email valido.");

                Console.Write("Ingrese Fecha de nacimiento (yyyy-mm-dd):");
                bool EsFecha = DateTime.TryParse(Console.ReadLine(), out DateTime fechaNacimiento);
                if (!EsFecha)
                    errors.Add("=>Ingrese una fecha valida (yyyy-mm-dd).");
                DateTime max = new DateTime(2015, 12, 31);
                if (fechaNacimiento >= max)
                    errors.Add("=>La fecha no debe contener datos superiores al año 2015.");
                DateTime min = new DateTime(1800, 12, 31);
                if (fechaNacimiento <= min)
                    errors.Add("=>La fecha no debe contener datos inferiores al año 1880.");
                cliente.FechaNacimiento = fechaNacimiento;

                if (errors.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Su regitro no fue exitoso, verifique los datos ingresados:");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    errors.ForEach(Console.WriteLine);
                    Console.ResetColor();
                    continue;
                }

                var db = Utilidades.ObtenerConexionPorTipo(dbKind);
                ClienteRepository clienteRepository = new ClienteRepository(db);
                clienteRepository.Insertar(cliente);
                Console.WriteLine("¡Su nuevo registro se ha guardado en la base de datos!");
                break;
            }
        }


        public static bool EsEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            Regex Verif = new Regex(@"^[A-Za-z0-9_.\-]+@[A-Za-z0-9_\-]+\.([A-Za-z0-9_\-]+\.)*[A-Za-z][A-Za-z]+$", RegexOptions.IgnoreCase);
            return Verif.Match(email).Success;
        }


        public static IEnumerable<Cliente> ObtenerRegistros(DbKind dbKind)
        {
            var db = Utilidades.ObtenerConexionPorTipo(dbKind);
            ClienteRepository clienteRepository = new ClienteRepository(db);
            var dbResult = clienteRepository.ObtenerTodos();
            return dbResult;
        }


        public static void SalirConsola()
        {
            Console.Beep();
            Environment.Exit(0);
        }


        public static void ExportarRegistros(DbKind dbKind)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\DESARROLLADOR 9\Documents\DatabaseClientes.txt");
                var dbResult = ObtenerRegistros(dbKind);
                dbResult.ToList().ForEach(sw.WriteLine);
                sw.Close();
                Console.WriteLine("Los registro se han exportado al archivo DatabaseClientes.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }


        public static int SeleccionarOpcion()
        {
            Console.WriteLine();
            Console.WriteLine("Menu de opciones:");
            Console.WriteLine("=> 1 - Ingresar un nuevo registro.");
            Console.WriteLine("=> 2 - Obtener todos los registros.");
            Console.WriteLine("=> 3 - Exportar los registros a un archivo de texto.");
            Console.WriteLine("=> 4 - Salir del programa.");
            Console.WriteLine("=> 5 - Volver.");
            int opcion = 0;
            switch (Console.ReadLine())
            {
                case "1":
                    opcion = 1;
                    break;
                case "2":
                    opcion = 2;
                    break;
                case "3":
                    opcion = 3;
                    break;
                case "4":
                    opcion = 4;
                    break;
                case "5":
                    opcion = 5;
                    break;
                default:
                    opcion = 7;
                    break;
            }
            return opcion;
        }


        public static void EjecutarOpcion(int opcion, DbKind dbKind)
        {
            switch (opcion)
            {
                case 1:
                    InsertarCliente(dbKind);
                    Console.WriteLine();
                    CargarMenu();
                    break;
                case 2:
                    ObtenerRegistros(dbKind).ToList().ForEach(Console.WriteLine);
                    Console.WriteLine();
                    CargarMenu();
                    break;
                case 3:
                    ExportarRegistros(dbKind);
                    Console.WriteLine();
                    CargarMenu();
                    break;
                case 4:
                    SalirConsola();
                    break;
                case 5:
                    SeleccionarDB();
                    SeleccionarOpcion();
                    EjecutarOpcion(opcion, dbKind);
                    break;
                default:
                    Console.WriteLine("Debe seleccionar una opcion.");
                    CargarMenu();
                    break;
            }
        }


        public static void CargarMenu()
        {
            var dbKind = SeleccionarDB();
            var opcion = SeleccionarOpcion();
            EjecutarOpcion(opcion, dbKind);
        }
    }
}

