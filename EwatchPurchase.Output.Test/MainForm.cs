using EwatchPurchase.Output.Test.Configuration;
using EwatchPurchase.Output.Test.EF_Model.PurchaseProcessSystemDBModel;
using EwatchPurchase.Output.Test.Method;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EwatchPurchase.Output.Test
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        /// <summary>
        /// Costofferform資料List
        /// </summary>
        private List<Costofferform> costofferforms = new List<Costofferform>();
        /// <summary>
        /// Group Costofferform資料List
        /// </summary>
        private List<Costofferform> groupcostofferform = new List<Costofferform>();
        /// <summary>
        /// Purchaseplans資料List
        /// </summary>
        private List<PurchasePlan> purchaseplans = new List<PurchasePlan>();
        /// <summary>
        /// Purchaseplans Table表pk值
        /// </summary>
        int pk_number = 1;
        /// <summary>
        /// Purchaseplans Table表ProjectItem值
        /// </summary>
        int ProjectItem_number = 1;
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

        private void PurchasePlansimpleButton_Click(object sender, EventArgs e)
        {
            PlanOutputsimpleButton.Enabled = true;
            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();

            #region 將資料從請購單匯入至請購計畫資料庫
            costofferforms = SQLMethod.Count_Costofferform();
            purchaseplans = SQLMethod.Count_purchaseplan();
            groupcostofferform = SQLMethod.Group_costofferform();
            pk_number = purchaseplans.Select(g => g.pk).Count();
            if (pk_number != 0)
            {
                pk_number = purchaseplans.Select(g => g.pk).Count() +1;
            }
            else
            {
                pk_number = 1;
            }
            for (int i = 0; i < groupcostofferform.Count; i++)
            {
                for (int j = 0; j < costofferforms.Count - 1; j++)
                {
                    var first = costofferforms[j].ProjectCode;
                    if (first == groupcostofferform[i].ProjectCode)
                    {
                        if (first.Length == 5)
                        {
                            string content = $"{pk_number},'20M190',{ProjectItem_number},'{first}','謝偉華','{costofferforms[j].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','張雅玲', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                            SQLMethod.Insert_purchaseplan(content);
                            pk_number += 1;
                            ProjectItem_number += 1;
                            break;
                        }
                        else
                        {
                            string content = $"{pk_number},'20M190',{ProjectItem_number},'{first.Substring(0, 5)}','謝偉華','{costofferforms[j].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','張雅玲', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                            SQLMethod.Insert_purchaseplan(content);
                            pk_number += 1;
                            ProjectItem_number += 1;
                            string content1 = $"{pk_number},'20M190',{ProjectItem_number},'{first.Substring(6, 5)}','謝偉華','{costofferforms[j].ProjectName}', '{ DateTime.Now.ToString("yyyy/MM/dd")}','{null}','張雅玲', '{ DateTime.Now.ToString("yyyy/MM/dd")}', '{null}','{null}','{null}','{null}','{null}','{null}','{null}','{null}','{groupcostofferform[i].Money}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{null}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}','{Math.Round((int)groupcostofferform[i].Money * 0.9)}',0,'{null}','{null}' ";
                            SQLMethod.Insert_purchaseplan(content1);
                            pk_number += 1;
                            ProjectItem_number += 1;
                            break;
                        }
                    }
                }
            }
            #endregion

            #region 請購計畫顯示
            string grammar = "USE [PurchaseProcessSystemDB] Select * FROM PurchasePlan Where ProjectNO = '20M190' Order By ProjectCode";
            DataTable dataTable = SQLMethod.OutPutTable(grammar);
            gridControl1.DataSource = dataTable;
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.Columns[0].BestFit();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].BestFit();
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].BestFit();
            gridView1.Columns[2].Caption = "項次";
            gridView1.Columns[3].BestFit();
            gridView1.Columns[3].Caption = "請款編號";
            gridView1.Columns[4].BestFit();
            gridView1.Columns[4].Caption = "請購人";
            gridView1.Columns[5].BestFit();
            gridView1.Columns[5].Caption = "請購內容";
            gridView1.Columns[6].BestFit();
            gridView1.Columns[6].Caption = "預計掛件日期";
            gridView1.Columns[7].BestFit();
            gridView1.Columns[7].Caption = "實際掛件日期";
            gridView1.Columns[8].BestFit();
            gridView1.Columns[8].Caption = "採購承辦";
            gridView1.Columns[9].BestFit();
            gridView1.Columns[9].Caption = "預定決商日";
            gridView1.Columns[10].BestFit();
            gridView1.Columns[10].Caption = "實際決商日";
            gridView1.Columns[11].BestFit();
            gridView1.Columns[11].Caption = "長交期設備";
            gridView1.Columns[12].BestFit();
            gridView1.Columns[12].Caption = "聯繫單編號";
            gridView1.Columns[13].BestFit();
            gridView1.Columns[13].Caption = "異常單編號";
            gridView1.Columns[14].BestFit();
            gridView1.Columns[14].Caption = "廠商";
            gridView1.Columns[15].BestFit();
            gridView1.Columns[15].Caption = "訂單編號";
            gridView1.Columns[16].BestFit();
            gridView1.Columns[16].Caption = "聯絡人";
            gridView1.Columns[17].BestFit();
            gridView1.Columns[17].Caption = "連絡電話";
            gridView1.Columns[18].BestFit();
            gridView1.Columns[18].Caption = "估算成本";
            gridView1.Columns[19].BestFit();
            gridView1.Columns[19].Caption = "執行目標(A)";
            gridView1.Columns[20].BestFit();
            gridView1.Columns[20].Caption = "已發包金額";
            gridView1.Columns[21].BestFit();
            gridView1.Columns[21].Caption = "未發包金額";
            gridView1.Columns[22].BestFit();
            gridView1.Columns[22].Caption = "小計(B)";
            gridView1.Columns[23].BestFit();
            gridView1.Columns[23].Caption = "差異金額(A)-(B)";
            gridView1.Columns[24].BestFit();
            gridView1.Columns[24].Caption = "累積已計價金額";
            gridView1.Columns[25].BestFit();
            gridView1.Columns[25].Caption = "備註";
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                gridView1.Columns[i].OptionsColumn.AllowEdit = false;
            }
            #endregion
        }

        private void PlanOutputsimpleButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xlsx|*xlsx";
            saveFileDialog1.Title = "Export Data";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                gridView1.ExportToXlsx($"{saveFileDialog1.FileName}.xlsx");
            }
        }
    }
}
