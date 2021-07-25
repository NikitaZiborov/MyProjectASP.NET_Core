using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using BusinessLogic.Entities;
using CourseWork.Models;
using AutoMapper;

namespace CourseWork.Controllers
{
    public class PlayersController : Controller
    {
        public static int currentEditionID;

        public IActionResult AddPage()
        {
            return View();
        }
        public IActionResult AddPlayer(string name, string surname, string year, bool health, int salary)
        {
            PlayerModel player = new PlayerModel()
            {
                Name = name,
                Surname = surname,
                Date = year,
                IsInGame = false,
                IsHealthy = health,
                Salary = salary
            };

            using (var db = new BL())
                db.AddPlayer(Mapper.Map<PlayerBL>(player));

            return Redirect("~/Home/Index");
        }

        public IActionResult SearchPage()
        {
            return View();
        }
        public IActionResult SearchResult(string name, string surname)
        {
            List<PlayerModel> foundPlayers = new List<PlayerModel>();

            using (var db = new BL())
            {
                foreach (PlayerModel player in Mapper.Map<List<PlayerModel>>(db.GetPlayers()))
                {
                    if (player.Name == name && player.Surname == surname)
                        foundPlayers.Add(player);
                }
            }

            return View("ShowAllPage", foundPlayers);
        }

        public IActionResult ShowAllPage()
        {
            List<PlayerModel> list = null;

            using (var db = new BL())
                list = Mapper.Map<List<PlayerModel>>(db.GetPlayers());

            return View(list);
        }
        public IActionResult Delete(int id)
        {
            using (var db = new BL())
                db.RemovePlayer(id);

            return Redirect("~/Home/Index");
        }
        public IActionResult DeleteAllPlayers()
        {
            using (var db = new BL())
            {
                foreach (PlayerModel player in Mapper.Map<List<PlayerModel>>(db.GetPlayers()))
                    db.RemovePlayer(player.Id);
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult EditPage(int id)
        {
            PlayerModel player = null;

            using (var db = new BL())
            {
                var players = Mapper.Map<List<PlayerModel>>(db.GetPlayers());
                foreach (PlayerModel model in players)
                {
                    if (model.Id == id)
                    {
                        player = model;
                        break;
                    }
                }
            }

            if (player != null)
            {
                currentEditionID = player.Id;
                ViewData["PlayerName"] = player.Name;
                ViewData["PlayerSurname"] = player.Surname;
                ViewData["PlayerYear"] = player.Date;
                ViewData["PlayerHealth"] = player.IsHealthy;
                ViewData["PlayerSalary"] = player.Salary;
            }

            return View();
        }
        public IActionResult SaveChanges(int id, string name, string surname, string year, bool health, int salary)
        {
            PlayerModel player = new PlayerModel()
            {
                Id = currentEditionID,
                Name = name,
                Surname = surname,
                Date = year,
                IsHealthy = health,
                Salary = salary
            };

            using (var db = new BL())
                db.UpdatePlayer(Mapper.Map<PlayerBL>(player));

            return Redirect("~/Home/Index");
        }
    }
}
