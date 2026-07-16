using Microsoft.Extensions.Logging;
using Ombra.Infrastructure;
using Ombra.Repositories;
using Ombra.ViewModels;

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

		// Registra il provider nativo di SQLite: senza questa chiamata sqlite-net-pcl lancia
		// un'eccezione alla prima query, che passa inosservata perché OnAppearing è async void.
		SQLitePCL.Batteries_V2.Init();

		var dbPath = Path.Combine(FileSystem.AppDataDirectory, "ombra.db3");
		builder.Services.AddSingleton(new OmbraDatabase(dbPath));
		builder.Services.AddSingleton<IOmbrelloneRepository, OmbrelloneRepository>();
		builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();
		builder.Services.AddSingleton<IPrenotazioneRepository, PrenotazioneRepository>();

		builder.Services.AddTransient<MainViewModel>();
		builder.Services.AddTransient<MainPage>();

		return builder.Build();
	}
}
