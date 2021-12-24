using KursovayaMVC.Middlewares;
using KursovayaMVC.Models;
using KursovayaMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KursovayaMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiceRepository _serviceRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly CheckServiceDataMiddleware _checkModelMiddleware;

        public HomeController(ILogger<HomeController> logger, ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
            _logger = logger;
            _checkModelMiddleware = new CheckServiceDataMiddleware(new CheckServiceInDbMiddleware(_serviceRepository));
        }

        public IActionResult Index()
        {
            var list = _serviceRepository.ReadAll();
            return View(new Order() { ServiceList = list.ToList() });
        }
        [HttpGet]
        [Route("add")]
        public async Task AddService(Service service)
        {
            try
            {
                _checkModelMiddleware.Execute(service);
                await _serviceRepository.Create(service);
                await _serviceRepository.Save();
            }
            catch(Exception ex) { Error(1); }
            finally
            {
                Index();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error(int id)
        {
            return View(new ErrorViewModel { RequestId = id.ToString() });
        }
    }
}