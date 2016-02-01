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
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace ACWindowTitleBlockUtil
{
    class Program : ServiceBase
    {
        static void Main(string[] args)
        {
            // new Program().findTitle();
            ServiceBase.Run(new Program());
        }

        public void findTitle()
        {
            string[] blockTitles = new string[] { "nba", "minecraft", "craftbukkit", "学生双语报吧"};
            List<string> finalBlocks = new List<string>();

            foreach (string temp in blockTitles)
            {
                finalBlocks.Add(temp);
                finalBlocks.Add(temp.ToUpper());
                StringBuilder sb = new StringBuilder();
                sb.Append(temp.Substring(0, 1).ToUpper());
                sb.Append(temp.Substring(1));
                //Console.WriteLine(sb.ToString());
                finalBlocks.Add(sb.ToString());
                sb.Clear();
            }

            foreach (string temp in finalBlocks)
            {
                Console.WriteLine(temp);
            }

            while (true)
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    foreach (string want in finalBlocks)
                    {
                        if (p.MainWindowTitle.Contains(want))
                        {
                            if (p.ProcessName.Equals("explorer.exe") || p.ProcessName.Equals("Explorer.exe") || p.ProcessName.Equals("EXPLORER.exe"))
                            {
                                continue;
                            }
                            p.Kill();
                        }
                        else
                        {
                            continue;
                        }
                        Thread.Sleep(2000); // It may contain bugs.
                    }
                }
            }

        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(new ThreadStart(findTitle));
            t.IsBackground = true;
            t.Name = "Find Thread";
            t.Start();
        }
    }
}
