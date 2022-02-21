using ListDebuggerVisualizer_Model;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;


[assembly: DebuggerVisualizer(
	typeof(ListDebuggerVisualizer.ListDebuggerVisualizerClient),
	typeof(ListDebuggerVisualizer_Debuggee.VisualizerJsonObjectSource),
	Target = typeof(List<>),
	Description = "List Debugger Visualizer")
]

namespace ListDebuggerVisualizer
{
	public class ListDebuggerVisualizerClient : DialogDebuggerVisualizer
	{
		override protected void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
		{
			try
			{
				var provider = (IVisualizerObjectProvider2)objectProvider;
				var jsonStream = objectProvider.GetData();
				var reader = new StreamReader(jsonStream);
				string json = reader.ReadToEnd();
				ShowVisualizer(json);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"List Debugger Visualizer Exception while getting object data: {ex.Message}");
			}
		}

		public static void ShowVisualizer(string json)
		{
			var container = JsonConvert.DeserializeObject<VisualizerDataContainer>(json);

			if (container.IsPrimitive || container.TypeName.ToLower() == "string")
			{
				var list = JsonConvert.DeserializeObject<IEnumerable<object>>(container.JsonData);
				var table = PrimitiveListToDataTable(list, container.TypeName);
				ListDebuggerVisualizerClient.ShowVisualizerForm(table);
			}
			else
			{
				var list = JsonConvert.DeserializeObject<IEnumerable<ExpandoObject>>(container.JsonData);
				var table = ExpandosListToDataTable(list, container.TypeName);
				ListDebuggerVisualizerClient.ShowVisualizerForm(table);
			}
		}

		private static void ShowVisualizerForm(DataTable table)
		{
			if (table == null)
			{
				MessageBox.Show("No data.");
			}
			else
			{
				var mf = new MainForm();
				mf.SetData(table);
				mf.ShowDialog();
			}
		}

		private static DataTable ExpandosListToDataTable(IEnumerable<ExpandoObject> list, string tableName)
		{
			var dict = list.Cast<IDictionary<string, object>>();
			return DictionaryListToDataTable(dict, tableName);
		}

		private static DataTable PrimitiveListToDataTable(IEnumerable<object> list, string tableName)
		{
			var dict = list.Select(i => new Dictionary<string, object> { { "Value", i } });
			return DictionaryListToDataTable(dict, tableName);
		}

		private static DataTable DictionaryListToDataTable(IEnumerable<IDictionary<string, object>> list, string tableName)
		{
			if (list == null || !list.Any())
			{
				return null;
			}
			var table = new DataTable(tableName);
			foreach (var prop in list.First())
			{
				table.Columns.Add(new DataColumn(prop.Key, prop.Value.GetType()));
			}

			foreach (var row in list)
			{
				var data = table.NewRow();
				foreach (var prop in row)
				{
					data[prop.Key] = prop.Value;
				}
				table.Rows.Add(data);
			}
			return table;
		}

	}

}
