using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public class Instructor
  {
    private int _id;
    private string _name;
    private string _userName;
    private string _password;
    private string _address;
    private string _email;

    public Instructor(string name, string userName, string password, string address, string email, int id = 0)
    {
      _id = id;
      _name = name;
      _userName = userName;
      _password = password;
      _address = address;
      _email = email;
    }
    public override bool Equals(System.Object otherInstructor)
    {

      if (!(otherInstructor is Instructor))
      {
        return false;
      }
      else
      {
        Instructor newInstructor = (Instructor) otherInstructor;
        bool idEquality = (this.GetId() == newInstructor.GetId());
        bool nameEquality = (this.GetName() == newInstructor.GetName());
        bool userNameEquality = (this.GetUserName() == newInstructor.GetUserName());
        bool passwordEquality = (this.GetPassword() == newInstructor.GetPassword());
        bool addressEquality = (this.GetAddress() == newInstructor.GetAddress());
        bool emailEquality = (this.GetEmail() == newInstructor.GetEmail());
        return (idEquality && nameEquality && userNameEquality && passwordEquality && addressEquality && emailEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public string GetUserName()
    {
      return _userName;
    }

    //REMOVE AND REPLACE WITH ENCRYPTED PASSWORD RETURN
    public string GetPassword()
    {
      return _password;
    }
    //

    public string GetAddress()
    {
      return _address;
    }
    public string GetEmail()
    {
        return _email;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO instructors (instructor_name, username, password, address, email) OUTPUT INSERTED.id VALUES (@Name, @UserName, @Password, @Address, @Email);", conn);

      cmd.Parameters.AddWithValue("@Name", this.GetName());
      cmd.Parameters.AddWithValue("@UserName", this.GetUserName());
      cmd.Parameters.AddWithValue("@Password", this.GetPassword());//Change to GetEncrypted
      cmd.Parameters.AddWithValue("@Address", this.GetAddress());
      cmd.Parameters.AddWithValue("@Email", this.GetEmail());
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

    public static List<Instructor> GetAll()
    {
      List<Instructor> allInstructors = new List<Instructor>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM instructors;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int instructorId = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string userName = rdr.GetString(2);
        string password = rdr.GetString(3);
        string address = rdr.GetString(4);
        string email = rdr.GetString(5);

        Instructor newInstructor = new Instructor(name, userName, password, address, email, instructorId);
        allInstructors.Add(newInstructor);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allInstructors;
    }

    public static Instructor Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM instructors WHERE id = @InstructorId;", conn);
      SqlParameter instructorIdParameter = new SqlParameter("@InstructorId", id.ToString());
      cmd.Parameters.Add(instructorIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundInstructorId = 0;
      string foundInstructorName = null;
      string foundInstructorUserName = null;
      string foundInstructorPassword = null;
      string foundInstructorAddress = null;
      string foundInstructorEmail = null;
      while(rdr.Read())
      {
        foundInstructorId = rdr.GetInt32(0);
        foundInstructorName = rdr.GetString(1);
        foundInstructorUserName = rdr.GetString(2);
        foundInstructorPassword = rdr.GetString(3);
        foundInstructorAddress = rdr.GetString(4);
        foundInstructorEmail = rdr.GetString(5);

      }
      Instructor foundInstructor = new Instructor(foundInstructorName, foundInstructorUserName, foundInstructorPassword, foundInstructorAddress, foundInstructorEmail, foundInstructorId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundInstructor;
    }

  //   public void AddVenue(Venue newVenue)
  //  {
  //    SqlConnection conn = DB.Connection();
  //    conn.Open();
   //
  //    SqlCommand cmd = new SqlCommand("INSERT INTO instructors_venues (instructor_id, venue_id) VALUES (@InstructorId, @VenueId);", conn);
   //
  //    SqlParameter instructorIdParameter = new SqlParameter();
  //    instructorIdParameter.ParameterName = "@InstructorId";
  //    instructorIdParameter.Value = this.GetId();
  //    cmd.Parameters.Add(instructorIdParameter);
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
  //   SqlCommand cmd = new SqlCommand("SELECT venues.* FROM venues JOIN instructors_venues ON (instructors_venues.venue_id = venues.id) JOIN instructors ON (instructors.id = instructors_venues.instructor_id) WHERE instructor_id = @InstructorId;", conn);
  //   SqlParameter instructorIdParameter = new SqlParameter();
  //   instructorIdParameter.ParameterName = "@InstructorId";
  //   instructorIdParameter.Value = this.GetId();
  //   cmd.Parameters.Add(instructorIdParameter);
  //   SqlDataReader rdr = cmd.ExecuteReader();
  //
  //   List<Venue> allVenues = new List<Venue> {};
  //   while(rdr.Read())
  //   {
  //     int venueId = rdr.GetInt32(0);
  //     string venueName = rdr.GetString(1);
  //     string venueCity = rdr.GetString(2);
  //     Venue newVenue = new Venue(venueName, venueCity, venueId);
  //     allVenues.Add(newVenue);
  //   }
  //   if (rdr != null)
  //   {
  //     rdr.Close();
  //   }
  //
  //   return allVenues;
  // }

  public static void Update(string newName, string newUserName, string newPassword, string address, string newEmail, int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE instructors SET instructor_name = @Name, username = @UserName, password = @Password, address = @Address, @Email = email  WHERE id = @InstructorId;", conn);

      cmd.Parameters.AddWithValue("@Name", newName);
      cmd.Parameters.AddWithValue("@UserName", newUserName);
      cmd.Parameters.AddWithValue("@Password", newPassword);
      cmd.Parameters.AddWithValue("@Address", address);
      cmd.Parameters.AddWithValue("@Email", newEmail);
      cmd.Parameters.AddWithValue("@InstructorId", id.ToString());

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

    public void Remove()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM instructors_schools WHERE instructor_id = @InstructorId;", conn);
      cmd.Parameters.AddWithValue("@InstructorId", this.GetId());
      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static void RemoveAInstructor(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM instructors WHERE id = @InstructorId;", conn);
      cmd.Parameters.AddWithValue("@InstructorId", id.ToString());
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
      SqlCommand cmd = new SqlCommand("Delete FROM instructors;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
