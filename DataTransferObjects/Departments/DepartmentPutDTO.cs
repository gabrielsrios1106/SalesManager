using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Departments
{
    public class DepartmentPutDTO
    {
        public int Id { get; set; }

        [Display(Name = "Nome do departamento")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string DepartmentName { get; set; }

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public DepartmentPutDTO() { }

        public DepartmentPutDTO(DepartmentGetDTO departmentGetDTO)
        {
            Id = departmentGetDTO.Id;
            DepartmentName = departmentGetDTO.DepartmentName;
            UserId = departmentGetDTO.UserId;
        }
    }
}
