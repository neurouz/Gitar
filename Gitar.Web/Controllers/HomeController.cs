using Gitar.Domain.Contracts.Data;
using Gitar.Domain.Models;
using Gitar.Web.Models;
using Infrastructure.Data.Json.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gitar.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var obj1 = new GitUser()
            //{
            //    Name = "prvi"
            //};

            //var obj2 = new GitUser()
            //{
            //    Name = "drugi"
            //};

            //var obj3 = new GitUser()
            //{
            //    Name = "treci"
            //};

            //var obj4 = new GitUser()
            //{
            //    Name = "cetvrti"
            //};

            //var obj5 = new GitUser()
            //{
            //    Name = "peti"
            //};

            //var result1 = await _gitUserRepository.CreateAsync(obj1);
            //var result2 = await _gitUserRepository.CreateAsync(obj2);
            //var result3 = await _gitUserRepository.CreateAsync(obj3);
            //var result4 = await _gitUserRepository.CreateAsync(obj4);
            //var result5 = await _gitUserRepository.CreateAsync(obj5);

            //await _gitUserRepository.DeleteAsync(obj2.Id);

            //var user = await _gitUserRepository.GetAsync(x => x.IsActive == false);

            //var oneUser = await _gitUserRepository.GetByKeyAsync(obj1.Id);

            //oneUser.ModifyDate = DateTime.Now;
            //oneUser.Name = "novi prvi";

            //await _gitUserRepository.UpdateAsync(oneUser);

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