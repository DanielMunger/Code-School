using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public class Student
  {
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _userName;
    private string _password;
    private string _address;
    private string _email;

    public Student(string firstName, string lastName, string userName, string password, string address, string email, int id = 0)
    {
      _id = id;
      _firstName = firstName;
      _lastName = lastName;
      _userName = userName;
      _password = password;
      _address = address;
      _email = email;
    }
    public override bool Equals(System.Object otherStudent)
    {

      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = (this.GetId() == newStudent.GetId());
        bool firstNameEquality = (this.GetFirstName() == newStudent.GetFirstName());
        bool lastNameEquality = (this.GetLastName() == newStudent.GetLastName());
        bool userNameEquality = (this.GetUserName() == newStudent.GetUserName());
        bool passwordEquality = (this.GetPassword() == newStudent.GetPassword());
        bool addressEquality = (this.GetAddress() == newStudent.GetAddress());
        bool emailEquality = (this.GetEmail() == newStudent.GetEmail());
        return (idEquality && firstNameEquality && lastNameEquality && userNameEquality && passwordEquality && addressEquality && emailEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetFirstName()
    {
      return _firstName;
    }
    public string GetLastName()
    {
      return _lastName;
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

      SqlCommand cmd = new SqlCommand("INSERT INTO students (first_name, last_name, username, password, address, email) OUTPUT INSERTED.id VALUES (@StudentFirstName, @StudentLastName, @UserName, @Password, @Address, @Email);", conn);

      cmd.Parameters.AddWithValue("@StudentFirstName", this.GetFirstName());
      cmd.Parameters.AddWithValue("@StudentLastName", this.GetLastName());
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

    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string firstName = rdr.GetString(1);
        string lastName = rdr.GetString(2);
        string userName = rdr.GetString(3);
        string password = rdr.GetString(4);
        string address = rdr.GetString(5);
        string email = rdr.GetString(6);


        Student newStudent = new Student(firstName, lastName, userName, password, address, email, studentId);
        allStudents.Add(newStudent);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allStudents;
    }

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
      SqlParameter schoolIdParameter = new SqlParameter("@StudentId", id.ToString());
      cmd.Parameters.Add(schoolIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStudentId = 0;
      string foundStudentFirstName = null;
      string foundStudentLastName = null;
      string foundStudentUserName = null;
      string foundStudentPassword = null;
      string foundStudentAddress = null;
      string foundStudentEmail = null;
      while(rdr.Read())
      {
        foundStudentId = rdr.GetInt32(0);
        foundStudentFirstName = rdr.GetString(1);
        foundStudentLastName = rdr.GetString(2);
        foundStudentUserName = rdr.GetString(3);
        foundStudentPassword = rdr.GetString(4);
        foundStudentAddress = rdr.GetString(5);
        foundStudentEmail = rdr.GetString(6);

      }
      Student foundStudent = new Student(foundStudentFirstName, foundStudentLastName, foundStudentUserName, foundStudentPassword, foundStudentAddress, foundStudentEmail, foundStudentId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundStudent;
    }

  //   public void AddVenue(Venue newVenue)
  //  {
  //    SqlConnection conn = DB.Connection();
  //    conn.Open();
   //
  //    SqlCommand cmd = new SqlCommand("INSERT INTO schools_venues (school_id, venue_id) VALUES (@StudentId, @VenueId);", conn);
   //
  //    SqlParameter schoolIdParameter = new SqlParameter();
  //    schoolIdParameter.ParameterName = "@StudentId";
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
  //   SqlCommand cmd = new SqlCommand("SELECT venues.* FROM venues JOIN schools_venues ON (schools_venues.venue_id = venues.id) JOIN schools ON (schools.id = schools_venues.school_id) WHERE school_id = @StudentId;", conn);
  //   SqlParameter schoolIdParameter = new SqlParameter();
  //   schoolIdParameter.ParameterName = "@StudentId";
  //   schoolIdParameter.Value = this.GetId();
  //   cmd.Parameters.Add(schoolIdParameter);
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

  public static void Update(string newFirstName, string newLastName, string newUserName, string newPassword, string address, string newEmail, int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE students SET first_name = @FirstName, last_name = @LastName, username = @UserName, password = @Password, address = @Address, @Email = email  WHERE id = @StudentId;", conn);

      cmd.Parameters.AddWithValue("@FirstName", newFirstName);
      cmd.Parameters.AddWithValue("@LastName", newLastName);
      cmd.Parameters.AddWithValue("@UserName", newUserName);
      cmd.Parameters.AddWithValue("@Password", newPassword);
      cmd.Parameters.AddWithValue("@Address", address);
      cmd.Parameters.AddWithValue("@Email", newEmail);
      cmd.Parameters.AddWithValue("@StudentId", id.ToString());

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

    public static void RemoveAStudent(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @StudentId;", conn);
      cmd.Parameters.AddWithValue("@StudentId", id.ToString());
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
      SqlCommand cmd = new SqlCommand("Delete FROM students;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
