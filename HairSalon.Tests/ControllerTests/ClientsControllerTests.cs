using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using System;


namespace HairSalon.Tests
{
	[TestClass]
	public class ClientsControllerTest
	{

    [TestMethod]
		public void Index_ReturnsCorrectView_True()
		{
			ClientsController controller = new ClientsController();

			ActionResult indexView = controller.Index();

			Assert.IsInstanceOfType(indexView, typeof(ViewResult));
		}
    
		[TestMethod]
     public void New_ReturnsCorrectView_True()
     {
       ClientsController controller = new ClientsController();
       ActionResult newView = controller.New();
       Assert.IsInstanceOfType(newView, typeof(ViewResult));
     }

		 [TestMethod]
     public void Show_ReturnsCorrectActionType_ViewResult()
     {
       ClientsController controller = new ClientsController();
       IActionResult view = controller.Show(1);
       Assert.IsInstanceOfType(view, typeof(ViewResult));
     }
	}
}