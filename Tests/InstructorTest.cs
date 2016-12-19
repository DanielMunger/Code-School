using Xunit;
using System;
using System.Collections.Generic;

namespace Kickstart
{
  public class InstructorTest : IDisposable
  {
    public InstructorTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<Instructor> allInstructors = new List<Instructor>{};
      List<Instructor> testList = new List<Instructor>{};
      Instructor newInstructor = new Instructor("Daniel", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      testList.Add(newInstructor);

      newInstructor.Save();
      allInstructors = Instructor.GetAll();

      Assert.Equal(testList, allInstructors);
    }

    // [Fact]
    // public void Test_GetVenuesAssociatedWithInstructor()
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
    public void Test_CheckUpdateInstructorInfo_True()
    {
      Instructor newInstructor = new Instructor("Daniel", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      newInstructor.Save();
      int id = newInstructor.GetId();
      Instructor.Update("Loren", "dmunger", "password", "123 fake st", "mungerda@gmail.com", id);
      Instructor updated = Instructor.Find(id);
      Assert.Equal(updated.GetName(), "Loren");
    }

    [Fact]
    public void Test_CheckDeleteInstructor_False()
    {
      Instructor testInstructor = new Instructor("Daniel", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      testInstructor.Save();
      List<Instructor> result = Instructor.GetAll();
      Instructor.RemoveAInstructor(testInstructor.GetId());
      List<Instructor> deleted = Instructor.GetAll();
      bool isEqual = (result == deleted);
      Assert.Equal(false, isEqual);
    }

    public void Dispose()
    {
      Instructor.DeleteAll();
    }
  }
}
