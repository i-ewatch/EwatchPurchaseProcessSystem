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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EwatchPurchase.Excel.Test
{
    public partial class Form1 : Form
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
        /// 檔案名稱完整路徑
        /// </summary>
        public string[] Header { get; set; }
        /// <summary>
        /// Ecexl檔案
        /// </summary>
        public XSSFWorkbook xworkbook { get; set; }//xlsx
        public List<ICell> cell1 = new List<ICell>();
        public List<ICell> cell2 = new List<ICell>();
        public List<ICell> cell3 = new List<ICell>();
        public List<ICell> cell4 = new List<ICell>();
        public List<ICell> cell5 = new List<ICell>();
        public List<ICell> cell6 = new List<ICell>();
        public List<ICell> cell7 = new List<ICell>();
        public List<ICell> cell8 = new List<ICell>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Importbutton_Click(object sender, EventArgs e)
        {
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
                        }
                    }
                }
                catch (DirectoryNotFoundException ex) { Log.Error(ex, $" [{ReportPath}] KWH查無此資料夾路徑"); }
                catch (FileNotFoundException ex) { Log.Error(ex, $"KWH查無此資料檔案"); }
                catch (Exception ex) { Log.Error(ex, $"KWH資料匯入失敗  檔案名稱{FieldName}"); }
            }
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = Convert.ToString(cell1[0]);
            dataGridView1.Columns[1].Name = Convert.ToString(cell2[0]);
            dataGridView1.Columns[2].Name = Convert.ToString(cell3[0]);
            dataGridView1.Columns[3].Name = Convert.ToString(cell4[0]);
            dataGridView1.Columns[4].Name = Convert.ToString(cell5[0]);
            dataGridView1.Columns[5].Name = Convert.ToString(cell6[0]);
            dataGridView1.Columns[6].Name = Convert.ToString(cell7[0]);
            dataGridView1.Columns[7].Name = Convert.ToString(cell8[0]);
            for (int i = 1; i < cell1.Count; i++)
            {
                dataGridView1.Rows.Add(cell1[i], cell2[i], cell3[i], cell4[i], cell5[i], cell6[i], cell7[i], cell8[i]);
            }
        }
    }
}
