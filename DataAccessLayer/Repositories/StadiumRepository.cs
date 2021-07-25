using System.Collections.Generic;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.EF;

namespace DataAccessLayer.Repositories
{
    public class StadiumRepository : IRepository<Stadium>
    {
        private Context DB;

        public StadiumRepository(Context context)
        {
            DB = context;
        }

        public IEnumerable<Stadium> ReadAll()
        {
            return DB.Stadiums;
        }
        public Stadium Read(int id)
        {
            return DB.Stadiums.Find(id);
        }
        public void Create(Stadium stadium)
        {
            DB.Stadiums.Add(stadium);
        }
        public void Update(Stadium stadium)
        {
            var previousStadium = DB.Stadiums.Find(stadium.Id);

            if (previousStadium != null)
            {
                DB.Stadiums.Remove(previousStadium);
                Stadium newStadium = new Stadium()
                {
                    Places = stadium.Places,
                    Cost = stadium.Cost
                };

                DB.Stadiums.Add(newStadium);
            }
        }
        public void Delete(int id)
        {
            Stadium stadium = DB.Stadiums.Find(id);
            if (stadium != null)
                DB.Stadiums.Remove(stadium);
        }
    }
}
