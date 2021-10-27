using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using serversideproject.Codes;
using serversideproject.Areas.Database.Models;
using Microsoft.AspNetCore.Authorization;

namespace serversideproject.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("[controller]/[action]")]
    [Authorize("RequireAuthenticatedUser")]
    public class LogininfoesController : Controller
    {
        private readonly TestContext _context;
        private readonly IHashingexamples _hashingexamples;


        public LogininfoesController(TestContext context, IHashingexamples hashingexamples)
        {
            _context = context;
            _hashingexamples = hashingexamples;
        }

        // GET: Database/Logininfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Database/Logininfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,Password,Salt")] Logininfo logininfo)
        {
            if (ModelState.IsValid)
            {
                //Overwrite input password wit hashed pw.
                logininfo.Password = _hashingexamples.Bcrypthash(logininfo.Password);
                _context.Add(logininfo);
                await _context.SaveChangesAsync();
                return View("Views/Home/Index.cshtml");
            }
            return View(logininfo);
        }

        private bool LogininfoExists(int id)
        {
            return _context.Logininfos.Any(e => e.Id == id);
        }
    }
}
