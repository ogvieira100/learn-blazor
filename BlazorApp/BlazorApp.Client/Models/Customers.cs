using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Client.Models
{

    public enum UF { 
    
        SP = 1,
        RJ = 2, 

    }
    public class Address
    {
        [Required(ErrorMessage = "Endereço requerido.")]
        [Range(3, 200, ErrorMessage = "Atenção nome deve conter entre 3 a 200 caracteres")]
        public string? Street { get; set; }

        [Required(ErrorMessage = "Número é requerido.")]
        [RegularExpression(@"^(SN|.{1,20})$", ErrorMessage = "O valor deve ser 'SN' ou ter no máximo 20 caracteres.")]
        public string? Number { get; set; }
    }   

    public class Customers
    {
        [Required(ErrorMessage ="Atenção nome requerido")]
        [Range(3,200,ErrorMessage = "Atenção nome deve conter entre 3 a 200 caracteres")]
        public  string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Atenção email inválido")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Número de telefone inválido. O telefone deve conter 10 ou 11 dígitos.")]
        public string? Telphone { get; set; }

        [Required(ErrorMessage = "Atenção nome requerido")]
        [Range(3, 200, ErrorMessage = "Atenção nome deve conter entre 3 a 200 caracteres")]
        public  string? SurName { get; set; }
        public List<Address> Addresses { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF inválido. O CPF deve conter exatamente 11 dígitos.")]
        public string? CPF { get; set; }
        public Customers()
        {
            Addresses = new List<Address>();    
        }
    }
}
