
namespace EwatchPurchase.Output.Test
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PurchasePlansimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.PlanOutputsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.PlanOutputsimpleButton);
            this.panelControl1.Controls.Add(this.PurchasePlansimpleButton);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1198, 55);
            this.panelControl1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 54);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1198, 710);
            this.panelControl2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1198, 710);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // PurchasePlansimpleButton
            // 
            this.PurchasePlansimpleButton.Appearance.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PurchasePlansimpleButton.Appearance.Options.UseFont = true;
            this.PurchasePlansimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.PurchasePlansimpleButton.Location = new System.Drawing.Point(1063, 2);
            this.PurchasePlansimpleButton.Name = "PurchasePlansimpleButton";
            this.PurchasePlansimpleButton.Size = new System.Drawing.Size(133, 51);
            this.PurchasePlansimpleButton.TabIndex = 2;
            this.PurchasePlansimpleButton.Text = "請購計畫預覽";
            this.PurchasePlansimpleButton.Click += new System.EventHandler(this.PurchasePlansimpleButton_Click);
            // 
            // PlanOutputsimpleButton
            // 
            this.PlanOutputsimpleButton.Appearance.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlanOutputsimpleButton.Appearance.Options.UseFont = true;
            this.PlanOutputsimpleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.PlanOutputsimpleButton.Enabled = false;
            this.PlanOutputsimpleButton.Location = new System.Drawing.Point(930, 2);
            this.PlanOutputsimpleButton.Name = "PlanOutputsimpleButton";
            this.PlanOutputsimpleButton.Size = new System.Drawing.Size(133, 51);
            this.PlanOutputsimpleButton.TabIndex = 3;
            this.PlanOutputsimpleButton.Text = "請購計畫輸出";
            this.PlanOutputsimpleButton.Click += new System.EventHandler(this.PlanOutputsimpleButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 764);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "MainForm";
            this.Text = "專案採購流程管理系統";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton PurchasePlansimpleButton;
        private DevExpress.XtraEditors.SimpleButton PlanOutputsimpleButton;
    }
}

