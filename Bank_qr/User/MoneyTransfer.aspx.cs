using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_MoneyTransfer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cid"] == null)
        {
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {
            BindAccount();
            string userid = Session["cid"].ToString();
            SqlDataAdapter _fromadp1 = new SqlDataAdapter("select accountno, balance from Customer_registraion where cid='" + userid + "'", _DBConnection._objcon);
            DataTable _fromdt1 = new DataTable();
            _fromadp1.Fill(_fromdt1);
            int currentbalance = 0;
            if (_fromdt1.Rows.Count > 0)
            {

                currentbalance = Convert.ToInt32(_fromdt1.Rows[0]["balance"]);
                lblamount.Text = currentbalance.ToString();
            }


        }
    }

    private void BindAccount()
    {
        string userid = Session["cid"].ToString();
        SqlDataAdapter _adp = new SqlDataAdapter("select cid,accountno from Customer_registraion where cid!='" + userid + "'", _DBConnection._objcon);
        DataTable _dt = new DataTable();
        _adp.Fill(_dt);
        if (_dt.Rows.Count > 0)
        {

            ddlaccountno.DataSource = _dt;
            ddlaccountno.DataTextField = "accountno";
            ddlaccountno.DataValueField = "accountno";
            ddlaccountno.DataBind();
        }
        ddlaccountno.Items.Insert(0, new ListItem("--select account no--", "0"));
    }
    protected void ddlaccountno_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            SqlDataAdapter _objadp = new SqlDataAdapter("select (firstname +'  '+ lastname) as name, username, password, emailid, mobileno, IMEIno, accountno, balance from dbo.Customer_registraion where accountno='" + ddlaccountno.SelectedValue + "'", _DBConnection._objcon);
            DataTable _objdt = new DataTable();
            _objadp.Fill(_objdt);
            if (_objdt.Rows.Count > 0)
            {
                lblname.Text = _objdt.Rows[0]["name"].ToString();
            }
        }
        catch (Exception)
        {

            throw;
        }
    
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {




            string userid = Session["cid"].ToString();

            SqlDataAdapter _fromadpp = new SqlDataAdapter("select  balance from Customer_registraion where cid='" + userid + "'", _DBConnection._objcon);
            DataTable _fromdtt = new DataTable();
            _fromadpp.Fill(_fromdtt);

            int currentbalance1 = 0;
            if (_fromdtt.Rows.Count > 0)
            {

                currentbalance1 = Convert.ToInt32(_fromdtt.Rows[0]["balance"]);

            }



            
            if (currentbalance1 <= Convert.ToInt32(txtamount.Text))
            {
                Response.Write("<script> alert('amount is not sufficiant for transfer.')</script>");


            }

            else
            {




                //insert credit details


                SqlCommand _cmdobj = new SqlCommand("insert into moneytransfer_details(userid,to_account_no,amount,date,transn_type) values(@userid,@to_account_no,@amount,@date,@transn_type)", _DBConnection._objcon);
                _cmdobj.Parameters.AddWithValue("@userid", userid);
                _cmdobj.Parameters.AddWithValue("@to_account_no", ddlaccountno.SelectedValue);
                _cmdobj.Parameters.AddWithValue("@amount", txtamount.Text);
                _cmdobj.Parameters.AddWithValue("@date", DateTime.Now.Date);
                _cmdobj.Parameters.AddWithValue("@transn_type", "");
                _DBConnection._objcon.Open();
                _cmdobj.ExecuteNonQuery();
                _DBConnection._objcon.Close();


                //select fromaccount number and currebalance
                SqlDataAdapter _fromadp = new SqlDataAdapter("select accountno, balance from Customer_registraion where cid='" + userid + "'", _DBConnection._objcon);
                DataTable _fromdt = new DataTable();
                _fromadp.Fill(_fromdt);
                string accountnumber = "";
                int currentbalance = 0;
                if (_fromdt.Rows.Count > 0)
                {
                    accountnumber = _fromdt.Rows[0]["accountno"].ToString();
                    currentbalance = Convert.ToInt32(_fromdt.Rows[0]["balance"]);

                }

                //check sufficient balace for transfer
                if (currentbalance <= Convert.ToInt32(txtamount.Text))
                {
                    Response.Write("<script> alert('amount is not sufficiant for transfer.')</script>");
                }
                else
                {
                    int _update_from_amount = Convert.ToInt32(currentbalance - Convert.ToInt32(txtamount.Text));

                    SqlCommand cmdupdate = new SqlCommand("update Customer_registraion set balance=@balance where cid='" + userid + "' or accountno='" + accountnumber + "'", _DBConnection._objcon);
                    cmdupdate.Parameters.AddWithValue("@balance", _update_from_amount);
                    _DBConnection._objcon.Open();
                    cmdupdate.ExecuteNonQuery();
                    _DBConnection._objcon.Close();

                    //update money transfer 

                    SqlCommand cmdcredit = new SqlCommand("update moneytransfer_details set transn_type='debit' where userid='" + userid + "'", _DBConnection._objcon);
                    _DBConnection._objcon.Open();
                    cmdcredit.ExecuteNonQuery();
                    _DBConnection._objcon.Close();

                    //select current balance of the toaccount number

                    SqlDataAdapter _toadp = new SqlDataAdapter("select accountno, balance from Customer_registraion where accountno='" + ddlaccountno.SelectedValue + "'", _DBConnection._objcon);
                    DataTable _todt = new DataTable();
                    _toadp.Fill(_todt);
                    if (_todt.Rows.Count > 0)
                    {
                        int tocurrentbalance = Convert.ToInt32(_todt.Rows[0]["balance"]);

                        int update_to_amount = Convert.ToInt32(tocurrentbalance + Convert.ToInt32(txtamount.Text));

                        SqlCommand cmd = new SqlCommand("update Customer_registraion set balance=@balance where accountno='" + ddlaccountno.SelectedValue + "'", _DBConnection._objcon);
                        cmd.Parameters.AddWithValue("@balance", update_to_amount);
                        _DBConnection._objcon.Open();
                        cmd.ExecuteNonQuery();
                        _DBConnection._objcon.Close();


                        //update money transfer 

                        SqlCommand cmddebit = new SqlCommand("update moneytransfer_details set transn_type='credit' where  to_account_no='" + ddlaccountno.SelectedValue + "'", _DBConnection._objcon);
                        _DBConnection._objcon.Open();
                        cmddebit.ExecuteNonQuery();
                        _DBConnection._objcon.Close();
                    }

                }
            }
        }
        catch (Exception)
        {

            throw;
        }
            txtamount.Text = "";
            ddlaccountno.Items.Clear();
            lblname.Text = "";
            lblamount.Text = "";
        }
    
    }
