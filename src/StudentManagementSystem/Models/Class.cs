namespace StudentManagementSystem.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public int TeacherId { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Staff> Staff { get; set; }
        
    }
}