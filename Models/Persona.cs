using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VintageStuff.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [Required]  
        public string Nombre { get; set; }
        public List<Camiseta> Camisetas { get; set; } = new List<Camiseta>();
        public List<Zapato> Zapatos { get; set; } = new List<Zapato>();
        public List<Pantalon> Pantalones { get; set; } = new List<Pantalon>();
        public byte[] Foto { get; set; }
        [NotMapped]
        [Display(Name = "Fotografía")]
        public string FotoBase64 { get; set; }
    }
}