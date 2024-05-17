namespace q_wallet.Domain.Enums
{
	public enum EventType
	{
		None = 0,

		//Bank Account
		BankAccountCreatedEvent = 1,
		BankAccountCreditedEvent = 2,
		BankAccountDebitedEvent = 3,
		BankAccountDeletedEvent = 4,

		//User Account
		UserAccountCreatedEvent = 5,
		UserAccountUpdatedEvent = 6,
		UserAccountDeletedEvent = 7,
	}
}
