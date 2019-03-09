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
      List<Stylist> allstylists = Stylist.GetAll();
      return View("Index", allstylists);
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(id);
      List<Client> stylistsClients = selectedStylist.GetClients();
      List<Client> allClients = Client.GetAll();
      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      model.Add("stylist", selectedStylist);
      model.Add("stylistsClients", stylistsClients);
      model.Add("clients", allClients);
      model.Add("specialties", allSpecialties);
      model.Add("stylistSpecialties", stylistSpecialties);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients/new")]
    public ActionResult AddClient(int stylistId, int clientId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(clientId);
      stylist.AddClient(client);
      return RedirectToAction("Show",  new { id = stylistId });
    }

    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult AddSpecialty(int stylistId, int specialtyId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Specialty specialty = Specialty.Find(specialtyId);
      stylist.AddSpecialty(specialty);
      return RedirectToAction("Show",  new { id = stylistId });
    }

    [HttpGet("/stylists/{id}/edit")]
    public ActionResult Edit(int id)
    {
      Stylist newStylist = Stylist.Find(id);
      return View(newStylist);
    }

    [HttpPost("/stylists/{id}/edit")]
    public ActionResult EditPost(int id, string name)
    {
      Stylist newStylist = Stylist.Find(id);
      newStylist.Edit(name);
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Stylist newStylist = Stylist.Find(id);
      newStylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/deleteall")]
    public ActionResult DeleteAll(int id)
    {
      Stylist.DeleteAll();
      return RedirectToAction("Index");
    }


	}
}
