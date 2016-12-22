using Nancy;
using Nancy.Cookies;
using System.Collections.Generic;
using System.Linq;
using Kickstart;
using System;

namespace Kickstart
{

  public class HomeModule : NancyModule
  {
    public HomeModule()
    { //Route for initial splash page
      Get["/"] = _ =>{
        return View["index.cshtml"];
      };
      // Routes for Landing Page Navbar
      Get["/main"] = _ => {
        return View["main.cshtml"];
      };
      Get["/tracks"] = _ =>
      {
        List<Track> allTracks = Track.GetAll();
        return View["tracks.cshtml", allTracks];
      };
      Get["/instructors"] = _ =>
      {
        List<Instructor> allInstructors = Instructor.GetAll();
        return View["instructors.cshtml", allInstructors];
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
      //Student Details routing
      Get["/student/details/{id}"] = parameters =>{
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Student foundStudent = Student.Find(parameters.id);
        Track newTrack = foundStudent.GetTrack();
        List<Track> allTracks = Track.GetAll();
        List<Course> courses = foundStudent.GetCourses();
        List<Grade> grades = new List<Grade> {};
        foreach(Course course in courses)
        {
          Grade newGrade = foundStudent.GetGrades(course.GetId());
          grades.Add(newGrade);
        }
        model.Add("student", foundStudent);
        model.Add("track", newTrack);
        model.Add("courses", courses);
        model.Add("grades", grades);
        model.Add("availtracks", allTracks);
        return View["student_details.cshtml", model];
      };

      Post["/student/add-track"] =_=>{
        Student foundStudent = Student.Find(Request.Form["student-id"]);
        foundStudent.DeleteTrackFromStudent();
        Track selectedTrack = Track.Find(Request.Form["track-id"]);
        foundStudent.AddTrack(selectedTrack);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Track> allTracks = Track.GetAll();
        List<Course> courses = foundStudent.GetCourses();
        List<Grade> grades = new List<Grade> {};
        foreach(Course course in courses)
        {
          Grade newGrade = foundStudent.GetGrades(course.GetId());
          grades.Add(newGrade);
        }
        model.Add("student", foundStudent);
        model.Add("track", selectedTrack);
        model.Add("courses", courses);
        model.Add("grades", grades);
        model.Add("availtracks", allTracks);
        return View["student_details.cshtml", model];
      };

      Post["/student/update-grade"] = _ =>{
        Student foundStudent = Student.Find(Request.Form["student-id"]);
        int studentId =(Request.Form["student-id"]);
        int courseId = (Request.Form["course-id"]);
        Grade.Update(Request.Form["attendance"], Request.Form["grade"], studentId, courseId);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        List<Track> allTracks = Track.GetAll();
        Track newTrack = foundStudent.GetTrack();
        List<Course> courses = foundStudent.GetCourses();
        List<Grade> grades = new List<Grade> {};
        foreach(Course course in courses)
        {
          Grade newGrade = foundStudent.GetGrades(course.GetId());
          grades.Add(newGrade);
        }
        model.Add("student", foundStudent);
        model.Add("track", newTrack);
        model.Add("courses", courses);
        model.Add("grades", grades);
        model.Add("availtracks", allTracks);
        return View["student_details.cshtml", model];
      };
      Get["/student/tracks/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Track selectedTrack = Track.Find(parameters.id);
        List<Student> allStudents = selectedTrack.GetStudents();
        model.Add("allStudents", allStudents);
        model.Add("selectedTrack", selectedTrack);
        return View["all_students.cshtml", model];
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
      Get["/schools/{id}"] = parameters =>
      {
        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        School currentSchool = School.Find(parameters.id);
        List<Track> schoolTracks = currentSchool.GetTracks();
        List<Track> allTracks = Track.GetAll();
        List<Track> displayTracks = new List<Track>{};
        List<Instructor> schoolInstructors = currentSchool.GetInstructors();
        List<Instructor> allInstructors = Instructor.GetAll();
        List<Instructor> displayInstructors = new List<Instructor>{};

        for(int i =0; i < allTracks.Count; i++)
        {
          if (schoolTracks.Contains(allTracks[i]) == false)
          {
            displayTracks.Add(allTracks[i]);
          }
        }

        for(int i =0; i < allInstructors.Count; i++)
        {
          if (schoolInstructors.Contains(allInstructors[i]) == false)
          {
            displayInstructors.Add(allInstructors[i]);
          }
        }

        myDict.Add("school", currentSchool);
        myDict.Add("tracks", schoolTracks);
        myDict.Add("availtracks", displayTracks);
        myDict.Add("currentinstructors", schoolInstructors);
        myDict.Add("availinstructors", displayInstructors);
        return View["school_details.cshtml", myDict];
      };
      Post["/tracks/addto"] = _ =>
      {
        int schoolId = Request.Form["school-id"];
        int trackId = Request.Form["track-id"];
        School currentSchool = School.Find(schoolId);
        Track selectedTrack = Track.Find(trackId);
        currentSchool.AddTrack(selectedTrack);

        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        List<Track> schoolTracks = currentSchool.GetTracks();
        List<Track> allTracks = Track.GetAll();
        List<Track> displayTracks = new List<Track>{};
        List<Instructor> schoolInstructors = currentSchool.GetInstructors();
        List<Instructor> allInstructors = Instructor.GetAll();
        List<Instructor> displayInstructors = new List<Instructor>{};

        for(int i =0; i < allTracks.Count; i++)
        {
          if (schoolTracks.Contains(allTracks[i]) == false)
          {
            displayTracks.Add(allTracks[i]);
          }
        }

        for(int i =0; i < allInstructors.Count; i++)
        {
          if (schoolInstructors.Contains(allInstructors[i]) == false)
          {
            displayInstructors.Add(allInstructors[i]);
          }
        }

        myDict.Add("school", currentSchool);
        myDict.Add("tracks", schoolTracks);
        myDict.Add("availtracks", displayTracks);
        myDict.Add("currentinstructors", schoolInstructors);
        myDict.Add("availinstructors", displayInstructors);
        return View["school_details.cshtml", myDict];
      };
      Post["/track/delete/{id}"] = parameters =>
      {
        int schoolId = Request.Form["school-id"];
        School currentSchool = School.Find(schoolId);
        currentSchool.RemoveATrack(parameters.id);

        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        List<Track> schoolTracks = currentSchool.GetTracks();
        List<Track> allTracks = Track.GetAll();
        List<Track> displayTracks = new List<Track>{};
        List<Instructor> schoolInstructors = currentSchool.GetInstructors();
        List<Instructor> allInstructors = Instructor.GetAll();
        List<Instructor> displayInstructors = new List<Instructor>{};

        for(int i =0; i < allTracks.Count; i++)
        {
          if (schoolTracks.Contains(allTracks[i]) == false)
          {
            displayTracks.Add(allTracks[i]);
          }
        }

        for(int i =0; i < allInstructors.Count; i++)
        {
          if (schoolInstructors.Contains(allInstructors[i]) == false)
          {
            displayInstructors.Add(allInstructors[i]);
          }
        }

        myDict.Add("school", currentSchool);
        myDict.Add("tracks", schoolTracks);
        myDict.Add("availtracks", displayTracks);
        myDict.Add("currentinstructors", schoolInstructors);
        myDict.Add("availinstructors", displayInstructors);
        return View["school_details.cshtml", myDict];
      };
      Get["/track/edit/{id}"] = parameters => {
        return View["track_edit.cshtml"];
      };
      Post["/track/edit/{id}"] = parameters => {
        Track selectedTrack = Track.Find(parameters.id);
        return View["track_edit.cshtml", selectedTrack];
      };
      Post["/instructors/addto"] = _ =>
      {
        int schoolId = Request.Form["school-id"];
        int instructorId = Request.Form["instructor-id"];
        School currentSchool = School.Find(schoolId);
        Instructor selectedInstructor = Instructor.Find(instructorId);
        currentSchool.AddInstructor(selectedInstructor);

        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        List<Instructor> schoolInstructors = currentSchool.GetInstructors();
        List<Instructor> allInstructors = Instructor.GetAll();
        List<Instructor> displayInstructors = new List<Instructor>{};
        List<Track> schoolTracks = currentSchool.GetTracks();
        List<Track> allTracks = Track.GetAll();
        List<Track> displayTracks = new List<Track>{};

        for(int i =0; i < allTracks.Count; i++)
        {
          if (schoolTracks.Contains(allTracks[i]) == false)
          {
            displayTracks.Add(allTracks[i]);
          }
        }

        for(int i =0; i < allInstructors.Count; i++)
        {
          if (schoolInstructors.Contains(allInstructors[i]) == false)
          {
            displayInstructors.Add(allInstructors[i]);
          }
        }

        myDict.Add("school", currentSchool);
        myDict.Add("tracks", schoolTracks);
        myDict.Add("availtracks", displayTracks);
        myDict.Add("currentinstructors", schoolInstructors);
        myDict.Add("availinstructors", displayInstructors);
        return View["school_details.cshtml", myDict];
      };
      Get["/instructor/update/{id}"] = parameters =>
      {
        Instructor selectedInstructor = Instructor.Find(parameters.id);
        return View["instructor_edit.cshtml", selectedInstructor];
      };
      Post["/instructor/remove/{id}"] = parameters =>
      {
        Instructor selectedInstructor = Instructor.Find(parameters.id);
        selectedInstructor.Remove();
        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        School currentSchool = School.Find(Request.Form["school-id"]);
        List<Track> schoolTracks = currentSchool.GetTracks();
        List<Track> allTracks = Track.GetAll();
        List<Track> displayTracks = new List<Track>{};
        List<Instructor> schoolInstructors = currentSchool.GetInstructors();
        List<Instructor> allInstructors = Instructor.GetAll();
        List<Instructor> displayInstructors = new List<Instructor>{};

        for(int i =0; i < allTracks.Count; i++)
        {
          if (schoolTracks.Contains(allTracks[i]) == false)
          {
            displayTracks.Add(allTracks[i]);
          }
        }

        for(int i =0; i < allInstructors.Count; i++)
        {
          if (schoolInstructors.Contains(allInstructors[i]) == false)
          {
            displayInstructors.Add(allInstructors[i]);
          }
        }

        myDict.Add("school", currentSchool);
        myDict.Add("tracks", schoolTracks);
        myDict.Add("availtracks", displayTracks);
        myDict.Add("currentinstructors", schoolInstructors);
        myDict.Add("availinstructors", displayInstructors);
        return View["school_details.cshtml", myDict];
      };


      //Routes for Tracks
      Post["/track/update/{id}"] = parameters => {
        Track.Update(Request.Form["course-name"], parameters.id);
        List<Track> allTracks = Track.GetAll();
        return View["tracks.cshtml", allTracks];
      };
      Post["/track/remove/{id}"] = parameters => {
        Track selectedTrack = Track.Find(parameters.id);
        selectedTrack.Delete();
        List<Track> allTracks = Track.GetAll();
        return View["tracks.cshtml", allTracks];
      };
      Get["/tracks/add"] = _ =>
      {
        return View["track_add.cshtml"];
      };
      Post["/tracks/add"] = _ =>
      {
        Track newTrack = new Track(Request.Form["track-name"]);
        newTrack.Save();
        List<Track> allTracks = Track.GetAll();
        return View["tracks.cshtml", allTracks];
      };
      Get["/courses/addto/{id}"] = parameters =>
      {
        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        List<Course> allCourses = Course.GetAll();
        myDict.Add("allcourses", allCourses);
        myDict.Add("currenttrack", Track.Find(parameters.id));
        return View["course_add.cshtml", myDict];
      };


      Post["/courses/addto"] = _ =>
      {
        string newName = Request.Form["course-name"];
        Track selectedTrack = Track.Find(Request.Form["track-id"]);
        List<Track> allTracks = Track.GetAll();
        List<Course> allCourses = Course.GetAll();

        bool Exists = false;
        foreach (Course course in allCourses)
        {
          if (newName.ToLower() == course.GetName().ToLower())
          {
            Exists = true;
          }
        }

        if (!Exists)
        {
          Course newCourse = new Course(newName);
          newCourse.Save();
          selectedTrack.AddCourse(newCourse);
         }

        return View["tracks.cshtml", allTracks];
      };


      Post["/courses/addexisting"] = _ =>
      {
        Track selectedTrack = Track.Find(Request.Form["track-id"]);
        Course selectedCourse = Course.Find(Request.Form["course-id"]);
        selectedTrack.AddCourse(selectedCourse);
        List<Track> allTracks = Track.GetAll();
        return View["tracks.cshtml", allTracks];
      };
      Post["/course/remove/{course_id}/{track_id}"] = parameters=>
      {
        int courseId = parameters.course_id;
        int trackId = parameters.track_id;
        Course selectedCourse = Course.Find(courseId);
        Track selectedTrack = Track.Find(trackId);
        selectedTrack.RemoveACourse(selectedCourse.GetId());
        List<Track> allTracks = Track.GetAll();
        return View["tracks.cshtml", allTracks];
      };

      // Routes for login
      Post["/account/login"] = _ =>
      {
        string username = Request.Form["user-name"];
        string pwd = Request.Form["user-password"];
        Student foundStudent = Student.FindByLogin(username);
        string encrypted = foundStudent.GetPassword();
        bool loggedIn = PasswordStorage.VerifyPassword(pwd, encrypted);
        //Add check for username
        if(!loggedIn)
        {
          string error = "Invalid Login";
          return View["login.cshtml", error];
        }
        NancyCookie newCookie = new NancyCookie("name", foundStudent.GetUserName());
        NancyCookie adminCookie = new NancyCookie("bool", "false");
        return View["main.cshtml", foundStudent].WithCookie(newCookie).WithCookie(adminCookie);
        //TODO make cookie accept two parameters for admin or student
      };

      Get["/account/logout"] = _ =>
      {
        if (Request.Cookies.ContainsKey("name") == true)
        {
          if (Request.Cookies["name"] != null)
          {
              NancyCookie newCookie = new NancyCookie("name", "");
              newCookie.Expires = DateTime.Now.AddDays(-1d);
              return View["index.cshtml"].WithCookie(newCookie);
          }
        }
        return View["index.cshtml"];
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
        string encrypted = PasswordStorage.CreateHash(password);
        Student newStudent = new Student(firstName, lastName, userName, encrypted, address, email);
        newStudent.Save();
        NancyCookie newCookie = new NancyCookie("name", newStudent.GetUserName());
        return View["main.cshtml"].WithCookie(newCookie);
      };

      Get["/instructor/create"] = _ =>{
        return View["instructor_add.cshtml"];
      };
      Post["/instructor/create"] = _ =>
      {
        string name = Request.Form["name"];
        string userName = Request.Form["username"];
        string password = Request.Form["password"];
        string address = Request.Form["address"];
        string email = Request.Form["email"];
        Instructor newInstructor = new Instructor(name, userName, password, address, email);
        newInstructor.Save();
        List<Instructor> allInstructors = Instructor.GetAll();
        return View["instructors.cshtml", allInstructors];
      };

      //Routes for deletion
      Get["/student/delete/{id}"] = parameters =>{
          Student selectedStudent = Student.Find(parameters.id);
          return View["student_delete.cshtml", selectedStudent];
      };

      Post["/student/deleted/{id}"] = parameters =>{
        Student.RemoveAStudent(parameters.id);
        return View["main.cshtml"];
      };

      //Routes for Updating
      Get["/student/update/{id}"] = parameters =>{
        Student selectedStudent = Student.Find(parameters.id);
        return View["student_edit.cshtml", selectedStudent];
      };

      Post["/student/updated/{id}"] = parameters =>{
        Student selectedStudent = Student.Find(parameters.id);
        Student.Update(Request.Form["first-name"], Request.Form["last-name"], Request.Form["username"], Request.Form["password"], Request.Form["address"], Request.Form["email"], parameters.id);
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Student foundStudent = Student.Find(parameters.id);
        Track newTrack = foundStudent.GetTrack();
        List<Track> allTracks = Track.GetAll();
        List<Course> courses = foundStudent.GetCourses();
        List<Grade> grades = new List<Grade> {};
        foreach(Course course in courses)
        {
          Grade newGrade = foundStudent.GetGrades(course.GetId());
          grades.Add(newGrade);
        }
        model.Add("student", foundStudent);
        model.Add("track", newTrack);
        model.Add("courses", courses);
        model.Add("grades", grades);
        model.Add("availtracks", allTracks);
        return View["student_details.cshtml", model];
      };

      Post["/instructor/update/{id}"] = parameters =>
      {
        Instructor selectedInstructor = Instructor.Find(parameters.id);
        Instructor.Update(Request.Form["name"], Request.Form["username"], Request.Form["password"], Request.Form["address"], Request.Form["email"], parameters.id);
        return View["main.cshtml"];
      };
    }
  }
}
