using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientsController: Controller
    {
			[HttpGet("/stylists/{stylistId}/clients/new")]
    	public ActionResult New(int stylistId)
      {
       Stylist stylist = Stylist.Find(stylistId);
       return View(stylist);
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int stylistId, int clientId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpPost("/clients/delete")]
    public ActionResult DeleteAll()
    {
      Client.ClearAll();
      return View();
    }

    //CREATES RESTFUL EDIT ROUTE
    //URL PATH INCLUDES ITEM ID AND stylist ID
    //PASSES THEM BOTH INTO EDIT ROUTE AS PARAMETERS
    //USING FIND METHOD - WE LOCATE ITEM AND stylist ADD THEM TO A Dictionary NAMED MODEL, AND PASS THE Dictionary INTO THE VIEW

    [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
    public ActionResult Edit(int stylistId, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      Client client = Client.Find(clientId);
      model.Add("client", client);
      return View(model);
    }


    // [HttpPost("/stylists/{stylistId}/clients/{clientId}")]
    // public ActionResult Update (int stylistId, int clientId, string newDescription)
    // {
    //   Client client = Client.Find(clientId);
    //   Client.Edit(newDescription);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Stylist stylist = Stylist.Find(stylistId);
    //   model.Add("stylist", stylist);
    //   model.Add("client", client);
    //   return View("Show", model);
    // }

    // [HttpGet("/stylists/{stylistId}/clients/{clientId}/delete")]
    // public ActionResult Delete (int clientId)
    // {
    //   Client.Delete(clientId);
    //   return View();
    // }



		}
}
//         [HttpGet("/stylists/{stylistId}/clients/new")]
//         public ActionResult New(int stylistId)
//         {
//             Stylist stylist = Stylist.Find(stylistId);
//             return View(stylist);
//         }

//         [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
// 				public ActionResult Show(int stylistId, int clientId)
// 				{
// 					Client client = Client.Find(clientId);
// 					Dictionary<string, object> model = new Dictionary<string, object>();
// 					Stylist stylist = Stylist.Find(stylistId);
// 					model.Add("client", client);
// 					model.Add("stylist", stylist);
// 					return View(model);
// 				}

				
// 			}

// }