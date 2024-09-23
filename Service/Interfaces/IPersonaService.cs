using DueñoMascota.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DueñoMascota.Services.Interfaces
{
    public interface IPersonaService
    {
        Task<List<Persona>> ObtenerTodasAsync();
        Task<Persona> ObtenerPorIdAsync(int idPersona);
        Task CrearAsync(Persona persona);
        Task ActualizarAsync(Persona persona);
        Task EliminarAsync(int idPersona);
    }
}
