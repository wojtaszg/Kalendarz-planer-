using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Kalendarz
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EventsEntity eve;
        /// <summary>
        /// 
        /// </summary>
        public List<ListBox> listBoxes = new List<ListBox>();
        /// <summary>
        /// 
        /// </summary>
        public List<Label> labelsDay = new List<Label>();
        List<ListBox> toDo = new List<ListBox>();
        /// <summary>
        /// 
        /// </summary>
        public DateTime data = DateTime.Parse(DateTime.Now.ToShortDateString());
        WindowNewCity newCity = new WindowNewCity();
        /// <summary>
        /// Konstruktor klasy
        /// Ustawia taktowanie zegara
        /// Wykonuje pierwsze połączenie z API
        /// Dodaje elementy(listboxy i labele) do odpowiednich list
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LabelDate.Content = DateTime.Now.ToString();
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();

            OpenWeatherAPI.API openWeatherAPI = new OpenWeatherAPI.API("a185292b07bca94dfdc3e856ddd7eaeb");
            OpenWeatherAPI.Query query = openWeatherAPI.Query("Siechnice");
            ApiTextBox.Text = string.Format("{0}\nTemperatura {1} °C\nMaksymalna: {2} °C\nMinimalna {3} °C\nWiatr {4} m/s",
                query.Name, query.Main.Temperature.CelsiusCurrent, query.Main.Temperature.CelsiusMaximum, query.Main.Temperature.CelsiusMinimum, query.Wind.SpeedMetersPerSecond, query.Wind.Gust);
            listBoxes.Add(ListBox1); listBoxes.Add(ListBox2); listBoxes.Add(ListBox3); listBoxes.Add(ListBox4); listBoxes.Add(ListBox5); listBoxes.Add(ListBox6); listBoxes.Add(ListBox7);
            labelsDay.Add(LabelDay1); labelsDay.Add(LabelDay2); labelsDay.Add(LabelDay3); labelsDay.Add(LabelDay4); labelsDay.Add(LabelDay5); labelsDay.Add(LabelDay6); labelsDay.Add(LabelDay7);
            toDo.Add(ListBoxToDo1); toDo.Add(ListBoxToDo2); toDo.Add(ListBoxToDo3); toDo.Add(ListBoxToDo4); toDo.Add(ListBoxToDo5); toDo.Add(ListBoxToDo6); toDo.Add(ListBoxToDo7);
        }

        /// <summary>
        /// Metoda odpowiedzialna za wybór daty w kalendarzu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Calendar_SelectedDatesChanged(object sender,
            SelectionChangedEventArgs e)
        {
            // ... Get reference.
            var calendar = sender as Calendar;

            // ... See if a date is selected.
            if (calendar.SelectedDate.HasValue)
            {
                // ... Display SelectedDate in Title.
                DateTime date = calendar.SelectedDate.Value;
                this.Title = date.ToShortDateString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Metoda wykonująca się co interval = 1s 
        /// Sprawdza zmiany w danych pobranych z API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            LabelDate.Content = DateTime.Now.ToString();
 
            if (newCity.ButtonClick == true)
            {
                ApiTextBox.Text = newCity.CityName;
            }
            calendar7Days();
        }

        /// <summary>
        /// Wczytanie głównego okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            eve = new EventsEntity();
            //DataGrid1.ItemsSource = eve.ListOfEvents.ToList();
        }

        /// <summary>
        /// Metoda wyświetlająca wszystkie wydarzenia z wybranego w kalendarzu dnia
        /// po kliknięciu przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelctedDayEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = cldSample.SelectedDate.Value.ToString("yyyy/MM/dd");
                DateTime d = DateTime.Parse(s);

                var culture = new System.Globalization.CultureInfo("pl-PL");
                var day = culture.DateTimeFormat.GetDayName(d.DayOfWeek);
                using (var ctx = new EventsEntity())
                {
                    var items = ctx.ListOfEvents
                      .Where(x => x.Date == d)
                      .OrderBy(x => x.Time)
                      .Select(x => new
                      {
                          Nr = x.EventId,
                          Opis = x.Description,
                          Dzien = day,
                          Godzina = x.Time
                      }).ToList();
                    DataGrid2.ItemsSource = items;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Wybierz datę z kalendarza");
            }
            
        }

    
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void TextBoxDesc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        /// <summary>
        /// Metoda tworząca kolekcje danych z wydarzeń wybranego dnia
        /// i wyświetlająca jej zawartość w podanym listboxie
        /// </summary>
        /// <param name="d"></param>
        /// <param name="listBox"></param>
        private void weeklyEvents(DateTime d, ListBox listBox)
        {
            var culture = new System.Globalization.CultureInfo("pl-PL");
            var day = culture.DateTimeFormat.GetDayName(d.DayOfWeek);
            using (var ctx = new EventsEntity())
            {
                var items = ctx.ListOfEvents
                  .Where(x => x.Date == d)
                  .OrderBy(x => x.Time)
                  .Select(x => new
                  {
                      Opis = x.Description,
                      Dzien = day,
                      Godzina = x.Time
                  }).ToList();

                foreach (var i in items)
                {
                    string x = i.Opis + "  " + i.Godzina.ToString() + "\n";
                    listBox.Items.Add(x);
                }
            }
        }


        /// <summary>
        /// Metoda tworząca kolekcje danych z zadań wybranego dnia
        /// i wyświetlająca jej zawartość w podanym listboxie
        /// </summary>
        /// <param name="d"></param>
        /// <param name="listBox"></param>
        private void weeklyTasks(DateTime d, ListBox listBox)
        {
            using (var ctx = new EventsEntity())
            {
                var items = ctx.ListOfTasks
                  .Where(x => x.Date == d)
                  .Select(x => new
                  {
                      Opis = x.Description,
                  }).ToList();

                foreach (var i in items)
                {
                    string x = i.Opis + "\n";
                    listBox.Items.Add(x);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void calendar7Days()
        {
            foreach (ListBox listBox in listBoxes)
                listBox.Items.Clear();
            foreach (ListBox lis in toDo)
                lis.Items.Clear();
            //string a = DateTime.Now.ToShortDateString();
            DateTime d = data;//DateTime.Parse(a);
            DateTime x = d;
            var culture = new System.Globalization.CultureInfo("pl-PL");
            var day = culture.DateTimeFormat.GetDayName(d.DayOfWeek);
            int dayOfYear = d.DayOfYear;
            int v = d.DayOfYear;
            int i = 0;
            List<string> list = new List<string>();
            List<string> daty = new List<string>();

            foreach (ListBox listBox in listBoxes)
            {
                weeklyEvents(d, listBox);
                day = culture.DateTimeFormat.GetDayName(d.DayOfWeek);
                list.Add(day);
                daty.Add(d.ToShortDateString());
                d = new DateTime(DateTime.Now.Year, 1, 1).AddDays(v);
                v++;
            }

            v = x.DayOfYear;
            foreach (ListBox lis in toDo)
            {
                weeklyTasks(x, lis);
                x = new DateTime(DateTime.Now.Year, 1, 1).AddDays(v);
                v++;
            }

            foreach (Label label in labelsDay)
            {
                label.Content = daty[i] + "\n" + list[i];
                i++;
                day = culture.DateTimeFormat.GetDayName((d.DayOfWeek + 1));
            }
        }

        /// <summary>
        /// Metoda odpowiadająca za wczytanie oraz wyświetlenia 
        /// wydarzeń oraz zadań w przeciągu tygodnia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowCalendar_Clik(object sender, RoutedEventArgs e)
        {
            calendar7Days();
        }

      
        /// <summary>
        /// Metoda otwierająca nowe okno dialogowe po naciśnięciu przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChangeCity_Click(object sender, RoutedEventArgs e)
        {
            newCity.Show();
        }

        /// <summary>
        /// Metoda odpowiedzialna za utworzenie okna dialogowego do wprowadzenia nowego wydarzenia
        /// oraz przekazania jemu jako parametr wybranej z kalendarza daty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddEvent_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                cldSample.SelectedDate.Value.ToString("yyyy/MM/dd");
                string s = cldSample.SelectedDate.Value.ToString("yyyy/MM/dd");
                DateTime d = DateTime.Parse(s);
                s = d.ToShortDateString();
                d = DateTime.Parse(s);
                Window2 window2 = new Window2(s);
                window2.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Przed dodaniem wydarzenia wybierz datę z kalendarza!");
            }
        }

        /// <summary>
        /// Metoda wykonująca się w momencie zamknięcia aplikacji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            newCity.Close();
        }

        /// <summary>
        /// Metoda odpowiedzialna za utworzenie okna dialogowego do wprowadzenia nowego zadania
        /// oraz przekazania jemu jako parametr wybranej z kalendarza daty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonTask_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                cldSample.SelectedDate.Value.ToString("yyyy/MM/dd");
                string s = cldSample.SelectedDate.Value.ToString("yyyy/MM/dd");
                DateTime d = DateTime.Parse(s);
                s = d.ToShortDateString();
                WindowNewTask newTask = new WindowNewTask(s);
                newTask.Show();
            }
            catch
            {
                MessageBox.Show("Przed dodaniem zadania wybierz datę z kalendarza!");
            } 
        }

        /// <summary>
        /// Metoda wyświetlająca wszystkie zadania w dniu
        /// wybraznym z kalendarza po wciśnięciu przycisku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string s = cldSample.SelectedDate.Value.ToString("yyyy/MM/dd");
                DateTime d = DateTime.Parse(s);

                var culture = new System.Globalization.CultureInfo("pl-PL");
                var day = culture.DateTimeFormat.GetDayName(d.DayOfWeek);
                using (var ctx = new EventsEntity())
                {
                    var items = ctx.ListOfTasks
                      .Where(x => x.Date == d)
                      .Select(x => new
                      {
                          Nr = x.TaskId,
                          Opis = x.Description,
                          Dzien = day,
                      }).ToList();
                    DataGridTask.ItemsSource = items;
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Wybierz datę z kalendarza");
            }
        }

        /// <summary>
        /// Metoda otwierająca okno dialogowe umożliwiające usunięcie wydarzenia/zadania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEventRemove_Click(object sender, RoutedEventArgs e)
        {
            WindowRemoveObject windowRO = new WindowRemoveObject();
            windowRO.Show();
        }

       
        private void ButtonNextWeek_Click(object sender, RoutedEventArgs e)
        {
            int x = data.DayOfYear + 6;
            data = new DateTime(DateTime.Now.Year, 1, 1).AddDays(x);
            calendar7Days();
        }

        private void ButtonPreviousweek_Click(object sender, RoutedEventArgs e)
        {
            int x = data.DayOfYear - 8;
            data = new DateTime(DateTime.Now.Year, 1, 1).AddDays(x);
            calendar7Days();
        }

    }
}
   
