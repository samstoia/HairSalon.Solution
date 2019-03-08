using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
	public class SpecialtiesController: Controller
	{
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/specialties")]
    public ActionResult Create(string name)
    {
      Specialty newSpecialty = new Specialty(name);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View("Index", allSpecialties);
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Stylist> specialtyStylists = selectedSpecialty.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("specialtyStylists", specialtyStylists);
      model.Add("allStylists", allStylists);
      return View(model);
    }

    [HttpPost("/specialties/{specialtyId}/stylists/new")]
    public ActionResult AddStylist(int stylistId, int specialtyId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Specialty specialty = Specialty.Find(specialtyId);
      specialty.AddStylist(stylist);
      return RedirectToAction("Show",  new { id = specialtyId });
    }
  }
}