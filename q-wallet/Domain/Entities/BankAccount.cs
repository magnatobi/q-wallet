using q_wallet.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace q_wallet.Domain.Entities
{
	public class BankAccount : EntityBase
	{
        public BankAccount() 
        {
			//Set default account balance 
			AccountBalance = 0.00D;
			
            User = new UserAccount();
            AccountType = new BankAccountType();
        }

        [MaxLength(10)]
        public long AccountNumber { get; internal set; }
        public double AccountBalance { get; internal set; }

        //Navigation Properties
        public int AccountTypeId { get; set; }
        public virtual BankAccountType AccountType { get; set; }
        public Guid UserId { get; set; }
        public virtual UserAccount User { get; set;}
    }
}
