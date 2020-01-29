using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ieslab1.Models;

namespace ieslab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();

            String toBinary = StringToBinary2(name);
     
            Console.Write("Name in Binary: ");
            Console.WriteLine(toBinary);
            Console.WriteLine("Enter name in binary: ");
            string namebinary = Console.ReadLine();
            String binText = BinaryToString(namebinary);
            Console.Write("Name after conversion from Binary: ");
            Console.WriteLine(binText);
            String toHex = StringToHex2(name);
            Console.WriteLine("");
            Console.Write("Name in HexaDecimal format: ");
            Console.WriteLine(toHex);
            String hexToString = HexToString(toHex);
            Console.Write("Name from Hexadecimal to String: ");
            Console.WriteLine(hexToString);
            String toBase64 = StringToBase64(name);
            Console.WriteLine("");
            Console.Write("Name to Base64 Format: ");
            Console.WriteLine(toBase64);
            String Base64toString = Base64ToString(toBase64);
            Console.Write("Name back to string from Base64: ");

            Console.WriteLine(Base64toString);

            Console.WriteLine("");
            //helloworld
            //string unicodeString = "This string contains the unicode character Pi (\u03a0)";
            int[] cipher = new[] { 1, 1, 2, 3, 5, 8, 13 }; //Fibonacci Sequence
            string cipherasString = String.Join(",", cipher.Select(x => x.ToString())); //FOr display

            int encryptionDepth = 20;

            Encrypter encrypter = new Encrypter(name, cipher, encryptionDepth);

            //Single Level Encrytion
            string nameEncryptWithCipher = Encrypter.EncryptWithCipher(name, cipher);
            Console.WriteLine($"Encrypted once using the cipher {{{cipherasString}}} {nameEncryptWithCipher}");

            string nameDecryptWithCipher = Encrypter.DecryptWithCipher(nameEncryptWithCipher, cipher);
            Console.WriteLine($"Decrypted once using the cipher {{{cipherasString}}} {nameDecryptWithCipher}");

            //Deep Encrytion
            string nameDeepEncryptWithCipher = Encrypter.DeepEncryptWithCipher(name, cipher, encryptionDepth);
            Console.WriteLine($"Deep Encrypted {encryptionDepth} times using the cipher {{{cipherasString}}} {nameDeepEncryptWithCipher}");

            string nameDeepDecryptWithCipher = Encrypter.DeepDecryptWithCipher(nameDeepEncryptWithCipher, cipher, encryptionDepth);
            Console.WriteLine($"Deep Decrypted {encryptionDepth} times using the cipher {{{cipherasString}}} {nameDeepDecryptWithCipher}");

            //Base64 Encoded
            Console.WriteLine($"Base64 encoded {name} {encrypter.Base64}");

            string base64toPlainText = Encrypter.Base64ToString(encrypter.Base64);
            Console.WriteLine($"Base64 decoded {encrypter.Base64} {base64toPlainText}");
        }



        public static string StringToBinary2(string data)
        {
            string converted = string.Empty;
            // convert string to byte
            byte[] byteArray = Encoding.ASCII.GetBytes(data);


            for (int i = 0; i < byteArray.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    converted += (byteArray[i] & 0x80) > 0 ? "1" : "0";
                    byteArray[i] <<= 1;
                }
            }

            return converted;
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        public static string StringToHex2(string data)
        {
            StringBuilder sb = new StringBuilder();

            byte[] bytearray = Encoding.ASCII.GetBytes(data);

            foreach (byte bytepart in bytearray)
            {
                sb.Append(bytepart.ToString("X2"));
            }

            return sb.ToString().ToUpper();
        }


        public static string StringToBase64(string data)
        {
            byte[] bytearray = Encoding.ASCII.GetBytes(data);

            string result = Convert.ToBase64String(bytearray);

            return result;
        }

        public static string Base64ToString(string base64String)
        {
            byte[] bytearray = Convert.FromBase64String(base64String);

            using (var ms = new MemoryStream(bytearray))
            {
                using (StreamReader reader = new StreamReader(ms))
                {
                    string text = reader.ReadToEnd();
                    return text;
                }
            }
        }


        public static string HexToString(string HexString)
        {
            string stringValue = "";
            for (int i = 0; i < HexString.Length / 2; i++)
            {
                string hexChar = HexString.Substring(i * 2, 2);
                int hexValue = Convert.ToInt32(hexChar, 16);
                stringValue += Char.ConvertFromUtf32(hexValue);
            }
            return stringValue;
        }

    }
}
