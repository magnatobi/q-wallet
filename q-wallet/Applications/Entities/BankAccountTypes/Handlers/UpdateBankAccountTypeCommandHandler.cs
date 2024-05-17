using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using q_wallet.Applications.Entities.BankAccountTypes.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccountTypes.Handlers
{
	/// <summary>
	/// Update each Bank account type via handler 
	/// </summary>
	public class UpdateBankAccountTypeCommandHandler : IRequestHandler<UpdateBankAccountTypeCommand, BankAccountTypeResponse>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountTypeRepository repository;
		private readonly ILogger<UpdateBankAccountTypeCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		/// <param name="repository"></param>
		public UpdateBankAccountTypeCommandHandler(IMapper mapper,
													 ILogger<UpdateBankAccountTypeCommandHandler> logger,
													 IBankAccountTypeRepository repository)
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
		public async Task<BankAccountTypeResponse> Handle(UpdateBankAccountTypeCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(UpdateBankAccountTypeCommandHandler).Name} request handler initialised!");

			//define the response model
			var response = new BankAccountType();

			try
			{
				//Log information
				logger.LogInformation($"Data request containing {request}, is trying to update {nameof(BankAccountType)} through {typeof(UpdateBankAccountTypeCommandHandler).Name}");

				//First, get the record to be updated
				var record = await repository.GetByExpression(x => x.Id == request.Id && !x.IsDeleted).FirstOrDefaultAsync();

				//Check variable status
				if (record?.Id > 0)
				{
					//map the request to the entity
					var entity = mapper.Map(request, record);

					//process the request using the entity
					response = await repository.UpdateAsync(entity);

					//Log information
					logger.LogInformation($"{nameof(BankAccountType)} data containing {response}, was updated successfully by handler: {typeof(UpdateBankAccountTypeCommandHandler).Name}");
				}
			}
			catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccountType)} data containing {request}, could not be updated by handler: {typeof(UpdateBankAccountTypeCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Handler: {typeof(UpdateBankAccountTypeCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return mapper.Map<BankAccountTypeResponse>(response);
		}
	}
}