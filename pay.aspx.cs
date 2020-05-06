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

public partial class pay : System.Web.UI.Page
{
    string mobe2,randomNumber;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        Label5.Text = Session["nam"].ToString();
        MySqlConnection con = new MySqlConnection("Server=127.0.0.1;User id= root;Password=;Database=money;");
        con.Open();
        MySqlCommand cmd = con.CreateCommand();
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandText = "select * from logi where st='" + "ff" + "' ";
        cmd.ExecuteNonQuery();
        DataTable dt = new DataTable();
        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        da.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        }
        catch(Exception ex)
        {

            Response.Write("<script>alert('Please relogin')</script>");
            Response.Redirect("Login.aspx");
        }
    }
    protected void ss(object sender, EventArgs e)
    {
        GridViewRow gr = GridView1.SelectedRow;
        TextBox3.Text = gr.Cells[1].Text;
        TextBox5.Text = gr.Cells[1].Text;
        TextBox2.Text = gr.Cells[1].Text;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        MySqlConnection con = new MySqlConnection("Server=127.0.0.1;User id= root;Password=;Database=money;");

        con.Open();
        MySqlCommand ss = new MySqlCommand("select * from regi where nam='" + TextBox3.Text + "'", con);
        MySqlDataAdapter s = new MySqlDataAdapter();
        s.SelectCommand = ss;
        DataSet ds = new DataSet();
        s.Fill(ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            string mobe = ds.Tables[0].Rows[0]["ph"].ToString();
            string mobe1 = ds.Tables[0].Rows[0]["nam"].ToString();
           
            if(Session["OTP"].ToString()==TextBox4.Text)
            { 
            WebClient client = new WebClient();
            string baseurl = "https://www.txtguru.in/imobile/api.php?username=kgvgowrish&password=Thylak@123&source=VIDGAN&dmobile=" + mobe + " &unicode=false&flash=false&message=" + "Money is Sucessfully Transfered" + '\n' + "By" + '\n'+Session["nam"]+'\n';

            Stream data = client.OpenRead(baseurl);
            StreamReader reader = new StreamReader(data);
            string s21 = reader.ReadToEnd();
            data.Close();
            reader.Close();
            MySqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = "update logi SET st='" + "vv" + "' where nam='" + TextBox3.Text + "'";
            cmd1.ExecuteNonQuery();
            Response.Write("<script>alert('Money is Sucessfully Transferred ')</script>");
            }
            else
            {

                Response.Write("<script>alert('Please Check Again')</script>");
            }
        }
        con.Close();
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