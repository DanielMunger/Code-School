using Xunit;
using System;
using System.Collections.Generic;

namespace Kickstart
{
  public class GradeTest : IDisposable
  {
    public GradeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<Grade> allGrades = new List<Grade>{};
      List<Grade> testList = new List<Grade>{};
      Grade newGrade = new Grade("Good", "F");
      testList.Add(newGrade);

      newGrade.Save();
      allGrades = Grade.GetAll();

      Assert.Equal(testList, allGrades);
    }

    // [Fact]
    // public void Test_GetVenuesAssociatedWithGrade()
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
    // 
    // [Fact]
    // public void Test_CheckUpdateGradeInfo_True()
    // {
    //   Grade newGrade = new Grade("Good", "F");
    //   newGrade.Save();
    //   int id = newGrade.GetId();
    //   Grade.Update("Loren", "A", id);
    //   Grade updated = Grade.Find(id);
    //   Assert.Equal(updated.GetAttendance(), "Loren");
    // }

    [Fact]
    public void Test_CheckDeleteGrade_False()
    {
      Grade testGrade = new Grade("Good", "F");
      testGrade.Save();
      List<Grade> result = Grade.GetAll();
      Grade.RemoveAGrade(testGrade.GetId());
      List<Grade> deleted = Grade.GetAll();
      bool isEqual = (result == deleted);
      Assert.Equal(false, isEqual);
    }

    public void Dispose()
    {
      Grade.DeleteAll();
    }
  }
}
