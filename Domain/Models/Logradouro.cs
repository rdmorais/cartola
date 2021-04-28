namespace ApiCliente.Domain.Models
{
    public class Logradouro
    {

        public int Id {get; set;}

        public string Endereco {get; set;} 

        public int ClienteId {get; set;}

        public Cliente Cliente {get; set;}
    }
}