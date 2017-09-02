using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace _397_FinalProject_Part2
{
    public partial class Store : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    fillddlBookAndgvDisplay();
                    fillddlModify();
                    fillgvCart();
                }
            }
        }

        protected void fillddlBookAndgvDisplay()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Book";
            SqlCommand cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlBooks.DataSource = dt;
            ddlBooks.DataTextField = "Name";
            ddlBooks.DataValueField = "Name";
            ddlBooks.DataBind();
            gvDisplay.DataSource = dt;
            gvDisplay.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string book = ddlBooks.SelectedItem.ToString();
            if (inCart(book))
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;
                lblError.Text = "Book in cart.";
            }
            else
            {
                if (int.Parse(tbxQuantity.Text) <= 0 || int.Parse(tbxQuantity.Text) > bookInStock(book))
                {
                    lblError.Visible = true;
                    lblError.ForeColor = Color.Red;
                    lblError.Text = "Quantity Invalid";
                }
                else
                {
                    lblError.Visible = false;
                    lblError.Text = "";
                    addBook(book);
                }
            }
        }

        public void addBook(string book)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "insert into Cart (UserName, Book, Quantity) Values(@u, @b, @q)";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@b", book);
            cmd.Parameters.AddWithValue("@q", int.Parse(tbxQuantity.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Store.aspx");
        }

        public bool inCart(string book)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Cart where UserName=@u";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["Book"].ToString().Equals(book))
                {
                    return true;
                }
            }
            rdr.Close();
            conn.Close();
            return false;
        }

        public int bookInStock(string book)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Book where Name = @n";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@n", book);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["Name"].ToString().Equals(book))
                {
                    return int.Parse(rdr["Quantity"].ToString());
                }
            }
            rdr.Close();
            conn.Close();
            return 0;
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("Summary.aspx");
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            if (ddlModify.Items.Count != 0) {
                if (int.Parse(tbxModify.Text) <= 0 || int.Parse(tbxModify.Text) > bookInStock(ddlModify.SelectedItem.Text))
                {
                    lblModifyError.Visible = true;
                    lblModifyError.ForeColor = Color.Red;
                    lblModifyError.Text = "Quantity/Selection Invalid";
                }
                else
                {
                    lblModifyError.Visible = false;
                    lblModifyError.Text = "";
                    modify();
                }
            } else
            {
                lblModifyError.Visible = true;
                lblModifyError.ForeColor = Color.Red;
                lblModifyError.Text = "Selection Invalid";
            }
        }

        public void modify()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "update Cart set Quantity=@q where UserName=@u and Book=@b";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            cmd.Parameters.AddWithValue("@b", ddlModify.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@q", int.Parse(tbxModify.Text));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Store.aspx");
        }

        public void fillgvCart()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select Book, Quantity from Cart where UserName=@u";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvCart.DataSource = dt;
            gvCart.DataBind();
        }

        public void fillddlModify()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Cart where UserName=@u";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlModify.DataSource = dt;
            ddlModify.DataTextField = "Book";
            ddlModify.DataValueField = "Quantity";
            ddlModify.DataBind();
        }
    }
}