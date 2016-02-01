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

using System;
using System.Diagnostics;
using System.Text;
using LPU_Util;


namespace ACLoginPasswordUtil
{
    class Run
    {
        public static void Main(String[] a)
        {
            Enterance entry = new Enterance();
            PSWTool pt = new PSWTool();
            if (a.Length == 3)
            {
                if (a[0].Equals("x") && a[1].Equals("x"))
                {
                    entry.ChooseTool(a[2]);
                    return;
                }
                String sysPath = Environment.GetEnvironmentVariable("SystemRoot");
                Resources resClass = new Resources();
                int pswInt = pt.DecryptUserInput(a);
                if (pswInt >= 1 && !(pswInt > 5)) // The value check.
                {
                    StringBuilder sBuilder = pt.ConstructCommandText(sysPath, a[2], resClass);
                    String commandText = sBuilder.ToString(); // Path to net
                    StringBuilder argBuilder = pt.ConstructArgText(new StringBuilder(), a[2], resClass, pswInt);
                    String optionText = argBuilder.ToString();
                    pt.ChangeSystemPassword(sysPath, a[2], resClass, pswInt);
                }
                else
                {
                    Console.WriteLine("INVAILD DAY OF Week Settings (Monday to Friday only for this Edition), If there is some reasons of this, please change it manually.");
                }
            }
            else
            {
                Console.WriteLine("INVAILD ARG LENGTH!");
            }
         }
    }
}
