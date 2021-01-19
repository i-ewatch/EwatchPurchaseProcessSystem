using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using EwatchPurchase.SQL.Test.Configuration;
using EwatchPurchase.SQL.Test.Method;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EwatchPurchase.SQL.Test
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 檔案瀏覽
        /// </summary>
        public OpenFileDialog Openfile;
        /// <summary>
        /// 報表資料夾路徑
        /// </summary>
        public string ReportPath { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// Ecexl檔案
        /// </summary>
        public XSSFWorkbook xworkbook { get; set; }//xlsx
        /// <summary>
        /// 資料抓取List
        /// </summary>
        public List<ICell> cell1 = new List<ICell>();
        public List<ICell> cell2 = new List<ICell>();
        public List<ICell> cell3 = new List<ICell>();
        public List<ICell> cell4 = new List<ICell>();
        public List<ICell> cell5 = new List<ICell>();
        public List<ICell> cell6 = new List<ICell>();
        public List<ICell> cell7 = new List<ICell>();
        public List<ICell> cell8 = new List<ICell>();
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        public MainForm()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
        }

        private void OpenFilesimpleButton_Click(object sender, EventArgs e)
        {
            InsertSQLsimpleButton.Enabled = true;
            if (gridControl1.DataSource != null)
            {
                gridView1.Columns.Clear();
            }
            #region excel資料匯入
            Openfile = new OpenFileDialog() { Filter = "*.Xlsx| *.xlsx" };
            if (Openfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ReportPath = Openfile.FileName;
                    if (ReportPath != null)
                    {
                        using (FileStream file = new FileStream($"{ReportPath}", FileMode.Open, FileAccess.Read))
                        {
                            xworkbook = new XSSFWorkbook(file);//Ecexl檔案載入
                            int sheet = xworkbook.NumberOfSheets;//取得分頁數量
                            for (int Sheetnum = 1; Sheetnum < 2; Sheetnum++)
                            {
                                var data = xworkbook.GetSheetAt(Sheetnum);//載入分頁資訊
                                for (int Rownum = 9; Rownum < data.LastRowNum; Rownum++)//每一行資料
                                {
                                    IRow row = data.GetRow(Rownum);
                                    #region 資料抓取
                                    if (row.GetCell(0).ToString() == "" & row.GetCell(1).ToString() == "" & row.GetCell(2).ToString() == "" & row.GetCell(3).ToString() == "" & row.GetCell(4).ToString() == "" & row.GetCell(5).ToString() == "" & row.GetCell(6).ToString() == "" & row.GetCell(7).ToString() == "")
                                    {
                                    }
                                    else
                                    {
                                        if (row.GetCell(7).ToString() == "以下空白")
                                        {
                                            cell1.Add(row.GetCell(0));
                                            cell2.Add(row.GetCell(1));
                                            cell3.Add(row.GetCell(2));
                                            cell4.Add(row.GetCell(3));
                                            if (row.GetCell(4).CellType == CellType.Formula)
                                            {
                                                row.GetCell(4).SetCellType(CellType.String);
                                                cell5.Add(row.GetCell(4));
                                            }
                                            else
                                            {
                                                cell5.Add(row.GetCell(4));
                                            }
                                            if (row.GetCell(5).CellType == CellType.Formula)
                                            {
                                                row.GetCell(5).SetCellType(CellType.String);
                                                cell6.Add(row.GetCell(5));
                                            }
                                            else
                                            {
                                                cell6.Add(row.GetCell(5));
                                            }
                                            cell7.Add(row.GetCell(6));
                                            cell8.Add(row.GetCell(7));
                                            break;
                                        }
                                        else
                                        {
                                            cell1.Add(row.GetCell(0));
                                            cell2.Add(row.GetCell(1));
                                            cell3.Add(row.GetCell(2));
                                            cell4.Add(row.GetCell(3));
                                            if (row.GetCell(4).CellType == CellType.Formula)
                                            {
                                                row.GetCell(4).SetCellType(CellType.String);
                                                cell5.Add(row.GetCell(4));
                                            }
                                            else
                                            {
                                                cell5.Add(row.GetCell(4));
                                            }
                                            if (row.GetCell(5).CellType == CellType.Formula)
                                            {
                                                row.GetCell(5).SetCellType(CellType.String);
                                                cell6.Add(row.GetCell(5));
                                            }
                                            else
                                            {
                                                cell6.Add(row.GetCell(5));
                                            }
                                            cell7.Add(row.GetCell(6));
                                            cell8.Add(row.GetCell(7));
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
                catch (DirectoryNotFoundException ex) { Log.Error(ex, $" [{ReportPath}] 查無此資料夾路徑"); }
                catch (FileNotFoundException ex) { Log.Error(ex, $"查無此資料檔案"); }
                catch (Exception ex) { Log.Error(ex, $"資料匯入失敗  檔案名稱{FieldName}"); }
            }
            #endregion
            #region GridView顯示匯入excel資料
            labelControl2.Text = ReportPath.Split('.')[0].Split('\\')[ReportPath.Split('.')[0].Split('\\').Length - 1];
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(Convert.ToString(cell1[0]));
            dataTable.Columns.Add(Convert.ToString(cell2[0]));
            dataTable.Columns.Add(Convert.ToString(cell3[0]));
            dataTable.Columns.Add(Convert.ToString(cell4[0]));
            dataTable.Columns.Add(Convert.ToString(cell5[0]));
            dataTable.Columns.Add(Convert.ToString(cell6[0]));
            dataTable.Columns.Add(Convert.ToString(cell7[0]));
            dataTable.Columns.Add(Convert.ToString(cell8[0]));
            for (int i = 1; i < cell1.Count; i++)
            {
                dataTable.Rows.Add(cell1[i], cell2[i], cell3[i], cell4[i], cell5[i], cell6[i], cell7[i], cell8[i]);
            }
            gridControl1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns[0].BestFit();
            gridView1.Columns[1].BestFit();
            gridView1.Columns[2].BestFit();
            gridView1.Columns[3].BestFit();
            gridView1.Columns[4].BestFit();
            gridView1.Columns[5].BestFit();
            gridView1.Columns[6].BestFit();
            gridView1.Columns[7].BestFit();
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            #endregion
        }
        private void InsertSQLsimpleButton_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "")
            {
                MessageBox.Show("專案名稱未填入!!");
            }
            else
            {
                SQLSettings = InitialMethod.InitialSQLSetting();
                SQLMethod = new SQLMethod() { setting = SQLSettings };
                SQLMethod.SQLConnect();
                int j = SQLMethod.Count_Costofferform().Select(g => g.pk).Count();
                if (j == 0)
                {
                    j = 1;
                    string NO = textEdit1.Text;
                    for (int i = 1; i < cell1.Count; i++)
                    {
                        string content = $"{j}, '{NO}','{cell1[i].ToString()}', '{cell2[i].ToString()}','{cell3[i].ToString()}','{cell4[i].ToString()}','{cell5[i].ToString()}','{cell6[i].ToString()}','{cell7[i].ToString()}','{cell8[i].ToString()}'";
                        SQLMethod.Insert_costofferforms(content);
                        j += 1;
                    }
                }
                else
                {
                    j = SQLMethod.Count_Costofferform().Select(g => g.pk).Count() +1;
                    string NO = textEdit1.Text;
                    for (int i = 1; i < cell1.Count; i++)
                    {
                        string content = $"{j}, '{NO}','{cell1[i].ToString()}', '{cell2[i].ToString()}','{cell3[i].ToString()}','{cell4[i].ToString()}','{cell5[i].ToString()}','{cell6[i].ToString()}','{cell7[i].ToString()}','{cell8[i].ToString()}'";
                        SQLMethod.Insert_costofferforms(content);
                        j += 1;
                    }
                }
            }
        }
    }
}
