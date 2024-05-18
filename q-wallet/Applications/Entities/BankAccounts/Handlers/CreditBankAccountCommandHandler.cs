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
	/// Credit bank account for user 
	/// </summary>
	public class CreditBankAccountCommandHandler : IRequestHandler<CreditBankAccountCommand, BankAccountResponse>
	{
		private readonly IMapper mapper;
		private readonly IBankAccountRepository repository;
		private readonly ILogger<CreditBankAccountCommandHandler> logger;

		/// <summary>
		/// Initialise parameters via Constructor 
		/// </summary>
		/// <param name="repository"></param>
		/// <param name="mapper"></param>
		/// <param name="logger"></param>
		public CreditBankAccountCommandHandler(IBankAccountRepository repository, IMapper mapper, ILogger<CreditBankAccountCommandHandler> logger)
		{
			this.repository = repository;
			this.mapper = mapper;
			this.logger = logger;

			//Log information
			this.logger.LogInformation($"{typeof(CreditBankAccountCommandHandler).Name} constructor initialised successfully!");
		}

		/// <summary>
		/// Handle mediatR POST request for credit user bank account
		/// </summary>
		/// <param name="request"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<BankAccountResponse> Handle(CreditBankAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(CreditBankAccountCommandHandler).Name} request handler initialised!");

			//Instantiate the model
			var response = new BankAccount();

            try
            {
                //Log information
                logger.LogInformation($"Data request containing {request}, is trying to update {nameof(BankAccount)} through {typeof(CreditBankAccountCommandHandler).Name}");

                //First, get the record to be updated
                var record = await repository.GetByExpression(x => x.UserId == request.UserId && x.AccountNumber == request.AccountNumber && !x.IsDeleted)
                                             .FirstOrDefaultAsync();

                //Check variable status
                if (record?.Id > 0)
                {
                    //Update entity record
                    record.LastModifiedOn = DateTime.Now;
                    record.LastModifiedBy = record.UserId;

                    //process the request using the entity
                    response = await repository.CreditBankAccountAsync(request.DepositAmount, record);

                    //Log information
                    logger.LogInformation($"{nameof(BankAccount)} data containing {response}, was updated successfully by handler: {typeof(CreditBankAccountCommandHandler).Name}");
                }
            }
            catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be created by handler: {typeof(CreditBankAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Entity: {request}, Handler: {typeof(CreditBankAccountCommandHandler).Name}");
			}

			//map the responsed returned to the entity custom response
			return this.mapper.Map<BankAccountResponse>(response);
		}
	}
}

