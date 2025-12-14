using Microsoft.EntityFrameworkCore;
using TTMS.Data.Models;

namespace TTMS.Data.Context
{
    public class TTMSContext(DbContextOptions<TTMSContext> options) : DbContext(options)
    {
        public DbSet<DimUser> Users => Set<DimUser>();
        public DbSet<DimTeam> Teams => Set<DimTeam>();
        public DbSet<FactTask> Tasks => Set<FactTask>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DimUser>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Email).IsUnique();
            });


            modelBuilder.Entity<DimTeam>(entity =>
            {
                entity.HasKey(x => x.Id);
            });


            modelBuilder.Entity<FactTask>(entity =>
            {
                entity.HasKey(x => x.Id);


                entity.HasOne(x => x.AssignedToUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(x => x.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(x => x.CreatedByUser)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(x => x.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(x => x.Team)
                    .WithMany(t => t.Tasks)
                    .HasForeignKey(x => x.TeamId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
