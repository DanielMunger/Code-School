using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public class Course
  {
    private int _id;
    private string _name;

    public Course(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherCourse)
    {

      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = (this.GetId() == newCourse.GetId());
        bool nameEquality = (this.GetName() == newCourse.GetName());
        return (idEquality && nameEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (course_name) OUTPUT INSERTED.id VALUES (@CourseName);", conn);

      cmd.Parameters.AddWithValue("@CourseName", this.GetName());
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Course newCourse = new Course(name, courseId);
        allCourses.Add(newCourse);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allCourses;
    }

    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
      cmd.Parameters.AddWithValue("@CourseId", id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundCoursename = null;

      while(rdr.Read())
      {
        foundCourseId = rdr.GetInt32(0);
        foundCoursename = rdr.GetString(1);
      }
      Course foundCourse = new Course(foundCoursename, foundCourseId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundCourse;
    }

  //   public void AddVenue(Venue newVenue)
  //  {
  //    SqlConnection conn = DB.Connection();
  //    conn.Open();
   //
  //    SqlCommand cmd = new SqlCommand("INSERT INTO courses_venues (school_id, venue_id) VALUES (@CourseId, @VenueId);", conn);
   //
  //    SqlParameter schoolIdParameter = new SqlParameter();
  //    schoolIdParameter.ParameterName = "@CourseId";
  //    schoolIdParameter.Value = this.GetId();
  //    cmd.Parameters.Add(schoolIdParameter);
   //
  //    SqlParameter venueIdParameter = new SqlParameter();
  //    venueIdParameter.ParameterName = "@VenueId";
  //    venueIdParameter.Value = newVenue.GetId();
  //    cmd.Parameters.Add(venueIdParameter);
   //
  //    cmd.ExecuteNonQuery();
   //
  //    if(conn!= null)
  //    {
  //      conn.Close();
  //    }
  //  }
  //
  //  public List<Venue> GetVenues()
  // {
  //   SqlConnection conn = DB.Connection();
  //   conn.Open();
  //
  //   SqlCommand cmd = new SqlCommand("SELECT venues.* FROM venues JOIN courses_venues ON (courses_venues.venue_id = venues.id) JOIN courses ON (courses.id = courses_venues.school_id) WHERE school_id = @CourseId;", conn);
  //   SqlParameter schoolIdParameter = new SqlParameter();
  //   schoolIdParameter.ParameterName = "@CourseId";
  //   schoolIdParameter.Value = this.GetId();
  //   cmd.Parameters.Add(schoolIdParameter);
  //   SqlDataReader rdr = cmd.ExecuteReader();
  //
  //   List<Venue> allVenues = new List<Venue> {};
  //   while(rdr.Read())
  //   {
  //     int venueId = rdr.GetInt32(0);
  //     string venueName = rdr.GetString(1);
  //     string venuename = rdr.GetString(2);
  //     Venue newVenue = new Venue(venueName, venuename, venueId);
  //     allVenues.Add(newVenue);
  //   }
  //   if (rdr != null)
  //   {
  //     rdr.Close();
  //   }
  //
  //   return allVenues;
  // }

  public static void Update(string newname, int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE courses SET course_name = @CourseName WHERE id = @CourseId;", conn);

      cmd.Parameters.AddWithValue("@CourseName", newname);
      cmd.Parameters.AddWithValue("@CourseId", id.ToString());

      SqlDataReader rdr = cmd.ExecuteReader();

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void RemoveACourse(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id = @CourseId;", conn);
      cmd.Parameters.AddWithValue("@CourseId", id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("Delete FROM courses;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
