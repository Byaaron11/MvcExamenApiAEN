using Microsoft.AspNetCore.Mvc;
using MvcExamenApiAEN.Models;
using MvcExamenApiAEN.Services;

namespace MvcExamenApiAEN.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceSeriesPersonajes service;

        public SeriesController(ServiceSeriesPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeries();
            return View(series);
        }

        public async Task<IActionResult> Details(int id)
        {
            Serie serie = await this.service.FindSerieAsync(id);
            return View(serie);
        }
    }
}
