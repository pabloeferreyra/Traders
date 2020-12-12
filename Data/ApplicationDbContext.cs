﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Traders.Models;

namespace Traders.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MovementsViewModel>()
            .Property(b => b.DateMov)
            .HasDefaultValueSql("getdate()");
            builder.Entity<BankAccountsViewModel>()
                .HasMany(m => m.Movements)
                .WithOne(b => b.BankAccounts);
            builder.Entity<BadgesViewModel>()
                .HasMany(m => m.Movements)
                .WithOne(b => b.Badges);
        }
        public DbSet<BadgesViewModel> Badges { get; set; }
        public DbSet<MovementsViewModel> Movements { get; set; }
        public DbSet<BankAccountsViewModel> BankAccounts { get; set; }
    }
}
