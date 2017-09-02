using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _397_FinalProject_Part2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (checkUserName() && checkPassword())
            {
                Session["user"] = tbxUserName.Text;
                lblMessage.Text = "Welcome back";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Enabled = true;
                lblMessage.Visible = true;
                Response.AddHeader("refresh", "2; url=Store.aspx");
            }
            else
            {
                lblMessage.Text = "Username or Password is incorrect";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Enabled = true;
                lblMessage.Visible = true;
            }
        }

        public bool checkUserName()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select UserName from Users";
            SqlCommand cmd = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["UserName"].ToString().Equals(tbxUserName.Text))
                {
                    return true;
                }
            }
            rdr.Close();
            conn.Close();
            return false;
        }

        public bool checkPassword()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select Password from Users where UserName=@u";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", tbxUserName.Text);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["Password"].ToString().Equals(encryptedPassword()))
                {
                    return true;
                }
            }
            rdr.Close();
            conn.Close();
            return false;
        }

        protected string encryptedPassword()
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            sha1.ComputeHash(ASCIIEncoding.ASCII.GetBytes(tbxPassword.Text));
            byte[] result = sha1.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            string hashedPasssword = strBuilder.ToString();
            return hashedPasssword;
        }
    }
}