using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/mesa")]

    public class MesaController(IMesaRepository mesaRepository) : ControllerBase
    {
        [Authorize(Roles =$"{Constants.Mesero}")]
        [HttpGet]// obtiene todas las mesas disponibles (para mesero)
        public async Task <IActionResult> GetMesaList()
        {
            try
            {
                var mesas = await mesaRepository.GetMesasDisponiblesAsync();
                var response = new
                {
                    items = mesas.Select(mesa => new
                    {
                        mesa.Id,
                        mesa.Numero,
                        mesa.Disponible
                    })
                };
                return Ok(response);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}
