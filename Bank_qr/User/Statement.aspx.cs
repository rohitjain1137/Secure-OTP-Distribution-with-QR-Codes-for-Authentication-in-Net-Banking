using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Statement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["cid"] == null)
        //{
        //    Response.Redirect("Login.aspx");
        //}
        if (!IsPostBack)
        {
            bindgridview();
        }
    }
    protected void bindgridview()
    {
        string userid = Session["cid"].ToString(); ;
       //     Session["cid"].ToString();
        SqlDataAdapter adp = new SqlDataAdapter("select c.cid, (firstname+''+lastname) as name,c.accountno as FromAccountNo, c.balance,m.userid, m.to_account_no as ToAccountNo, amount as creditamount,m.transn_type,format( m.date ,'dd-MM-yy') as date from  moneytransfer_details m join Customer_registraion c on m.userid=c.cid where c.cid='" + userid + "'", _DBConnection._objcon);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            gvCourse.DataSource = dt;
            gvCourse.DataBind();
        }
    }
}