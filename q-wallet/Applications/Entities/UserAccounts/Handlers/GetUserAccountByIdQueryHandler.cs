using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using q_wallet.Applications.Entities.UserAccounts.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.UserAccounts.Handlers
{
	/// <summary>
	/// Retrieve single user account by user id 
	/// </summary>
	public class GetUserAccountByIdQueryHandler : IRequestHandler<GetUserAccountByIdQuery, UserAccountResponse>
	{
		private readonly IUserAccountRepository repository;
		private readonly ILogger<GetUserAccountByIdQueryHandler> logger;
		private readonly IMapper mapper;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public GetUserAccountByIdQueryHandler(IUserAccountRepository repository,
													 ILogger<GetUserAccountByIdQueryHandler> logger,
													 IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(GetUserAccountByIdQueryHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR GET request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<UserAccountResponse> Handle(GetUserAccountByIdQuery request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(GetUserAccountByIdQueryHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new UserAccount();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to fetch {nameof(UserAccount)} through {typeof(GetUserAccountByIdQueryHandler).Name}");

				//process the request using the entity repository
				response = await repository.GetByExpression(x => x.UserId == request.UserId && !x.IsDeleted).FirstOrDefaultAsync();

				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {response}, was fetched successfully by handler: {typeof(GetUserAccountByIdQueryHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {request}, could not be fetched by handler: {typeof(GetUserAccountByIdQueryHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(GetUserAccountByIdQueryHandler).Name}");
			}

			//map the response returned to the entity custom response
			return mapper.Map<UserAccountResponse>(response);
		}
	}
}