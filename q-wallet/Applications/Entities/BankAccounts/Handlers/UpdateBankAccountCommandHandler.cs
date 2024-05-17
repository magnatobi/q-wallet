using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Update each Bank account via handler 
	/// </summary>
	public class UpdateBankAccountCommandHandler : IRequestHandler<UpdateBankAccountCommand, BankAccountResponse>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountRepository repository;
		private readonly ILogger<UpdateBankAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		/// <param name="repository"></param>
		public UpdateBankAccountCommandHandler(IMapper mapper,
													 ILogger<UpdateBankAccountCommandHandler> logger,
													 IBankAccountRepository repository)
		{
			this.mapper = mapper;
			this.logger = logger;
			this.repository = repository;
		}

		/// <summary>
		/// Handle mediatR UPDATE request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		/// <exception cref="EntityNotFoundException"></exception>
		public async Task<BankAccountResponse> Handle(UpdateBankAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(UpdateBankAccountCommandHandler).Name} request handler initialised!");

			//define the response model
			var response = new BankAccount();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to update {nameof(BankAccount)} through {typeof(UpdateBankAccountCommandHandler).Name}");

				//First, get the record to be updated
				var record = await repository.GetByExpression(x => x.UserId == request.UserId && !x.IsDeleted).FirstOrDefaultAsync();

				//Check variable status
				if (record?.Id > 0)
				{
					//map the request to the entity
					var entity = mapper.Map(request, record);

					//process the request using the entity
					response = await repository.UpdateAsync(entity);

					//Log information
					logger.LogInformation($"{nameof(BankAccount)} data containing {response}, was updated successfully by handler: {typeof(UpdateBankAccountCommandHandler).Name}");
				}
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be updated by handler: {typeof(UpdateBankAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(UpdateBankAccountCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return mapper.Map<BankAccountResponse>(response);
		}
	}
}
