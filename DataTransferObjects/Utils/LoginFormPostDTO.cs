using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Utils
{
    public class LoginFormPostDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um email valido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Password { get; set; }

        public LoginFormPostDTO() { }
    }
}
