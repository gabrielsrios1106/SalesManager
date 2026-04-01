namespace DataTransferObjects.Departments
{
    public class DepartmentGetDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; }

        public byte Status { get; set; } = 1;

        public int UserId { get; set; }

        public DepartmentGetDTO() { }
    }
}
