using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Interfaces;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Debit bank account for user 
	/// </summary>
	public class DebitBankAccountCommandHandler : IRequestHandler<DebitBankAccountCommand, BankAccountResponse>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountRepository repository;
		private readonly ILogger<DebitBankAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public DebitBankAccountCommandHandler(IBankAccountRepository repository, IMapper mapper, ILogger<DebitBankAccountCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(DebitBankAccountCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR POST request for debit user bank account
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<BankAccountResponse> Handle(DebitBankAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(DebitBankAccountCommandHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new BankAccount();

            try
            {
                //Log information
                logger.LogInformation($"Data request containing {request}, is trying to update {nameof(BankAccount)} through {typeof(DebitBankAccountCommandHandler).Name}");

                //First, get the record to be debited
                var record = await repository.GetByExpression(x => x.UserId == request.UserId && x.AccountNumber == request.AccountNumber && !x.IsDeleted)
                                             .FirstOrDefaultAsync();

                //Check variable status
                if (record?.Id > 0)
                {
                    //Update entity record
                    record.LastModifiedOn = DateTime.Now;
                    record.LastModifiedBy = record.UserId;

                    //process the request using the entity
                    response = await repository.DebitBankAccountAsync(request.WithdrawalAmount, record);

                    //Log information
                    logger.LogInformation($"{nameof(BankAccount)} data containing {response}, was updated successfully by handler: {typeof(DebitBankAccountCommandHandler).Name}");
                }
            }
            catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be created by handler: {typeof(DebitBankAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Entity: {request}, Handler: {typeof(DebitBankAccountCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return this.mapper.Map<BankAccountResponse>(response);
		}
	}
}

