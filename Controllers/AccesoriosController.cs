using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OutletCelular.Models;

namespace OutletCelular.Controllers
{
    public class AccesoriosController : Controller
    {
        private readonly OutletCelularContext _context;

        public AccesoriosController(OutletCelularContext context)
        {
            _context = context;
        }

        // GET: Accesorios
        public async Task<IActionResult> Index(string nombreBusqueda, string marcaBusqueda, decimal? precioMinimo, decimal? precioMaximo, string ordenamiento)
        {
            IQueryable<Accesorio> accesorios = _context.Accesorios.AsQueryable();

            // Aqui voy a hacer la consulta con las condiciones de búsqueda
            if (!String.IsNullOrEmpty(nombreBusqueda))
            {
                accesorios = accesorios.Where(s => s.Nombre.Contains(nombreBusqueda));
            }

            if (!String.IsNullOrEmpty(marcaBusqueda))
            {
                accesorios = accesorios.Where(x => x.Marca == marcaBusqueda);
            }

            if (precioMinimo.HasValue)
            {
                accesorios = accesorios.Where(y => y.Precio >= precioMinimo);
            }

            if (precioMaximo.HasValue)
            {
                accesorios = accesorios.Where(y => y.Precio <= precioMaximo);
            }

            // Aqui hacemos el ordenamiento
            switch (ordenamiento)
            {
                case "nombre_desc":
                    accesorios = accesorios.OrderByDescending(s => s.Nombre);
                    break;
                case "precio_asc":
                    accesorios = accesorios.OrderBy(s => s.Precio);
                    break;
                case "precio_desc":
                    accesorios = accesorios.OrderByDescending(s => s.Precio);
                    break;
                default:
                    accesorios = accesorios.OrderBy(s => s.Nombre);
                    break;
            }

            return View(await accesorios.AsNoTracking().ToListAsync());
        }

        // GET: Accesorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accesorio = await _context.Accesorios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accesorio == null)
            {
                return NotFound();
            }

            return View(accesorio);
        }

        // GET: Accesorios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accesorios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,Marca,Stock")] Accesorio accesorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accesorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accesorio);
        }

        // GET: Accesorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accesorio = await _context.Accesorios.FindAsync(id);
            if (accesorio == null)
            {
                return NotFound();
            }
            return View(accesorio);
        }

        // POST: Accesorios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,Marca,Stock")] Accesorio accesorio)
        {
            if (id != accesorio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accesorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccesorioExists(accesorio.Id))
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
            return View(accesorio);
        }

        // GET: Accesorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accesorio = await _context.Accesorios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accesorio == null)
            {
                return NotFound();
            }

            return View(accesorio);
        }

        // POST: Accesorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accesorio = await _context.Accesorios.FindAsync(id);
            _context.Accesorios.Remove(accesorio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccesorioExists(int id)
        {
            return _context.Accesorios.Any(e => e.Id == id);
        }
    }
}
