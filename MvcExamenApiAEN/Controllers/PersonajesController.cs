using Microsoft.AspNetCore.Mvc;
using MvcExamenApiAEN.Models;
using MvcExamenApiAEN.Services;

namespace MvcExamenApiAEN.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceSeriesPersonajes service;

        public PersonajesController(ServiceSeriesPersonajes service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index(int idSerie)
        {
            List<Personaje> personas = await this.service.GetPersonajesSerie(idSerie);
            return View(personas);
        }

        public async Task<IActionResult> Details(int idpj)
        {
            Personaje pj = await this.service.FindPersonajeAsync(idpj);
            return View(pj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje);
            return RedirectToAction("Index", new {idSerie = personaje.IdSerie});
        }

        public async Task<IActionResult> Update(int id)
        {
            Personaje pj = await this.service.FindPersonajeAsync(id);
            return View(pj);
        }

        [HttpPost]

        public async Task<IActionResult> Update(Personaje personaje)
        {
            await this.service.UpdatePersonajeSerieAsync(personaje);
            return RedirectToAction("Index", new { idSerie = personaje.IdSerie });
        }

        public async Task<IActionResult> Delete(int idpersonaje, int idserie)
        {
            await this.service.DeletePersonajeAsync(idpersonaje);
            return RedirectToAction("Index", new {idSerie = idserie});
        }

    }
}
