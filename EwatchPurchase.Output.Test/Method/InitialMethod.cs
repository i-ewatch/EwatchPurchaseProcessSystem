using Newtonsoft.Json;
using Serilog;
using EwatchPurchase.Output.Test.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchase.Output.Test.Method
{
    public class InitialMethod
    {
        /// <summary>
        /// 工作路徑
        /// </summary>
        private static readonly string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

        #region 資料庫連線
        /// <summary>
        /// 資料庫連線
        /// </summary>
        /// <returns></returns>
        public static SQLSetting InitialSQLSetting()
        {
            SQLSetting setting = new SQLSetting();
            if (!Directory.Exists($"{WorkPath}\\stf"))
                Directory.CreateDirectory($"{WorkPath}\\stf");
            string SettingPath = $"{WorkPath}\\stf\\SQL.json";
            try
            {
                if (File.Exists(SettingPath))
                {
                    string json = File.ReadAllText(SettingPath, Encoding.UTF8);
                    setting = JsonConvert.DeserializeObject<SQLSetting>(json);
                }
                else
                {
                    SQLSetting Setting = new SQLSetting()
                    {
                        DataSource = "127.0.0.1",
                        InitialCatalog = "PurchaseProcessSystemDB",
                        UserID = "sa",
                        Password = "1234"
                    };
                    setting = Setting;
                    string output = JsonConvert.SerializeObject(setting, Formatting.Indented, new JsonSerializerSettings());
                    File.WriteAllText(SettingPath, output);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "System setting initial 資料庫JSON failed.");
            }
            return setting;
        }
        #endregion
    }
}
