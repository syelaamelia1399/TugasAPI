using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Employee
    {
        public Employee(int Id, string FullName, string Email, DateTime BirthDate)
        {
            this.Id = Id;
            this.FullName = FullName;
            this.Email = Email;
            this.BirthDate = BirthDate;
        }
        public Employee()
        {

        }

        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
