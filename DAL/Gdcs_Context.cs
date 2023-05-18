using DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Gdcs_Context : IdentityDbContext<User>
    {
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<Jop> Jops { get; set; }
        public DbSet<JopForm> JopForms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
   


            base.OnModelCreating(modelBuilder);

        }
        public Gdcs_Context(DbContextOptions<Gdcs_Context> options):base(options)
        {

        }
    }
}
