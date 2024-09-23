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
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _connectionString;

        public PersonaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevConnection");
        }

        public async Task InsertarAsync(Persona persona)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_persona", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accion", "I");
                cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("@apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("@cedula", persona.Identificacion);
                cmd.Parameters.AddWithValue("@fnac", persona.FechaNacimiento);
                cmd.Parameters.AddWithValue("@edad", persona.Edad);
                cmd.Parameters.AddWithValue("@estatura", persona.Estatura);
                cmd.Parameters.AddWithValue("@direccion", persona.Direccion);
                cmd.Parameters.AddWithValue("@telefono", persona.Telefono);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ActualizarAsync(Persona persona)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_persona", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accion", "U");
                cmd.Parameters.AddWithValue("@idP", persona.IdPersona);
                cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("@apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("@cedula", persona.Identificacion);
                cmd.Parameters.AddWithValue("@fnac", persona.FechaNacimiento);
                cmd.Parameters.AddWithValue("@edad", persona.Edad);
                cmd.Parameters.AddWithValue("@estatura", persona.Estatura);
                cmd.Parameters.AddWithValue("@direccion", persona.Direccion);
                cmd.Parameters.AddWithValue("@telefono", persona.Telefono);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task EliminarAsync(int idPersona)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_persona", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accion", "D");
                cmd.Parameters.AddWithValue("@idP", idPersona);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Persona> ObtenerPorIdAsync(int idPersona)
        {
            Persona persona = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_persona", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "C");
                cmd.Parameters.AddWithValue("@idP", idPersona);

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        persona = new Persona
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
                        };
                    }
                }
            }

            return persona;
        }

        public async Task<List<Persona>> ObtenerTodasAsync()
        {
            var personas = new List<Persona>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_crud_persona", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@accion", "G");

                conn.Open();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var persona = new Persona
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
                        };
                        personas.Add(persona);
                    }
                }
            }

            return personas;
        }
    }
}
