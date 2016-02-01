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
using System.ServiceProcess;
using System.Threading;

namespace ACProcessBlockUtil
{
    class Run:ServiceBase
    {
        public static void kill()
        {
            //if(File.Exists("")){}
            while (true)
            {
                Process[] ieProcArray = Process.GetProcessesByName("iexplore");
                //Console.WriteLine(ieProcArray.Length);
                if (ieProcArray.Length == 0)
                {
                    continue;
                }
                foreach(Process p in ieProcArray){
                    p.Kill();
                }
                Thread.Sleep(2000);
            }
        }

        public static void Main(String[] a)
        {
            ServiceBase.Run(new Run());
        }

        protected override void OnStart(String[] a){
            Thread t = new Thread(new ThreadStart(kill));
            t.IsBackground = true;
            t.Start();
        }
    }
}