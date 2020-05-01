using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kalendarz
{
    /// <summary>
    /// Interaction logic for WindowNewCity.xaml
    /// </summary>
    public partial class WindowNewCity : Window
    {
        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public WindowNewCity()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Nazwa nowego miasta
        /// </summary>
        public string CityName;

        /// <summary>
        /// Zmienna pomocnicza
        /// </summary>
        public bool ButtonClick = false;

        /// <summary>
        /// Metoda dodająca nowe miasto oraz pobierająca dla niego dane z API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddCity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenWeatherAPI.API openWeatherAPI = new OpenWeatherAPI.API("a185292b07bca94dfdc3e856ddd7eaeb");
                OpenWeatherAPI.Query query = openWeatherAPI.Query(TextBoxCity.Text);
                CityName = string.Format("{0}\nTemperatura {1} °C\nMaksymalna: {2} °C\nMinimalna {3} °C\nWiatr {4} m/s",
                query.Name, query.Main.Temperature.CelsiusCurrent, query.Main.Temperature.CelsiusMaximum, query.Main.Temperature.CelsiusMinimum, query.Wind.SpeedMetersPerSecond, query.Wind.Gust);

            }
            catch (Exception)
            {
                Hide();
            }
            
            ButtonClick = true;
            this.Hide();
        }

        /// <summary>
        /// Meotda zwracająca nazwę miasta
        /// </summary>
        /// <returns></returns>
        public string ReturnCityName()
        {
            return CityName;
        }
    }
}
