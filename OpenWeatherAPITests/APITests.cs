using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenWeatherAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Tests
{
	[TestClass()]
	public class APITests
	{
		

		[TestMethod()]
		public void QueryTest()
		{
			var api = new API("a185292b07bca94dfdc3e856ddd7eaeb");
			var actual = api.Query("Wrocław,PL");
			Assert.IsTrue(actual.ValidRequest);
		}
	}
}
