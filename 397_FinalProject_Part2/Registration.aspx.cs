using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace _397_FinalProject_Part2
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            checkErrors();
            if (noErrors())
            {
                if (userExists())
                {
                    lblMessage.Text = "User exists.";
                    lblMessage.ForeColor = Color.Red;

                }
                else
                {
                    register();
                    lblMessage.Text = "You have registered.";
                    lblMessage.ForeColor = Color.Red;
                    Session["user"] = tbxUserName.Text;
                    Response.AddHeader("refresh", "2; url=Store.aspx");
                }
            }
        }

        public void register()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "insert into Users (UserName, Password) Values(@u, @p)";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", tbxUserName.Text);
            cmd.Parameters.AddWithValue("@p", encryptedPassword());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
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


        public bool userExists()
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

        public void checkErrors()
        {
            if (!tbxUserName.Text.Equals(""))
            {
                lblUserNameError.Text = "";
                lblUserNameError.Visible = false;
            }
            else
            {
                lblUserNameError.Text = "User Name is required";
                lblUserNameError.Visible = true;
            }

            if (!tbxPassword.Text.Equals(""))
            {
                lblPasswordError.Text = "";
                lblPasswordError.Visible = false;
                if (tbxPassword.Text.Length > 5 && !containsSpecialChar(tbxPassword.Text))
                {
                    lblPasswordError2.Text = "";
                    lblPasswordError2.Visible = false;
                }
                else
                {
                    lblPasswordError2.Text = "Password must have atleast 6 characters without special characters";
                    lblPasswordError2.Visible = true;
                }
            }
            else
            {
                lblPasswordError.Text = "Password is required";
                lblPasswordError.Visible = true;
                lblPasswordError2.Text = "";
                lblPasswordError2.Visible = false;
            }

            if (!tbxConfirm.Text.Equals(""))
            {
                lblConfirmError.Text = "";
                lblConfirmError.Visible = false;
                if (!tbxConfirm.Text.Equals(tbxPassword.Text))
                {
                    lblConfirmError2.Text = "Passwords must be the same";
                    lblConfirmError2.Visible = true;
                }
                else
                {
                    lblConfirmError2.Text = "";
                    lblConfirmError2.Visible = false;
                }
            }
            else
            {
                lblConfirmError.Text = "Confirm Password is required";
                lblConfirmError.Visible = true;
                lblConfirmError2.Text = "";
                lblConfirmError2.Visible = false;
            }
        }

        public bool noErrors()
        {
            if (
                lblUserNameError.Text.Equals("") &&
                lblPasswordError.Text.Equals("") &&
                lblPasswordError2.Text.Equals("") &&
                lblConfirmError.Text.Equals("") &&
                lblConfirmError2.Text.Equals(""))
            {
                return true;
            }
            return false;
        }

        public bool containsSpecialChar(string word)
        {
            foreach (char c in word)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
}