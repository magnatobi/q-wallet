using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Delete Bank account via handler 
	/// </summary>
	public class DeleteBankAccountCommandHandler : IRequestHandler<DeleteBankAccountCommand, bool>
	{
		private readonly IBankAccountRepository repository;
		private readonly IMapper mapper;
		private readonly ILogger<DeleteBankAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public DeleteBankAccountCommandHandler(IBankAccountRepository repository,
													 IMapper mapper,
													 ILogger<DeleteBankAccountCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(DeleteBankAccountCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR DELETE request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> Handle(DeleteBankAccountCommand request, CancellationToken cancellationToken)
		{
			var isDeleted = false;

			//Log information
			logger.LogInformation($"{typeof(DeleteBankAccountCommandHandler).Name} request handler initialised!");
			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to delete {nameof(BankAccount)} through {typeof(DeleteBankAccountCommandHandler).Name}");

				//First, get the record to be updated
				var record = await repository.GetByExpression(x => x.UserId == request.UserId && x.AccountNumber == request.AccountNumber).FirstOrDefaultAsync();

				//Check for null
				if (record != null)
				{
					//change the status to true
					record.IsDeleted = true;

                    //Update record
                    record.LastModifiedOn = DateTime.Now;
                    record.LastModifiedBy = record.UserId;

                    //process the request using the entity
                    await repository.UpdateAsync(record);

					//set as true if the deletion was successful
					isDeleted = true;

					//Log information
					logger.LogInformation($"{nameof(BankAccount)} data containing {record}, was deleted successfully by handler: {typeof(DeleteBankAccountCommandHandler).Name}");
				}
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be deleted by handler: {typeof(DeleteBankAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(DeleteBankAccountCommandHandler).Name}");
			}

			return isDeleted;
		}
	}
}
