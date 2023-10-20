using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebListaDeEspera.Data;
using WebListaDeEspera.Models;

namespace WebListaDeEspera.Controllers
{
    
    public class ListaEsperaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListaEsperaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ListaEspera
        public async Task<IActionResult> Index()
        {
            return _context.ListaEspera != null ?
                        View(await _context.ListaEspera.OrderBy(x => x.DataCadastro).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.ListaEspera'  is null.");
        }

        // GET: ListaEspera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ListaEspera == null)
            {
                return NotFound();
            }

            var listaEspera = await _context.ListaEspera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaEspera == null)
            {
                return NotFound();
            }

            return View(listaEspera);
        }

        // GET: ListaEspera/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaEspera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,Email,DataCadastro,Finalizado")] ListaEspera listaEspera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaEspera);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listaEspera);
        }

        // GET: ListaEspera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ListaEspera == null)
            {
                return NotFound();
            }

            var listaEspera = await _context.ListaEspera.FindAsync(id);
            
            if (listaEspera == null)
            {
                return NotFound();
            }
            
            return View(listaEspera);
        }

        // POST: ListaEspera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Telefone,Email,DataCadastro,Finalizado")] ListaEspera listaEspera)
        {
            if (id != listaEspera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(listaEspera.Finalizado == true)
                    {
                        _context.Remove(listaEspera);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Update(listaEspera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaEsperaExists(listaEspera.Id))
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
            return View(listaEspera);
        }

        // GET: ListaEspera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ListaEspera == null)
            {
                return NotFound();
            }

            var listaEspera = await _context.ListaEspera
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaEspera == null)
            {
                return NotFound();
            }

            return View(listaEspera);
        }

        // POST: ListaEspera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ListaEspera == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ListaEspera'  is null.");
            }
            var listaEspera = await _context.ListaEspera.FindAsync(id);
            if (listaEspera != null)
            {
                _context.ListaEspera.Remove(listaEspera);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaEsperaExists(int id)
        {
            return (_context.ListaEspera?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
