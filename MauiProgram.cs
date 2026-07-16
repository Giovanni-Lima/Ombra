using Microsoft.Extensions.Logging;
using Ombra.Infrastructure;
using Ombra.Repositories;

namespace Ombra;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var dbPath = Path.Combine(FileSystem.AppDataDirectory, "ombra.db3");
		builder.Services.AddSingleton(new OmbraDatabase(dbPath));
		builder.Services.AddSingleton<IOmbrelloneRepository, OmbrelloneRepository>();
		builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
		builder.Services.AddSingleton<IPrenotazioneRepository, PrenotazioneRepository>();

		return builder.Build();
	}
}
