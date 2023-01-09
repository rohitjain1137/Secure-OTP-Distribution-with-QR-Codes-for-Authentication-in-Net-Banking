using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["id"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            Bindgrid();
        }
        if (Request.QueryString["msg"] == "update")
        {
            Response.Write("<script>alert('record updated successfully.');</script>");
        }
        if (Request.QueryString["msg"] == "add")
        {
            Response.Write("<script>alert('record added successfully.');</script>");
        }
    }
    protected void Bindgrid()
    {
        SqlDataAdapter _adpobj = new SqlDataAdapter("Select * from Customer_registraion", _DBConnection._objcon);
        DataTable _dt = new DataTable();
        _adpobj.Fill(_dt);
        if (_dt.Rows.Count > 0)
        {
            gvCourse.DataSource = _dt;
            gvCourse.DataBind();
        }
    }
    protected void usergrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCourse.PageIndex = e.NewPageIndex;
        Bindgrid();
    }
}