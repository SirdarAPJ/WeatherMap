using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherMap.Helpers;
using WeatherMap.Infraestructure.DataTransfer;
using WeatherMap.Interfaces.Services;
using WeatherMap.Views;
using Xamarin.Forms;

namespace WeatherMap.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<WeatherResult> _favourites;
        private ICommand _searchCommand;
        private ICommand _clearCommand;
        private ICommand _itemSelectedCommand;

        private readonly IWeatherMapApiClient _weatherApiClient;
        private readonly IPageDialogService _dialogService;

        public ObservableCollection<WeatherResult> Favourites
        {
            get { return _favourites; }
            set { SetProperty(ref _favourites, value); }
        }

        public MainPageViewModel(INavigationService navigationService,
                                 IWeatherMapApiClient weatherApiClient,
                                 IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Clima";

            _weatherApiClient = weatherApiClient;
            _dialogService = dialogService;
        }

        private async void LoadListItems()
        {
            var fav = Cache.Instance.Get<WeatherResult>("FAV");

            if (fav == null || fav.Count == 0)
            {
                Favourites = new ObservableCollection<WeatherResult>();
                await _dialogService.DisplayAlertAsync(Title, "Lista de cidades favoritas vazia", "Ok");
            }
            else
                Favourites = new ObservableCollection<WeatherResult>(fav);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            LoadListItems();

            base.OnNavigatingTo(parameters);
        }

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command(async () =>
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("fav", _favourites);

            await NavigationService.NavigateAsync(nameof(CityListPage), navParameters);
        }));

        public ICommand ClearCommand => _clearCommand ?? (_clearCommand = new Command(() =>
        {
            Cache.Instance.Clear();
            LoadListItems();
        }));

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new Command<ItemTappedEventArgs>(async (parameter) =>
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("cityId", ((WeatherResult)parameter.Item).Id);
            navParameters.Add("fav", _favourites);

            await NavigationService.NavigateAsync(nameof(CityDetailPage), navParameters);

        }));

    }
}
