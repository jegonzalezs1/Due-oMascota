using DueñoMascota.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DueñoMascota.Repository.Interfaces
{
    public interface IMascotaRepository
    {
        Task InsertarAsync(Mascota mascota);
        Task ActualizarAsync(Mascota mascota);
        Task EliminarAsync(int idMascota);
        Task<Mascota> ObtenerPorIdAsync(int idMascota);
        Task<List<Mascota>> ObtenerTodasAsync();
    }
}
