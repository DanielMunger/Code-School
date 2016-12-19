using Nancy;
using System.Collections.Generic;
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
      Get["/locations"] = _ =>
      {
        List<School> allSchools = School.GetAll();
        return View["locations.cshtml", allSchools];
      };
      Get["/account/create"] = _ =>
      {
        return View["new_account.cshtml"];
      };
      Get["/account/login"] = _ =>
      {
        return View["login.cshtml"];
      };

      // Routes for Locations Page
      Get["/locations/add"] = _ =>
      {
        return View["add_school.cshtml"];
      };
      Post["/locations/add"] = _ =>
      {
        School newSchool = new School(Request.Form["school-city"], Request.Form["school-address"], Request.Form["school-phone"]);
        newSchool.Save();
        return View["add_school.cshtml", newSchool];
      };
      Get["/locations/{id}"] = parameters =>
      {
        Dictionary<string, object> myDict = new Dictionary<string, object>{};
        School currentSchool = School.Find(parameters.id);
        myDict.Add("tracks", School.GetTracks());
        myDict.Add("instructors", School.GetInstructors());
        return View["location_details.cshtml"];
      };


      // Routes for login
      Post["/account/login"] = _ =>
      {
        string username = Request.Form["user-name"];
        string pwd = Request.Form["user-password"];
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
        return View["index.cshtml"];
      };
    }
  }
}
