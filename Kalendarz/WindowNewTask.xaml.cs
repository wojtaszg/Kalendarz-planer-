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
    /// Interaction logic for WindowNewTask.xaml
    /// </summary>
    public partial class WindowNewTask : Window
    {
        EventsEntity entit;
        /// <summary>
        /// 
        /// </summary>
        public string data;
        /// <summary>
        /// Konstruktor domyślny
        /// </summary>
        public WindowNewTask()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor parametryczny inicjujący pole data 
        /// </summary>
        /// <param name="d"></param>
        public WindowNewTask(string d)
        {
            InitializeComponent();
            data = d;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Metoda tworząca nowe zadania w wybranym z kalendarza dniu
        /// Dane są wpisywane w pola textbox
        /// Zadanie jest dodawane do bazy danych
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddTask_Click(object sender, RoutedEventArgs e)
        {
            entit = new EventsEntity();
            ListOfTasks abc = new ListOfTasks();
            int i = 0;
            try
            {
                abc.Description = Description.Text;
                abc.Date = DateTime.Parse(data);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Należy wypełnić pole");
                this.Close();
                i = 2;
            }

            if (i == 0)
            {
                entit.ListOfTasks.Add(abc);
                entit.SaveChanges();
                this.Close();
            }

        }
    }
}
