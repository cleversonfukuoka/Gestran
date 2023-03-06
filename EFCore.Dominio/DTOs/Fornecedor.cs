namespace EFCore.Dominio
{
    public class FornecedorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public ICollection<Endereco> Enderecos { get;set;}


    }
}
