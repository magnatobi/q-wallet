using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using q_wallet.Applications.Entities.BankAccounts.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Retrieve single Bank account by Bank id 
	/// </summary>
	public class GetBankAccountByUserIdQueryHandler : IRequestHandler<GetBankAccountByUserIdQuery, BankAccountResponse>
	{
		private readonly IBankAccountRepository repository;
		private readonly ILogger<GetBankAccountByUserIdQueryHandler> logger;
		private readonly IMapper mapper;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public GetBankAccountByUserIdQueryHandler(IBankAccountRepository repository,
													 ILogger<GetBankAccountByUserIdQueryHandler> logger,
													 IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(GetBankAccountByUserIdQueryHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR GET request
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<BankAccountResponse> Handle(GetBankAccountByUserIdQuery request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(GetBankAccountByUserIdQueryHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new BankAccount();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to fetch {nameof(BankAccount)} through {typeof(GetBankAccountByUserIdQueryHandler).Name}");

				//process the request using the entity repository
				response = await repository.GetByExpression(x => x.UserId == request.UserId && !x.IsDeleted).FirstOrDefaultAsync();

				//Update account balance
				response.AccountBalance = await repository.GetUserBalanceAsync(response.UserId);

				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {response}, was fetched successfully by handler: {typeof(GetBankAccountByUserIdQueryHandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be fetched by handler: {typeof(GetBankAccountByUserIdQueryHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(GetBankAccountByUserIdQueryHandler).Name}");
			}

			//map the response returned to the entity custom response
			return mapper.Map<BankAccountResponse>(response);
		}
	}
}