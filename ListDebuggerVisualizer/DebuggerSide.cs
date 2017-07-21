using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;


[assembly: DebuggerVisualizer(typeof(ListDebuggerVisualizer.ListDebuggerVisualizerClient), typeof(VisualizerObjectSource), Target = typeof(List<>),
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
      var data = (IList)objectProvider.GetObject();
      var itemType = data.GetType().GetProperty("Item").PropertyType;
      if (itemType == typeof(string))
      {
        var prim = new List<PrimitiveListItem>();
        foreach (string strItem in data.Cast<string>())
        {
          var pli = new PrimitiveListItem();
          pli.Value = strItem;
          prim.Add(pli);
        }
        ListDebuggerVisualizerClient.ShowVisualizerForm(prim, itemType.Name);
      }
      else if (itemType.IsPrimitive)
      {
        var prim = new List<PrimitiveListItem>();
        foreach (object objItem in data.Cast<object>())
        {
          var pli = new PrimitiveListItem();
          pli.Value = objItem.ToString();
          prim.Add(pli);
        }
        ListDebuggerVisualizerClient.ShowVisualizerForm(prim, itemType.Name);
      }
      else
      {
        ListDebuggerVisualizerClient.ShowVisualizerForm(data, itemType.Name);
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
}
