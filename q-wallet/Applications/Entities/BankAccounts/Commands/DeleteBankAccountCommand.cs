using MediatR;

namespace q_wallet.Applications.Entities.BankAccounts.Commands
{
	/// <summary>
	/// Delete bank account by the id
	/// </summary>
	public class DeleteBankAccountCommand : IRequest<bool>
	{
		public long AccountNumber { get; set; }
		public Guid UserId { get; set; }

		/// <summary>
		/// Inject via constructor
		/// </summary>
		/// <param name="id"></param>
		public DeleteBankAccountCommand(long accountNumber, Guid userId)
		{
			AccountNumber = accountNumber;
			UserId = userId;
		}
	}
}
