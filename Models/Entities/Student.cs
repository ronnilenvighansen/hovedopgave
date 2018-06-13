namespace MentorOnlineV3.Models.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Subscribed { get; set; }
        public int Role { get; set; }
        public int Paymentinfo { get; set; }

    }

}