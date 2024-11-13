namespace CrudAPi.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateOnly Data_cadastro { get; set; }
    }
}
