using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using q_wallet.Applications.Entities.UserAccounts.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.UserAccounts.Handlers
{
	/// <summary>
	/// Retrieve all user accounts available in the database
	/// </summary>
	public class GetAllUserAccountQueryHandler : IRequestHandler<GetAllUserAccountQuery, IList<UserAccountResponse>>
	{
		private readonly IUserAccountRepository repository;
		private readonly ILogger<GetAllUserAccountQueryHandler> logger;
		private readonly IMapper mapper;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public GetAllUserAccountQueryHandler(IUserAccountRepository repository, IMapper mapper,
													 ILogger<GetAllUserAccountQueryHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(GetAllUserAccountQueryHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR GET request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IList<UserAccountResponse>> Handle(GetAllUserAccountQuery request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(GetAllUserAccountQueryHandler).Name} request handler initialised!");

			//Instantiate the model
			IEnumerable<UserAccount> response = new List<UserAccount>();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to fetch a list of {nameof(UserAccount)} through {typeof(GetAllUserAccountQueryHandler).Name}");

				//process the request using the entity
				response = await repository.GetByExpressionAsync(x => !x.IsDeleted);

				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {response}, was fetched successfully by handler: {typeof(GetAllUserAccountQueryHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {request}, could not be fetched by handler: {typeof(GetAllUserAccountQueryHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(GetAllUserAccountQueryHandler).Name}");
			}

			//map the response returned to the entity custom response
			return mapper.Map<List<UserAccountResponse>>(response);
		}
	}
}

