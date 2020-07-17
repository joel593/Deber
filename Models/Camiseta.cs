using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VintageStuff.Models
{
    public class Camiseta
    {
        public Camiseta()
        {
        }
        public Camiseta(string marca, string modelo)
        {
            Marca = marca;
            Modelo = modelo;
        }
        public int Id { get; set; }
               
        [System.ComponentModel.DataAnnotations.Display(Description = "Esta es la marca")]
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public byte[] Foto { get; set; }
        [NotMapped]
        [Display(Name = "Fotografía")]
        public string FotoBase64 { get; set; }

    }
}