using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using q_wallet.Applications.Entities.UserAccounts.Commands;
using q_wallet.Applications.Entities.UserAccounts.Queries;
using q_wallet.Applications.Responses;
using q_wallet.Applications.Responses.Common;
using q_wallet.Controllers.Common;

namespace q_wallet.Controllers
{
	/// <summary>
	/// Manage all user account endpoints
	/// </summary>
	[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status401Unauthorized)]
	public class UserAccountController : BaseController
	{
		private readonly IMediator mediator;
		private readonly ILogger<UserAccountController> logger;
		private readonly IApiResponse<UserAccountResponse> response;


		/// <summary>
		/// Initialise parameters via Constructor
		/// </summary>
		/// <param name="mediator"></param>
		/// <param name="logger"></param>
		public UserAccountController(IMediator mediator, ILogger<UserAccountController> logger, IApiResponse<UserAccountResponse> response)
		{
			this.mediator = mediator;
			this.logger = logger;
			this.response = response;
		}


		/// <summary>
		/// Method to get all user accounts
		/// </summary>
		/// <returns></returns>
		[HttpGet("user-accounts")]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IApiResponse<UserAccountResponse>>> GetAll()
		{
			var query = new GetAllUserAccountQuery();
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
		/// Method to get account by user id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("user-accounts/{userId}")]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IApiResponse<UserAccountResponse>>> GetById(Guid userId)
		{
			//Query and send to handler
			var query = new GetUserAccountByIdQuery(userId);
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
		/// Method to create/onboard a new user 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost("user-accounts/create")]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IApiResponse<UserAccountResponse>>> Create([FromBody] CreateUserAccountCommand request)
		{
			var record = await this.mediator.Send(request);

			// Check for null
			if (record == null || record.Id == 0)
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status500InternalServerError;
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
		/// Method to update an existing user account
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPut("user-accounts/update")]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IApiResponse<UserAccountResponse>>> Update([FromBody] UpdateUserAccountCommand request)
		{
			var record = await this.mediator.Send(request);

			// Check for null
			if (record == null)
			{
				//Set response if not record found
				this.response.Success = false;
				this.response.Code = StatusCodes.Status500InternalServerError;
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
		/// Method to delete an existing account
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete("user-accounts/{userId}/delete")]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(IApiResponse<UserAccountResponse>), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(Guid userId)
		{
			//Query and send to the handler
			var query = new DeleteUserAccountCommand(userId);
			bool status = await this.mediator.Send(query);

			// Check for null or false
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
