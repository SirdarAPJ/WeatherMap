using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherMap.Helpers;
using WeatherMap.Infraestructure.DataTransfer;
using WeatherMap.Views;
using Xamarin.Forms;

namespace WeatherMap.ViewModels
{
    public class CityListViewModel : ViewModelBase
    {
        private ObservableCollection<WeatherResult> _favourites;
        private ObservableCollection<City> _cities;
        private List<City> _allCities;
        private ICommand _searchCommand;
        private ICommand _itemSelectedCommand;

        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        public CityListViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Selecione a Cidade";
        }

        private void LoadListItems()
        {

            _allCities = Json.DeserializeResource<List<City>>("city.list.json");
            Cities = new ObservableCollection<City>(_allCities.ToList());
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            LoadListItems();

            _favourites = parameters["fav"] as ObservableCollection<WeatherResult>;

            base.OnNavigatingTo(parameters);
        }

        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        {
            if (text?.Length >= 1)
            {
                Cities = new ObservableCollection<City>(_allCities.Where(w => w.Name.ToUpper().Contains(text.ToUpper())));
            }
            else
            {
                Cities = new ObservableCollection<City>(_allCities);
            }

        }));

        public ICommand ItemSelectedCommand => _itemSelectedCommand ?? (_itemSelectedCommand = new Command<ItemTappedEventArgs>(async (parameter) =>
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("cityId", ((City)parameter.Item).Id);
            navParameters.Add("fav", _favourites);

            await NavigationService.NavigateAsync(nameof(CityDetailPage), navParameters);

        }));


    }
}
