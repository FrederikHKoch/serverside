using Microsoft.AspNetCore.Mvc;
using serversideproject.Codes;
using serversideproject.Models;
using System.Diagnostics;

namespace serversideproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHashingexamples _hashingexamples;

        public HomeController(ILogger<HomeController> logger, IHashingexamples hashingexamples)
        {
            _logger = logger;
            _hashingexamples = hashingexamples;

        }

        public IActionResult Index(string myUsername, string myPassword)
        {
            IndexModel? indexModel = null;
            if (myPassword != null)
            { 
                string hashedValueAsString = _hashingexamples.Bcrypthash(myPassword);
                indexModel = new IndexModel() { OriginalText = myUsername, HashedValueAsString = hashedValueAsString };
            }
            return View(model: indexModel);
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