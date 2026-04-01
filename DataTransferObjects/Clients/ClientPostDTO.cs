using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Clients
{
    public class ClientPostDTO
    {
        [Display(Name = "Nome do cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ClientName { get; set; }

        [Display(Name = "Estado do cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ClientAddressState { get; set; }

        [Display(Name = "Cidade do cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ClientAddressCity { get; set; }

        [Display(Name = "CEP do cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ClientCEP { get; set; }


        [Display(Name = "Email do cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string ClientEmail { get; set; }

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public ClientPostDTO()
        {
                
        }

        public ClientPostDTO(int idUser)
        {
            UserId = idUser;
        }

    }
}
