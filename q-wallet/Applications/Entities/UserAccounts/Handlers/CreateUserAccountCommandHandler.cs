using AutoMapper;
using MediatR;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Entities.UserAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.UserAccounts.Handlers
{
	/// <summary>
	/// Create UserAccount handler and return the newly created data
	/// </summary>
	public class CreateUserAccountCommandhandler : IRequestHandler<CreateUserAccountCommand, UserAccountResponse>
	{
		private readonly IMapper mapper;
		private readonly IUserAccountRepository repository;
		private readonly ILogger<CreateUserAccountCommandhandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public CreateUserAccountCommandhandler(IUserAccountRepository repository, IMapper mapper, ILogger<CreateUserAccountCommandhandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(CreateUserAccountCommandhandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR POST request for create user account
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<UserAccountResponse> Handle(CreateUserAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(CreateUserAccountCommandhandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new UserAccount();

			//map the request to the entity
			var entity = this.mapper.Map<UserAccount>(request);

			//Log information
			logger.LogInformation($"Data request containing {request}, is trying to create {nameof(UserAccount)} through {typeof(CreateUserAccountCommandhandler).Name}");

			try
			{
                //Update entity
                entity.CreatedOn = DateTime.Now;
                entity.LastModifiedOn = DateTime.Now;
                //entity.CreatedBy = entity.UserId;
                //entity.LastModifiedBy = entity.UserId;

                //process the request using the entity
                response = await this.repository.AddAsync(entity);

				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {entity}, was saved successfully by handler: {typeof(CreateUserAccountCommandhandler).Name}");
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {entity}, could not be created by handler: {typeof(CreateUserAccountCommandhandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Entity: {entity}, Handler: {typeof(CreateUserAccountCommandhandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return this.mapper.Map<UserAccountResponse>(response);
		}
	}
}
