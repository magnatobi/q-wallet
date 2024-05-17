using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.BankAccountTypes.Queries
{
	/// <summary>
	/// Retrieve all bank account types
	/// </summary>
	public class GetAllBankAccountTypeQuery : IRequest<IList<BankAccountTypeResponse>>
	{
	}
}
