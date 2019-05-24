using Factories;//Fabrica de creacion.
using System;
using System.Collections.Generic;
using System.Data.Common;
using CapaEntidades;

namespace CapaDatos
{
    /// <summary>
    /// Creamos la clase publica, la cual sera la encargada de implementar la interfaz IRepository y le pasaremos la entidad Cliente. 
    /// </summary>
    public class ClienteRepository : IRepository<Cliente>
    {
        //Los miembros privados son accesibles solamente dentro de la clase o estructura donde se definen.
        //Creamos la variable _db de la clase DbConnection para representar la conexion a la base de datos.
        private DbConnection _db;
        /// <summary>
        /// Abrimos la conexion.
        /// =>: Define el cuerpo de la expresion. 
        /// </summary>
        /// <param variable database="db"></param>
        public ClienteRepository(DataBase db) => _db = db.AbrirConexion();

        //Metodos sin uso.
        public void Actualizar(Cliente entity)
        {
            throw new NotImplementedException();
        }


        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }


        public void Insertar(Cliente entity)
        {
            //Using: Implementa el IDisposable en un bloque de codigo.
            //Idisposable: Libera los recursos de un bloque de codigo. 
            //Var: Variable local con asignacion que el compilador deducira.
            //CreateCommand: Crea y devueleve una conexion.
            using (var cmd = _db.CreateCommand())
            {
                cmd.Connection = _db;
                cmd.CommandText = @"
                        INSERT INTO
                            Clientes(Nombre, Telefono, Email, FechaNacimiento)
                        VALUES (
                            @Nombre,
                            @Tel,
                            @Email,
                            @FhNacimiento
                        )
                       ";
                var param = cmd.CreateParameter();
                param.ParameterName = "@Nombre";
                param.Value = entity.Nombre;
                cmd.Parameters.Add(param);

                param = cmd.CreateParameter();
                param.ParameterName = "@Tel";
                param.Value = entity.Telefono;
                cmd.Parameters.Add(param);

                param = cmd.CreateParameter();
                param.ParameterName = "@Email";
                param.Value = entity.Email;
                cmd.Parameters.Add(param);

                param = cmd.CreateParameter();
                param.ParameterName = "@FhNacimiento";
                param.Value = entity.FechaNacimiento;
                cmd.Parameters.Add(param);

                //Ejecuta una instruccion Sql.
                cmd.ExecuteNonQuery();
            }
        }


        public IEnumerable<Cliente> ObtenerTodos()
        {
            var clientes = new List<Cliente>();
            using (var cmd = _db.CreateCommand())
            {
                cmd.Connection = _db;
                cmd.CommandText = @"
                    SELECT 
                       Id, Nombre, Telefono, Email, FechaNacimiento
                    FROM  
                       Clientes
                    ";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var cliente = new Cliente
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = Convert.ToString(reader["Nombre"]),
                        Telefono = Convert.ToDecimal(reader["Telefono"]),
                        Email = Convert.ToString(reader["Email"]),
                        FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"])
                    };

                    clientes.Add(cliente);
                }
            }
            return clientes;
        }
    }
}
