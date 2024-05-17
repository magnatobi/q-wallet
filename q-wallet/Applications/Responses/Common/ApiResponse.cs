namespace q_wallet.Applications.Responses.Common
{
	public class ApiResponse<T> : IApiResponse<T> where T : class
	{
		/// <summary>
		/// Init the constructor
		/// </summary>
		public ApiResponse()
		{
			Data = new List<T>();
		}

		public bool Success { get; set; } = false;
		public int Code { get; set; }
		public string Message { get; set; } = string.Empty;
		public IList<T>? Data { get; set; }
	}
}
