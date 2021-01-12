using Dapper;
using Serilog;
using EwatchPurchase.SQL.Test.Configuration;
using EwatchPurchase.SQL.Test.EF_Model.PurchaseProcessSystemDBModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchase.SQL.Test.Method
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

        #region excel資料匯入資料庫
        public List<Costofferform> Insert_costofferforms(string content)
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = $"USE [PurchaseProcessSystemDB] INSERT INTO [Costofferform] (pk, ProjectNO, ProjectItem, ProjectName, ProjectUnit, ProjectAmount, Price, Money, Remark, ProjectCode";
                    grammar += $" ) VALUES ({content})";
                    var values = conn.Query<Costofferform>(grammar).ToList();
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
        #region pk值抓取
        public List<Costofferform> Count_Costofferform()
        {
            try
            {
                using (var conn = new SqlConnection(scsb.ConnectionString))
                {
                    string grammar = "SELECT * FROM [PurchaseProcessSystemDB].[dbo].[Costofferform]";
                    var values = conn.Query<Costofferform>(grammar).ToList();
                    return values;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "抓取pk值失敗");
                return null;
            }
        }
        #endregion
    }
}
