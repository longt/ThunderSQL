using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThunderSQL;
using ThunderSQL.Runtime;
using System.Data;

namespace ThunderSQL_WebTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var context = new DataContext();
            // asp.net 数据库读测试 
            /*
            string sql = "select car_name from cars where car_id=12";
            string value = context.GetValue<string>(sql);       //返回第一行第一列数据;
            Label1.Text = value;
            */
             string sql = "select * from cars";
             GridView1.DataSource = context.GetDataTable(sql);
             GridView1.DataBind();
        }
       
    }
}