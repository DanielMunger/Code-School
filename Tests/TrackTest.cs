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

    [Fact]
    public void Test_GetStudentsAssociatedWithTrack()
    {
      List<Student> allStudents = new List<Student>{};
      List<Student> testStudents = new List<Student>{};

      Track newTrack = new Track("C#");
      newTrack.Save();

      Student newStudent = new Student("Student", "Student", "student1", "password", "123 fake st", "student@gmail.com");
      newStudent.Save();

      newTrack.AddStudent(newStudent);
      allStudents = newTrack.GetStudents();
      testStudents.Add(newStudent);

      Assert.Equal(testStudents, allStudents);
    }

    [Fact]
    public void Test_GetInstructorAssociatedWithTrack()
    {
      List<Instructor> allInstructors = new List<Instructor>{};
      List<Instructor> testInstructors = new List<Instructor>{};

      Track newTrack = new Track("C#");
      newTrack.Save();

      Instructor newInstructor = new Instructor("Instructor", "student1", "password", "123 fake st", "student@gmail.com");
      newInstructor.Save();

      newTrack.AddInstructor(newInstructor);
      allInstructors = newTrack.GetInstructors();
      testInstructors.Add(newInstructor);

      Assert.Equal(testInstructors, allInstructors);
    }

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
      Student.DeleteAll();
    }
  }
}
