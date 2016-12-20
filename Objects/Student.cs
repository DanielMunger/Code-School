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
    public override int GetHashCode()
    {
      return this.GetFirstName().GetHashCode();
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

    public static Student FindByLogin(string user, string pass)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE username = @UserName AND password = @Password;", conn);
      cmd.Parameters.AddWithValue("@UserName", user);
      cmd.Parameters.AddWithValue("@Password", pass);
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

    public void AddTrack(Track newTrack)
   {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO students_tracks (student_id, track_id) VALUES (@StudentId, @TrackId);", conn);

     cmd.Parameters.AddWithValue("@StudentId", this.GetId());
     cmd.Parameters.AddWithValue("@TrackId", newTrack.GetId());

     cmd.ExecuteNonQuery();

     if(conn!= null)
     {
       conn.Close();
     }
   }

   public Track GetTrack()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT tracks.* FROM tracks JOIN students_tracks ON (tracks.id = students_tracks.track_id) JOIN students ON (students_tracks.student_id = students.id) WHERE student_id = @StudentId;", conn);
    cmd.Parameters.AddWithValue("@StudentId", this.GetId());

    SqlDataReader rdr = cmd.ExecuteReader();

    int trackId = 0;
    string trackName = null;
    while(rdr.Read())
    {
      trackId = rdr.GetInt32(0);
      trackName = rdr.GetString(1);
    }

    Track newTrack = new Track(trackName, trackId);

    if (rdr != null)
    {
      rdr.Close();
    }

    return newTrack;
  }

  public List<Course> GetCourses()
 {
   SqlConnection conn = DB.Connection();
   conn.Open();

   SqlCommand cmd = new SqlCommand("SELECT courses.* FROM courses JOIN courses_tracks ON (courses.id = courses_tracks.course_id) JOIN students_tracks ON (students_tracks.track_id = courses_tracks.track_id) WHERE student_id = @StudentId;", conn);
   cmd.Parameters.AddWithValue("@StudentId", this.GetId());

   SqlDataReader rdr = cmd.ExecuteReader();

   List<Course> allCourse = new List<Course> {};
   while(rdr.Read())
   {
     int courseId = rdr.GetInt32(0);
     string courseName = rdr.GetString(1);
     Course newCourse = new Course(courseName, courseId);
     allCourse.Add(newCourse);
   }
   if (rdr != null)
   {
     rdr.Close();
   }

   return allCourse;
 }


  public void AddGrade(Grade newGrade, int courseId)
   {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO courses_grades_students (course_id, student_id, grade_id) VALUES (@CourseId, @StudentId, @GradeId);", conn);

     cmd.Parameters.AddWithValue("@StudentId", this.GetId());
     cmd.Parameters.AddWithValue("@GradeId", newGrade.GetId());
     cmd.Parameters.AddWithValue("@CourseId", courseId);

     cmd.ExecuteNonQuery();

     if(conn!= null)
     {
       conn.Close();
     }
   }

  public Grade GetGrades(int courseId)
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT grades.* FROM grades JOIN courses_grades_students ON (grades.id = courses_grades_students.grade_id) JOIN students ON (courses_grades_students.student_id = students.id) JOIN courses ON (courses_grades_students.course_id = courses.id) WHERE student_id = @StudentId AND course_id = @CourseId;", conn);

    cmd.Parameters.AddWithValue("@StudentId", this.GetId());
    cmd.Parameters.AddWithValue("@CourseId", courseId);

    SqlDataReader rdr = cmd.ExecuteReader();

    int gradeId = 0;
    string attendance = null;
    string grade = null;
    while(rdr.Read())
    {
      gradeId = rdr.GetInt32(0);
      attendance = rdr.GetString(1);
      grade = rdr.GetString(2);
    }

    Grade newGrade = new Grade(attendance, grade, gradeId);

    if (rdr != null)
    {
      rdr.Close();
    }

    return newGrade;
  }

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

      SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @StudentId; DELETE FROM schools_students WHERE student_id = @StudentId; DELETE FROM students_tracks WHERE student_id = @StudentId; DELETE FROM courses_grades_students WHERE student_id = @StudentId; ", conn);
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
