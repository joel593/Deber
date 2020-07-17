using System.Collections.Generic;

namespace VintageStuff.Models
{
    public class Bodega
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Persona Bodeguero { get; set; }
        public List<Pantalon> Pantalones { get; set; }
        public List<Polin> Polines { get; set; }
        public List<Boxer> Boxeres { get; set; }
        public List<Top> Topes { get; set; }
        public List<Camiseta> Camisetas { get; set; }
    }
}
