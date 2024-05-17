using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccountTypes.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccountTypes.Handlers
{
	/// <summary>
	/// Retrieve all Bank account types available in the database
	/// </summary>
	public class GetAllBankAccountTypeQueryHandler : IRequestHandler<GetAllBankAccountTypeQuery, IList<BankAccountTypeResponse>>
	{
		private readonly IBankAccountTypeRepository repository;
		private readonly ILogger<GetAllBankAccountTypeQueryHandler> logger;
		private readonly IMapper mapper;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public GetAllBankAccountTypeQueryHandler(IBankAccountTypeRepository repository, IMapper mapper,
													 ILogger<GetAllBankAccountTypeQueryHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(GetAllBankAccountTypeQueryHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR GET request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IList<BankAccountTypeResponse>> Handle(GetAllBankAccountTypeQuery request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(GetAllBankAccountTypeQueryHandler).Name} request handler initialised!");

			//Instantiate the model
			IEnumerable<BankAccountType> response = new List<BankAccountType>();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to fetch a list of {nameof(BankAccountType)} through {typeof(GetAllBankAccountTypeQueryHandler).Name}");

				//process the request using the entity
				response = await repository.GetByExpressionAsync(x => !x.IsDeleted);

				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {response}, was fetched successfully by handler: {typeof(GetAllBankAccountTypeQueryHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {request}, could not be fetched by handler: {typeof(GetAllBankAccountTypeQueryHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(GetAllBankAccountTypeQueryHandler).Name}");
			}

			//map the response returned to the entity custom response
			return mapper.Map<List<BankAccountTypeResponse>>(response);
		}
	}
}