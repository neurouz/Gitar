using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Models;
using Gitar.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gitar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<GitUser, int> _gitUserRepository;

        public HomeController(ILogger<HomeController> logger, IRepository<GitUser, int> gitUserRepository)
        {
            _logger = logger;
            _gitUserRepository = gitUserRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _gitUserRepository.CreateAsync(null);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}