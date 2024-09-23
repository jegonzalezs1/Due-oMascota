using DueñoMascota.Models;
using DueñoMascota.Repository;
using DueñoMascota.Repository.Interfaces;
using DueñoMascota.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DueñoMascota.Services
{
    public class MascotaService : IMascotaService
    {
        private readonly IMascotaRepository _mascotaRepository;

        public MascotaService(IMascotaRepository mascotaRepository)
        {
            _mascotaRepository = mascotaRepository;
        }

        public async Task<List<Mascota>> ObtenerTodasAsync()
        {
            return await _mascotaRepository.ObtenerTodasAsync();
        }

        public async Task<Mascota> ObtenerPorIdAsync(int idMascota)
        {
            return await _mascotaRepository.ObtenerPorIdAsync(idMascota);
        }

        public async Task CrearAsync(Mascota mascota)
        {
            await _mascotaRepository.InsertarAsync(mascota);
        }

        public async Task ActualizarAsync(Mascota mascota)
        {
            await _mascotaRepository.ActualizarAsync(mascota);
        }

        public async Task EliminarAsync(int idMascota)
        {
            await _mascotaRepository.EliminarAsync(idMascota);
        }
    }
}
