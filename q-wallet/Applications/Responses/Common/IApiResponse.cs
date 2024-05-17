namespace q_wallet.Applications.Responses.Common
{
	public interface IApiResponse<T>
	{
		bool Success { get; set; }
		int Code { get; set; }
		string Message { get; set; }
		IList<T>? Data { get; set; }
	}
}
