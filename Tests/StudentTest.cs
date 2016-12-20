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

    [Fact]
    public void Test_GetTracksAssociatedWithStudent()
    {
      Student newStudent = new Student("Daniel", "Munger", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
      newStudent.Save();

      Track newTrack = new Track("Track");
      newTrack.Save();

      newStudent.AddTrack(newTrack);
      Track testTrack = newStudent.GetTrack();

      Assert.Equal(newTrack, testTrack);
    }

    // [Fact]
    // public void Test_GetGradeAssociatedWithStudent()
    // {
    //   List<Grade> allGrades = new List<Grade>{};
    //   List<Grade> testGrades = new List<Grade>{};
    //
    //   Student newStudent = new Student("Daniel", "Munger", "dmunger", "password", "123 fake st", "mungerda@gmail.com");
    //   newStudent.Save();
    //
    //   Course newCourse = new Course("math");
    //   newCourse.Save();
    //
    //   Grade newGrade = new Grade("attendance", "F");
    //   newGrade.Save();
    //
    //   newStudent.AddGrade(newGrade, newCourse.GetId());
    //   allGrades = newStudent.GetGrades(newCourse.GetId());
    //   testGrades.Add(newGrade);
    //
    //   Assert.Equal(testGrades, allGrades);
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
