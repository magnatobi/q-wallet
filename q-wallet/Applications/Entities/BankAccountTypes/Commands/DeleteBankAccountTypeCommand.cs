using MediatR;

namespace q_wallet.Applications.Entities.BankAccountTypes.Commands
{

	/// <summary>
	/// Delete existing bank account type and send to the request handler for processing
	/// </summary>
	public class DeleteBankAccountTypeCommand : IRequest<bool>
	{
		public int Id { get; set; }

		/// <summary>
		/// Inject via constructor
		/// </summary>
		/// <param name="id"></param>
		public DeleteBankAccountTypeCommand(int id)
		{
			Id = id;
		}
	}
}
