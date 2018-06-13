using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MentorOnlineV3.Models;
using MentorOnlineV3.Models.Entities;


namespace MentorOnlineV3.Controllers
{
    public class HomeController : Controller
    {
        // Temp solution instead of real login
        // VIGTIGT: ID 2 må ikke slettes for mentor eller student, eller virker login ikke
        // Normalt må man ikke bruge statiske globale variabler på den her måde
        public static int currentUserID = 3;
 
        MentorDbContext db = new MentorDbContext();


        public IActionResult ChangeUser(int number) {
           currentUserID = number;
           return View("~/Views/Home/Index.cshtml");
        }

         [HttpGet]
         public IActionResult ChangeOn() {
           
           // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Mentor ment2 = mentorDbContext.Mentors.Single(ment => ment.MentorId == currentUserID);

           ViewBag.UserName = ment2.Name;
           ViewBag.number = currentUserID;

          
          
           ViewBag.ment = db.Mentors.ToList();
           Mentor ment3 = db.Mentors 
                    .Where(b => b.MentorId == currentUserID) 
                    .FirstOrDefault();
                    if(ment3 != null){
                        ment3.Online = 1;
                        db.SaveChanges();
                    }    

           return View("~/Views/Home/Mentor.cshtml");
         }

        [HttpGet]
        public IActionResult ChangeOff() {
             // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Mentor ment2 = mentorDbContext.Mentors.Single(ment => ment.MentorId == currentUserID);

           ViewBag.UserName = ment2.Name;
           ViewBag.number = currentUserID;

         

            ViewBag.ment = db.Mentors.ToList();
            Mentor ment3 = db.Mentors 
                    .Where(b => b.MentorId == currentUserID) 
                    .FirstOrDefault();
                    if(ment3 != null){
                        ment3.Online = 0;
                        db.SaveChanges();
                    }    

           return View("~/Views/Home/Mentor.cshtml");
         }

         public IActionResult ChangeSubscribedOn() {
              // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Student stud2 = mentorDbContext.Students.Single(stud => stud.StudentId == currentUserID);

           ViewBag.UserName = stud2.Name;
           ViewBag.number = currentUserID;
          
           ViewBag.stud = db.Students.ToList();
           ViewBag.ment = db.Mentors.ToList();
           Student stud3 = db.Students
                    .Where(b => b.StudentId == currentUserID) 
                    .FirstOrDefault();
                    if(stud3 != null){
                        stud3.Subscribed = 1;
                        db.SaveChanges();
                    }    

           return View("~/Views/Home/StudentPage.cshtml");
         }

         public IActionResult ChangeSubscribedOff() {
              // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Student stud2 = mentorDbContext.Students.Single(stud => stud.StudentId == currentUserID);

           ViewBag.UserName = stud2.Name;
           ViewBag.number = currentUserID;
          
           ViewBag.stud = db.Students.ToList();
           ViewBag.ment = db.Mentors.ToList();
           Student stud3 = db.Students
                    .Where(b => b.StudentId == currentUserID) 
                    .FirstOrDefault();
                    if(stud3 != null){
                        stud3.Subscribed = 0;
                        db.SaveChanges();
                    }    

           return View("~/Views/Home/StudentPage.cshtml");
         }
    
    

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

        // Create mentor
        [HttpGet]
        public IActionResult CreateMentor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMentor(Mentor ment)
        {
            db.Mentors.Add(ment);
            db.SaveChanges();
            return View();
        }

        //Create student
         [HttpGet]
        public IActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateStudent(Student stud)
        {
            db.Students.Add(stud);
            db.SaveChanges();
            return View();
        }
        
        // search metode
        // søg på - id
        // få udskrevet alt info on den ene person
        public IActionResult SearchMentor(int id = 1)
        {
            ViewBag.number = id;

            ViewBag.ment = db.Mentors.ToList();

            return View();
        }

         public IActionResult SearchStudent(int id = 1)
        {
            ViewBag.number = id;

            ViewBag.stud = db.Students.ToList();

            return View();
        }

    //Update Mentor

