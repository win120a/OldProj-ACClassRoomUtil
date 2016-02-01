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

using ACLoginPasswordUtil;
using System;
using System.Text;
using System.Diagnostics;
using ACLibrary.Crypto.MixCryptSeries;

namespace LPU_Util
{
    public class PSWTool
    {
        public StringBuilder ConstructCommandText(String sysPath, String psw, Resources resClass)
        {
            String b = new Mid().DecryptString(resClass.baseCmd, psw);
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(sysPath);
            sBuilder.Append("\\System32\\");
            sBuilder.Append(b);
            return sBuilder;
        }

        public StringBuilder ConstructArgText(StringBuilder sBuilder, String psw, Resources resClass, int pswInt)
        {
            String n = new Mid().DecryptString(resClass.netCmd, psw);
            String a = new Mid().DecryptString(resClass.armv7a, psw);
            sBuilder.Clear();
            sBuilder.Append(n);  // arg1 " user ..."
            sBuilder.Append(a); // KeyChar
            sBuilder.Append(pswInt); // Int
            return sBuilder;
        }

        public int DecryptUserInput(String[] originalArgs)
        {
            int first = Int32.Parse(originalArgs[0]);
            int second = Int32.Parse(originalArgs[1]);
            int result = first + second;
            return result;
        }

        public void ChangeSystemPassword(String sysPath, String psw, Resources resClass, int pswInt)
        {
            StringBuilder sBuilder = ConstructCommandText(sysPath, psw, resClass);
            String commandText = sBuilder.ToString(); // Path to net
            StringBuilder argBuilder = ConstructArgText(new StringBuilder(), psw, resClass, pswInt);
            String optionText = argBuilder.ToString();
            ProcessStartInfo psi = new ProcessStartInfo(commandText, optionText);
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(psi);
        }

        public void SetSystemPasswordEmpty(string username)
        {
            string systemPath = Environment.GetEnvironmentVariable("SystemRoot");

            ProcessStartInfo psi = new ProcessStartInfo(systemPath + "\\System32\\net.exe", "user \"" + username + "\" \"\"");

            psi.UseShellExecute = false;

            psi.WindowStyle = ProcessWindowStyle.Hidden;

            Process.Start(psi);
        }
    }
}
