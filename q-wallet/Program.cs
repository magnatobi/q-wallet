using q_wallet.Infrastructure.Data;

namespace q_wallet
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args)
				.Build()
				//.MigrateDatabase<DataContext>((context, services) =>
				//{
				//	var logger = services.GetService<ILogger<DataContextSeed>>();
				//	DataContextSeed.SeedAsync(context, logger).Wait();
				//})
				.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		}
	}
}