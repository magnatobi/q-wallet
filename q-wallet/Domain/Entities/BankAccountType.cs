using q_wallet.Domain.Common;

namespace q_wallet.Domain.Entities
{
	public class BankAccountType : EntityBase
	{
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
