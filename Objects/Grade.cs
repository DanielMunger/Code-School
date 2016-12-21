using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public class Grade
  {
    private int _id;
    private string _attendance;
    private string _grade;

    public Grade(string attendance, string grade, int id = 0)
    {
      _id = id;
      _attendance = attendance;
      _grade = grade;
    }
    public override bool Equals(System.Object otherGrade)
    {

      if (!(otherGrade is Grade))
      {
        return false;
      }
      else
      {
        Grade newGrade = (Grade) otherGrade;
        bool idEquality = (this.GetId() == newGrade.GetId());
        bool attendanceEquality = (this.GetAttendance() == newGrade.GetAttendance());
        bool gradeEquality = (this.GetGrade() == newGrade.GetGrade());
        return (idEquality && attendanceEquality && gradeEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetAttendance().GetHashCode();
    }
    public int GetId()
    {
      return _id;
    }
    public string GetAttendance()
    {
      return _attendance;
    }
    public string GetGrade()
    {
      return _grade;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO grades (attendance, grade) OUTPUT INSERTED.id VALUES (@Attendance, @Grade);", conn);

      cmd.Parameters.AddWithValue("@Attendance", this.GetAttendance());
      cmd.Parameters.AddWithValue("@Grade", this.GetGrade());
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

    public static List<Grade> GetAll()
    {
      List<Grade> allGrades = new List<Grade>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM grades;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int trackId = rdr.GetInt32(0);
        string attendance = rdr.GetString(1);
        string grade = rdr.GetString(2);

        Grade newGrade = new Grade(attendance, grade, trackId);
        allGrades.Add(newGrade);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allGrades;
    }

    public static Grade Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM grades WHERE id = @GradeId;", conn);
      cmd.Parameters.AddWithValue("@GradeId", id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundGradeId = 0;
      string foundAttendance = null;
      string foundGrade = null;

      while(rdr.Read())
      {
        foundGradeId = rdr.GetInt32(0);
        foundAttendance = rdr.GetString(1);
        foundGrade = rdr.GetString(2);
      }
      Grade newGrade = new Grade(foundAttendance, foundGrade, foundGradeId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return newGrade;
    }


    public static void Update(string newAttendance, string newGrade, int studentId, int courseId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE grades SET attendance = @Attendance, grade = @Grade FROM grades JOIN courses_grades_students ON (courses_grades_students.grade_id = grades.id) WHERE student_id = @StudentId AND course_id = @CourseId;", conn);

      cmd.Parameters.AddWithValue("@Attendance", newAttendance);
      cmd.Parameters.AddWithValue("@Grade", newGrade);
      cmd.Parameters.AddWithValue("@StudentId", studentId.ToString());
      cmd.Parameters.AddWithValue("@CourseId", courseId.ToString());

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

    public static void RemoveAGrade(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM grades WHERE id = @GradeId;", conn);
      cmd.Parameters.AddWithValue("@GradeId", id.ToString());
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
      SqlCommand cmd = new SqlCommand("Delete FROM grades;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
