namespace DotNetProjectAPI.Models
{
    public class Computer
    {
        public int id { get; set; }
        public required string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool is_enabled { get; set; }
        public int? type_id { get; set; }
        public int? room_id { get; set; }
    }
}
