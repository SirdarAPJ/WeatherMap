using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WeatherMap.Helpers;
using WeatherMap.Infraestructure.DataTransfer;
using WeatherMap.Interfaces.Services;
using Xamarin.Forms;

namespace WeatherMap.ViewModels
{
    public class CityDetailViewModel : ViewModelBase
    {
        const string _APP_ID = "2bac87e0cb16557bff7d4ebcbaa89d2f";
        const string _LANG = "pt";
        const string _UNITS = "metric";

        private ObservableCollection<WeatherResult> _favourites;

        private WeatherResult _details;

        private readonly IWeatherMapApiClient _weatherApiClient;

        private ICommand _saveCommand;

        private string _icon;
        private string _cityName;
        private string _weather;
        private float _temperature;
        private float _max;
        private float _min;

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }
        public string CityName
        {
            get { return _cityName; }
            set { SetProperty(ref _cityName, value); }
        }

        public string Weather
        {
            get { return _weather; }
            set { SetProperty(ref _weather, value); }
        }

        public float Temperature
        {
            get { return _temperature; }
            set { SetProperty(ref _temperature, value); }
        }

        public float Max
        {
            get { return _max; }
            set { SetProperty(ref _max, value); }
        }

        public float Min
        {
            get { return _min; }
            set { SetProperty(ref _min, value); }
        }

        public CityDetailViewModel(INavigationService navigationService,
                                   IWeatherMapApiClient weatherApiClient)
            : base(navigationService)
        {
            Title = "Detalhes";
            _weatherApiClient = weatherApiClient;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            _favourites = parameters["fav"] as ObservableCollection<WeatherResult>;

            _details = await _weatherApiClient.GetWeatherMap(parameters["cityId"].ToString(), _APP_ID, _LANG, _UNITS);

            if (_details != null)
            {
                Icon = string.Concat("http://openweathermap.org/img/w/", _details.Weather[0].Icon, ".png");
                CityName = _details.Name;
                Temperature = _details.Main.Temp;
                Max = _details.Main.Temp_max;
                Min = _details.Main.Temp_min;
                Weather = _details.Weather[0].Description;
            }

        }

        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new Command<string>((text) =>
        {
            if (!_favourites.Any(a => a.Id == _details.Id))
            {
                _favourites.Add(_details);
            }

            SaveCache();

        }));

        private void SaveCache()
        {
            Cache.Instance.Save(_favourites.ToList(), "FAV");
        }

    }
}
