using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace Kalendarz
{
    /// <summary>
    /// Interaction logic for WindowRemoveObject.xaml
    /// </summary>
    public partial class WindowRemoveObject : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public WindowRemoveObject()
        {
            InitializeComponent();
        }

        private void ButtonRemoveEvent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Int32.Parse(TextBoxID.Text);
                using (var ctx = new EventsEntity())
                {
                    var events = new ListOfEvents { EventId = id};
                    ctx.ListOfEvents.Attach(events);
                    ctx.ListOfEvents.Remove(events);
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Wydarzenie o takim ID nie istnieje");
            }
            this.Close();
        }

        private void ButtonRemoveTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Int32.Parse(TextBoxID.Text);
                using (var ctx = new EventsEntity())
                {
                    var task = new ListOfTasks { TaskId = id };
                    ctx.ListOfTasks.Attach(task);
                    ctx.ListOfTasks.Remove(task);
                    ctx.SaveChanges();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Zadanie o takim ID nie istnieje");
            }
            this.Close();
        }
    }
}
