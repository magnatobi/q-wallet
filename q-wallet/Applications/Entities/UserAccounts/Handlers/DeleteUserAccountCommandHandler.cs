using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using q_wallet.Applications.Entities.UserAccounts.Commands;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.UserAccounts.Handlers
{
	/// <summary>
	/// Delete user account via handler 
	/// </summary>
	public class DeleteUserAccountCommandHandler : IRequestHandler<DeleteUserAccountCommand, bool>
	{
		private readonly IUserAccountRepository repository;
		private readonly IMapper mapper;
		private readonly ILogger<DeleteUserAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public DeleteUserAccountCommandHandler(IUserAccountRepository repository,
													 IMapper mapper,
													 ILogger<DeleteUserAccountCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(DeleteUserAccountCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR DELETE request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> Handle(DeleteUserAccountCommand request, CancellationToken cancellationToken)
		{
			var isDeleted = false;

			//Log information
			logger.LogInformation($"{typeof(DeleteUserAccountCommandHandler).Name} request handler initialised!");
			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to delete {nameof(UserAccount)} through {typeof(DeleteUserAccountCommandHandler).Name}");

				//First, get the record to be updated
				var record = await repository.GetByIdAsync(request.UserId);

				//Check for null
				if (record != null)
				{
					//change the status to true
					record.IsDeleted = true;

					//process the request using the entity
					await repository.UpdateAsync(record);

					//set as true if the deletion was successful
					isDeleted = true;

					//Log information
					logger.LogInformation($"{nameof(UserAccount)} data containing {record}, was deleted successfully by handler: {typeof(DeleteUserAccountCommandHandler).Name}");
				}
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {request}, could not be deleted by handler: {typeof(DeleteUserAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(DeleteUserAccountCommandHandler).Name}");
			}

			return isDeleted;
		}
	}
}
