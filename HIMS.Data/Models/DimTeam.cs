namespace TTMS.Data.Models
{
    public class DimTeam
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;


        public ICollection<FactTask> Tasks { get; set; } = [];
    }
}
