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
    public class WindowNewTaskTests
    {
        [TestMethod()]
        public void WindowNewTaskTest()
        {
            MainWindow window = new MainWindow();
            string x = window.data.ToString();
            WindowNewTask task = new WindowNewTask(x);
            Assert.AreEqual(x, task.data);
        }
    }
}