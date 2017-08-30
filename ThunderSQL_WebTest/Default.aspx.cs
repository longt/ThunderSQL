using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThunderSQL;

namespace ThunderSQL_WebTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new DataContext();
            string sql = "select car_name from cars where car_id=12";
            string value = context.GetValue<string>(sql);//返回第一行第一列数据;
            Label1.Text = value;
        }
    }
}