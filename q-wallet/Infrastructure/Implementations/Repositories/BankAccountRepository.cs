using Microsoft.EntityFrameworkCore;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Enums;
using q_wallet.Domain.Events;
using q_wallet.Domain.Interfaces;
using q_wallet.Infrastructure.Data;
using System.Reflection.Emit;
using System.Security.Principal;

namespace q_wallet.Infrastructure.Implementations.Repositories
{
	public class BankAccountRepository : RepositoryBase<BankAccount>, IBankAccountRepository
	{
		public BankAccountRepository(DataContext dbContext) : base(dbContext)
		{
		}

		/// <summary>
		/// Generate account number
		/// </summary>
		/// <returns></returns>
		public long GetAccountNumber()
		{
			String r = new Random().Next(0, 99999999).ToString("D6");
			string formatedNumber = String.Format("00{0}", r);
			long accountNumber = Convert.ToInt64(formatedNumber);			
			return accountNumber;
		}

		/// <summary>
		/// Override the existing add bank account	
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public async Task<BankAccount> CreateBankAccountAsync(BankAccount account)
		{
			//Add bank account
			await AddAsync(account);

			//Add account created event
			var @event = new BankAccountEvent()
			{
				EventType = EventType.BankAccountCreatedEvent,
				EventName = "Bank Account Created Successfully",
				Amount = account.AccountBalance,
				Balance = account.AccountBalance,
				UserId = account.UserId,
				CreatedOn = DateTime.Now,
			};

			//Apply event
			await this.ApplyEvent(@event);

			return account;
		}

		/// <summary>
		///  Deposit
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public async Task<BankAccount> Deposit(double amount, BankAccount account)
		{
			if (amount > 0)
			{
				//Update account balance
				account.AccountBalance += amount;

				//Update account balance
				await UpdateAsync(account);

				//Add account created event
				var @event = new BankAccountEvent()
				{
					EventType = EventType.BankAccountCreditedEvent,
					EventName = "Bank Account Credited Successfully",
					Amount = -amount,
					Balance = account.AccountBalance,
					UserId = account.UserId,
					CreatedOn = DateTime.Now
				};

				//Apply event
				await this.ApplyEvent(@event);

			}

			return account;
		}

		/// <summary>
		/// Withdrawal process
		/// </summary>
		/// <param name="amount"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public async Task<BankAccount> Withdraw(double amount, BankAccount account)
		{
			if (amount > 0 && amount > account.AccountBalance)
			{
				//Get user balance
				var balance = await GetUserBalanceAsync(account.UserId);

				//Process transaction if the amount is less
				//than what is in the account transaction
				if (balance >= amount)
				{
					//Update account balance
					account.AccountBalance -= amount;

					//Update account balance
					await UpdateAsync(account);

					//Add account created event
					var @event = new BankAccountEvent()
					{
						EventType = EventType.BankAccountDebitedEvent,
						EventName = "Bank Account Debited Successfully",
						Amount = -amount,
						Balance = account.AccountBalance,
						UserId = account.UserId,
						CreatedOn = DateTime.Now
					};

					//Apply event
					await this.ApplyEvent(@event);
				}                
			}

			return account;
		}

		/// <summary>
		/// Get user account balance
		/// </summary>
		/// <returns></returns>
		public async Task<double> GetUserBalanceAsync(Guid userId)
		{
			//Initialise
			double balance = 0;

			//Get user transaction events history
			var history = await _dbContext.BankAccountEvents.Where(x => x.UserId == userId).ToListAsync();

			//Get balance by calculating the total amount of transactions
			for (int i = 0; i < history.Count; i++)
			{
				balance += history[i].Amount;
			}

			return balance;
		}

		/// <summary>
		/// Apply event to bank account
		/// </summary>
		/// <param name="event"></param>
		private async Task ApplyEvent(BankAccountEvent @event)
		{
			_dbContext.BankAccountEvents.Add(@event);
			await SaveChangesAsync();
		}


	}
}
