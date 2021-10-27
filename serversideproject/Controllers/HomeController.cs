using Microsoft.AspNetCore.DataProtection;
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
        private readonly IDataProtector _protector;

        public HomeController(ILogger<HomeController> logger,
            IHashingexamples hashingexamples,
            IDataProtectionProvider protector)
        {
            _logger = logger;
            _hashingexamples = hashingexamples;
            //Unique created key to encrypt with this dataprotecter.
            _protector = protector.CreateProtector("serversideproject.HomeController.Frederik");
        }

        public IActionResult Index(string myUsername, string myPassword, string encryptPW)
        {
            //Running Hashing...
            IndexModel? indexModel = null;
            if (myPassword != null)
            {
                string hashedValueAsString = _hashingexamples.Bcrypthash(myPassword);
                indexModel = new IndexModel() { OriginalText = myUsername, HashedValueAsString = hashedValueAsString };
            }

            //Running Encryption...
            if (encryptPW != null)
            {
                string protectedPayload = _protector.Protect(encryptPW);
                string unprotectedPayload = _protector.Unprotect(protectedPayload);

                indexModel = new IndexModel() { EncryptedValueAsString = protectedPayload, OriginalEncryptionText = unprotectedPayload };

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