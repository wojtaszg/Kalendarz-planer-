using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Tests
{
	[TestClass()]
	public class TemperatureObjTests
	{
		[TestMethod()]
		public void TemperatureObjTest()
		{
			double temp = 28300;
			double min = 27300;
			double max = 28500; 
			TemperatureObj obj = new TemperatureObj(temp, min, max);
			Assert.AreEqual(obj.KelvinCurrent, temp / 100);
			Assert.AreEqual(obj.KelvinMaximum, max / 100);
			Assert.AreEqual(obj.KelvinMinimum, min/ 100);
		}

		[TestMethod()]
		public void TemperatureObjTest1()
		{
			double temp = 28300;
			double min = 27300;
			double max = 28500;
			TemperatureObj obj = new TemperatureObj(temp, min, max);
			Assert.AreNotEqual(obj.KelvinCurrent, temp);
			Assert.AreNotEqual(obj.KelvinMaximum, max);
			Assert.AreNotEqual(obj.KelvinMinimum, min);
		}

	}
}
