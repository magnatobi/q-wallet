using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System.Security;
using Microsoft.EntityFrameworkCore;
using q_wallet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using q_wallet.Domain.Events;

namespace q_wallet.Infrastructure.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		#region Required

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//User account
			modelBuilder.Entity<UserAccount>()
				.HasKey(e => e.UserId);
			modelBuilder.Entity<UserAccount>()
				.Property(f => f.Id)
				.ValueGeneratedOnAdd()
				.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

			//Bank account
			//modelBuilder.Entity<BankAccount>()
			//	.HasKey(e => e.UserId);
			//modelBuilder.Entity<BankAccount>()
			//	.Property(f => f.Id)
			//	.ValueGeneratedOnAdd()
			//	.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);


		}

		#endregion Required

		//Set up the Entities
		public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<BankAccount> BankAccounts { get; set; }
		public DbSet<BankAccountType> BankAccountTypes { get; set; }
		public DbSet<BankAccountEvent> BankAccountEvents { get; set; }
	}
}
