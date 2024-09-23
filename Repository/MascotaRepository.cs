using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DueñoMascota.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using DueñoMascota.Repository.Interfaces;

namespace DueñoMascota.Repository
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly string _connectionString;

        public MascotaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevConnection");
        }

        public async Task InsertarAsync(Mascota mascota)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_mascota", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accion", "I");
                cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
                cmd.Parameters.AddWithValue("@especie", mascota.Especie);
                cmd.Parameters.AddWithValue("@raza", mascota.Raza);
                cmd.Parameters.AddWithValue("@color", mascota.Color);
                cmd.Parameters.AddWithValue("@edad", mascota.Edad);
                cmd.Parameters.AddWithValue("@idP", mascota.IdPersona);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ActualizarAsync(Mascota mascota)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_mascota", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accion", "U");
                cmd.Parameters.AddWithValue("@idM", mascota.IdMascota);
                cmd.Parameters.AddWithValue("@nombre", mascota.Nombre);
                cmd.Parameters.AddWithValue("@especie", mascota.Especie);
                cmd.Parameters.AddWithValue("@raza", mascota.Raza);
                cmd.Parameters.AddWithValue("@color", mascota.Color);
                cmd.Parameters.AddWithValue("@edad", mascota.Edad);
                cmd.Parameters.AddWithValue("@idP", mascota.IdPersona);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarAsync(int idMascota)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_mascota", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accion", "D");
                cmd.Parameters.AddWithValue("@idM", idMascota);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Mascota> ObtenerPorIdAsync(int idMascota)
        {
            Mascota mascota = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_mascota", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "C");
                cmd.Parameters.AddWithValue("@idM", idMascota);

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        mascota = new Mascota
                        {
                            IdMascota = Convert.ToInt32(reader["id_mascota"]),
                            Nombre = reader["nombre_mascota"].ToString(),
                            Especie = reader["especie"].ToString(),
                            Raza = reader["raza"].ToString(),
                            Color = reader["color"].ToString(),
                            Edad = Convert.ToInt32(reader["edad"]),
                            IdPersona = Convert.ToInt32(reader["id_persona"]),
                            Dueno = new Persona
                            {
                                IdPersona = Convert.ToInt32(reader["id_persona"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Identificacion = reader["identificacion"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                                Edad = Convert.ToInt32(reader["edad"]),
                                Estatura = Convert.ToDecimal(reader["estatura"]),
                                Direccion = reader["direccion"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            }
                        };
                    }
                }
            }

            return mascota;
        }

        public async Task<List<Mascota>> ObtenerTodasAsync()
        {
            var mascotas = new List<Mascota>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_mascota", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "G");

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var mascota = new Mascota
                        {
                            IdMascota = Convert.ToInt32(reader["id_mascota"]),
                            Nombre = reader["nombre_mascota"].ToString(),
                            Especie = reader["especie"].ToString(),
                            Raza = reader["raza"].ToString(),
                            Color = reader["color"].ToString(),
                            Edad = Convert.ToInt32(reader["edad"]),
                            IdPersona = Convert.ToInt32(reader["id_persona"]),
                            Dueno = new Persona
                            {
                                IdPersona = Convert.ToInt32(reader["id_persona"]),
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Identificacion = reader["identificacion"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                                Edad = Convert.ToInt32(reader["edad"]),
                                Estatura = Convert.ToDecimal(reader["estatura"]),
                                Direccion = reader["direccion"].ToString(),
                                Telefono = reader["telefono"].ToString()
                            }
                        };
                        mascotas.Add(mascota);
                    }
                }
            }

            return mascotas;
        }
    }
}
