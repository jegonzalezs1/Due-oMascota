using DueñoMascota.Models;
using DueñoMascota.Repository;
using DueñoMascota.Repository.Interfaces;
using DueñoMascota.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DueñoMascota.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaService(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<List<Persona>> ObtenerTodasAsync()
        {
            return await _personaRepository.ObtenerTodasAsync();
        }

        public async Task<Persona> ObtenerPorIdAsync(int idPersona)
        {
            return await _personaRepository.ObtenerPorIdAsync(idPersona);
        }

        public async Task CrearAsync(Persona persona)
        {
            await _personaRepository.InsertarAsync(persona);
        }

        public async Task ActualizarAsync(Persona persona)
        {
            await _personaRepository.ActualizarAsync(persona);
        }

        public async Task EliminarAsync(int idPersona)
        {
            await _personaRepository.EliminarAsync(idPersona);
        }
    }
}
