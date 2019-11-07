using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace LoginSecurity
{
    public class Connection
    {
        public static OleDbConnection CurrentConnection
        {
            get
            {
                //链接数据库
                return new OleDbConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["AccessConnectString2"].ToString());//从web.config中读取数据库连接字符串
            }
            private set
            {

            }
        }
    }
}