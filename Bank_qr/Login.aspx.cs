using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{

    string constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["id"] != null)
        {
            Session.Abandon();
            Session.Clear();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {


        SqlDataAdapter _adpobj = new SqlDataAdapter("select id,username, password from Admin_Login where username='" + txtUsername.Text + "' and password='" + txtPass.Text + "'", _DBConnection._objcon);
        DataTable _dtobj = new DataTable();
        _adpobj.Fill(_dtobj);
        if (_dtobj.Rows.Count > 0)
        {
            int id = Convert.ToInt16(_dtobj.Rows[0]["id"]);
            Session["id"] = id;
            Response.Redirect("Admin/Home.aspx");
            //Response.Write("<script>alert('Successful.');</script>");
        }
        else
        {
            Response.Write("<script>alert('Invalid Username and Password.');</script>");
        }
    
    }
   

}