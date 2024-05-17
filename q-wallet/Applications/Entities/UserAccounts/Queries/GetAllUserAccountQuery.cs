using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.UserAccounts.Queries
{
	/// <summary>
	/// Retrieve all user accounts
	/// </summary>
	public class GetAllUserAccountQuery : IRequest<IList<UserAccountResponse>>
	{
	}
}
