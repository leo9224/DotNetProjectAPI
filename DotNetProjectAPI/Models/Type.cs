namespace DotNetProjectAPI.Models
{
    public class Type
    {
        public int id { get; set; }
        public required string class_name {  get; set; }
        public required string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public bool is_enabled { get; set; }
    }
}
