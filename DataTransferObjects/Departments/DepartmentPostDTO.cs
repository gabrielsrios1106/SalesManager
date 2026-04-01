using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Departments
{
    public class DepartmentPostDTO
    {
        [Display(Name = "Nome do departamento")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string DepartmentName { get; set; }

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public DepartmentPostDTO(int idUser)
        {
            UserId = idUser;
        }

        public DepartmentPostDTO() { }
    }
}
