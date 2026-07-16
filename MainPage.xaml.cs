using Ombra.ViewModels;

namespace Ombra;

public partial class MainPage : ContentPage
{
	private readonly MainViewModel _viewModel;

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;

		// Impostato da code-behind invece che con <OnIdiom> in XAML: il MauiXamlInflator=SourceGen
		// di questo SDK non risolve correttamente OnIdiom<IItemsLayout> come elemento XAML annidato
		// (prova a castare l'oggetto OnIdiom stesso a IItemsLayout invece di chiamarne ProvideValue).
		var span = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 8 : 4;
		var spacing = DeviceInfo.Idiom == DeviceIdiom.Tablet ? 12 : 8;
		OmbrelloniCollectionView.ItemsLayout = new GridItemsLayout(span, ItemsLayoutOrientation.Vertical)
		{
			HorizontalItemSpacing = spacing,
			VerticalItemSpacing = spacing
		};
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		try
		{
			await _viewModel.LoadAsync();
		}
		catch (Exception ex)
		{
			await DisplayAlert("Errore caricamento dati", ex.Message, "OK");
		}
	}
}
