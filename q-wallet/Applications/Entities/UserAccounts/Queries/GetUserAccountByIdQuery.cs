using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.UserAccounts.Queries
{
	/// <summary>
	/// Retrieve each user account by the userId
	/// </summary>
	public class GetUserAccountByIdQuery : IRequest<UserAccountResponse>
	{
		public Guid UserId { get; set; }

		/// <summary>
		/// Inject via the constructor
		/// </summary>
		/// <param name="id"></param>
		public GetUserAccountByIdQuery(Guid id)
		{
			UserId = id;
		}
	}

}
