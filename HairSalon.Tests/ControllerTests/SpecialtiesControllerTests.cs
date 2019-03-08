using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;
using System;


namespace HairSalon.Tests
{
	[TestClass]
	public class SpecialtiesControllerTest
	{

    [TestMethod]
		public void Index_ReturnsCorrectView_True()
		{
			SpecialtiesController controller = new SpecialtiesController();

			ActionResult indexView = controller.Index();

			Assert.IsInstanceOfType(indexView, typeof(ViewResult));
		}
    
		[TestMethod]
     public void New_ReturnsCorrectView_True()
     {
       SpecialtiesController controller = new SpecialtiesController();
       ActionResult newView = controller.New();
       Assert.IsInstanceOfType(newView, typeof(ViewResult));
     }

		 [TestMethod]
     public void Show_ReturnsCorrectActionType_ViewResult()
     {
       SpecialtiesController controller = new SpecialtiesController();
       IActionResult view = controller.Show(1);
       Assert.IsInstanceOfType(view, typeof(ViewResult));
     }
	}
}