using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Puma.Prey.Common.Rest;
using Puma.Prey.Rabbit.EF;
using Puma.Prey.Raccoon.Models;

namespace Puma.Prey.Raccoon.Controllers
{
    public class HuntController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new RabbitDBContext())
            {
                return View(context.Hunts.OrderByDescending(c => c.Id).ToList());
            }
        }

        public ActionResult Contest(int id)
        {
            var model = RestClient.Get<Rabbit.Models.Hunt>(string.Format("hunt/{0}", id));
            return View(model);
        }

        public ActionResult Enter(int id)
        {
            var model = new HuntModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Enter(int id, HuntModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.HuntAccepted = HuntUtil.HuntIsValid(model.Id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //Show the success view
            return View(model);
        }

        [HttpPost]
        public FileResult Download(string fileName)
        {
            return new FilePathResult("C:\\temp\\downloads\\" + fileName, "application/octet-stream");
        }
    }
}