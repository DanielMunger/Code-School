using Xunit;
using System;
using System.Collections.Generic;

namespace Kickstart
{
  public class TrackTest : IDisposable
  {
    public TrackTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<Track> allTracks = new List<Track>{};
      List<Track> testList = new List<Track>{};
      Track newTrack = new Track("Daniel");
      testList.Add(newTrack);

      newTrack.Save();
      allTracks = Track.GetAll();

      Assert.Equal(testList, allTracks);
    }

    // [Fact]
    // public void Test_GetVenuesAssociatedWithTrack()
    // {
    //   List<Venue> allVenues = new List<Venue>{};
    //   List<Venue> testVenues = new List<Venue>{};
    //
    //   Band newBand = new Band("Band");
    //   newBand.Save();
    //
    //   Venue newVenue = new Venue("Venue", "San Francisco");
    //   newVenue.Save();
    //
    //   newBand.AddVenue(newVenue);
    //   allVenues = newBand.GetVenues();
    //   testVenues.Add(newVenue);
    //
    //   Assert.Equal(testVenues, allVenues);
    // }

    [Fact]
    public void Test_CheckUpdateTrackInfo_True()
    {
      Track newTrack = new Track("Daniel");
      newTrack.Save();
      int id = newTrack.GetId();
      Track.Update("Loren", id);
      Track updated = Track.Find(id);
      Assert.Equal(updated.GetName(), "Loren");
    }

    [Fact]
    public void Test_CheckDeleteTrack_False()
    {
      Track testTrack = new Track("Daniel");
      testTrack.Save();
      List<Track> result = Track.GetAll();
      Track.RemoveATrack(testTrack.GetId());
      List<Track> deleted = Track.GetAll();
      bool isEqual = (result == deleted);
      Assert.Equal(false, isEqual);
    }

    public void Dispose()
    {
      Track.DeleteAll();
    }
  }
}
