using Microsoft.AspNetCore.Mvc;

namespace q_wallet.Controllers.Common
{
	//[Route("api/[controller]")]
	//[ApiVersion("1.0")]
	[Route("q-wallet/api/v{version:apiVersion}")]
	[Consumes("application/json")]
	[Produces("application/json")]
	[ApiController]
	public class BaseController : ControllerBase
	{
	}
}
