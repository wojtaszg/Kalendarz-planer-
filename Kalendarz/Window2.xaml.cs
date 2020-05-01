using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kalendarz
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        /// <summary>
        /// Połączenie entity framework
        /// </summary>
        EventsEntity entit;
        /// <summary>
        /// Zmienna typu string przechowująca datę
        /// </summary>
        public string data;
        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public Window2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor parametryczny inicjujący pole data
        /// </summary>
        /// <param name="x"></param>
        public Window2(string x)
        {
            InitializeComponent();
            data = x;
        }

        /// <summary>
        /// Metoda dodająca do bazy danych nowe wydarzenie po wciśnięciu przycisku
        /// Dane są wprowadzane w polach textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddEvent_Click(object sender, RoutedEventArgs e)
        {
            entit = new EventsEntity();
            ListOfEvents abc = new ListOfEvents();
            int i = 0;
            try
            {
                abc.Description = DescTextBox.Text;
                abc.Date = (DateTime.Parse(data));
                abc.Time = TimeSpan.Parse(TimeTextBox.Text);
                
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Należy wypełnić pola");
                this.Close();
                i = 2;
            }

            if(i == 0)
            {
                entit.ListOfEvents.Add(abc);
                entit.SaveChanges();
                this.Close();
            }
            
        }
  
    }
}
