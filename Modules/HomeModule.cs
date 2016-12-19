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
        return View["locations.cshtml"];
      };
      Get["/account/create"] = _ =>
      {
        return View["new_account.cshtml"];
      };
      Get["/account/login"] = _ =>
      {
        return View["login.cshtml"];
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
