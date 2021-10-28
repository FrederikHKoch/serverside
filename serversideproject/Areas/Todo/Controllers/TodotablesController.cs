using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using serversideproject.Areas.Todo.Models;

namespace serversideproject.Areas.Todo.Controllers
{
    [Area("Todo")]
    [Route("[controller]/[action]")]
    [Authorize("RequireAuthenticatedUser")]
    public class TodotablesController : Controller
    {
        private readonly TodolistdbContext _context;
        private readonly IDataProtector _protector;

        public TodotablesController(TodolistdbContext context, IDataProtectionProvider protector)
        {
            _context = context;
            //Unique created key to encrypt with this dataprotecter.
            _protector = protector.CreateProtector("serversideproject.HomeController.Frederik");
        }

        // GET: Todo/Todotables
        public async Task<IActionResult> Index()
        {
            string? user = User.Identity.Name;
            var model = await _context.Todotables.Where(a => a.Username == user).ToListAsync();
            bool isNotEmpty = model.Count > 0;
            if (isNotEmpty)
            {
                foreach (Todotable item in model)
                {
                    if (item.Description != null)
                    {
                        item.Description = _protector.Unprotect(item.Description);
                    }
                    item.Title = _protector.Unprotect(item.Title);
                }
                return View(model);
            }
            else
            {
                return View(new List<Todotable>());
            }
        }

        // GET: Todo/Todotables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todotable = await _context.Todotables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todotable == null)
            {
                return NotFound();
            }

            return View(todotable);
        }

        // GET: Todo/Todotables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Todotables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Username")] Todotable todotable)
        {
            if (ModelState.IsValid)
            {
                todotable.Title = _protector.Protect(todotable.Title);
                if (todotable.Description != null)
                {
                    todotable.Description = _protector.Protect(todotable.Description);
                }

                _context.Add(todotable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todotable);
        }

        // GET: Todo/Todotables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todotable = await _context.Todotables.FindAsync(id);
            if (todotable == null)
            {
                return NotFound();
            }
            return View(todotable);
        }

        // POST: Todo/Todotables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Username")] Todotable todotable)
        {
            if (id != todotable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todotable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodotableExists(todotable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todotable);
        }

        // GET: Todo/Todotables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todotable = await _context.Todotables
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todotable == null)
            {
                return NotFound();
            }

            return View(todotable);
        }

        // POST: Todo/Todotables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todotable = await _context.Todotables.FindAsync(id);
            _context.Todotables.Remove(todotable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodotableExists(int id)
        {
            return _context.Todotables.Any(e => e.Id == id);
        }
    }
}
