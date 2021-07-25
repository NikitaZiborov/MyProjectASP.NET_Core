using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.EF;
using DataAccessLayer.Repositories;

namespace DataAccessLayer
{
    public class UnitOfWork: IUnitOfWork
    {
        private Context DataBase { get; }
        private PlayerRepository playersRepository;
        private GameRepository gamesRepository;
        private StadiumRepository stadiumRepository;

        public UnitOfWork()
        {
            DataBase = new Context();
        }

        public IRepository<Player> Players
        {
            get
            {
                if (playersRepository == null)
                    playersRepository = new PlayerRepository(DataBase);
                return playersRepository;
            }
        }
        public IRepository<Game> Games
        {
            get
            {
                if (gamesRepository == null)
                    gamesRepository = new GameRepository(DataBase);
                return gamesRepository;
            }
        }
        public IRepository<Stadium> Stadiums
        {
            get
            {
                if (stadiumRepository == null)
                    stadiumRepository = new StadiumRepository(DataBase);
                return stadiumRepository;
            }
        }
        public void Save()
        {
            DataBase.SaveChanges();
        }

        public void Dispose()
        {
            DataBase.Dispose();
        }
    }
}
