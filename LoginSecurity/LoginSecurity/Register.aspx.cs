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
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace LoginSecurity
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager1.RegisterAsyncPostBackControl(Button1);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //保证验证码正确
            string vCode = Session["CheckCode"].ToString();
            if (txtValidatedcode.Text.Trim().ToUpper() != vCode.ToUpper())
            {
                Label3.Text = "验证码错误";
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Startup", "alert('ValidatedCode is right!');", true);
            }
            //保证邮箱格式正确
            Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            bool flagNum = false;
            bool flagAlpha = false;

            String name = this.TextBox1.Text;
            String password = this.TextBox2.Text;
            string password2 = this.TextBox3.Text;
            string mail = this.TextBox4.Text;
            //验证用户名不为空，两次密码输入一致且密码强度足够（长度大于8位含数字字母），邮箱格式正确
            if(name=="")
            {
                this.Label3.Text = "用户名不能为空";
                return;
            }
            if(password!=password2)
            {
                this.Label3.Text = "两次密码输入不一致";
                return;
            }
            if (password.Length <= 8)
            {
                this.Label3.Text = "密码长度不足【8位以上】";
                return;
            }
            foreach(char ch in password)
            {
                if (Char.IsNumber(ch))
                    flagNum = true;
                if (Char.IsLetter(ch))
                    flagAlpha = true;
            }
            if(!flagAlpha||!flagNum)
            {
                this.Label3.Text = "密码强度不够，至少由数字字母组成";
                return;
            }
            if(!r.IsMatch(mail))
            {
                this.Label3.Text = "邮箱格式不正确";
                return;
            }



            //验证用户名无重名
            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["AccessConnectString2"].ConnectionString;
            conn.Open();

            OleDbCommand command = conn.CreateCommand();
            string sqlString = String.Format("Select * from usertable where username='{0}'", name);
            command.CommandText = sqlString;
            OleDbDataReader reader = command.ExecuteReader();

            string repetition = "";
            while (reader.Read())
            {
                if (reader["UserName"].ToString() == name)
                {
                    repetition = reader["Salt"].ToString();
                    break;
                }
            }
            if(repetition!="")
            {
                this.Label3.Text = "用户名重复，请重新输入";
                return;
            }
            conn.Close();



        //生成salt值
        int ran;
            byte[] saltB = new byte[8];
            Random rand = new Random((int)(DateTime.Now.Ticks % 1000000));
            //生成8字节原始数据
            for (int i = 0; i < 8; i++)
                //while循环剔除非字母和数字的随机数
                do
                {
                    //数字范围是ASCII码中字母数字和一些符号
                    ran = rand.Next(48, 122);
                    saltB[i] = Convert.ToByte(ran);
                } while ((ran >= 58 && ran <= 64) || (ran >= 91 && ran <= 96));
            //转换成8位String类型               
            string salt = Encoding.ASCII.GetString(saltB);


            //开始用MD5加密，数据库内保存哈希值，登陆验证用用户输入的密码哈希与数据库内的哈希比对
            string SaltPassword = password + salt;
            string s1;
            byte[] result = Encoding.Default.GetBytes(SaltPassword.Trim());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            s1 = BitConverter.ToString(output).Replace("-", ""); 
            //s1是密码加salt的哈希值

            if (!GuestBook.Add(name, s1,salt))
                this.Label3.Text = "注册失败";
            else
            {
                this.Label3.Text = "恭喜！";
                this.Label6.Text = "注册成功！";
                Thread.Sleep(1200);
                Response.Redirect("HomePage.aspx");
            }
                
        }
    }
}
