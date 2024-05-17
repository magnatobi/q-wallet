using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using q_wallet.Applications.Entities.BankAccountTypes.Commands;
using q_wallet.Applications.Entities.BankAccountTypes.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Applications.Responses.Common;
using q_wallet.Controllers.Common;

namespace q_wallet.Controllers
{
	/// <summary>
	/// Manage all BankAccountType endpoints
	/// </summary>
	[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status401Unauthorized)]
	public class BankAccountTypeController : BaseController
	{
		private readonly IMediator mediator;
		private readonly ILogger<BankAccountTypeController> logger;
		private readonly IApiResponse<BankAccountTypeResponse> response;


		/// <summary>
		/// Initialise parameters via Constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="logger"></param>
		public BankAccountTypeController(IMediator mediator, ILogger<BankAccountTypeController> logger, IApiResponse<BankAccountTypeResponse> response)
		{
			this.mediator = mediator;
			this.logger = logger;
			this.response = response;
		}


		/// <summary>
		/// Method to get all BankAccountTypes
		/// </summary>
		/// <returns></returns>
		[HttpGet("bank-account-types")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IApiResponse<BankAccountTypeResponse>>> GetAll()
		{
			//Send query to handler for processing
			var query = new GetAllBankAccountTypeQuery();
			var records = await this.mediator.Send(query);

			// Check for null
			if (records == null || records.Count == 0)
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status404NotFound;
				this.response.Message = "No record found!";
				this.response.Data = null;
			}
			else
			{
				//Set user response if found
				this.response.Success = true;
				this.response.Code = StatusCodes.Status200OK;
				this.response.Message = "Record found!";
				this.response.Data = records;
			}

			return Ok(this.response);
		}

		/// <summary>
		/// Method to get BankAccountTypes by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("bank-account-types/{id}")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IApiResponse<BankAccountTypeResponse>>> GetById(int id)
		{
			//Send query to handler for processing
			var query = new GetBankAccountTypeByIdQuery(id);
			var record = await this.mediator.Send(query);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status404NotFound;
				this.response.Message = "No record found!";
				this.response.Data = null;
			}
			else
			{
				//Set user response if found
				this.response.Success = true;
				this.response.Code = StatusCodes.Status200OK;
				this.response.Message = "Record found!";
				this.response.Data?.Add(record);
			}

			return Ok(this.response);
		}

		/// <summary>
		/// Method to create an BankAccountType
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("bank-account-types/create")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IApiResponse<BankAccountTypeResponse>>> Create([FromBody] CreateBankAccountTypeCommand request)
		{
			//Send query to handler for processing
			var record = await this.mediator.Send(request);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status400BadRequest;
				this.response.Message = "Error creating record!";
				this.response.Data = null;
			}
			else
			{
				//Set user response if found
				this.response.Success = true;
				this.response.Code = StatusCodes.Status200OK;
				this.response.Message = "Record created!";
				this.response.Data?.Add(record);
			}

			return Ok(this.response);
		}

		/// <summary>
		/// Method to update an existing BankAccountType
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("bank-account-types/update")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<IApiResponse<BankAccountTypeResponse>>> Update([FromBody] UpdateBankAccountTypeCommand request)
		{
			//Send query to handler for processing
			var record = await this.mediator.Send(request);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status400BadRequest;
				this.response.Message = "Error updating record!";
				this.response.Data = null;
			}
			else
			{
				//Set response if found
				this.response.Success = true;
				this.response.Code = StatusCodes.Status200OK;
				this.response.Message = "Record updated!";
				this.response.Data?.Add(record);
			}

			return Ok(this.response);
		}

		/// <summary>
		/// Method to delete an existing BankAccountType
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("bank-account-types/{id}/delete")]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<BankAccountTypeResponse>), StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Delete(int id)
		{
			//Send query to handler for processing
			var query = new DeleteBankAccountTypeCommand(id);
			bool status = await this.mediator.Send(query);

			// Check for null reference
			if (status)
			{
				//Set response if found
				this.response.Success = true;
				this.response.Code = StatusCodes.Status200OK;
				this.response.Message = "Record deleted successfully!";
				this.response.Data = null;
			}
			else
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status400BadRequest;
				this.response.Message = "Error deleting record!";
				this.response.Data = null;
			}

			return Ok(this.response);
		}
	}

}
