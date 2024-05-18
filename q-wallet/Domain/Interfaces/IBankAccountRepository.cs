using q_wallet.Domain.Entities;
using q_wallet.Domain.Events;

namespace q_wallet.Domain.Interfaces
{
	public interface IBankAccountRepository : IRepositoryBase<BankAccount>
	{
		long GenerateAccountNumber();
		Task<BankAccount> CreditBankAccountAsync(double amount, BankAccount account);
		Task<BankAccount> DebitBankAccountAsync(double amount, BankAccount account);
		Task<BankAccount> CreateBankAccountAsync(BankAccount account);
		Task<double> GetBankAccountBalanceByUserIdAsync(Guid userId);
	}


}
