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

  //   public void AddVenue(Venue newVenue)
  //  {
  //    SqlConnection conn = DB.Connection();
  //    conn.Open();
   //
  //    SqlCommand cmd = new SqlCommand("INSERT INTO tracks_venues (school_id, venue_id) VALUES (@TrackId, @VenueId);", conn);
   //
  //    SqlParameter schoolIdParameter = new SqlParameter();
  //    schoolIdParameter.ParameterName = "@TrackId";
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
  //   SqlCommand cmd = new SqlCommand("SELECT venues.* FROM venues JOIN tracks_venues ON (tracks_venues.venue_id = venues.id) JOIN tracks ON (tracks.id = tracks_venues.school_id) WHERE school_id = @TrackId;", conn);
  //   SqlParameter schoolIdParameter = new SqlParameter();
  //   schoolIdParameter.ParameterName = "@TrackId";
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
      SqlCommand cmd = new SqlCommand("Delete FROM tracks;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
