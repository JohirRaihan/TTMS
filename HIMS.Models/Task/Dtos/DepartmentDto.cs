namespace HIMS.Models.Department.Dtos
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
