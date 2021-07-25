using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using BusinessLogic.Entities;
using CourseWork.Models;
using AutoMapper;

namespace CourseWork.Controllers
{
    public class StadiumsController : Controller
    {
        public static int currentEditionID;

        public IActionResult AddPage()
        {
            return View();
        }
        public IActionResult AddStadium(int places, int costs)
        {
            StadiumModel stadium = new StadiumModel()
            {
                Places = places,
                Cost = costs
            };

            using (var db = new BL())
                db.AddStadium(Mapper.Map<StadiumBL>(stadium));

            return Redirect("~/Home/Index");
        }

        public IActionResult SearchPage()
        {
            return View();
        }
        public IActionResult SearchResult(int places)
        {
            List<StadiumModel> foundStadiums = new List<StadiumModel>();

            using (var db = new BL())
            {
                foreach (StadiumModel stadium in Mapper.Map<List<StadiumModel>>(db.GetStadiums()))
                {
                    if (stadium.Places >= places)
                        foundStadiums.Add(stadium);
                }
            }

            return View("ShowAllPage", foundStadiums);
        }

        public IActionResult ShowAllPage()
        {
            List<StadiumModel> list = null;

            using (var db = new BL())
                list = Mapper.Map<List<StadiumModel>>(db.GetStadiums());

            return View(list);
        }
        public IActionResult Delete(int id)
        {
            using (var db = new BL())
                db.RemoveStadium(id);

            return Redirect("~/Home/Index");
        }
        public IActionResult DeleteAllStadiums()
        {
            using (var db = new BL())
            {
                foreach (StadiumModel stadium in Mapper.Map<List<StadiumModel>>(db.GetStadiums()))
                    db.RemoveStadium(stadium.Id);
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult EditPage(int id)
        {
            StadiumModel stadium = null;

            using (var db = new BL())
            {
                var stadiums = Mapper.Map<List<StadiumModel>>(db.GetStadiums());
                foreach (StadiumModel model in stadiums)
                {
                    if (model.Id == id)
                    {
                        stadium = model;
                        break;
                    }
                }
            }

            if (stadium != null)
            {
                currentEditionID = stadium.Id;
                ViewData["StadiumPlaces"] = stadium.Places;
                ViewData["StadiumCost"] = stadium.Cost;
            }

            return View();
        }
        public IActionResult SaveChanges(int places, int costs)
        {
            StadiumModel stadium = new StadiumModel()
            {
                Id = currentEditionID,
                Places = places,
                Cost = costs
            };

            using (var db = new BL())
                db.UpdateStadium(Mapper.Map<StadiumBL>(stadium));

            return Redirect("~/Home/Index");
        }
    }
}
