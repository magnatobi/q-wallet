using q_wallet.Domain.Entities;
using q_wallet.Domain.Events;

namespace q_wallet.Domain.Interfaces
{
	public interface IBankAccountRepository : IRepositoryBase<BankAccount>
	{
		long GetAccountNumber();
		Task<BankAccount> Deposit(double amount, BankAccount account);
		Task<BankAccount> Withdraw(double amount, BankAccount account);
		Task<BankAccount> CreateBankAccountAsync(BankAccount account);
		Task<double> GetUserBalanceAsync(Guid userId);
	}


}
