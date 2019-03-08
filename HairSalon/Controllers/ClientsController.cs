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

    [HttpGet("/clients/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Client selectedClient = Client.Find(id);
      List<Stylist> clientStylists = selectedClient.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("selectedClient", selectedClient);
      model.Add("clientStylists", clientStylists);
      model.Add("allStylists", allStylists);
      return View(model);
    }

    [HttpPost("/clients/{clientId}/stylists/new")]
    public ActionResult AddStylist(int stylistId, int clientId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(clientId);
      client.AddStylist(stylist);
      return RedirectToAction("Show",  new { id = clientId });
    }


    [HttpGet("/clients/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Client newClient = Client.Find(id);
      return View(newClient);
    }

    [HttpPost("/clients/{id}/edit")]
    public ActionResult EditPost(int id, string name)
    {
      Client newClient = Client.Find(id);
      newClient.Edit(name);
      return RedirectToAction("Index");
    }

    [HttpGet("/clients/{id}/delete")]
    //HttpGet for delete because we are not going to new page to perform delete
    public ActionResult Delete(int id)
    {
      Client newClient = Client.Find(id);
      newClient.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/clients/deleteall")]
    public ActionResult DeleteAll(int id)
    {
      Client.DeleteAll();
      return RedirectToAction("Index");
    }
  }
}
       