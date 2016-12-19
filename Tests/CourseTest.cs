using Xunit;
using System;
using System.Collections.Generic;

namespace Kickstart
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<Course> allCourses = new List<Course>{};
      List<Course> testList = new List<Course>{};
      Course newCourse = new Course("Daniel");
      testList.Add(newCourse);

      newCourse.Save();
      allCourses = Course.GetAll();

      Assert.Equal(testList, allCourses);
    }

    // [Fact]
    // public void Test_GetVenuesAssociatedWithCourse()
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
    public void Test_CheckUpdateCourseInfo_True()
    {
      Course newCourse = new Course("Daniel");
      newCourse.Save();
      int id = newCourse.GetId();
      Course.Update("Loren", id);
      Course updated = Course.Find(id);
      Assert.Equal(updated.GetName(), "Loren");
    }

    [Fact]
    public void Test_CheckDeleteCourse_False()
    {
      Course testCourse = new Course("Daniel");
      testCourse.Save();
      List<Course> result = Course.GetAll();
      Course.RemoveACourse(testCourse.GetId());
      List<Course> deleted = Course.GetAll();
      bool isEqual = (result == deleted);
      Assert.Equal(false, isEqual);
    }

    public void Dispose()
    {
      Course.DeleteAll();
    }
  }
}
