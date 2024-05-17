using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccountTypes.Commands;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccountTypes.Handlers
{
	/// <summary>
	/// Delete Bank account type via handler 
	/// </summary>
	public class DeleteBankAccountTypeCommandHandler : IRequestHandler<DeleteBankAccountTypeCommand, bool>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountTypeRepository repository;
		private readonly ILogger<DeleteBankAccountTypeCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public DeleteBankAccountTypeCommandHandler(IBankAccountTypeRepository repository,
													 IMapper mapper,
													 ILogger<DeleteBankAccountTypeCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(DeleteBankAccountTypeCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR DELETE request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> Handle(DeleteBankAccountTypeCommand request, CancellationToken cancellationToken)
		{
			var isDeleted = false;

			//Log information
			logger.LogInformation($"{typeof(DeleteBankAccountTypeCommandHandler).Name} request handler initialised!");

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to delete {nameof(BankAccountType)} through {typeof(DeleteBankAccountTypeCommandHandler).Name}");

				//First, get the record to be updated
				var record = await repository.GetByIdAsync(request.Id);

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
					logger.LogInformation($"{nameof(BankAccountType)} data containing {record}, was deleted successfully by handler: {typeof(DeleteBankAccountTypeCommandHandler).Name}");
				}
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {request}, could not be deleted by handler: {typeof(DeleteBankAccountTypeCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(DeleteBankAccountTypeCommandHandler).Name}");
			}

			return isDeleted;
		}
	}
}
