using ListDebuggerVisualizer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListDebuggerVisualizer_Tester
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			ListDebuggerVisualizerClient.ShowVisualizer(GetJson("list1.json"));
			ListDebuggerVisualizerClient.ShowVisualizer(GetJson("list2.json"));
			ListDebuggerVisualizerClient.ShowVisualizer(GetJson("list3.json"));
		}

		private static string GetJson(string filename)
		{
			string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data\\" + filename);
			return File.ReadAllText(fullPath);
		}
	}
}
