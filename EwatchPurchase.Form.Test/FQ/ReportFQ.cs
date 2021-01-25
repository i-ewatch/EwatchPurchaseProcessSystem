using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace EwatchPurchase.Form.Test.FQ
{
    public partial class ReportFQ : DevExpress.XtraReports.UI.XtraReport
    {
        public ReportFQ()
        {
            InitializeComponent();
        }
        public void Textchange(string projectno, string buyno, string projectpurchaseer, string branch, string project, string appdate, string buylimitdate, string needdate, string pickup, string deliery, bool devicecheck, bool materialcheck, bool constructioncheck, bool hangcheck, bool elsecheck) 
        {
            ProjectNOxrLabel.Text = projectno;
            BuyNOxrLabel.Text = buyno;
            ProjectPurchaserxrTableCell.Text = projectpurchaseer;
            BranchxrTableCell.Text = branch;
            ProjectxrTableCell.Text = project;
            ApplicationDatexrTableCell.Text = appdate;
            BuyLimitDatexrTableCell.Text = buylimitdate;
            NeedDatexrTableCell.Text = needdate;
            PickupxrTableCell.Text = pickup;
            DeliveryxrTableCell.Text = deliery;
            DevicexrCheckBox.Checked = devicecheck;
            MaterialxrCheckBox.Checked = materialcheck;
            ConstructionxrCheckBox.Checked = constructioncheck;
            HangxrCheckBox.Checked = hangcheck;
            ElsexrCheckBox.Checked = elsecheck;
        }
    }
}
