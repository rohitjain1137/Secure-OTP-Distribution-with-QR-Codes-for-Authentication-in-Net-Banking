using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_User_Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        _DBConnection._objcon.Close();
        if (Session["id"] == null)
        {
            Response.Redirect("Admin_Login.aspx");
        }

        if (!IsPostBack && Request.QueryString["msg"] == "edit")
        {
            int uid = Convert.ToInt32(Request.QueryString["cid"]);
            SqlDataAdapter _adpobj = new SqlDataAdapter("SELECT firstname,lastname,username,emailid,mobileno,IMEIno,accountno,balance FROM Customer_registraion where cid=@cid", _DBConnection._objcon);
            _adpobj.SelectCommand.Parameters.AddWithValue("@cid", uid);
            DataTable _dtobj = new DataTable();
            _adpobj.Fill(_dtobj);
            if (_dtobj.Rows.Count > 0)
            {
                txtfirstname.Text = _dtobj.Rows[0]["firstname"].ToString();
                txtlastname.Text = _dtobj.Rows[0]["lastname"].ToString();
                txtusername.Text = _dtobj.Rows[0]["username"].ToString();
                txtmobileno.Text = _dtobj.Rows[0]["mobileno"].ToString();
                txtemailid.Text = _dtobj.Rows[0]["emailid"].ToString();
                txtimeino.Text = _dtobj.Rows[0]["IMEIno"].ToString();
                txtamount.Text = _dtobj.Rows[0]["balance"].ToString();
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
    
    
    if (Request.QueryString["msg"] == "edit")
            {
                int uid = Convert.ToInt32(Request.QueryString["cid"]);
                using (SqlCommand _cmd = new SqlCommand("UpdateNewCustomer", _DBConnection._objcon))
                {
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@firstname", txtfirstname.Text.ToString());
                    _cmd.Parameters.AddWithValue("@lastname", txtlastname.Text.ToString());
                    _cmd.Parameters.AddWithValue("@username", txtusername.Text.ToString());
                    _cmd.Parameters.AddWithValue("@emailid", txtemailid.Text.ToString());
                    _cmd.Parameters.AddWithValue("@mobileno", txtmobileno.Text.ToString());
                    _cmd.Parameters.AddWithValue("@IMEIno", Convert.ToInt64(txtimeino.Text));
                    _cmd.Parameters.AddWithValue("@balance", Convert.ToInt64(txtamount.Text));
                    _cmd.Parameters.AddWithValue("@cid", uid);
                    _DBConnection._objcon.Open();
                    _cmd.ExecuteNonQuery();
                    _DBConnection._objcon.Close();
                    Response.Redirect("ManageCustomer.aspx?msg=update");
                }
            }
            else
            {
                Random random = new Random();
                string accountno = string.Join(string.Empty, Enumerable.Range(0, 10).Select(number => random.Next(0, 9).ToString()));

                SqlDataAdapter _adpobj = new SqlDataAdapter("select * from Customer_registraion where emailid='" + txtemailid.Text + "'", _DBConnection._objcon);
                DataTable dt = new DataTable();
                _adpobj.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Email already exist');</script>");
                    return;
                }

                using (SqlCommand _cmd = new SqlCommand("InsertNewCustomer", _DBConnection._objcon))
                {
				string password = CreateRandomPassword(8);
                    _cmd.CommandType = CommandType.StoredProcedure;
                    _cmd.Parameters.AddWithValue("@firstname", txtfirstname.Text.ToString());
                    _cmd.Parameters.AddWithValue("@lastname", txtlastname.Text.ToString());
                    _cmd.Parameters.AddWithValue("@username", txtusername.Text.ToString());
                    _cmd.Parameters.AddWithValue("@password", password);
                    _cmd.Parameters.AddWithValue("@emailid", txtemailid.Text.ToString());
                    _cmd.Parameters.AddWithValue("@mobileno", txtmobileno.Text.ToString());
                    _cmd.Parameters.AddWithValue("@IMEIno", Convert.ToInt64(txtimeino.Text));
                    _cmd.Parameters.AddWithValue("@accountno", accountno);
                    _cmd.Parameters.AddWithValue("@balance", Convert.ToInt64(txtamount.Text));
                    _DBConnection._objcon.Open();
                    _cmd.ExecuteNonQuery();
                    _DBConnection._objcon.Close();
                    //send mail 

                    string email = txtemailid.Text.ToString();
                    string username = txtusername.Text;

                    try
                    {
                        SmtpClient SmtpServer = new SmtpClient();
                    MailMessage mail = new MailMessage();
                    SmtpServer.Credentials = new System.Net.NetworkCredential("rohit11371137@gmail.com", "vdhutjhhcrxvfqba");
                    SmtpServer.Port = 587;
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Host = "smtp.gmail.com";
                    mail = new MailMessage();
                    mail.From = new MailAddress("rohit11371137@gmail.com");

                    mail.To.Add(email);
                    mail.Subject = "Password";
                    mail.Body = "Your password is:" + password;
                    SmtpServer.Send(mail);
                        Response.Redirect("ManageCustomer.aspx?msg=add");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    } 
                }
            }
        }
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
