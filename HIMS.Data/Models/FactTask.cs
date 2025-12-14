namespace TTMS.Data.Models
{
    public class FactTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TTMS.Models.Task.Enums.TaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }


        public Guid AssignedToUserId { get; set; }
        public DimUser AssignedToUser { get; set; } = null!;


        public Guid CreatedByUserId { get; set; }
        public DimUser CreatedByUser { get; set; } = null!;


        public Guid TeamId { get; set; }
        public DimTeam Team { get; set; } = null!;
    }
}
