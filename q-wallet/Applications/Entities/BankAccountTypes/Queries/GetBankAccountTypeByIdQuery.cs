using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.BankAccountTypes.Queries
{
	/// <summary>
	/// Retrieve each bank account type category by the eventId
	/// </summary>
	public class GetBankAccountTypeByIdQuery : IRequest<BankAccountTypeResponse>
	{
		public int Id { get; set; }

		/// <summary>
		/// Inject via the constructor
		/// </summary>
		/// <param name="id"></param>
		public GetBankAccountTypeByIdQuery(int id)
		{
			Id = id;
		}
	}
}
