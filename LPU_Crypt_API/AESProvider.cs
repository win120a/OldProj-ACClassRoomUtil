/*
   Copyright (C) 2011-2014 AC Inc. (Andy Cheung)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

/*
 * Some part of code come from MVA's 20 C# Question Explained.
 * Copyright (C) Microsoft Corporation
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace LPU_Crypt_API
{
    public class AESProvider
    {
        static Aes CreateAES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Aes aes = new AesCryptoServiceProvider();
            aes.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            aes.IV = new byte[aes.BlockSize / 8];
            return aes;
        }


        public string EncryptString(string plainText, string password)
        {
            // first we convert the plain text into a byte array
            byte[] plainTextBytes = Encoding.Unicode.GetBytes(plainText);

            // use a memory stream to hold the bytes
            MemoryStream myStream = new MemoryStream();

            // create the key and initialization vector using the password
            Aes aes = CreateAES(password);

            // create the encoder that will write to the memory stream
            CryptoStream cryptStream = new CryptoStream(myStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            // we now use the crypto stream to write our byte array to the memory stream
            cryptStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptStream.FlushFinalBlock();

            // change the encrypted stream to a printable version of our encrypted string
            return Convert.ToBase64String(myStream.ToArray());
        }

        public string DecryptString(string encryptedText, string password)
        {
            // convert our encrypted string to a byte array
            byte[] encryptedTextBytes = Convert.FromBase64String(encryptedText);

            // create a memory stream to hold the bytes
            MemoryStream myStream = new MemoryStream();

            // create the key and initialization vector using the password
            Aes aes = CreateAES(password);

            // create our decoder to write to the stream
            CryptoStream decryptStream = new CryptoStream(myStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

            // we now use the crypto stream to the byte array
            decryptStream.Write(encryptedTextBytes, 0, encryptedTextBytes.Length);
            decryptStream.FlushFinalBlock();

            // convert our stream to a string value
            return Encoding.Unicode.GetString(myStream.ToArray());
        }
    }
}
