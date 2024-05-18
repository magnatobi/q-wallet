using MediatR;
using q_wallet.Applications.Responses;

namespace q_wallet.Applications.Entities.BankAccounts.Commands
{
    public class CreditBankAccountCommand : IRequest<BankAccountResponse>
    {
        public long AccountNumber { get; set; }
        public double DepositAmount { get; set; }
        public int AccountTypeId { get; set; }
        public Guid UserId { get; set; }
    }
}
