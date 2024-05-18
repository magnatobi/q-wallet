using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using q_wallet.Applications.Entities.BankAccounts.Commands;
using q_wallet.Applications.Entities.BankAccounts.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Applications.Responses.Common;
using q_wallet.Controllers.Common;

namespace q_wallet.Controllers
{

	/// <summary>
	/// For all banking related activities 
	/// </summary>
	[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status401Unauthorized)]
	public class BankAccountController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly ILogger<BankAccountController> _logger;
		private ApiResponse<BankAccountResponse> _response;

		/// <summary>
		/// Initialise parameters via Constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="logger"></param>
		public BankAccountController(IMediator mediator, ILogger<BankAccountController> logger)
		{
			this._mediator = mediator;
			this._logger = logger;

			//Initialise and set defaults
			this._response = new ApiResponse<BankAccountResponse>();
		}



		/// <summary>
		/// Method to get all bank accounts
		/// </summary>
		/// <returns></returns>
		[HttpGet("bank-accounts")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IApiResponse<BankAccountResponse>>> GetAll()
		{
			//Send query to handler for processing
			var query = new GetAllBankAccountQuery();
			var records = await this._mediator.Send(query);

			// Check for null
			if (records == null || records.Count == 0)
			{
				//Set response if not record found
				this._response.Success = false;
				this._response.Code = StatusCodes.Status404NotFound;
				this._response.Message = "No record found!";
				this._response.Data = null;
			}
			else
			{
				//Set user response if found
				this._response.Success = true;
				this._response.Code = StatusCodes.Status200OK;
				this._response.Message = "Record found!";
				this._response.Data = records;
			}

			return Ok(this._response);
		}

		/// <summary>
		/// Method to get bank accounts by user Id
		/// </summary>
		/// <param name="newsId"></param>
		/// <returns></returns>
		[HttpGet("bank-accounts/user/{userId}")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IApiResponse<BankAccountResponse>>> GetByUserId(Guid userId)
		{
			//Send query to handler for processing
			var query = new GetBankAccountByUserIdQuery(userId);
			var record = await this._mediator.Send(query);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this._response.Success = false;
				this._response.Code = StatusCodes.Status404NotFound;
				this._response.Message = "No record found!";
				this._response.Data = null;
			}
			else
			{
				//Set user response if found
				this._response.Success = true;
				this._response.Code = StatusCodes.Status200OK;
				this._response.Message = "Record found!";
				this._response.Data?.Add(record);
			}

			return Ok(this._response);
		}

		/// <summary>
		/// Method to open a bank accounts
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("bank-accounts/create")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IApiResponse<BankAccountResponse>>> Create([FromBody] CreateBankAccountCommand request)
		{
			//Send query to handler for processing
			var record = await this._mediator.Send(request);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this._response.Success = false;
				this._response.Code = StatusCodes.Status400BadRequest;
				this._response.Message = "Error creating record!";
				this._response.Data = null;
			}
			else
			{
				//Set user response if found
				this._response.Success = true;
				this._response.Code = StatusCodes.Status200OK;
				this._response.Message = "Record created!";
				this._response.Data?.Add(record);
			}

			return Ok(this._response);
		}

		/// <summary>
		/// Method to deposit into a bank account
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("bank-accounts/deposit")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IApiResponse<BankAccountResponse>>> Deposit([FromBody] CreditBankAccountCommand request)
		{
			//Send query to handler for processing
			var record = await this._mediator.Send(request);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if no record found
				this._response.Success = false;
				this._response.Code = StatusCodes.Status400BadRequest;
				this._response.Message = "Error updating record!";
				this._response.Data = null;
			}
			else
			{
				//Set user response if found
				this._response.Success = true;
				this._response.Code = StatusCodes.Status200OK;
				this._response.Message = "Record updated!";
				this._response.Data?.Add(record);
			}

			return Ok(this._response);
		}

		/// <summary>
		/// Method to withdraw from a bank account
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("bank-accounts/withdraw")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IApiResponse<BankAccountResponse>>> Withdraw([FromBody] DebitBankAccountCommand request)
		{
			//Send query to handler for processing
			var result = await this._mediator.Send(request);
			return Ok(result);
		}

		/// <summary>
		/// Method to update an existing bank accounts
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("bank-accounts/update")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IApiResponse<BankAccountResponse>>> Update([FromBody] UpdateBankAccountCommand request)
		{
			//Send query to handler for processing
			var record = await this._mediator.Send(request);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this._response.Success = false;
				this._response.Code = StatusCodes.Status400BadRequest;
				this._response.Message = "Error updating record!";
				this._response.Data = null;
			}
			else
			{
				//Set response if found
				this._response.Success = true;
				this._response.Code = StatusCodes.Status200OK;
				this._response.Message = "Record updated!";
				this._response.Data?.Add(record);
			}

			return Ok(this._response);
		}

		/// <summary>
		/// Method to delete an existing bank accounts
		/// </summary>
		/// <returns></returns>
		[HttpPost("bank-accounts/delete")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Delete(DeleteBankAccountCommand request)
		{
			//Send request to handler for processing
			bool status = await this._mediator.Send(request);

			// Check for null reference
			if (status)
			{
				//Set response if found
				this._response.Success = true;
				this._response.Code = StatusCodes.Status200OK;
				this._response.Message = "Record deleted successfully!";
				this._response.Data = null;
			}
			else
			{
				//Set response if not record found
				this._response.Success = false;
				this._response.Code = StatusCodes.Status400BadRequest;
				this._response.Message = "Error deleting record!";
				this._response.Data = null;
			}

			return Ok(this._response);
		}
	}
}
