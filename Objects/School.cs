using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Kickstart;

namespace Kickstart
{
  public class School
  {
    private int _id;
    private string _city;
    private string _address;
    private string _phone;

    public School(string city, string address, string phone, int id = 0)
    {
      _id = id;
      _city = city;
      _address = address;
      _phone = phone;
    }
    public override bool Equals(System.Object otherSchool)
    {

      if (!(otherSchool is School))
      {
        return false;
      }
      else
      {
        School newSchool = (School) otherSchool;
        bool idEquality = (this.GetId() == newSchool.GetId());
        bool cityEquality = (this.GetCity() == newSchool.GetCity());
        bool addressEquality = (this.GetAddress() == newSchool.GetAddress());
        bool phoneEquality = (this.GetPhone() == newSchool.GetPhone());
        return (idEquality && cityEquality && addressEquality && phoneEquality);
      }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetCity()
    {
      return _city;
    }
    public string GetAddress()
    {
      return _address;
    }
    public string GetPhone()
    {
      return _phone;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO schools (school_city, school_address, phone_number) OUTPUT INSERTED.id VALUES (@SchoolCity, @SchoolAddress, @SchoolPhone);", conn);

      SqlParameter cityParameter = new SqlParameter("@SchoolCity", this.GetCity());
      SqlParameter addressParameter = new SqlParameter("@SchoolAddress", this.GetAddress());
      SqlParameter phoneParameter = new SqlParameter("@SchoolPhone", this.GetPhone());

      cmd.Parameters.Add(cityParameter);
      cmd.Parameters.Add(addressParameter);
      cmd.Parameters.Add(phoneParameter);

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

    public static List<School> GetAll()
    {
      List<School> allSchools = new List<School>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM schools;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int schoolId = rdr.GetInt32(0);
        string city = rdr.GetString(1);
        string address = rdr.GetString(2);
        string phone = rdr.GetString(3);

        School newSchool = new School(city, address, phone, schoolId);
        allSchools.Add(newSchool);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allSchools;
    }

    public static School Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM schools WHERE id = @SchoolId;", conn);
      SqlParameter schoolIdParameter = new SqlParameter("@SchoolId", id.ToString());
      cmd.Parameters.Add(schoolIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundSchoolId = 0;
      string foundSchoolCity = null;
      string foundSchoolAddress = null;
      string foundSchoolPhone = null;
      while(rdr.Read())
      {
        foundSchoolId = rdr.GetInt32(0);
        foundSchoolCity = rdr.GetString(1);
        foundSchoolAddress = rdr.GetString(2);
        foundSchoolPhone = rdr.GetString(3);
      }
      School foundSchool = new School(foundSchoolCity, foundSchoolAddress, foundSchoolPhone, foundSchoolId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundSchool;
    }

    public void AddTrack(Track newTrack)
   {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO schools_tracks (school_id, track_id) VALUES (@SchoolId, @TrackId);", conn);

     cmd.Parameters.AddWithValue("@SchoolId", this.GetId());
     cmd.Parameters.AddWithValue("@TrackId", newTrack.GetId());

     cmd.ExecuteNonQuery();

     if(conn!= null)
     {
       conn.Close();
     }
   }

   public List<Track> GetTracks()
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT tracks.* FROM tracks JOIN schools_tracks ON (tracks.id = schools_tracks.track_id) JOIN schools ON (schools_tracks.school_id = schools.id) WHERE school_id = @SchoolId;", conn);
    cmd.Parameters.AddWithValue("@SchoolId", this.GetId());

    SqlDataReader rdr = cmd.ExecuteReader();

    List<Track> allTracks = new List<Track> {};
    while(rdr.Read())
    {
      int trackId = rdr.GetInt32(0);
      string trackName = rdr.GetString(1);
      Track newTrack = new Track(trackName, trackId);
      allTracks.Add(newTrack);
    }
    if (rdr != null)
    {
      rdr.Close();
    }

    return allTracks;
  }

  public static void Update(string newCity, string newAddress, string newPhone, int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE schools SET school_city = @SchoolCity, school_address = @SchoolAddress, phone_number = @SchoolPhone  WHERE id = @SchoolId;", conn);

      cmd.Parameters.AddWithValue("@SchoolCity", newCity);
      cmd.Parameters.AddWithValue("@SchoolAddress", newAddress);
      cmd.Parameters.AddWithValue("@SchoolPhone", newPhone);
      cmd.Parameters.AddWithValue("@SchoolId", id.ToString());

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

    public static void RemoveASchool(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM schools WHERE id = @SchoolId;", conn);
      cmd.Parameters.AddWithValue("@SchoolId", id.ToString());
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
      SqlCommand cmd = new SqlCommand("Delete FROM schools; Delete FROM schools_tracks;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
