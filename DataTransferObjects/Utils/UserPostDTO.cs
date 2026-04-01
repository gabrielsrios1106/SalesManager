using System.ComponentModel.DataAnnotations;


namespace DataTransferObjects.Utils
{
    public class UserPostDTO
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        public string CompleteName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O {0} é obrigatorio")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A {0} é obrigatorio")]
        public string Password { get; set; }

        [Display(Name = "Confirmar a senha")]
        [Required(ErrorMessage = "{0} é obrigatorio")]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais")]
        public string ConfirmPassword { get; set; }
    }
}
