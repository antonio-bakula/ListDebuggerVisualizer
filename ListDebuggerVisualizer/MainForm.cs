using ListDebuggerVisualizer_Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
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
		private DataTable data;
		private string listType { get; set; }

		private RadGridView grid;

		public MainForm()
		{
			InitializeComponent();
		}

		public void SetData(DataTable table)
		{
			this.data = table;
			this.listType = this.data.TableName;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			if (this.data == null)
			{
				return;
			}
			InitGrid();

			this.grid.DataSource = this.data;
			this.grid.AutoGenerateHierarchy = true;

			AutoFitColumns();
			ReadSettings();
			toolStripLabelTypeName.Text = "List item type: " + this.listType;
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

			var mySetting = settings.FirstOrDefault(ltis => ltis.Name == this.listType);
			if (mySetting != null)
			{
				this.Location = mySetting.Location;
				this.Size = mySetting.Size;
				if (File.Exists(mySetting.GridSettingsFile))
				{
					this.grid.LoadLayout(mySetting.GridSettingsFile);
				}
			}
		}

		private void SaveSettings()
		{
			ListTypeItemSettings mySetting = null;
			var settingsFile = GetSettingsStorageFile();

			var settings = GetSettingsList();
			if (settings != null)
			{
				mySetting = settings.FirstOrDefault(ltis => ltis.Name == this.listType);
			}
			else
			{
				settings = new List<ListTypeItemSettings>();
			}

			if (mySetting == null)
			{
				mySetting = new ListTypeItemSettings();
				mySetting.Name = this.listType;
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
			string json = JsonConvert.SerializeObject(settings);
			File.WriteAllText(settingsFile.FullName, json);
			this.grid.SaveLayout(mySetting.GridSettingsFile);
		}

		private List<ListTypeItemSettings> GetSettingsList()
		{
			var settingsFile = GetSettingsStorageFile();
			if (settingsFile.Exists)
			{
				string json = File.ReadAllText(settingsFile.FullName);
				return JsonConvert.DeserializeObject<List<ListTypeItemSettings>>(json);
			}
			else
			{
				return null;
			}
		}

		private FileInfo GetSettingsStorageFile()
		{
			string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var settingsFile = new FileInfo(Path.Combine(assemblyFolder, "ListDebuggerVisualizer\\settings.json"));
			if (!settingsFile.Directory.Exists)
			{
				settingsFile.Directory.Create();
			}
			return settingsFile;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.formLoaded)
			{
				SaveSettings();
			}
		}

		private void toolStripButtonExportToExcel_Click(object sender, EventArgs e)
		{
			using (ExcelPackage pack = new ExcelPackage())
			{
				ExcelWorksheet ws = pack.Workbook.Worksheets.Add(this.listType);
				ws.Cells["A1"].LoadFromDataTable(this.data, true);

				var sd = new SaveFileDialog();
				sd.FileName = this.listType + ".xlsx";
				sd.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
				if (sd.ShowDialog() == DialogResult.OK)
				{
					using (FileStream fs = new FileStream(sd.FileName, FileMode.Create))
					{
						pack.SaveAs(fs);
					}
				}
			}
		}

		private void toolStripButtonClearTypeSettings_Click(object sender, EventArgs e)
		{
			this.grid.DataSource = null;
			this.grid.DataSource = this.data;
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
