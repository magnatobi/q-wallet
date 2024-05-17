using MediatR;

namespace q_wallet.Applications.Entities.UserAccounts.Commands
{
	/// <summary>
	/// Delete user account by the userId
	/// </summary>
	public class DeleteUserAccountCommand : IRequest<bool>
	{
		public Guid UserId { get; set; }

		/// <summary>
		/// Inject via constructor
		/// </summary>
		/// <param name="userId"></param>
		public DeleteUserAccountCommand(Guid userId)
		{
			UserId = userId;
		}
	}
}
