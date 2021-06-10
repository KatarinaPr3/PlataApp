using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlataApp.Models;

namespace PlataApp.Controllers
{
    public class PlatasController : Controller
    {
        private PlataZadatakDBEntities db = new PlataZadatakDBEntities();

        // GET: Platas
        public ActionResult Index()
        {
            return View(db.Platas.ToList());
        }

       
          // GET: Platas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Platas/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Prezime,Adresa,NetoPlata,Bruto1,PoreskoOslobodjenje,PoreskaOsnovica,PoreskaStopa,PioZap,ZdrOsigZap,Nezaposlenost,PioPoslodavac,ZdrOsigPoslodavac,Bruto2")] Plata plata)
        {
            if (ModelState.IsValid)
            {
                plata.Bruto1 = (plata.NetoPlata - 1830) / 701 * 1000;
                plata.PoreskoOslobodjenje = 18300;
                plata.PoreskaOsnovica = plata.Bruto1 - 18300;
                if (plata.PoreskaOsnovica >= 405750)
                {
                    plata.PoreskaOsnovica = 405750;
                }

                plata.PoreskaStopa = plata.PoreskaOsnovica / 100 * 10;
                plata.PioZap = plata.Bruto1 / 100 * 14;
                plata.ZdrOsigZap = plata.Bruto1 / 10000 * 515;
                plata.Nezaposlenost = plata.Bruto1 / 10000 * 75;
                plata.ZdrOsigPoslodavac = plata.Bruto1 / 1000 * 115;
                plata.PioPoslodavac = plata.Bruto1 / 10000 * 515;
                plata.Bruto2 = plata.Bruto1 + plata.ZdrOsigPoslodavac + plata.PioPoslodavac;

                db.Platas.Add(plata);
                db.SaveChanges();
            }

            return View(plata);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
