using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.IO;

public partial class receive : System.Web.UI.Page
{
    string mobe2, randomNumber;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Label5.Text = Session["nam"].ToString();
        }
        catch (Exception ex)
        {

            Response.Redirect("Login.aspx");
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        MySqlConnection con1 = new MySqlConnection("Server=127.0.0.1;User id= root;Password=;Database=money;");

        con1.Open();
        MySqlCommand ss1 = new MySqlCommand("select * from regi where nam='" + Session["nam"] + "'", con1);
        MySqlDataAdapter sss = new MySqlDataAdapter();
        sss.SelectCommand = ss1;
        DataSet ds1 = new DataSet();
        sss.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {



            mobe2 = ds1.Tables[0].Rows[0]["ky"].ToString();

        }

        if( Session["OTP"].ToString()==TextBox4.Text)
        {
            MySqlConnection con = new MySqlConnection("Server=127.0.0.1;User id= root;Password=;Database=money;");
            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = " insert into logi(nam,loc,amt) values (@nam,@unam, @pass)";
            cmd.Parameters.AddWithValue("@nam", Session["nam"].ToString());
            cmd.Parameters.AddWithValue("@unam", TextBox1.Text);
            cmd.Parameters.AddWithValue("@pass", TextBox3.Text);
           
            cmd.ExecuteNonQuery();
            Response.Write("<script>alert('Amount Requested Sucessfully ')</script>");
            con.Close();

        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        MySqlConnection con1 = new MySqlConnection("Server=127.0.0.1;User id= root;Password=;Database=money;");

        con1.Open();
        MySqlCommand ss1 = new MySqlCommand("select * from regi where nam='" + Session["nam"] + "'", con1);
        MySqlDataAdapter sss = new MySqlDataAdapter();
        sss.SelectCommand = ss1;
        DataSet ds1 = new DataSet();
        sss.Fill(ds1);

        if (ds1.Tables[0].Rows.Count > 0)
        {



            mobe2 = ds1.Tables[0].Rows[0]["ph"].ToString();

        }
        Random generator = new Random();
        randomNumber = generator.Next(0, 1000000).ToString("D6");
        WebClient client = new WebClient();
        string baseurl = "https://www.txtguru.in/imobile/api.php?username=kgvgowrish&password=Thylak@123&source=VIDGAN&dmobile=" + mobe2 + " &unicode=false&flash=false&message=" + "Key For Transfer" + '\n' + randomNumber + '\n';
        Session["OTP"] = randomNumber;
        Stream data = client.OpenRead(baseurl);
        StreamReader reader = new StreamReader(data);
        string s21 = reader.ReadToEnd();
        data.Close();
        reader.Close();
        Response.Write("<script>alert('Password is sucessfully Generated ')</script>");
    }
}