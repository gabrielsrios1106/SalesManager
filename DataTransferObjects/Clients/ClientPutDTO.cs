using DataTransferObjects.Departments;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Clients
{
    public class ClientPutDTO
    {
        public int Id { get; set; }
        
        public DateTime CreatedAt = DateTime.Now;

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

        public ClientPutDTO() { }

        public ClientPutDTO(ClientGetDTO clientGetDTO)
        {
            Id = clientGetDTO.Id;
            CreatedAt = clientGetDTO.CreatedAt;
            ClientName = clientGetDTO.ClientName;
            ClientAddressCity = clientGetDTO.ClientAddressCity;
            ClientAddressState = clientGetDTO.ClientAddressState;
            ClientCEP = clientGetDTO.ClientCEP;
            ClientEmail = clientGetDTO.ClientEmail;
            UserId = clientGetDTO.UserId;
        }
    }
}
