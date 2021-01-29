using Dapper;
using EwatchPurchase.Form.Test.Configuration;
using EwatchPurchase.Form.Test.EF_Model.PurchaseProcessSystemDBModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchase.Form.Test.Method
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
        public List<Costofferform> Count_Costofferform(string projectnostring)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"USE [PurchaseProcessSystemDB] Select ProjectName as '名稱',ProjectUnit as '單位',ProjectAmount as '數量',Remark as '備註' FROM Costofferform Where ProjectCode = '{projectnostring}'";
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
    }
}
