# Random Code Generator

This code snipit creates non sequential random strings which could be used to create IDs. They are not gauranteed to be unique. 

You can change the characters array to be only characters you prefer, in this example I've changed it to only use characters that can't be confused with each other, and only one case. 

Original reference is this Stack Overflow post https://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings

```csharp
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
```