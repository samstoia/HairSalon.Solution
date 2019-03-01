using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
	[TestClass]
	public class StylistControllerTest
	{
		[TestMethod]
			public void Index_HasCorrectModelType_StylistList()
			{
			//Arrange
				ViewResult indexView = new HomeController().Index() as ViewResult;

				//Act
				var result = indexView.ViewData.Model;

				//Assert
				Assert.IsInstanceOfType(result, typeof(List<Stylist>));
			}

		[TestMethod]
		public void Create_ReturnsCorrectActionType_ViewResult()
		{
			//Arrange
			StylistsController controller = new StylistsController();
		
			//Act
			IActionResult view = controller.Create("Paula Abdul");
		
			//Assert
			Assert.IsInstanceOfType(view, typeof(ViewResult));
      }
	}

}