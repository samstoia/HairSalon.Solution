using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index_ReturnsCorrectView_True()
		{
			HomeController controller = new HomeController();

			ActionResult indexView = controller.Index();

			Assert.IsInstanceOfType(indexView, typeof(ViewResult));
		}

		[TestMethod]
		public void Index_HasCorrectModelType_ClientList()
		{
			//Arrange
			ViewResult indexView = new HomeController().Index() as ViewResult;

			//Act
			var result = indexView.ViewData.Model;

			//Assert
			Assert.IsInstanceOfType(result, typeof(List<Client>));
		}
	}
}