using System.Collections.Generic;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public class GameViewModel
    {
        public IEnumerable<PlayerModel> Players { get; set; }
        public IEnumerable<StadiumModel> Stadiums { get; set; }
    }
}
