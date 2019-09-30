using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace ListDebuggerVisualizer
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// if exceptions occures when Visualizer form is visible (and modal) Visual Studio chrashes when closing form, and user cannot close Visualizer form. It's annoying, very.
		/// </summary>
		private bool formLoaded = false;
		public IList Model { get; set; }
		public string ListType { get; set; }

		private RadGridView grid;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (this.Model == null)
			{
				return;
			}
			InitGrid();
			this.grid.DataSource = this.Model;
			AutoFitColumns();
			ReadSettings();
			toolStripLabelTypeName.Text = "List item type: " + this.ListType;
			this.formLoaded = true;
		}

		private void InitGrid()
		{
			ThemeResolutionService.ApplicationThemeName = "Office2010Blue";

			this.grid = new RadGridView();
			panelContent.Controls.Add(this.grid);

			this.grid.ReadOnly = true;
			this.grid.Dock = DockStyle.Fill;
			this.grid.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;

			this.grid.AllowSearchRow = true;

			this.grid.EnableFiltering = true;
			this.grid.MasterTemplate.ShowHeaderCellButtons = true;
			this.grid.MasterTemplate.ShowFilteringRow = false;

			this.grid.AutoGenerateHierarchy = true;
		}

		private void AutoFitColumns()
		{
			this.grid.MasterTemplate.BestFitColumns(BestFitColumnMode.AllCells);
		}

		private void ReadSettings()
		{
			var settings = GetSettingsList();
			if (settings == null)
				return;

			var mySetting = settings.FirstOrDefault(ltis => ltis.Name == this.ListType);
			if (mySetting != null)
			{
				this.Location = mySetting.Location;
				this.Size = mySetting.Size;
				this.grid.LoadLayout(mySetting.GridSettingsFile);
			}
		}

		private void SaveSettings()
		{
			ListTypeItemSettings mySetting = null;
			var settingsFile = new FileInfo(GetSettingsStorageFile());

			var settings = GetSettingsList();
			if (settings != null)
			{
				mySetting = settings.FirstOrDefault(ltis => ltis.Name == this.ListType);
			}
			else
			{
				settings = new List<ListTypeItemSettings>();
			}

			if (mySetting == null)
			{
				mySetting = new ListTypeItemSettings();
				mySetting.Name = this.ListType;
				mySetting.GridSettingsFile = settingsFile.DirectoryName + "\\grid_settings_" + mySetting.Name + ".xml";
				settings.Add(mySetting);
			}

			mySetting.Location = this.Location;

			if (this.WindowState == FormWindowState.Normal)
			{
				mySetting.Size = this.Size;
			}
			else
			{
				mySetting.Size = this.RestoreBounds.Size;
			}
			string xml = SerializeToXml(settings);
			File.WriteAllText(settingsFile.FullName, xml);
			this.grid.SaveLayout(mySetting.GridSettingsFile);
		}

		private List<ListTypeItemSettings> GetSettingsList()
		{
			string settingsFile = GetSettingsStorageFile();
			if (File.Exists(settingsFile))
			{
				return DeserializeFromXml<List<ListTypeItemSettings>>(settingsFile);
			}
			else
			{
				return null;
			}
		}

		private string GetSettingsStorageFile()
		{
			string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(assemblyFolder, "ListDebuggerVisualizerSettings.xml");
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.formLoaded)
			{
				SaveSettings();
			}
		}

		public static string SerializeToXml(object obj)
		{
			var serializer = new XmlSerializer(obj.GetType());
			StringWriter sw = new StringWriter();
			serializer.Serialize(sw, obj);
			return sw.ToString();
		}

		public static T DeserializeFromXml<T>(string xmlFilePath)
		{
			var serializer = new XmlSerializer(typeof(T));
			T obj = default(T);
			using (TextReader textReader = new StreamReader(xmlFilePath))
			{
				obj = (T)serializer.Deserialize(textReader);
			}
			return obj;
		}

		private void toolStripButtonExportToExcel_Click(object sender, EventArgs e)
		{
			var pack = new ExcelPackage();
			var ws = pack.Workbook.Worksheets.Add(this.ListType);

			int col = 1;
			int row = 1;
			var itemType = this.Model.GetType().GetProperty("Item").PropertyType;
			var properties = itemType.GetProperties();
			foreach (var prop in properties)
			{
				ws.Cells[row, col].Value = prop.Name;
				col++;
			}

			row++;
			col = 1;
			foreach (var item in this.Model)
			{
				foreach (var prop in properties)
				{
					var value = prop.GetValue(item, new object[0]);
					if (value != null)
						ws.Cells[row, col].Value = value.ToString();
					col++;
				}
				row++;
				col = 1;
			}

			var sd = new SaveFileDialog();
			sd.FileName = this.ListType + ".xlsx";
			sd.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
			if (sd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				using (FileStream fs = new FileStream(sd.FileName, FileMode.Create))
				{
					pack.SaveAs(fs);
				}
			}
		}

		private void toolStripButtonClearTypeSettings_Click(object sender, EventArgs e)
		{
			this.grid.DataSource = null;
			this.grid.DataSource = this.Model;
			AutoFitColumns();
			SaveSettings();
		}

	}

	[Serializable]
	public class ListTypeItemSettings
	{
		public string Name { get; set; }
		public Point Location { get; set; }
		public Size Size { get; set; }
		public string GridSettingsFile { get; set; }
	}
}
