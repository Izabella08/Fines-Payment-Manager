using DataAccess.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class FinesPaymentManagerDbContext : IdentityDbContext<IdentityUser>
    {
        IConfiguration _configuration;
        public FinesPaymentManagerDbContext(DbContextOptions<FinesPaymentManagerDbContext> options, IConfiguration configuration)
       : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<DriverEntity> DriverEntities { get; set; }
        public DbSet<FineEntity> FineEntities { get; set; }
        public DbSet<ComplaintEntity> ComplaintEntities { get; set; }
        public DbSet<PaymentEntity> PaymentEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("connectionString"), b => b.MigrationsAssembly("LayersOnWeb"));
        }
    }
}
