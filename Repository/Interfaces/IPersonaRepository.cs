using DueñoMascota.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DueñoMascota.Repository.Interfaces
{
    public interface IPersonaRepository
    {
        Task InsertarAsync(Persona persona);
        Task ActualizarAsync(Persona persona);
        Task EliminarAsync(int idPersona);
        Task<Persona> ObtenerPorIdAsync(int idPersona);
        Task<List<Persona>> ObtenerTodasAsync();
    }
}
