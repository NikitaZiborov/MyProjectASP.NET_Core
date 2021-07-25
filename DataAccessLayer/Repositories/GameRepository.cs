using System.Collections.Generic;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private Context DB;

        public GameRepository(Context context)
        {
            DB = context;
        }

        public IEnumerable<Game> ReadAll()
        {
            return DB.Games;
        }
        public Game Read(int id)
        {
            return DB.Games.Find(id);
        }
        public void Create(Game game)
        {
            DB.Games.Add(game);
        }
        public void Update(Game game)
        {
            DB.Games.Update(game);
        }
        public void Delete(int id)
        {
            Game game = DB.Games.Find(id);
            if (game != null)
                DB.Games.Remove(game);
        }
    }
}
