using EwatchPurchase.Form.Test.Configuration;
using EwatchPurchase.Form.Test.FQ;
using EwatchPurchase.Form.Test.Method;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EwatchPurchase.Form.Test
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 資料庫
        /// </summary>
        private SQLSetting SQLSettings { get; set; }
        /// <summary>
        /// SQL解析
        /// </summary>
        private SQLMethod SQLMethod;
        public Form1()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}\\log\\log-.txt",
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            SQLSettings = InitialMethod.InitialSQLSetting();
            SQLMethod = new SQLMethod() { setting = SQLSettings };
            SQLMethod.SQLConnect();
        }
        private void ReviewsimpleButton_Click(object sender, EventArgs e)
        {
            ReportFQ reportFQ = new ReportFQ();
            reportFQ.PaperKind = System.Drawing.Printing.PaperKind.A4;
            string projectno = ProjectNOtextEdit.Text;
            string buyno = BuyNOtextEdit.Text;
            string projectpurchaseer = ProjectPurchasertextEdit.Text;
            string branch = BranchtextEdit.Text;
            string project = ProjecttextEdit.Text;
            string appdate = ApplicationDatetextEdit.Text;
            string buylimitdate = BuyLimitDatetextEdit.Text;
            string needdate = NeedDatetextEdit.Text;
            string pickup = PickuptextEdit.Text;
            string deliery = DeliverytextEdit.Text;
            bool devicecheck = false;
            bool materialcheck = false;
            bool constructioncheck = false;
            bool hangcheck = false;
            bool elsecheck = false;
            bool inagreement = false;
            bool noagreement = false;
            bool just = false;
            bool suggest = false;
            bool remark = checkEdit1.Checked;
            switch (comboBoxEdit1.SelectedIndex)
            {
                case 0:
                    {
                        devicecheck = true;
                    }
                    break;
                case 1:
                    {
                        materialcheck = true;
                    }
                    break;
                case 2:
                    {
                        constructioncheck = true;
                    }
                    break;
                case 3:
                    {
                        hangcheck = true;
                    }
                    break;
                case 4:
                    {
                        elsecheck = true;
                    }
                    break;
            }
            switch (comboBoxEdit2.SelectedIndex)
            {
                case 0:
                    {
                        inagreement = true;
                    }
                    break;
                case 1:
                    {
                        noagreement = true;
                    }
                    break;
                case 2:
                    {
                        just = true;
                    }
                    break;
                case 3:
                    {
                        suggest = true;
                    }
                    break;
            }
            reportFQ.Textchange(projectno, buyno, projectpurchaseer, branch, project, appdate, buylimitdate, needdate, pickup, deliery, devicecheck, materialcheck, constructioncheck, hangcheck, elsecheck, inagreement, noagreement, just, suggest, remark);
            reportFQ.DataxrCrossTabChange();
            reportFQ.CreateDocument();
            documentViewer1.DocumentSource = reportFQ;
        }
    }
}
