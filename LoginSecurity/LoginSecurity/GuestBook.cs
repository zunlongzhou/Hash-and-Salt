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
using System.Data.OleDb;

namespace LoginSecurity
{
    public class GuestBook
    {
        public static OleDbDataReader Manage()
        {
            //操作数据库
            OleDbConnection conn = Connection.CurrentConnection;
            conn.Open();
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "Select * from GuestBook";
            OleDbDataReader reader = command.ExecuteReader();
            return reader;
        }
        public static Boolean Add(String name, String password,String salt)
        {
            OleDbConnection conn = Connection.CurrentConnection;
            conn.Open();
            try
            {
                OleDbCommand command = conn.CreateCommand();
                command.CommandText = String.Format("insert into usertable(userName,userPwd,salt) values('{0}','{1}','{2}')",name,password,salt);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}