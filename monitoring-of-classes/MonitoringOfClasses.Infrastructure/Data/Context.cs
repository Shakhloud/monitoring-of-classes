using Microsoft.EntityFrameworkCore;
using MonitoringOfClasses.Domain.Model;

namespace MonitoringOfClasses.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<Teacher> Teachers { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Teacher)
                .WithMany(t => t.Lessons) // У одного учителя может быть много занятий
                .HasForeignKey(l => l.TeacherId); // Указываем внешний ключ для связи
        }
    }
}