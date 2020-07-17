namespace VintageStuff.Models
{
    public class Fabrica
    {
        public Camiseta CrearCamiseta(string marca = "cuello cuadrado", string modelo = "polo")
        {
            Camiseta camiseta = new Camiseta(marca, modelo);
            return camiseta;
        }
    }
}
