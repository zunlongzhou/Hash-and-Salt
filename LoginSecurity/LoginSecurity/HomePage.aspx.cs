using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;
using System.Text;
using System.Security.Cryptography;

namespace LoginSecurity
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager1.RegisterAsyncPostBackControl(Button1);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //验证验证码的正确
            string vCode = Session["CheckCode"].ToString();
            if (txtValidatedcode.Text.Trim().ToUpper() != vCode.ToUpper())
            {
                Label3.Text = "验证码错误";
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Startup", "alert('ValidatedCode is right!');", true);
            }


            string uName = TextBox1.Text;
            string uPwd1 = TextBox2.Text;

            bool isTrue = false;
            //读取数据库
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AccessConnectString2"].ConnectionString;
            conn.Open();

            OleDbCommand command = conn.CreateCommand();
            string sqlString = String.Format("Select * from usertable where username='{0}'", uName);
            command.CommandText = sqlString;
            OleDbDataReader reader = command.ExecuteReader();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                System.Diagnostics.Debug.Write(reader.GetName(i).Trim());
            }

            //获取Salt值
            string salt="";
            while (reader.Read())
            {
                if (reader["UserName"].ToString() == uName)
                {
                    salt = reader["Salt"].ToString();
                    System.Diagnostics.Debug.Write(salt);
                    break;
                }
            }

            //reader.Read();
            //string saltx = reader["Salt"].ToString();
            //string salt = "XMDMv0Gh";
            string uPwd;
            uPwd1 = uPwd1 + salt;
            byte[] result = Encoding.Default.GetBytes(uPwd1.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            uPwd = BitConverter.ToString(output).Replace("-", "");
            reader.Close();



            sqlString = String.Format("Select * from usertable where username='{0}' and userpwd='{1}'", uName, uPwd);

            command.CommandText = sqlString;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader["UserName"].ToString() == uName &&
                  reader["UserPwd"].ToString() == uPwd)
                {
                    string saltx = reader["Salt"].ToString();
                    System.Diagnostics.Debug.Write(saltx);
                    Session["name"] = uName;
                    isTrue = true;
                    break;
                }
            }

            conn.Close();

            //验证用户名和口令+Salt的哈希值
            if (isTrue)
            {
                Session["userName"] = uName;
                Response.Redirect("LoginTo.aspx");
            }
            else
                Label3.Text = "用户名密码错误！";
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
        protected void imag(object sender, EventArgs e)
        {
            //点击图片刷新验证码
            validatedCode v = new validatedCode();
            string code = v.CreateVerifyCode();            //取随机码
            v.CreateImageOnPage(code, this.Context);       // 输出图片
            Session["CheckCode"] = code;                   //Session 取出验证码
        }
    }
}