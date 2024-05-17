using q_wallet.Domain.Entities;

namespace q_wallet.Applications.Responses
{
	public class BankAccountResponse
	{
		public int Id { get; set; }
		public long AccountNumber { get; set; }
		public double AccountBalance { get; set; }

		//Navigation Properties
		public int AccountTypeId { get; set; }
		public Guid UserId { get; set; }

		//Below Properties are Audit properties
		public Guid CreatedBy { get; set; } = Guid.Empty;
		public DateTime CreatedOn { get; set; }
		public Guid LastModifiedBy { get; set; } = Guid.Empty;
		public DateTime LastModifiedOn { get; set; }
	}
}
