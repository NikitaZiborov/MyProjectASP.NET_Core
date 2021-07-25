using System.Collections.Generic;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private Context DB;

        public PlayerRepository(Context context)
        {
            DB = context;
        }

        public IEnumerable<Player> ReadAll()
        {
            return DB.Players;
        }
        public Player Read(int id)
        {
            return DB.Players.Find(id);
        }
        public void Create(Player player)
        {
            DB.Players.Add(player);
        }
        public void Update(Player player)
        {
            var previousPlayer = DB.Players.Find(player.Id);

            if (previousPlayer != null)
            {
                DB.Players.Remove(previousPlayer);
                Player newPlayer = new Player()
                {
                    Name = player.Name,
                    Surname = player.Surname,
                    IsHealthy = player.IsHealthy,
                    IsInGame = player.IsInGame,
                    Salary = player.Salary,
                    Date = player.Date
                };

                DB.Players.Add(newPlayer);
            }
        }
        public void Delete(int id)
        {
            Player player = DB.Players.Find(id);
            if (player != null)
                DB.Players.Remove(player);
        }
    }
}
