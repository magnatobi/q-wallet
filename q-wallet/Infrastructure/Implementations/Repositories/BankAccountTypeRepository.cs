using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;
using q_wallet.Infrastructure.Data;

namespace q_wallet.Infrastructure.Implementations.Repositories
{
	public class BankAccountTypeRepository : RepositoryBase<BankAccountType>, IBankAccountTypeRepository
	{
		public BankAccountTypeRepository(DataContext dbContext) : base(dbContext)
		{
		}
	}
}
