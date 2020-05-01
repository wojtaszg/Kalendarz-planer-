using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kalendarz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalendarz.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void MainWindowTestLists()
        {
            MainWindow window = new MainWindow();
            Assert.AreEqual(7, window.labelsDay.Count());
            Assert.AreEqual(7, window.listBoxes.Count());
        }

        [TestMethod()]
        public void MainWindowTestDate()
        {
            MainWindow window = new MainWindow();
            Assert.AreEqual(DateTime.Now.DayOfWeek, window.data.DayOfWeek);
            Assert.AreEqual(DateTime.Now.Day, window.data.Day);
            Assert.AreEqual(DateTime.Now.DayOfYear, window.data.DayOfYear);
        }

        [TestMethod()]
        public void MainWindowTestDate1()
        {
            MainWindow window = new MainWindow();
            Assert.AreNotEqual(DateTime.Now, window.data);
            Assert.AreNotEqual(DateTime.Now.DayOfYear+30, window.data.DayOfYear);
        }


    }
}