using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using serversideproject.Areas.Todo.Models;

namespace serversideproject.Areas.Todo.Controllers
{
    [Area("Todo")]
    public class TodotablesController : Controller
    {
        private readonly TodolistdbContext _context;

        public TodotablesController(TodolistdbContext context)
        {
            _context = context;
        }

        // GET: Todo/Todotables
        public async Task<IActionResult> Index()
        {
            return View(await _context.Todotables.ToListAsync());
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
