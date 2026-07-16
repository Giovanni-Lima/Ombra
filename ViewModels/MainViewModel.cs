using CommunityToolkit.Mvvm.ComponentModel;
using Ombra.Models;
using Ombra.Repositories;

namespace Ombra.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IOmbrelloneRepository _ombrelloneRepository;

    [ObservableProperty]
    private List<Ombrellone> ombrelloni = new();

    public MainViewModel(IOmbrelloneRepository ombrelloneRepository)
    {
        _ombrelloneRepository = ombrelloneRepository;
    }

    public async Task LoadAsync()
    {
        Ombrelloni = await _ombrelloneRepository.GetAllAsync();
    }
}
