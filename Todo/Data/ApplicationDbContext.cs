using Microsoft.EntityFrameworkCore;  // ✅ Essential using statement
using TodoApp.Models;

namespace TodoApp.Data
{
    public class ApplicationDbContext : DbContext  // ✅ Correct inheritance
    {
        // ✅ Correct constructor - MUST match exactly
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
              
        {
        }

 
        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Configure TodoItem entity
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);  // ✅ Primary key
                entity.Property(e => e.Title)
                    .IsRequired()           // ✅ Required field
                    .HasMaxLength(100);     // ✅ Maximum length
                entity.Property(e => e.Description)
                    .HasMaxLength(500);     // ✅ Optional field with max length
                entity.Property(e => e.CreatedDate)
                    .IsRequired();          // ✅ Required field
            });
        }
    }
}