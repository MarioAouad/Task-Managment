using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Models;

namespace TaskManager.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Domain.Models.Task> Task { get; set; }
        public DbSet<TaskAssignment> TaskAssignment { get; set; }
        public DbSet<TimeSlice> TimeSlice { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship between EMPLOYEE and DEPARTMENT
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);

            /*modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.Department_Number);*/

            // Optional: Manager of department (self-reference to EMPLOYEE)
            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);

            /*modelBuilder.Entity<Department  >()
                .HasOne(d => d.Manager)
                .WithMany() // No navigation from EMPLOYEE to departments they manage
                .HasForeignKey(d => d.Manager_SSn)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete loop*/

            // PROJECT → DEPARTMENT (many projects belong to one department)
            modelBuilder.Entity<Domain.Models.Task>()
                .HasKey(t => t.Id);

            /*modelBuilder.Entity<Domain.Models.Task>()
                .HasOne(p => p.Department)
                .WithMany(d => d.Projects)
                .HasForeignKey((object p) => p.Department_Number);*/

            // WORKS_ON → EMPLOYEE and PROJECT (composite key + relationships)
            modelBuilder.Entity<TaskAssignment>()
                .HasKey(ta => ta.Id);

            /*modelBuilder.Entity<TaskAssignment>()
                .HasOne(wo => wo.Employee)
                .WithMany(e => e.WorksOns)
                .HasForeignKey(wo => wo.SSn);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(wo => wo.Project)
                .WithMany(p => p.WorksOns)
                .HasForeignKey(wo => wo.Project_Number);*/

            modelBuilder.Entity<TimeSlice>()
                .HasKey(ts => ts.Id);
        }

    }
}