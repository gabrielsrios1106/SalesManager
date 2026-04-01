namespace Models
{
    public class Department
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public byte Status { get; set; } = 1;
        public Department() { }
    }
}
