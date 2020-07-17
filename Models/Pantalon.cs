using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VintageStuff.Models
{
    public class Pantalon
    {
        public int Id { get; set; }
        public int Talla { get; set; }
        public string Marca { get; set; }
        public string Modelo{ get; set; }
        public byte[] Foto { get; set; }
        [NotMapped]
        [Display(Name = "Fotografía")]
        public string FotoBase64 { get; set; }
    }
}
