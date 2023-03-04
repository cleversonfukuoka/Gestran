namespace EFCore.WebAPI.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }        
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<Endereco> Enderecos { get; set; }

    }
}
