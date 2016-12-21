using Xunit;
using System.Collections.Generic;
using System;
using System.Text;
using System.Security.Cryptography;

namespace Kickstart
{
  public class PasswordTest
  {
    public PasswordTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=kickstartdb_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void PasswordHash_hashespassword_true()
    {
      string password = "password";
      string hashedPassword = PasswordStorage.CreateHash(password);
      Console.WriteLine(hashedPassword);
      bool result = PasswordStorage.VerifyPassword(password, hashedPassword);

      Assert.Equal(true, result);
    }
  }
}
