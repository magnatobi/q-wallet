using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccountTypes.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccountTypes.Handlers
{
	/// <summary>
	/// Retrieve a Bank account type by id 
	/// </summary>
	public class GetBankAccountTypeByIdQueryHandler : IRequestHandler<GetBankAccountTypeByIdQuery, BankAccountTypeResponse>
	{
		private readonly IBankAccountTypeRepository repository;
		private readonly ILogger<GetBankAccountTypeByIdQueryHandler> logger;
		private readonly IMapper mapper;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public GetBankAccountTypeByIdQueryHandler(IBankAccountTypeRepository repository,
													 ILogger<GetBankAccountTypeByIdQueryHandler> logger,
													 IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(GetBankAccountTypeByIdQueryHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR GET request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<BankAccountTypeResponse> Handle(GetBankAccountTypeByIdQuery request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(GetBankAccountTypeByIdQueryHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new BankAccountType();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to fetch {nameof(BankAccountType)} through {typeof(GetBankAccountTypeByIdQueryHandler).Name}");

				//process the request using the entity repository
				response = await repository.GetByIdAsync(request.Id);

				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {response}, was fetched successfully by handler: {typeof(GetBankAccountTypeByIdQueryHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {request}, could not be fetched by handler: {typeof(GetBankAccountTypeByIdQueryHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(GetBankAccountTypeByIdQueryHandler).Name}");
			}

			//map the response returned to the entity custom response
			return mapper.Map<BankAccountTypeResponse>(response);
		}
	}
}
