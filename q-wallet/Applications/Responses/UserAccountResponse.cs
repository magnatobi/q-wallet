namespace q_wallet.Applications.Responses
{
	public class UserAccountResponse
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string PlaceOfBirth { get; set; } = string.Empty;
		public string DateOfBirth { get; set; } = string.Empty;
		public string Nationality { get; set; } = string.Empty;
		public string Sex { get; set; } = string.Empty;

		//Below Properties are Audit properties
		public Guid CreatedBy { get; set; } = Guid.Empty;
		public DateTime CreatedOn { get; set; }
		public Guid LastModifiedBy { get; set; } = Guid.Empty;
		public DateTime LastModifiedOn { get; set; }
	}
}
