namespace StudentManagementSystem.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string Subject { get; set; }
    }
    
}