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
    public class Window2Tests
    {
        [TestMethod()]
        public void Window2Test()
        {
            MainWindow window = new MainWindow();
            string x = window.data.ToString();
            Window2 window2 = new Window2(x);
            Assert.AreEqual(x, window2.data);
        }
    }
}