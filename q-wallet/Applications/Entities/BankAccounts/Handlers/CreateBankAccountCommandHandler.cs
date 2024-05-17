using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Create bank account for user and return the newly created data
	/// </summary>
	public class CreateBankAccountCommandHandler : IRequestHandler<CreateBankAccountCommand, BankAccountResponse>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountRepository repository;
		private readonly ILogger<CreateBankAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public CreateBankAccountCommandHandler(IBankAccountRepository repository, IMapper mapper, ILogger<CreateBankAccountCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(CreateBankAccountCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR POST request for create user account
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<BankAccountResponse> Handle(CreateBankAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(CreateBankAccountCommandHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new BankAccount();

			//map the request to the entity
			var entity = this.mapper.Map<BankAccount>(request);

			//Log information
			logger.LogInformation($"Data request containing {request}, is trying to create {nameof(BankAccount)} through {typeof(CreateBankAccountCommandHandler).Name}");

			try
			{
				//Generate account number
				entity.AccountNumber = this.repository.GetAccountNumber();
				
				//process the request using the entity
				response = await this.repository.AddAsync(entity);

				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {entity}, was saved successfully by handler: {typeof(CreateBankAccountCommandHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {entity}, could not be created by handler: {typeof(CreateBankAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Entity: {entity}, Handler: {typeof(CreateBankAccountCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return this.mapper.Map<BankAccountResponse>(response);
		}
	}
}

