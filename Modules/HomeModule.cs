using Nancy;
using System.Collections.Generic;
using System.Linq;
using Kickstart;

namespace Kickstart
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      // Routes for Landing Page Navbar
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/courses"] = _ =>
      {
        return View["courses.cshtml"];
      };
      Get["/instructors"] = _ =>
      {
        return View["instructors.cshtml"];
      };
      Get["/schools"] = _ =>
      {
        List<School> allSchools = School.GetAll();
        return View["schools.cshtml", allSchools];
      };
      Get["/account/create"] = _ =>
      {
        return View["new_account.cshtml"];
      };
      Get["/account/login"] = _ =>
      {
        return View["login.cshtml"];
      };
      Get["/student/details/{id}"] = parameters =>{
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Student foundStudent = Student.Find(paramters.id);
        List<Track> tracks = foundStudent.GetTracks();
        List<Course> courses = foundStudent.GetCourses();
        model.Add("student", foundStudent);
        model.Add("tracks", tracks);
        model.Add("courses", courses);
        return View["student_detail.cshtml", model];
      };

      // Routes for School Locations Page
      Get["/schools/add"] = _ =>
      {
        return View["add_school.cshtml"];
      };
      Post["/schools/add"] = _ =>
      {
        School newSchool = new School(Request.Form["school-city"], Request.Form["school-address"], Request.Form["school-phone"]);
        newSchool.Save();
        return View["add_school.cshtml", newSchool];
      };
      // Get["/schools/{id}"] = parameters =>
      // {
      //   Dictionary<string, object> myDict = new Dictionary<string, object>{};
      //   School currentSchool = School.Find(parameters.id);
      //   List<Track> schoolTracks = currentSchool.GetTracks();
      //   List<Track> allTracks = Track.GetAll();
      //
      //   //TODO: Create list of tracks which are not already offered at this particular school, and pass into dictionary. Code was started in the two lines below, but is far from finished.
      //   // IEnumerable<Track> availableTracks = allTracks.Where(t => schoolTracks.Contains(t.GetName()) != true);
      //   // availableTracks.ToList();
      //
      //   // Instructor testInstructor = new Instructor("Robert'); DROP TABLE students;--", "bobbytables", "iminursqldb", "string address", "string email");
      //   // testInstructor.Save();
      //   // currentSchool.AddInstructor(testInstructor);
      //
      //   // Track newTrack = new Track("Watermelon Demolition");
      //   // newTrack.Save();
      //   // currentSchool.AddTrack(newTrack);
      //
      //   myDict.Add("school", currentSchool);
      //   myDict.Add("tracks", schoolTracks);
      //   myDict.Add("availtracks", availableTracks);
      //   myDict.Add("instructors", currentSchool.GetInstructors());
      //   return View["school_details.cshtml", myDict];
      // };


      // Routes for login
      Post["/account/login"] = _ =>
      {
        string username = Request.Form["user-name"];
        string pwd = Request.Form["user-password"];
        Student foundStudent = Student.FindByLogin(username, pwd);
        return View["index.cshtml", foundStudent];
      };

      // Routes for Account Creation
      Post["/account/create"] = _ =>
      {
        string firstName = Request.Form["first-name"];
        string lastName = Request.Form["last-name"];
        string userName = Request.Form["user-name"];
        string password = Request.Form["password"];
        string address = Request.Form["address"];
        string email = Request.Form["email"];
        Student newStudent = new Student(firstName, lastName, userName, password, address, email);
        newStudent.Save();
        return View["index.cshtml"];
      };
    }
  }
}
