using Microsoft.EntityFrameworkCore;
using MentorOnlineV3.Models.Entities;

namespace MentorOnlineV3.Models
{
    public class MentorDbContext : DbContext
    {
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlite("Filename=./MentorOnlineV3.db");
        } 
        
    }
    
}