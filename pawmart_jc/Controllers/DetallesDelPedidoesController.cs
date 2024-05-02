using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pawmart_jc.Models;

namespace pawmart_jc.Controllers
{
    public class DetallesDelPedidoesController : Controller
    {
        private readonly Pawmart_BDContext _context;

        public DetallesDelPedidoesController(Pawmart_BDContext context)
        {
            _context = context;
        }

        // GET: DetallesDelPedidoes
        public async Task<IActionResult> Index()
        {
            var pawmart_BDContext = _context.DetallesDelPedidos.Include(d => d.IdPedidoNavigation).Include(d => d.IdProductoNavigation);
            return View(await pawmart_BDContext.ToListAsync());
        }

        // GET: DetallesDelPedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetallesDelPedidos == null)
            {
                return NotFound();
            }

            var detallesDelPedido = await _context.DetallesDelPedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesDelPedido == null)
            {
                return NotFound();
            }

            return View(detallesDelPedido);
        }

        // GET: DetallesDelPedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: DetallesDelPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPedido,IdProducto,Cantidad,PrecioUnitario")] DetallesDelPedido detallesDelPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detallesDelPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallesDelPedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallesDelPedido.IdProducto);
            return View(detallesDelPedido);
        }

        // GET: DetallesDelPedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetallesDelPedidos == null)
            {
                return NotFound();
            }

            var detallesDelPedido = await _context.DetallesDelPedidos.FindAsync(id);
            if (detallesDelPedido == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallesDelPedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallesDelPedido.IdProducto);
            return View(detallesDelPedido);
        }

        // POST: DetallesDelPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPedido,IdProducto,Cantidad,PrecioUnitario")] DetallesDelPedido detallesDelPedido)
        {
            if (id != detallesDelPedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallesDelPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesDelPedidoExists(detallesDelPedido.Id))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "Id", "Id", detallesDelPedido.IdPedido);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", detallesDelPedido.IdProducto);
            return View(detallesDelPedido);
        }

        // GET: DetallesDelPedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetallesDelPedidos == null)
            {
                return NotFound();
            }

            var detallesDelPedido = await _context.DetallesDelPedidos
                .Include(d => d.IdPedidoNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallesDelPedido == null)
            {
                return NotFound();
            }

            return View(detallesDelPedido);
        }

        // POST: DetallesDelPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetallesDelPedidos == null)
            {
                return Problem("Entity set 'Pawmart_BDContext.DetallesDelPedidos'  is null.");
            }
            var detallesDelPedido = await _context.DetallesDelPedidos.FindAsync(id);
            if (detallesDelPedido != null)
            {
                _context.DetallesDelPedidos.Remove(detallesDelPedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallesDelPedidoExists(int id)
        {
          return (_context.DetallesDelPedidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
