using Xunit;
using System;
using System.Collections.Generic;

namespace Kickstart
{
  public class SchoolTest : IDisposable
  {
    public SchoolTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<School> allSchools = new List<School>{};
      List<School> testList = new List<School>{};
      School newSchool = new School("Portland", "123 fake street", "9709709999");
      testList.Add(newSchool);

      newSchool.Save();
      allSchools = School.GetAll();

      Assert.Equal(testList, allSchools);
    }

    // [Fact]
    // public void Test_GetVenuesAssociatedWithSchool()
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
    public void Test_CheckUpdateSchoolInfo_True()
    {
      School newSchool = new School("Portland", "123 fake street", "9709709999");
      newSchool.Save();
      int id = newSchool.GetId();
      School.Update("Seattle", "123 fake street", "9709709999", id);
      School updated = School.Find(id);
      Assert.Equal(updated.GetCity(), "Seattle");
    }

    [Fact]
    public void Test_CheckDeleteSchool_False()
    {
      School testSchool = new School("Portland", "123 fake street", "9709709999");
      testSchool.Save();
      List<School> result = School.GetAll();
      School.RemoveASchool(testSchool.GetId());
      List<School> deleted = School.GetAll();
      bool isEqual = (result == deleted);
      Assert.Equal(false, isEqual);
    }

    public void Dispose()
    {
      School.DeleteAll();
    }
  }
}
