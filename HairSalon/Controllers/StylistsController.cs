using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController: Controller
    {

			[HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);
      return View(model);
    }

    // This one creates new Clients within a given stylist, not new stylists:
    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(stylistId,clientName);
      newClient.Save();
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      return View("Show", model);
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult Delete (int stylistId)
    {
      Client.ClearAllWithin(stylistId);
      Stylist.Delete(stylistId);
      return View();
    }

		}
}



// 			[HttpGet("/stylists")]
// 			public ActionResult Index()
// 			{
// 			List<Stylist> allStylists = Stylist.GetAll();
// 				return View(allStylists);
// 			}

// 			[HttpGet("/stylists/new")]
// 			public ActionResult New()
// 			{
// 				return View();
// 			}

// 			[HttpPost("/stylists")]
// 			public ActionResult Create(string stylistName)
// 			{
// 				Stylist newStylist = new Stylist(stylistName);
// 				newStylist.Save();
// 				List<Stylist> allStylists = Stylist.GetAll();
// 				return View("Index", allStylists);
// 			}

// 			[HttpGet("/stylists/{id}")]
// 			public ActionResult Show(int id)
// 			{
// 				Dictionary<string, object> model = new Dictionary<string, object>();
// 				Stylist selectedStylist = Stylist.Find(id);
// 				List<Client> stylistClents = selectedStylist.GetClients();
// 				model.Add("stylist", selectedStylist);
// 				model.Add("clients", stylistClents);
// 				return View(model);
// 			}


// 			[HttpPost("/stylists/{id}/clients")]
// 			public ActionResult Create(int stylistId, string clientName)
// 			{
// 				Dictionary<string, object> model = new Dictionary<string, object>();
// 				Stylist foundStylist = Stylist.Find(stylistId);
// 				Client newClient = new Client(stylistId, clientName);
// 				newClient.Save();
// 				List<Client> stylistClients = foundStylist.GetClients();
// 				model.Add("clients", stylistClients);
// 				model.Add("stylist", foundStylist);
// 				return View("Show", model);
// 			}

// 			[HttpGet("/stylists/{stylistId}/delete")]
// 			public ActionResult Delete (int stylistId)
// 			{
// 				Client.ClearAllWithin(stylistId);
// 				Stylist.Delete(stylistId);
// 				return View();
// 			}

//     }

// }