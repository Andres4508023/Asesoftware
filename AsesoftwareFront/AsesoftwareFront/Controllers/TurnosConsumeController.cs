using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using ConsumeService;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AsesoftwareFront.Controllers
{
    public class TurnosConsumeController : Controller
    {
        private readonly ITurnoConsume _ITurnoConsume;

        public TurnosConsumeController(ITurnoConsume TurnoConsume)
        {
            _ITurnoConsume = TurnoConsume;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateTurnos()
        {
            List<Servicios> listServicios = await _ITurnoConsume.GetServicios();
            ViewBag.listServicios = new SelectList(listServicios, "Id_Servicios", "Nom_Servicios");

            var list = await _ITurnoConsume.GetTurnos();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTurnos(Turnos model)
        {
            bool response = await _ITurnoConsume.SaveTurnos(model);
            return View();
        }
    }
}