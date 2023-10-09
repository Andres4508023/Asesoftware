using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace AsesoftwareServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ParameterContext _context;
        public TurnosController(IConfiguration configuration, ParameterContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("GenerarTurnos")]
        public IActionResult GenerarTurnosDiarios(Turnos model)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    connection.Open();
                    using (var command = new SqlCommand("GenerarTurnosDiarios", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@FechaInicio", model.Fecha_inicio));
                        command.Parameters.Add(new SqlParameter("@FechaFin", model.Fecha_Fin));
                        command.Parameters.Add(new SqlParameter("@IdServicio", model.Id_Servicios));
                        command.ExecuteNonQuery();
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al generar los turnos: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("ConsultarServicios")]
        public async Task<IActionResult> GetServicios()
        {
            var servicios = await _context.Servicios
                .Select(s => new { s.Id_Servicios, s.Nom_Servicios })
                .ToListAsync();

            return Ok(servicios);
        }

        [HttpGet]
        [Route("ConsultarTurnos")]
        public async Task<ActionResult<IEnumerable<TurnoInfo>>> ConsultarTurnos()
        {
            var turnosInfo = await _context.TurnoInfo.FromSqlRaw("EXEC SP_CONSULTA_TURNOS").ToListAsync();

            return turnosInfo;
        }
    }
}
