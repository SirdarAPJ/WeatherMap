﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherMap.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CityDetailPage : ContentPage
	{
		public CityDetailPage ()
		{
			InitializeComponent ();
		}
	}
}