using System.Security.Cryptography;
using System.Text;

namespace QuizServer.Security
{
    public class SHA512Encrypter
    {
       
        public static string Encrypt(string data)
        {
            SHA512 shaM = new SHA512Managed();
            byte[] hash = shaM.ComputeHash(Encoding.ASCII.GetBytes(data));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}