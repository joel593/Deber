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
    public class PantalonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PantalonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pantalon
        public async Task<IActionResult> Index()
        {
            var pantalon = await _context.Camisetas.ToListAsync();
            pantalon.ForEach(cadaPantalon => cadaPantalon.FotoBase64 = $"data:image/png;base64,{Convert.ToBase64String(cadaPantalon.Foto)}");
            return View(pantalon);
        }
        public async Task<IActionResult> Comprar()
        {
            return View(await _context.Pantalones.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comprar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuarioActual = 1; //ToDo: Obtener el usuario actual logueado
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.Id == usuarioActual);
            var PantalonSeleccionado = await _context.Pantalones.FirstOrDefaultAsync(c => c.Id == id);
            if (PantalonSeleccionado == null)
            {
                return NotFound();
            }
            persona.Pantalones.Add(PantalonSeleccionado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


        // GET: Pantalones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pantalon = await _context.Pantalones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pantalon == null)
            {
                return NotFound();
            }

            return View(pantalon);
        }

        // GET: Pantalones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pantalones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PantalonViewModel pantalonViewModel)
        {
            if (ModelState.IsValid)
            {
                Pantalon pantalon = new Pantalon();
                pantalon.Marca = pantalonViewModel.Marca;
                pantalon.Modelo = pantalonViewModel.Modelo;
                pantalon.Foto = ConvertirArregloByte(pantalonViewModel.Foto);
                _context.Add(pantalon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pantalonViewModel);
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

        // GET: Pantalones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pantalon = await _context.Pantalones.FindAsync(id);
            if (pantalon == null)
            {
                return NotFound();
            }
            return View(pantalon);
        }

        // POST: Pantalones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Marca,Modelo,Id")] Pantalon pantalon)
        {
            if (id != pantalon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pantalon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PantalonesExists(pantalon.Id))
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
            return View(pantalon);
        }

        // GET: Pantalones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pantalon = await _context.Pantalones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pantalon == null)
            {
                return NotFound();
            }

            return View(pantalon);
        }

        // POST: Pantalones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pantalon = await _context.Pantalones.FindAsync(id);
            _context.Pantalones.Remove(pantalon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PantalonesExists(int id)
        {
            return _context.Pantalones.Any(e => e.Id == id);
        }
    }
}
