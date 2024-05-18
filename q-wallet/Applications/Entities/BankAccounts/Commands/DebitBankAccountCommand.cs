using MediatR;
using q_wallet.Applications.Responses;
using q_wallet.Applications.Responses.Common;

namespace q_wallet.Applications.Entities.BankAccounts.Commands
{
    public class DebitBankAccountCommand : IRequest<IApiResponse<BankAccountResponse>>
    {
        public long AccountNumber { get; set; }
        public double WithdrawalAmount { get; set; }
        public int AccountTypeId { get; set; }
        public Guid UserId { get; set; }
    }
}
