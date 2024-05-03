using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace CRUDoperations
{
    public partial class Sales : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["Salesconnect"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void ButtonSell_Click(object sender, EventArgs e)
        {
            if (isRecordPresent())
            {
                sellRecord();
                GridViewSales.DataBind();
                // LabelNotification.Text = "Record updated successful";
                ListBoxNotification.Items.Add("Record updated successful");
            }
            else
            {
                // LabelNotification.Text = "Record not found";
                ListBoxNotification.Items.Add("Record not found");
            }
        }

        protected void ButtonBuy_Click(object sender, EventArgs e)
        {

            if (isRecordPresent())
            {
                buyRecord();
                // LabelNotification.Text = "Data added to previous record";
                ListBoxNotification.Items.Add("Data added to previous record");
            }
            else
            {
                addRecord();
                // LabelNotification.Text = "New record added";
                ListBoxNotification.Items.Add("New record added");
            }
            GridViewSales.DataBind();
        }

        private bool isRecordPresent()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from sales where itemtype = @itemtype and itemsize=@itemsize";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@itemType", TextBoxItemType.Text);
            sqlCommand.Parameters.AddWithValue("@itemSize", TextBoxItemSize.Text);
            con.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void addRecord()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into Sales (itemtype,itemsize,itempiece,itemprice) values(@itemtype,@itemsize,@itempiece,@itemprice)";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@itemtype", TextBoxItemType.Text);
            sqlCommand.Parameters.AddWithValue("@itemsize", TextBoxItemSize.Text);
            sqlCommand.Parameters.AddWithValue("@itempiece", TextBoxItemPiece.Text);
            sqlCommand.Parameters.AddWithValue("@itemprice", TextBoxItemPrice.Text);
            con.Open();
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        private void sellRecord()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Sales SET itempiece = (itempiece - @itempiece)" +
                           "where itemtype = @itemtype and itemsize = @itemsize";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@itemtype", TextBoxItemType.Text);
            sqlCommand.Parameters.AddWithValue("@itemsize", TextBoxItemSize.Text);
            sqlCommand.Parameters.AddWithValue("@itempiece", TextBoxItemPiece.Text);
            con.Open();
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

        private void buyRecord()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update Sales SET itempiece = (itempiece + @itempiece)" +
                           "where itemtype = @itemtype and itemsize = @itemsize";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@itemtype", TextBoxItemType.Text);
            sqlCommand.Parameters.AddWithValue("@itemsize", TextBoxItemSize.Text);
            sqlCommand.Parameters.AddWithValue("@itempiece", TextBoxItemPiece.Text);
            con.Open();
            sqlCommand.ExecuteNonQuery();
            con.Close();
        }

       
    }
}