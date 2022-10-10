using System.ComponentModel.DataAnnotations;

namespace AgendamentoEventos.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [RegularExpression("([0-9]{2}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[\\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\\.]?[0-9]{3}[\\.]?[0-9]{3}[-]?[0-9]{2})")]
        public string CnpjCpf { get; set; }
        
        [Required(ErrorMessage = "O número de telefone é obrigatório")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail é inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "A senha deve conter de 8 a 32 carácteres")]
        public string Password { get; set; }
    }
}
