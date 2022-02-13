using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MySecurityLib
{
    public static class Class1
    {
       // public static bool GenerateOTP()
       // {
            //var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            //var stringChars1 = new char[6];
            //var random1 = new Random();

            //for (int i = 0; i < stringChars1.Length; i++)
            //{
            //    stringChars1[i] = chars1[random1.Next(chars1.Length)];
            //}

            //var str = new String(stringChars1);
       //     return true;
      //  }
        public static bool GetOTP(out string Finalotp, string otpType ="1", int len = 5)
        {
            //otptype 1 = alpha numeric
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            if (otpType == "1")
            {
                characters += alphabets + small_alphabets + numbers;
            }
            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
                
            }
            Finalotp = otp;
            return true;
        }

        public static bool Encrypt(string input, out string securedText)
        {
            string key = "sblw-3hn8-sqoy19";
            //string key = "b14ca5898a4e4133bbce2ea2315a1916";
           
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            securedText = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return true;
        }
        public static bool Decrypt(string input, out string plainText)
        {
            string key = "sblw-3hn8-sqoy19";
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            plainText = UTF8Encoding.UTF8.GetString(resultArray);
            return true;
        }
    }
}
