using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _397_FinalProject_Part2
{
    public partial class Summary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            fillgvCart();
            calculatePrice();
            updateStockQuantity();
            clearCart();
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

        public void calculatePrice()
        {
            float totalPrice = 0;
            List<Tuple<string, string>> cart = cartItems();
            List<Tuple<string, string>> prices = bookPrices();
            foreach (Tuple<string, string> item in cart)
            {
                foreach (Tuple<string, string> book in prices)
                {
                    if (item.Item1.Equals(book.Item1))
                    {
                        totalPrice += float.Parse(item.Item2) * float.Parse(book.Item2);
                    }
                }
            }
            lblPrice.Text = totalPrice.ToString();
        }

        public List<Tuple<string, string>> cartItems()
        {
            List<Tuple<string, string>> cart = new List<Tuple<string, string>>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Cart where UserName=@u";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cart.Add(new Tuple<string, string>(rdr["Book"].ToString(), rdr["Quantity"].ToString()));
            }
            rdr.Close();
            conn.Close();
            return cart;
        }

        public List<Tuple<string, string>> bookPrices()
        {
            List<Tuple<string, string>> prices = new List<Tuple<string, string>>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Book";
            SqlCommand cmd = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                prices.Add(new Tuple<string, string>(rdr["Name"].ToString(), rdr["Price"].ToString()));
            }
            rdr.Close();
            conn.Close();
            return prices;
        }

        public List<Tuple<string, string>> bookQuantity()
        {
            List<Tuple<string, string>> prices = new List<Tuple<string, string>>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "select * from Book";
            SqlCommand cmd = new SqlCommand(qry, conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                prices.Add(new Tuple<string, string>(rdr["Name"].ToString(), rdr["Quantity"].ToString()));
            }
            rdr.Close();
            conn.Close();
            return prices;
        }

        public List<Tuple<string, string>> newStockNumbers()
        {
            List<Tuple<string, string>> newNumbers = new List<Tuple<string, string>>();
            foreach (Tuple<string, string> item in cartItems())
            {
                foreach (Tuple<string, string> book in bookQuantity())
                {
                    if (item.Item1.Equals(book.Item1))
                    {
                        newNumbers.Add(new Tuple<string,string>(item.Item1, (int.Parse(book.Item2) - int.Parse(item.Item2)).ToString()));
                    }
                }
            }
            return newNumbers;
        }

        public void updateStockQuantity()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            foreach (Tuple<string,string> number in newStockNumbers())
            {
                string qry = "update Book set Quantity=@q where Name=@n";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@q", number.Item2);
                cmd.Parameters.AddWithValue("@n", number.Item1);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void clearCart()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Bookstore"].ConnectionString);
            string qry = "delete from Cart where UserName=@u";
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.Parameters.AddWithValue("@u", Session["user"].ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}