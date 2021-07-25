using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using BusinessLogic.Entities;
using CourseWork.Models;
using AutoMapper;
using CourseWork.ViewModels;

namespace CourseWork.Controllers
{
    public class GamesController : Controller
    {
        public static int currentEditionID;

        public IActionResult AddPage()
        {
            using (var db = new BL())
            {
                GameViewModel model = new GameViewModel()
                {
                    Players = Mapper.Map<List<PlayerModel>>(db.GetPlayers()),
                    Stadiums = Mapper.Map<List<StadiumModel>>(db.GetStadiums())
                };

                return View(model);
            }
        }
        public IActionResult AddGame(string date, int spectators, int place, int[] players, string result)
        {
            List<PlayerModel> chosenPlayers = new List<PlayerModel>();
            StadiumModel chosenPlace = null;
            Models.Result chosenResult = 0;

            using (var db = new BL())
            {
                List<PlayerModel> listPlayers = Mapper.Map<List<PlayerModel>>(db.GetPlayers());

                for (int i = 0; i < players.Length; i++)
                {
                    foreach (PlayerModel playerModel in listPlayers)
                    {
                        if (playerModel.Id == players[i])
                        {
                            chosenPlayers.Add(playerModel);
                            break;
                        }
                    }
                }

                foreach (StadiumModel model in Mapper.Map<List<StadiumModel>>(db.GetStadiums()))
                {
                    if (model.Id == place)
                    {
                        chosenPlace = model;
                        break;
                    }
                }

                switch (result)
                {
                    case "Won":
                        chosenResult = Models.Result.Won;
                        break;

                    case "Failed":
                        chosenResult = Models.Result.Failed;
                        break;

                    case "Noone":
                        chosenResult = Models.Result.Noone;
                        break;

                    case "NotPlayed":
                        chosenResult = Models.Result.NotPlayed;
                        break;
                }

                foreach (PlayerModel player in chosenPlayers)
                {
                    player.IsInGame = true;
                    db.UpdatePlayer(Mapper.Map<PlayerBL>(player));
                };

                GameModel game = new GameModel()
                {
                    Players = chosenPlayers,
                    Spectators = spectators,
                    Date = date,
                    Place = chosenPlace,
                    GameResult = chosenResult,
                };

                db.AddGame(Mapper.Map<GameBL>(game));
            }

            return Redirect("~/Home/Index");
        }

        public IActionResult ShowAllPage()
        {
            List<GameModel> list = null;

            using (var db = new BL())
                list = Mapper.Map<List<GameModel>>(db.GetGames());

            return View(list);
        }
    }
}
