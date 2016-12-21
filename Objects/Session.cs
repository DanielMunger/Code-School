using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public interface IUserIdentity
  {
    string UserName { get; set; }
  }

  public class Session : IUserIdentity
  {
    public string UserName { get; set; }
    public int UserId { get; set; }
    public bool IsTeacher { get; set; }

    public Session (string name, int id, bool teacher)
    {
      this.UserName = name;
      this.UserId = id;
      this.IsTeacher = teacher;
    }
  }
}
