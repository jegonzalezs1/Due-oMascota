using DueñoMascota.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DueñoMascota.Services.Interfaces
{
    public interface IMascotaService
    {
        Task<List<Mascota>> ObtenerTodasAsync();
        Task<Mascota> ObtenerPorIdAsync(int idMascota);
        Task CrearAsync(Mascota mascota);
        Task ActualizarAsync(Mascota mascota);
        Task EliminarAsync(int idMascota);
    }
}
