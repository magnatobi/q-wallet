using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using q_wallet.Applications.Entities.UserAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.UserAccounts.Handlers
{
	/// <summary>
	/// Update each user account via handler 
	/// </summary>
	public class UpdateUserAccountCommandHandler : IRequestHandler<UpdateUserAccountCommand, UserAccountResponse>
	{
		private readonly IMapper mapper;
		private readonly IUserAccountRepository repository;
		private readonly ILogger<UpdateUserAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		/// <param name="repository"></param>
		public UpdateUserAccountCommandHandler(IMapper mapper,
													 ILogger<UpdateUserAccountCommandHandler> logger,
													 IUserAccountRepository repository)
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
		public async Task<UserAccountResponse> Handle(UpdateUserAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(UpdateUserAccountCommandHandler).Name} request handler initialised!");

			//define the response model
			var response = new UserAccount();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to update {nameof(UserAccount)} through {typeof(UpdateUserAccountCommandHandler).Name}");

				//First, get the record to be updated
				var record = await repository.GetByExpression(x => x.UserId == request.UserId && !x.IsDeleted).FirstOrDefaultAsync();

				//Check variable status
				if (record?.Id > 0)
				{
					//map the request to the entity
					var entity = mapper.Map(request, record);

                    //Update entity
                    entity.LastModifiedOn = DateTime.Now;
                    entity.LastModifiedBy = entity.UserId;

                    //process the request using the entity
                    response = await repository.UpdateAsync(entity);

					//Log information
					logger.LogInformation($"{nameof(UserAccount)} data containing {response}, was updated successfully by handler: {typeof(UpdateUserAccountCommandHandler).Name}");
				}
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(UserAccount)} data containing {request}, could not be updated by handler: {typeof(UpdateUserAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(UpdateUserAccountCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return mapper.Map<UserAccountResponse>(response);
		}
	}
}
