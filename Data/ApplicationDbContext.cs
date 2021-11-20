using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZeroHunger.Model;

namespace ZeroHunger.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DryFoodDonation> DryFoodDonation { get; set; }
        public DbSet<Receiver> Receiver { get; set; }
        public DbSet<ReceiverFamily> ReceiverFamily { get; set; }
        public DbSet<SalaryGroup> SalaryGroup { get; set; }
    }
}
