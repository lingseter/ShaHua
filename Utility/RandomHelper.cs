using System;
using System.Text;

namespace Utility
{
    public class RandomHelper
    {
        private static char[] numAndChar =   
      {   
        '0','1','2','3','4','5','6','7','8','9',   
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
      };

        //生成一个四个字符的字符串
        public static string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }
            return checkCode;
        }

        public static string GenNum(int length = 6)
        {
            return new Random().Next().ToString().Substring(0, length);
        }

        public static string GenNumAndChar(int length = 6)
        {
            StringBuilder sb = new StringBuilder(62);
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(numAndChar[r.Next(62)]);
            }
            return sb.ToString();
        }
    }
}
