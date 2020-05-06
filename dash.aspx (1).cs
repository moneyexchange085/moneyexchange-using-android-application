using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dash : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            Label5.Text = Session["nam"].ToString();
        }
        catch(Exception ex)
        {

            Response.Redirect("Login.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("pay.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("receive.aspx");
    }
}