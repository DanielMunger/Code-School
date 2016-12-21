using System;
using System.Text;
using System.Security.Cryptography;

namespace Kickstart
{
  class PasswordStorage
  {
    public const int SALT_BYTES = 24;
    public const int HASH_BYTES = 18;
    public const int PBKDF2_ITERATIONS = 64000;

    public const int HASH_SECTIONS = 5;
    public const int HASH_ALGORITHM_INDEX = 0;
    public const int ITERATION_INDEX = 1;
    public const int HASH_SIZE_INDEX = 2;
    public const int SALT_INDEX = 3;
    public const int PBKDF2_INDEX = 4;

    public static string CreateHash(string password)
    {
      //SALTING WILL GO HERE
      byte[] salt = new byte[SALT_BYTES];
      RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
      csprng.GetBytes(salt);

      //HASHING
      byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

      String parts = "sha1:" +
          PBKDF2_ITERATIONS +
          ":" +
          hash.Length +
          ":" +
          Convert.ToBase64String(salt) +
          ":" +
          Convert.ToBase64String(hash);
      return parts;
    }

    public static bool VerifyPassword(string password, string goodHash)
        {
            char[] delimiter = { ':' };
            string[] split = goodHash.Split(delimiter);
            int iterations = 0;

            iterations = Int32.Parse(split[ITERATION_INDEX]);

            byte[] salt = null;

            salt = Convert.FromBase64String(split[SALT_INDEX]);

            byte[] hash = null;

            hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            int storedHashSize = 0;

            storedHashSize = Int32.Parse(split[HASH_SIZE_INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++) {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt)) {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }

  }
}
