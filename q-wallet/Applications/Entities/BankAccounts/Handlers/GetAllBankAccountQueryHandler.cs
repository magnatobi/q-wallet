using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccounts.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Retrieve all Bank accounts available in the database
	/// </summary>
	public class GetAllBankAccountQueryHandler : IRequestHandler<GetAllBankAccountQuery, IList<BankAccountResponse>>
	{
		private readonly IBankAccountRepository repository;
		private readonly ILogger<GetAllBankAccountQueryHandler> logger;
		private readonly IMapper mapper;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public GetAllBankAccountQueryHandler(IBankAccountRepository repository, IMapper mapper,
													 ILogger<GetAllBankAccountQueryHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(GetAllBankAccountQueryHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR GET request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IList<BankAccountResponse>> Handle(GetAllBankAccountQuery request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(GetAllBankAccountQueryHandler).Name} request handler initialised!");

			//Instantiate the model
			IEnumerable<BankAccount> response = new List<BankAccount>();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to fetch a list of {nameof(BankAccount)} through {typeof(GetAllBankAccountQueryHandler).Name}");

				//process the request using the entity
				response = await repository.GetByExpressionAsync(x => !x.IsDeleted);

				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {response}, was fetched successfully by handler: {typeof(GetAllBankAccountQueryHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be fetched by handler: {typeof(GetAllBankAccountQueryHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(GetAllBankAccountQueryHandler).Name}");
			}

			//map the response returned to the entity custom response
			return mapper.Map<List<BankAccountResponse>>(response);
		}
	}
}