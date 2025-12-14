namespace HIMS.Models.Permission.Dtos
{
    public class EmployeePermissionDto
    {
        public int PermissionId { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
    }
}
