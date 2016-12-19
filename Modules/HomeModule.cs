using Nancy;
using System.Collections.Generic;
using Namespace.Startup;

namespace Modules
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      // Routes for Landing Page
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
    }
  }
}
