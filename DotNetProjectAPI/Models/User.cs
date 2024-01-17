namespace DotNetProjectAPI.Models
{
    public class User
    {
        public int id { get; set; }
        public required string name { get; set; }
        public required string firstname { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public string salt { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool is_enabled { get; set; }
        public int? type_id { get; set; }
    }
}
