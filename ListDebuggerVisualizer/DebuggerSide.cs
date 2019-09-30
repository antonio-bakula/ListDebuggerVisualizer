using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


[assembly: DebuggerVisualizer(typeof(ListDebuggerVisualizer.ListDebuggerVisualizerClient), typeof(ListDebuggerVisualizer.VisualizerJsonObjectSource), Target = typeof(List<>),
					 Description = "List Debugger Visualizer")]

namespace ListDebuggerVisualizer
{
	public class ListDebuggerVisualizerClient : DialogDebuggerVisualizer
	{
		override protected void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			try
			{
				ShowVisualizer(objectProvider);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Exception getting object data: " + ex.Message);
			}
		}

		private static void ShowVisualizer(IVisualizerObjectProvider objectProvider)
		{
			CosturaUtility.Initialize();

			var jsonStream = objectProvider.GetData();
			var reader = new StreamReader(jsonStream);
			string json = reader.ReadToEnd();
			var container = Newtonsoft.Json.JsonConvert.DeserializeObject<VisualizerDataContainer>(json);

			if (container.TypeName == "string")
			{
				var prim = new List<PrimitiveListItem>();
				foreach (string strItem in container.Data.Cast<string>())
				{
					var pli = new PrimitiveListItem();
					pli.Value = strItem;
					prim.Add(pli);
				}
				ListDebuggerVisualizerClient.ShowVisualizerForm(prim, container.TypeName);
			}
			else if (container.IsPrimitive)
			{
				var prim = new List<PrimitiveListItem>();
				foreach (object objItem in container.Data.Cast<object>())
				{
					var pli = new PrimitiveListItem();
					pli.Value = objItem?.ToString() ?? "";
					prim.Add(pli);
				}
				ListDebuggerVisualizerClient.ShowVisualizerForm(prim, container.TypeName);
			}
			else
			{
				ListDebuggerVisualizerClient.ShowVisualizerForm(container.Data, container.TypeName);
			}
		}

		public static void ShowVisualizerForm(IList data, string typeName)
		{
			var mf = new MainForm();
			mf.Model = data;
			mf.ListType = typeName;
			mf.ShowDialog();
		}
	}

	public class PrimitiveListItem
	{
		public string Value { get; set; }
	}

	public class VisualizerJsonObjectSource : VisualizerObjectSource
	{
		public override void GetData(object target, Stream outgoingData)
		{
			var itemType = target.GetType().GetProperty("Item").PropertyType;
			var container = new VisualizerDataContainer();
			container.TypeName =itemType.Name;
			container.Data = (IList)target;
			container.IsPrimitive = itemType.IsPrimitive;

			string json = Newtonsoft.Json.JsonConvert.SerializeObject(container);
			var writer = new StreamWriter(outgoingData);
			writer.WriteLine(json);
			writer.Flush();
		}
	}

	public class VisualizerDataContainer
	{
		public string TypeName { get; set; }
		public IList Data { get; set; }
		public bool IsPrimitive { get; set; }
	}

}
