using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace UniqueNonSequentialStrings
{
    class Program
    {
        internal static readonly char[] chars =
            //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray(); 
            "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

        public static string GetUniqueKey(int size)
        {            
            byte[] data = new byte[4*size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }

            return result.ToString();
        }

        public static string GetUniqueKeyOriginal_BIASED(int size)
        {
            char[] chars =
                //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray(); 
                "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // x = ((id + salt) * multiplier) ModuleHandle pow()
            var dups = 0;
            var values = 0;
            var lastvalue = String.Empty;
            var list = new List<string>();
            for (int i = 0; i < 10000000; i++) {
                var value = GetUniqueKey(8);
                if (list.Contains(value)) {
                    Console.WriteLine($"Value Duplicate {value}");
                    dups++;
                    continue;
                }
                list.Add(value);
                lastvalue = value;
                values++;
                if (values % 10000 == 0) {
                    Console.WriteLine($"Created {values} unique codes with {dups} duplicate values like {value}.");
                }
            }

            Console.WriteLine($"Created {values} unique codes with {dups} duplicate values like {lastvalue}. Press any key to exit...");
            Console.ReadLine();
        }
    }
}
