using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor
{
    public class PomoDbContext : DbContext
    {
        public DbSet<Pomidor> Pomidors { get; set; }
        public DbSet<Recreation> Recreations { get; set; }
        public DbSet<TypeOfPet> TypeOfPets { get; set; }
        public DbSet<Pet> Pets { get; set; }

        public PomoDbContext(DbContextOptions<PomoDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeOfPet>().HasData(new TypeOfPet
            {
                ID = 1,
                Name = "Cat"
            });

        }  
    }
}
