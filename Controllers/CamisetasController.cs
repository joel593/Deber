using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VintageStuff.Data;
using VintageStuff.Models;
using VintageStuff.ViewModels;



namespace VintageStuff.Controllers
{
    public class CamisetasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamisetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Camisetas
        public async Task<IActionResult> Index()
        {
            var camisetas = await _context.Camisetas.ToListAsync();
            camisetas.ForEach(cadaCamiseta => cadaCamiseta.FotoBase64 = $"data:image/png;base64,{Convert.ToBase64String(cadaCamiseta.Foto)}");
            return View(camisetas);
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
            var camisetaSeleccionada = await _context.Camisetas.FirstOrDefaultAsync(c => c.Id == id);
            if(camisetaSeleccionada == null)
            {
                return NotFound();
            }
            persona.Camisetas.Add(camisetaSeleccionada);
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

            var camiseta = await _context.Camisetas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (camiseta == null)
            {
                return NotFound();
            }

            return View(camiseta);
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
        public async Task<IActionResult> Create(CamisaViewModel camisaViewModel)
        {
            if (ModelState.IsValid)
            {
                Camiseta camisa = new Camiseta();
                camisa.Marca = camisaViewModel.Marca;
                camisa.Modelo = camisaViewModel.Modelo;
                camisa.Foto = ConvertirArregloByte(camisaViewModel.Foto);
                _context.Add(camisa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(camisaViewModel);
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

            var camiseta = await _context.Camisetas.FindAsync(id);
            if (camiseta == null)
            {
                return NotFound();
            }
            return View(camiseta);
        }

        // POST: Camisetas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Marca,Modelo,Id")] Camiseta camiseta)
        {
            if (id != camiseta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(camiseta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CamisetaExists(camiseta.Id))
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
            return View(camiseta);
        }

        // GET: Camisetas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var camiseta = await _context.Camisetas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (camiseta == null)
            {
                return NotFound();
            }

            return View(camiseta);
        }

        // POST: Camisetas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var camiseta = await _context.Camisetas.FindAsync(id);
            _context.Camisetas.Remove(camiseta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CamisetaExists(int id)
        {
            return _context.Camisetas.Any(e => e.Id == id);
        }
    }
}
