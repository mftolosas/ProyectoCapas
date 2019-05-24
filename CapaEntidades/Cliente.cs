using System;
namespace CapaEntidades
{
    //Entidad cliente para almacenar nuestros registros.
    public class Cliente
    {
        //Propiedades.
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Telefono { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public override string ToString()
        {
            return $@"
 ===>  {Id} || {Nombre} || {Telefono} || {Email} || {FechaNacimiento.ToString("yyyy-mm-dd")}
            ";
        }
    }
}
