using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Responses;
using q_wallet.Applications.Responses.Common;
using q_wallet.Domain.Entities;
using q_wallet.Domain.Enums;
using q_wallet.Domain.Events;
using q_wallet.Domain.Interfaces;
using System.Security.Principal;

namespace q_wallet.Applications.Entities.BankAccounts.Handlers
{
	/// <summary>
	/// Debit bank account for user 
	/// </summary>
	public class DebitBankAccountCommandHandler : IRequestHandler<DebitBankAccountCommand, IApiResponse<BankAccountResponse>>
	{
		private readonly IMapper mapper;
        private IApiResponse<BankAccountResponse> response;
		private readonly IBankAccountRepository repository;
		private readonly ILogger<DebitBankAccountCommandHandler> logger;

        /// <summary>
        /// Initialise parameters via Constructor 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public DebitBankAccountCommandHandler(IBankAccountRepository repository, IMapper mapper, ILogger<DebitBankAccountCommandHandler> logger, IApiResponse<BankAccountResponse> response)
		{
			this.repository = repository;
            this.response = response;
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
		public async Task<IApiResponse<BankAccountResponse>> Handle(DebitBankAccountCommand request, CancellationToken cancellationToken)
		{
			//Log information
			logger.LogInformation($"{typeof(DebitBankAccountCommandHandler).Name} request handler initialised!");

			//Instantiate the model
			var model = new BankAccount();

            try
            {
                //Log information
                logger.LogInformation($"Data request containing {request}, is trying to update {nameof(BankAccount)} through {typeof(DebitBankAccountCommandHandler).Name}");

                //First, get the record to be debited
                var record = await repository.GetByExpression(x => x.UserId == request.UserId && x.AccountNumber == request.AccountNumber && !x.IsDeleted)
                                             .FirstOrDefaultAsync();

                //Check variable status
                if (record == null || record.Id == 0)
                {
                    //Set response if not record found
                    this.response.Success = true;
                    this.response.Code = StatusCodes.Status404NotFound;
                    this.response.Message = "No record found!";
                    this.response.Data = null;
                }

                //Check if amount is valid
                else if (request.WithdrawalAmount <= 0) 
                {
                    //Set response if amount is less than zero
                    this.response.Success = true;
                    this.response.Code = StatusCodes.Status400BadRequest;
                    this.response.Message = "Invalid withdrawal amount!";
                    this.response.Data = null;
                }

                //Check if amount is more the account balance
                else if (request.WithdrawalAmount > record.AccountBalance)
                {
                    //Set response 
                    this.response.Success = true;
                    this.response.Code = StatusCodes.Status403Forbidden;
                    this.response.Message = "Withdrawal amount cannot be more than balance!";
                    this.response.Data = null;
                }

                else
                {
                    //Get user balance
                    var balance = await this.repository.GetBankAccountBalanceByUserIdAsync(record.UserId);

                    //Process transaction if the amount is less
                    //than what is in the account transaction
                    if (balance >= request.WithdrawalAmount)
                    {
                        //Update entity record
                        record.LastModifiedOn = DateTime.Now;
                        record.LastModifiedBy = record.UserId;

                        //process the request using the entity
                        model = await repository.DebitBankAccountAsync(request.WithdrawalAmount, record);

                        //map response
                        var entity = this.mapper.Map<BankAccountResponse>(model);

                        //Set response 
                        this.response.Success = true;
                        this.response.Code = StatusCodes.Status200OK;
                        this.response.Message = "Withdrawal successful!";
                        this.response.Data.Add(entity);  

                        //Log information
                        logger.LogInformation($"{nameof(BankAccount)} data containing {response}, was updated successfully by handler: {typeof(DebitBankAccountCommandHandler).Name}");
                    }
                }                
            }
            catch (Exception ex)
			{
				//Log information
				logger.LogInformation($"{nameof(BankAccount)} data containing {request}, could not be created by handler: {typeof(DebitBankAccountCommandHandler).Name}");
				logger.LogInformation($"Message: {ex.Message}, InnerException: {ex.InnerException}, Request: {request}, Entity: {request}, Handler: {typeof(DebitBankAccountCommandHandler).Name}");

                //Set response if not record found
                this.response.Success = false;
                this.response.Code = StatusCodes.Status500InternalServerError;
                this.response.Message = "Error updating record!";
                this.response.Data = null;
            }

            //map the responsed returned to the entity custom response
            return this.response;
		}
	}
}

