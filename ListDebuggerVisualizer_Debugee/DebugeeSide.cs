using ListDebuggerVisualizer_Model;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace ListDebuggerVisualizer_Debuggee
{
	public class VisualizerJsonObjectSource : VisualizerObjectSource
	{
		public override void GetData(object target, Stream outgoingData)
		{
			var itemType = target.GetType().GetProperty("Item").PropertyType;
			var container = new VisualizerDataContainer();
			container.TypeName = itemType.Name;
			container.JsonData = JsonConvert.SerializeObject((IList)target);
			container.IsPrimitive = itemType.IsPrimitive;

			string json = JsonConvert.SerializeObject(container);
			var writer = new StreamWriter(outgoingData);
			writer.WriteLine(json);
			writer.Flush();
		}
	}

}
