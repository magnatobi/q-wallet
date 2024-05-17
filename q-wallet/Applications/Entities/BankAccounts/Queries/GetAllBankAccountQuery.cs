using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.BankAccounts.Queries
{
	public class GetAllBankAccountQuery : IRequest<IList<BankAccountResponse>>
	{
	}
}
