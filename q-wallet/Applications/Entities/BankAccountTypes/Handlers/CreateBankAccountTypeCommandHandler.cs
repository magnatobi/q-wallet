using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccountTypes.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccountTypes.Handlers
{
	/// <summary>
	/// Create bank account type for user when creating a bank account
	/// </summary>
	public class CreateBankAccountTypeCommandHandler : IRequestHandler<CreateBankAccountTypeCommand, BankAccountTypeResponse>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountTypeRepository repository;
		private readonly ILogger<CreateBankAccountTypeCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public CreateBankAccountTypeCommandHandler(IBankAccountTypeRepository repository, IMapper mapper, ILogger<CreateBankAccountTypeCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(CreateBankAccountTypeCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR POST request 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<BankAccountTypeResponse> Handle(CreateBankAccountTypeCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(CreateBankAccountTypeCommandHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new BankAccountType();

			//map the request to the entity
			var entity = this.mapper.Map<BankAccountType>(request);

			//Log information
			logger.LogInformation($"Data request containing {request}, is trying to create {nameof(BankAccountType)} through {typeof(CreateBankAccountTypeCommandHandler).Name}");

			try
			{				
				//process the request using the entity
				response = await this.repository.AddAsync(entity);

				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {entity}, was saved successfully by handler: {typeof(CreateBankAccountTypeCommandHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {entity}, could not be created by handler: {typeof(CreateBankAccountTypeCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Entity: {entity}, Handler: {typeof(CreateBankAccountTypeCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return this.mapper.Map<BankAccountTypeResponse>(response);
		}
	}
}

