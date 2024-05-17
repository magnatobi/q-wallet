using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.BankAccounts.Queries
{
	public class GetBankAccountByUserIdQuery : IRequest<BankAccountResponse>
	{
		public Guid UserId { get; set; }

		/// <summary>
		/// Inject via constructor
		/// </summary>
		/// <param name="id"></param>
		public GetBankAccountByUserIdQuery(Guid userId)
		{
			UserId = userId;
		}
	}
}
