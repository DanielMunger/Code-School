using Xunit;
using System;
using System.Collections.Generic;

namespace Kickstart
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_SaveToDataBase_GetAll()
    {
      List<Student> allStudents = new List<Student>{};
      List<Student> testList = new List<Student>{};
      Student newStudent = new Student("Daniel", "Munger", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      testList.Add(newStudent);

      newStudent.Save();
      allStudents = Student.GetAll();

      Assert.Equal(testList, allStudents);
    }

    // [Fact]
    // public void Test_GetVenuesAssociatedWithStudent()
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
    public void Test_CheckUpdateStudentInfo_True()
    {
      Student newStudent = new Student("Daniel", "Munger", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      newStudent.Save();
      int id = newStudent.GetId();
      Student.Update("Loren", "Munger", "dmunger", "password", "123 fake st", "mungerda@gmail.com", id);
      Student updated = Student.Find(id);
      Assert.Equal(updated.GetFirstName(), "Loren");
    }

    [Fact]
    public void Test_CheckDeleteStudent_False()
    {
      Student testStudent = new Student("Daniel", "Munger", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      testStudent.Save();
      List<Student> result = Student.GetAll();
      Student.RemoveAStudent(testStudent.GetId());
      List<Student> deleted = Student.GetAll();
      bool isEqual = (result == deleted);
      Assert.Equal(false, isEqual);
    }

    public void Dispose()
    {
      Student.DeleteAll();
    }
  }
}
