using Dapper;
using Serilog;
using EwatchPurchase.Output.Test.Configuration;
using EwatchPurchase.Output.Test.EF_Model.PurchaseProcessSystemDBModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace EwatchPurchase.Output.Test.Method
{
    public class SQLMethod
    {
        /// <summary>
        /// 資料庫連結資訊
        /// </summary>
        public SqlConnectionStringBuilder scsb;
        /// <summary>
        /// 資料庫JSON
        /// </summary>
        public SQLSetting setting { get; set; }
        private SqlCommand sqlCommand = null;
        #region 資料庫連結
        /// <summary>
        /// EF資料庫連結
        /// </summary>
        /// <param name="DataBaseType">資料庫類型</param>
        public void SQLConnect()
        {
            scsb = new SqlConnectionStringBuilder()
            {
                DataSource = setting.DataSource,
                InitialCatalog = setting.InitialCatalog,
                UserID = setting.UserID,
                Password = setting.Password
            };
        }
        #endregion

        #region 成本報價單資料抓取
        public List<Costofferform> Count_Costofferform()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT * FROM [PurchaseProcessSystemDB].[dbo].[Costofferform] Where ProjectCode != ''";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取請購計畫資料失敗");
                return null;
            }
        }
        #endregion

        #region 請購計畫表資料抓取
        public List<PurchasePlan> Count_purchaseplan()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT * FROM [PurchaseProcessSystemDB].[dbo].[PurchasePlan] Where ProjectNO = '20M190'";
                    var values = conn.Query<PurchasePlan>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取請購計畫資料失敗");
                return null;
            }
        }
        #endregion

        #region 整理請購編號
        public List<Costofferform> Group_costofferform()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT ProjectCode,SUM(Money) as Money FROM [PurchaseProcessSystemDB].[dbo].[Costofferform] Where ProjectCode != '' and ProjectCode != '以下空白' Group By ProjectCode";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取請購計畫資料失敗");
                return null;
            }
        }
        #endregion

        #region excel資料匯入資料庫
        public List<PurchasePlan> Insert_purchaseplan(string content)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"USE [PurchaseProcessSystemDB] INSERT INTO [PurchasePlan] (pk, ProjectNO, ProjectItem, ProjectCode, ProjectPurchaser, ProjectContent, EstimatedMailDate, RealMailDate, BuyMan, EstimatedDecisionDate, RaelDecisionDate, LongTimeDevice, ContactCode, ErrorCode, Vendor, OrderCode, ContactPerson, ContactPersonPhone, EstimatedCost, ExecutionGoal, PackageCoin, NuPackageCoin, Subtotal, DiffCoin, AccCoin, Remark";
                    grammar += $" ) VALUES ({content})";
                    var values = conn.Query<PurchasePlan>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "資料匯入資料庫失敗");
                return null;
            }
        }
        #endregion

        #region 輸出報表
        public DataTable OutPutTable(string grammar)
        {
            var logconsole = new LoggerConfiguration().WriteTo.Console().CreateLogger();
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    DataTable dataTable = new DataTable();
                    DataSet dataSet = new DataSet();
                    sqlCommand = new SqlCommand(grammar, conn);
                    SqlDataAdapter sqlData = new SqlDataAdapter(sqlCommand);
                    dataSet.Clear();
                    sqlData.Fill(dataSet);
                    dataTable = dataSet.Tables[0];
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                logconsole.Information(ex.ToString());
                Log.Logger.Error(ex, "資料匯入Table錯誤");
                return null;
            }
        }
        #endregion
    }
}
