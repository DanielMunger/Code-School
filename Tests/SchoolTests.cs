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

    [Fact]
    public void Test_GetTracksAssociatedWithSchool()
    {
      List<Track> allTracks = new List<Track>{};
      List<Track> testTracks = new List<Track>{};

      School newSchool = new School("Portland", "123 fake street", "9709709999");
      newSchool.Save();

      Track newTrack = new Track("Track");
      newTrack.Save();

      newSchool.AddTrack(newTrack);
      allTracks = newSchool.GetTracks();
      testTracks.Add(newTrack);

      Assert.Equal(testTracks, allTracks);
    }

    [Fact]
    public void Test_GetStudentsAssociatedWithSchool()
    {
      List<Student> allStudents = new List<Student>{};
      List<Student> testStudents = new List<Student>{};

      School newSchool = new School("Portland", "123 fake street", "9709709999");
      newSchool.Save();

      Student newStudent = new Student("Student", "Student", "student1", "password", "123 fake st", "student@gmail.com");
      newStudent.Save();

      newSchool.AddStudent(newStudent);
      allStudents = newSchool.GetStudents();
      testStudents.Add(newStudent);

      Assert.Equal(testStudents, allStudents);
    }

    [Fact]
    public void Test_GetInstructorsAssociatedWithSchool()
    {
      List<Instructor> allInstructors = new List<Instructor>{};
      List<Instructor> testInstructors = new List<Instructor>{};

      School newSchool = new School("Portland", "123 fake street", "9709709999");
      newSchool.Save();

      Instructor newInstructor = new Instructor("Student", "student1", "password", "123 fake st", "student@gmail.com");
      newInstructor.Save();

      newSchool.AddInstructor(newInstructor);
      allInstructors = newSchool.GetInstructors();
      testInstructors.Add(newInstructor);

      Assert.Equal(testInstructors, allInstructors);
    }

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
      Student.DeleteAll();
      Track.DeleteAll();
      Instructor.DeleteAll();
    }
  }
}
