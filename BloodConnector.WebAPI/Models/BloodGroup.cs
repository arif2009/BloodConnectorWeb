namespace BloodConnector.WebAPI.Models
{
    public class BloodGroup
    {
        public int ID { get; set; }
        public string Symbole { get; set; }
        public User User { get; set; }
    }
}