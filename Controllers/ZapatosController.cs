using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageStuff.Data;
using VintageStuff.Models;
using VintageStuff.ViewModels;

namespace VintageStuff.Controllers
{
    public class ZapatosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZapatosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Camisetas
        public async Task<IActionResult> Index()
        {
            var zapatos = await _context.Camisetas.ToListAsync();
            zapatos.ForEach(cadaZapato => cadaZapato.FotoBase64 = $"data:image/png;base64,{Convert.ToBase64String(cadaZapato.Foto)}");
            return View(zapatos);
        }

        public async Task<IActionResult> Comprar()
        {
            return View(await _context.Zapatos.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comprar(int? id )
        {
            if(id == null)
            {
                return NotFound();
            }
            var usuarioActual = 1; //ToDo: Obtener el usuario actual logueado
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Id == usuarioActual);
            var ZapatoSeleccionada = await _context.Zapatos.FirstOrDefaultAsync(c => c.Id == id);
            if(ZapatoSeleccionada == null)
            {
                return NotFound();
            }
            persona.Zapatos.Add(ZapatoSeleccionada);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


        // GET: Camisetas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zapato = await _context.Zapatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zapato == null)
            {
                return NotFound();
            }

            return View(zapato);
        }

        // GET: Camisetas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Camisetas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZapatoViewModel zapatoViewModel)
        {
            if (ModelState.IsValid)
            {
                Zapato zapato = new Zapato();
                zapato.Marca = zapatoViewModel.Marca;
                zapato.Modelo = zapatoViewModel.Modelo;
                zapato.Foto = ConvertirArregloByte(zapatoViewModel.Foto);
                _context.Add(zapato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zapatoViewModel);
        }

        private byte[] ConvertirArregloByte(IFormFile formFile)
        {
            if (formFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    var tamanoMaximo = 2097152; //Si el archivo es mas grande usar streaming
                    if (memoryStream.Length < tamanoMaximo)
                        return memoryStream.ToArray();
                }
            }
            return null;
        }
        // GET: Camisetas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zapato = await _context.Camisetas.FindAsync(id);
            if (zapato == null)
            {
                return NotFound();
            }
            return View(zapato);
        }

        // POST: Camisetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Marca,Modelo,Id")] Zapato zapato)
        {
            if (id != zapato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zapato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZapatosExists(zapato.Id))
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
            return View(zapato);
        }

        // GET: Camisetas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zapato = await _context.Zapatos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zapato == null)
            {
                return NotFound();
            }

            return View(zapato);
        }

        // POST: Camisetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zapato = await _context.Zapatos.FindAsync(id);
            _context.Zapatos.Remove(zapato);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZapatosExists(int id)
        {
            return _context.Zapatos.Any(e => e.Id == id);
        }
    }
}
