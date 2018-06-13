namespace MentorOnlineV3.Models.Entities
{
    public class Mentor
    {
        public int MentorId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Subjects { get; set; }
        public int Role { get; set; }
        public int Online { get; set; }
        public string Imgurl { get; set; }

    }

}