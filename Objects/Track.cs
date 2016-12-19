using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public class Track
  {
    private int _id;
    private string _name;

    public Track(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherTrack)
    {

      if (!(otherTrack is Track))
      {
        return false;
      }
      else
      {
        Track newTrack = (Track) otherTrack;
        bool idEquality = (this.GetId() == newTrack.GetId());
        bool nameEquality = (this.GetName() == newTrack.GetName());
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

      SqlCommand cmd = new SqlCommand("INSERT INTO tracks (track_name) OUTPUT INSERTED.id VALUES (@TrackName);", conn);

      cmd.Parameters.AddWithValue("@TrackName", this.GetName());
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

    public static List<Track> GetAll()
    {
      List<Track> allTracks = new List<Track>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tracks;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int trackId = rdr.GetInt32(0);
        string name = rdr.GetString(1);

        Track newTrack = new Track(name, trackId);
        allTracks.Add(newTrack);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allTracks;
    }

    public static Track Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tracks WHERE id = @TrackId;", conn);
      cmd.Parameters.AddWithValue("@TrackId", id.ToString());
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundTrackId = 0;
      string foundTrackname = null;

      while(rdr.Read())
      {
        foundTrackId = rdr.GetInt32(0);
        foundTrackname = rdr.GetString(1);
      }
      Track foundTrack = new Track(foundTrackname, foundTrackId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundTrack;
    }


    public void AddStudent(Student newStudent)
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO students_tracks (student_id, track_id) VALUES (@StudentId, @TrackId);", conn);

     cmd.Parameters.AddWithValue("@TrackId", this.GetId());
     cmd.Parameters.AddWithValue("@StudentId", newStudent.GetId());

     cmd.ExecuteNonQuery();

     if(conn!= null)
     {
       conn.Close();
     }
    }

    public List<Student> GetStudents()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT students.* FROM students JOIN students_tracks ON (students.id = students_tracks.student_id) JOIN tracks ON (students_tracks.track_id = tracks.id) WHERE track_id = @TrackId;", conn);
      cmd.Parameters.AddWithValue("@TrackId", this.GetId());

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Student> allStudents = new List<Student> {};
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentFirstName = rdr.GetString(1);
        string studentLastName = rdr.GetString(2);
        string studentUserName = rdr.GetString(3);
        string studentPassword = rdr.GetString(4);
        string studentAddress = rdr.GetString(5);
        string studentEmail = rdr.GetString(6);
        Student newStudent = new Student(studentFirstName, studentLastName, studentUserName, studentPassword, studentAddress, studentEmail, studentId);
        allStudents.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      return allStudents;
    }

    public void AddInstructor(Instructor newInstructor)
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO instructors_tracks (instructor_id, track_id) VALUES (@InstructorId, @TrackId);", conn);

     cmd.Parameters.AddWithValue("@TrackId", this.GetId());
     cmd.Parameters.AddWithValue("@InstructorId", newInstructor.GetId());

     cmd.ExecuteNonQuery();

     if(conn!= null)
     {
       conn.Close();
     }
    }

    public List<Instructor> GetInstructors()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT instructors.* FROM instructors JOIN instructors_tracks ON (instructors.id = instructors_tracks.instructor_id) JOIN tracks ON (instructors_tracks.track_id = tracks.id) WHERE track_id = @TrackId;", conn);
      cmd.Parameters.AddWithValue("@TrackId", this.GetId());

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Instructor> allInstructors = new List<Instructor> {};
      while(rdr.Read())
      {
        int instructorId = rdr.GetInt32(0);
        string instructorName = rdr.GetString(1);
        string instructorUserName = rdr.GetString(2);
        string instructorPassword = rdr.GetString(3);
        string instructorAddress = rdr.GetString(4);
        string instructorEmail = rdr.GetString(5);
        Instructor newInstructor = new Instructor(instructorName, instructorUserName, instructorPassword, instructorAddress, instructorEmail, instructorId);
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

    public static void Update(string newname, int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE tracks SET track_name = @TrackName WHERE id = @TrackId;", conn);

      cmd.Parameters.AddWithValue("@TrackName", newname);
      cmd.Parameters.AddWithValue("@TrackId", id.ToString());

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

    public static void RemoveATrack(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM tracks WHERE id = @TrackId;", conn);
      cmd.Parameters.AddWithValue("@TrackId", id.ToString());
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
      SqlCommand cmd = new SqlCommand("Delete FROM tracks; Delete FROM schools_tracks;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
