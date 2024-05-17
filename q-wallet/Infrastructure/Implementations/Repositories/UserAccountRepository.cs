using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;
using q_wallet.Infrastructure.Data;

namespace q_wallet.Infrastructure.Implementations.Repositories
{
	public class UserAccountRepository : RepositoryBase<UserAccount>, IUserAccountRepository
	{
		public UserAccountRepository(DataContext dbContext) : base(dbContext)
		{
		}
	}
}
