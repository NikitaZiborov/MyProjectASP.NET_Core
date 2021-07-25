namespace DataAccessLayer.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Date { get; set; }
        public bool IsInGame { get; set; }
        public bool IsHealthy { get; set; }
        public int Salary { get; set; }
    }
}
