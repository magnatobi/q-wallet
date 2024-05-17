using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.BankAccountTypes.Commands
{
	public class CreateBankAccountTypeCommand : IRequest<BankAccountTypeResponse>
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		//Below Properties are Audit properties
		public Guid CreatedBy { get; set; } = Guid.Empty;
		public DateTime CreatedOn { get; set; }
		public Guid LastModifiedBy { get; set; } = Guid.Empty;
		public DateTime LastModifiedOn { get; set; }
	}
}
