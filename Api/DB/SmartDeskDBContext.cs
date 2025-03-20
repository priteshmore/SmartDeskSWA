using Api.DB.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DB
{
    public class SmartDeskDBContext : DbContext
    {

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public SmartDeskDBContext(DbContextOptions<SmartDeskDBContext> options) : base(options)
        { 
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Answer)
                .WithOne(a => a.Question)
                .HasForeignKey<Answer>(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .Property(q => q.MostAskedCategory)
                .HasDefaultValue(1)  // Default value as 1
                .IsRequired();  // Ensures the value is always present
        }
    }
}