     [HttpGet]
        public IActionResult UpdateMentor(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateMentor(int id, string name, string password, string email, string subjects, int role, int online, string imgurl)
        {
            Mentor ment = new Mentor()
            {
                MentorId = id,
                Name = name,
                Password = password,
                Email = email,
                Subjects = subjects,
                Role = role,
                Online = online,
                Imgurl = imgurl
            };
            db.Update(ment);
            db.SaveChanges();
            return View();
        }

    //Update Student

    [HttpGet]
        public IActionResult UpdateStudent(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateStudent(int id, string name, string password, string email, int subscribed, int role, int paymentinfo)
        {
            Student stud = new Student()
            {
                StudentId = id,
                Name = name,
                Password = password,
                Email = email,
                Subscribed = subscribed,
                Role = role,
                Paymentinfo = paymentinfo,
            };
            db.Update(stud);
            db.SaveChanges();
            return View();
        }

        
    [HttpGet]
        public IActionResult UpdateMessage(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdateMessage(int id, string text, string sender, string receiver, string topic)
        {
            Message mess = new Message()
            {
                MessageId = id,
                Text = text,
                Sender = sender,
                Receiver = receiver,
                Topic = topic,
            };
            db.Update(mess);
            db.SaveChanges();
            return View();
        }


        //DeleteMentor

         [HttpGet]
        public IActionResult DeleteMentor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteMentor(int id)
        {
            Mentor ment = new Mentor() { MentorId = id };
            db.Mentors.Remove(ment);
            db.SaveChanges();
            return View();
        }

          [HttpGet]
        public IActionResult DeleteStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            Student stud = new Student() { StudentId = id };
            db.Students.Remove(stud);
            db.SaveChanges();
            return View();
        }

        Dictionary<string, string> tempSubjectToTag = new Dictionary<string, string>{{"læsning","Dansk"}};
       
        public string CheckDictonary(string tag) {
            // Iterate through dictionary
            foreach(KeyValuePair<string, string> entry in tempSubjectToTag)
            {
                  // If value matches key, return. (This means that only mentor can be shown - which is not good)
                 // do something with entry.Value or entry.Key
                 if (tag.ToLower().Contains(entry.Key)) {
                   
                     return entry.Value;
                     
                 }
            }   


            return "no";
           
        }

         [HttpGet]
        public IActionResult StudentPage(string id)

        {
           // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Student stud2 = mentorDbContext.Students.Single(stud => stud.StudentId == currentUserID);

           ViewBag.UserName = stud2.Name;
           ViewBag.number = currentUserID;
           ViewBag.stud = db.Students.ToList();
            
            // New list to temporary store variables when iterating through query
            List<Mentor> mentorList = new List<Mentor>();
            
           // If nothing is typed yet in the search bar just display all mentors
           if (id == null) {
                ViewBag.ment = db.Mentors.ToList();
            
           }
           // When a subject is typed in searchbar
           else { 
               // Assuming that id is a subject
               // Wierd IQueryable variable where the mentors subject equals the subject passed as param
              // var mentor = db.Mentors.Where(ment2 => id.ToLower().Contains(ment2.Subjects.ToLower())); 
       
             var mentor = db.Mentors.Where(ment2 => id.ToLower().Contains(ment2.Subjects.ToLower()));  
               // Iteratate through the query
               foreach(var row in mentor) {
                 // Make a new object to add to the list
                 Mentor mentor2 = new Mentor(){
                     MentorId = row.MentorId,
                     Name = row.Name,
                     Password = row.Password,
                     Email = row.Email,
                     Subjects = row.Subjects,
                     Role = row.Role,
                     Online = row.Online,
                     Imgurl = row.Imgurl
                 };
                 mentorList.Add(mentor2);
               }

            // ------- Compare with tag -----
            var mentorTag = db.Mentors.Where(ment3 => ment3.Subjects.ToLower().Equals(CheckDictonary(id).ToLower()));
            Console.Write(id);
            Console.Write(CheckDictonary(id).ToLower());

            // Iteratate through the query
               foreach(var row in mentorTag) {
                 // Make a new object to add to the list
                 Mentor mentor3 = new Mentor(){
                     MentorId = row.MentorId,
                     Name = row.Name,
                     Password = row.Password,
                     Email = row.Email,
                     Subjects = row.Subjects,
                     Role = row.Role,
                     Online = row.Online,
                     Imgurl = row.Imgurl
                 };
                 mentorList.Add(mentor3);
               }

             // Set the viewbag variable to the temp list we just created
             ViewBag.ment = mentorList;    
           }        
            return View();
        }

       

         [HttpGet]
        public IActionResult SendMailMentor()
        {
            // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Mentor ment2 = mentorDbContext.Mentors.Single(ment => ment.MentorId == currentUserID);

           ViewBag.UserName = ment2.Name;

            return View();
        }
        [HttpPost]
        
        public IActionResult SendMailMentor(Message mess)
        {
            db.Messages.Add(mess);
            db.SaveChanges();
            return View();
        }



           [HttpGet]
        public IActionResult SendMailStudent()
        {
             // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Student stud2 = mentorDbContext.Students.Single(stud => stud.StudentId == currentUserID);

           ViewBag.UserName = stud2.Name;
            return View();
        }
        [HttpPost]
        public IActionResult SendMailStudent(Message mess)
        {
            db.Messages.Add(mess);
            db.SaveChanges();
            return View();
        }

        
        public IActionResult GetMailMentor(String id)
        {
           List<Message> mess = new List<Message>();

           // Make an instance to get info about this mentor (use ment2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Mentor ment2 = mentorDbContext.Mentors.Single(ment => ment.MentorId == currentUserID);

           // Another wierd Query 
           var message = db.Messages.Where(mess2 => mess2.Receiver.ToLower() == ment2.Name.ToLower()); 
         
           // Iteratate through the query
           foreach(var row in message) {
              // Make a new object to add to the list
              Message message2 = new Message(){
                MessageId = row.MessageId,
                Receiver = row.Receiver,
                Sender = row.Sender,
                Text = row.Text,
                Topic = row.Topic  
              };
              mess.Add(message2);
              }
             // Set the viewbag variable to the temp list we just created
             ViewBag.messagesPrint = mess;
    
             return View();
        }

         [HttpGet]
        public IActionResult GetMailStudent(String id)
        {
            
           List<Message> mess = new List<Message>();

           // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Student stud2 = mentorDbContext.Students.Single(stud => stud.StudentId == currentUserID);

           // Another wierd Query 
           var message = db.Messages.Where(mess2 => mess2.Receiver.ToLower() == stud2.Name.ToLower()); 
         
           // Iteratate through the query
           foreach(var row in message) {
              // Make a new object to add to the list
              Message message2 = new Message(){
                MessageId = row.MessageId,
                Receiver = row.Receiver,
                Sender = row.Sender,
                Text = row.Text,
                Topic = row.Topic  
              };
              mess.Add(message2);
              }
             // Set the viewbag variable to the temp list we just created
             ViewBag.messagesPrint = mess;
    
             return View();
        } 

       
         public IActionResult GetMailMentors(String id)
        {
            List<Message> mess = new List<Message>();

           // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Mentor ment2 = mentorDbContext.Mentors.Single(ment => ment.MentorId == currentUserID);

           // Another wierd Query 
           var message = db.Messages.Where(mess2 => mess2.Receiver.ToLower() == ment2.Name.ToLower()); 
         
           // Iteratate through the query
           foreach(var row in message) {
              // Make a new object to add to the list
              Message message2 = new Message(){
                MessageId = row.MessageId,
                Receiver = row.Receiver,
                Sender = row.Sender,
                Text = row.Text,
                Topic = row.Topic  
              };
              mess.Add(message2);
              }
             // Set the viewbag variable to the temp list we just created
             ViewBag.messagesPrint = mess;
    
             return View();
        }
        
         [HttpGet]
        public IActionResult Mentor(int id)
        {
            // Make an instance to get info about this student (use stud2 for reference)
           MentorDbContext mentorDbContext = new MentorDbContext();
           Mentor ment2 = mentorDbContext.Mentors.Single(ment => ment.MentorId == currentUserID);

           ViewBag.UserName = ment2.Name;
           ViewBag.number = currentUserID;
           ViewBag.ment = db.Mentors.ToList();
          // ViewBag.online = "";

       
           

           return View();
        }

        [HttpGet]
        public IActionResult Online()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Offline()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Online(int id)
        {
            Mentor ment = db.Mentors 
                    .Where(b => b.MentorId == currentUserID) 
                    .FirstOrDefault();
                    if(ment != null){
                        ment.Online = 1;
                        db.SaveChanges();
                    }    
            return View();
        }
        
        [HttpPost]
        public IActionResult Offline(int id)
        {
            Mentor ment = db.Mentors 
                    .Where(b => b.MentorId == currentUserID) 
                    .FirstOrDefault();
                    if(ment != null){
                        ment.Online = 0;
                        db.SaveChanges();
                    }    
            return View();
        }

        [HttpGet]
        public IActionResult Student()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unsubscribe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Subscribe(int id)
        {
            Student stud = db.Students 
                    .Where(b => b.StudentId == currentUserID) 
                    .FirstOrDefault();
                    if(stud != null)
                    {
                        stud.Subscribed = 1;
                        db.SaveChanges();
                    }    
            return View();
        }

        [HttpPost]
        public IActionResult Unsubscribe(int id)
        {
            Student stud = db.Students 
                    .Where(b => b.StudentId == currentUserID) 
                    .FirstOrDefault();
                    if(stud != null)
                    {
                        stud.Subscribed = 0;
                        db.SaveChanges();
                    }    
            return View();
        }

    }
}
