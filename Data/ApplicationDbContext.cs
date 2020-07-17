using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VintageStuff.Models;

namespace VintageStuff.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet <Pantalon> Pantalones { get; set; }
        public DbSet<Camiseta> Camisetas { get; set; }
        public DbSet<Polin> Polines { get; set; }
        public DbSet<Boxer> Boxeres { get; set; }
        public DbSet<Top> Topes { get; set; }
        public DbSet<RopaFoto> RopaFotos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Zapato> Zapatos { get; set; }
        
    }
}
