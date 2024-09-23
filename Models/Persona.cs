using System;

namespace DueñoMascota.Models
{    
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public decimal Estatura { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
