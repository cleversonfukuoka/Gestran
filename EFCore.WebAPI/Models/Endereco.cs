namespace EFCore.WebAPI.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public Cidade Cidade { get; set; }        
        public string CEP { get; set; }

    }
}
