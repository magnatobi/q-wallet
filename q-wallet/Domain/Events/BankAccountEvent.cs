using q_wallet.Domain.Common;
using q_wallet.Domain.Entities;

namespace q_wallet.Domain.Events
{
	public class BankAccountEvent : EventBase
	{
        public BankAccountEvent()
        {
            User = new UserAccount();
        }

        public double Amount { get; set; }
        public double Balance { get; set; }

        //Navigation Reference
        public Guid UserId { get; set; }
        public virtual UserAccount User { get; set; }
    }
}
