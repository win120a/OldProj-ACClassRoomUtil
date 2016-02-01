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

namespace LPU_Crypt_API
{
    public class MixCrypt_Strongest
    {
        public String encrypt(String plainText, String password)
        {
            // 3Rijndael
            RijndaelProvider rp = new RijndaelProvider();
            string rp1 = rp.EncryptString(plainText, password);
            string rp2 = rp.EncryptString(rp1, password);
            string rp3 = rp.EncryptString(rp2, password);

            // 3RC2
            RC2Provider rc2 = new RC2Provider();
            string rc2_1 = rc2.EncryptString(rp3, password);
            string rc2_2 = rc2.EncryptString(rc2_1, password);
            string rc2_3 = rc2.EncryptString(rc2_2, password);

            // 3DES
            DESProvider des = new DESProvider();
            string des1 = des.EncryptString(rc2_3, password);
            string des2 = des.EncryptString(des1, password);
            string des3 = des.EncryptString(des2, password);

            // 3AES
            AESProvider aes = new AESProvider();
            string aes1 = aes.EncryptString(des3, password);
            string aes2 = aes.EncryptString(aes1, password);
            string aes3 = aes.EncryptString(aes2, password);

            return aes3;
        }

        public String decrypt(String Source, String password)
        {
            // string plain = testEncrypt.DecryptString(encText, password);

            // 3AES
            AESProvider aes = new AESProvider();
            string aes1 = aes.DecryptString(Source, password);
            string aes2 = aes.DecryptString(aes1, password);
            string aes3 = aes.DecryptString(aes2, password);

            // 3DES
            DESProvider des = new DESProvider();
            string des1 = des.DecryptString(aes3, password);
            string des2 = des.DecryptString(des1, password);
            string des3 = des.DecryptString(des2, password);

            // 3RC2
            RC2Provider rc2 = new RC2Provider();
            string rc2_1 = rc2.DecryptString(des3, password);
            string rc2_2 = rc2.DecryptString(rc2_1, password);
            string rc2_3 = rc2.DecryptString(rc2_2, password);

            // 3Rijndael
            RijndaelProvider rp = new RijndaelProvider();
            string rp1 = rp.DecryptString(rc2_3, password);
            string rp2 = rp.DecryptString(rp1, password);
            string rp3 = rp.DecryptString(rp2, password);

            return rp3;
        }
    }
}
