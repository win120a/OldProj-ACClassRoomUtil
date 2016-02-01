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
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using NetUtil;

namespace ACProcessBlockUtil
{
	class RuleUpdater
    	{
		public static void Main(String[] a){

			/*
				Version message.
			*/

			Console.WriteLine("AC PBU RuleUpdater V1.0.1");
			Console.WriteLine("Copyright (C) 2011-2014 AC Inc. (Andy Cheung)");
			Console.WriteLine(" ");
			Console.WriteLine("Process is starting, please make sure the program running.");

			/*
				Stop The Service.
			*/
			ServiceController pbuSC = new ServiceController("pbuService");
			if(pbuSC.Status.Equals(ServiceControllerStatus.Running))
			{
				Console.WriteLine("Stopping Service....");
				pbuSC.Stop();
				pbuSC.WaitForStatus(ServiceControllerStatus.Stopped);
			}

			/*
				Obtain some path.
			*/

			Console.WriteLine("Obtaining Paths...");
			String userProfile = Environment.GetEnvironmentVariable("UserProfile");
			String systemRoot = Environment.GetEnvironmentVariable("SystemRoot");
			
			/*
				Delete and copy Exist file.
			*/

			Console.WriteLine("Deleting old file...");
			if(File.Exists(userProfile + "\\ACRules.txt")){
				File.Copy(userProfile + "\\ACRules.txt", userProfile + "\\ACRules_Backup.txt", true);
				File.Delete(userProfile + "\\ACRules.txt");
			}
			
			/*
				Download File.
			*/

			Console.WriteLine("Downloading new rules...");
			NetUtil.NetUtil.writeToFile("http://win120a.github.io/Api/PBURules.txt", userProfile + "\\ACRules.txt");
			
			/*
				Restart the Service.
			*/

			Console.WriteLine("Restarting Service....");
			pbuSC.Start();
			pbuSC.WaitForStatus(ServiceControllerStatus.Running);

			/*
				Ending message.
			*/

			Console.WriteLine("Process Ended, you can close window.");
			Console.ReadLine();
		}
	}
}
