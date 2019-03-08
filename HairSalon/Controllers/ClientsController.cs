using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController: Controller
  {

    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/clients/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/clients")]
    public ActionResult Create(string name)
    {
      Client newClient = new Client(name);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
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

    // [HttpGet("/stylists/{stylistId}/clients/{clientId}/delete")]
    // public ActionResult Delete (int clientId)
    // {
    //   Client.Delete(clientId);
    //   return View();
    // }

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
  }
}
       