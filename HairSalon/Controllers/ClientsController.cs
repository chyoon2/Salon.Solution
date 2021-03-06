using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Linq;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;

    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      if(_db.Stylists.ToList().Count == 0)
      {
        ViewBag.Stylist= 0;
      }
      else{
        ViewBag.Stylist= 1;
      }
      List<Client> model = _db.Clients.Include(Clients => Clients.Stylist).ToList();
      return View(model);
    }

    public ActionResult Show(int id)
    {
      Client thisClient = _db.Clients.Include(Clients => Clients.Stylist).FirstOrDefault(Clients => Clients.ClientId == id);
      return View(thisClient);
    }
    
    public ActionResult Create()
    {
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId",
      "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client)
    {
      _db.Clients.Add(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(Clients => Clients.ClientId == id);
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(Clients => Clients.ClientId == id);
      return View(thisClient);
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Client thisClient = _db.Clients.FirstOrDefault(Clients => Clients.ClientId == id);
      _db.Clients.Remove(thisClient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
