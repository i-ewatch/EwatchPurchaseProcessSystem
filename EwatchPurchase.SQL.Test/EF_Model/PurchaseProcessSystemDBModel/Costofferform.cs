using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwatchPurchase.SQL.Test.EF_Model.PurchaseProcessSystemDBModel
{
    public partial class Costofferform
    {
        public int pk { get; set; }
        public string ProjectNO { get; set; }
        public string ProjectItem { get; set; }
        public string ProjectName { get; set; }
        public string ProjectUnit { get; set; }
        public string ProjectAmount { get; set; }
        public int Price { get; set; }
        public int Money { get; set; }
        public string Remark { get; set; }
        public string ProjectCode { get; set; }
    }
}
