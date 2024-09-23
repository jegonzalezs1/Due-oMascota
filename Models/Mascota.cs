namespace DueñoMascota.Models
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public string Color { get; set; }
        public int Edad { get; set; }
        public int IdPersona { get; set; }
        public Persona? Dueno { get; set; }
    }
}
