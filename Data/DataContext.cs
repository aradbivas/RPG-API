using Microsoft.EntityFrameworkCore;

namespace DotNet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill{Id =1, Name="Fireball", Damadge=30},
                new Skill{Id =2, Name="Frenzy", Damadge=20},
                new Skill{Id =3, Name="Blizzard", Damadge=50}
            );
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Weapon> Weapons {get; set;}

        public DbSet<Skill> Skills  {get;set;}
        
    }
}