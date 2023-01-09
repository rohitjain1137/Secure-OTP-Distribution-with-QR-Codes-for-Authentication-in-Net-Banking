using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class User : System.Web.UI.Page
{
    string constr = ConfigurationManager.ConnectionStrings["constr"].ToString();
    SqlConnection con;
    SqlCommand cmd;
    SqlDataAdapter da;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        ImgQrCode.Visible = false;
        btn_login.Visible = false;
    }

    protected void btn_OTP_Click(object sender, EventArgs e)
    {

        if (txtUsername.Text == "")
        {
            Response.Write("<script>alert('enter emailid.');</script>");
            return;
        }
        string userid = "";
        try
            {
                SqlDataAdapter _adp = new SqlDataAdapter("select * from Customer_registraion where emailid=@emailid", _DBConnection._objcon);
                _adp.SelectCommand.Parameters.AddWithValue("@emailid", txtUsername.Text);
                DataTable _dt = new DataTable();
                _adp.Fill(_dt);
                if (_dt.Rows.Count > 0)
                {
                    userid = _dt.Rows[0]["cid"].ToString();
                    Session["cid"] = userid;
                    //Response.Redirect("../User/QRGenration.aspx");
                    //OTP genration----------------
                    char[] _otp = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    string _strrandom = string.Empty;
                    Random rndm = new Random();

                    int _NoOfChar = Convert.ToInt32(5);

                    for (int i = 0; i < _NoOfChar; i++)
                    {
                        int pos = rndm.Next(1, _otp.Length);
                        if (!_strrandom.Contains(_otp.GetValue(pos).ToString()))
                            _strrandom += _otp.GetValue(pos);
                        else
                            i--;
                    }


                    string original_otp = _strrandom.ToString();
                    string encrypted_otp = this.Encrypt(original_otp);

                    ////Insert otp and userdeatils

                    try
                    {
                        SqlDataAdapter _adpuser = new SqlDataAdapter("select * from OTPDetails where userid='" + userid + "'", _DBConnection._objcon);
                        DataTable _dtuser = new DataTable();
                        _adpuser.Fill(_dtuser);
                        if (_dtuser.Rows.Count > 0)
                        {
                            using (SqlCommand _objcmd = new SqlCommand("update OTPDetails set otp=@otp ,encrypt_otp=@encrypt_otp,date=getdate() where userid=@userid", _DBConnection._objcon))
                            {
                                _objcmd.Parameters.AddWithValue("@userid", userid);
                                _objcmd.Parameters.AddWithValue("@otp", original_otp);
                                _objcmd.Parameters.AddWithValue("@encrypt_otp", encrypted_otp);
                             //changed   // _objcmd.Parameters.AddWithValue("@date", DateTime.Now);
                                _DBConnection._objcon.Open();
                                _objcmd.ExecuteNonQuery();
                                Response.Write("<script>alert('inserted details.');</script>");
                                _DBConnection._objcon.Close();
                            }
                        }
                        else
                        {
                            using (SqlCommand _objcmd = new SqlCommand("insert into OTPDetails(userid ,otp ,encrypt_otp,date,andro_otp,andro_date) values(@userid ,@otp ,@encrypt_otp,getdate(),@andro_otp,@andro_date)", _DBConnection._objcon))
                            {
                                _objcmd.Parameters.AddWithValue("@userid", userid);
                                _objcmd.Parameters.AddWithValue("@otp", original_otp);
                                _objcmd.Parameters.AddWithValue("@encrypt_otp", encrypted_otp);
                                ////_objcmd.Parameters.AddWithValue("@date", DateTime.Now);
                                _objcmd.Parameters.AddWithValue("@andro_otp", DBNull.Value);
                                _objcmd.Parameters.AddWithValue("@andro_date", DBNull.Value);
                                _DBConnection._objcon.Open();
                                _objcmd.ExecuteNonQuery();
                                Response.Write("<script>alert('inserted details.');</script>");
                                _DBConnection._objcon.Close();

                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    ////encrpted OTP to genrate QR code
                    QRCodeEncoder encode = new QRCodeEncoder();
                    Bitmap bi = encode.Encode(original_otp);
                    bi.Save(Server.MapPath("~/images/ORImage/first.jpeg"), ImageFormat.Jpeg);
                    ImgQrCode.ImageUrl = "~/images/ORImage/first.jpeg";
                    ImgQrCode.Visible = true;
                    btn_login.Visible = true;
                    //RequiredFieldValidator1.Enabled = false;
                }
                else
                {
                    Response.Write("<script>alert('invalid username.');</script>");
                }
            }
            catch (Exception)
            {
              
                throw;
            }
        txtUsername.Text = "";
        }

        private string Encrypt(string clearText)
        {
            string userid = Session["cid"].ToString();

            SqlDataAdapter _adp = new SqlDataAdapter("Select * from Customer_registraion where cid='" + userid + "'", _DBConnection._objcon);
            DataTable dt = new DataTable();
            _adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string IMEIdentity = dt.Rows[0]["IMEIno"].ToString();
                string EncryptionKey = IMEIdentity.ToString();
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }

            }
            return clearText;
        }

        protected void btn_login_Click1(object sender, EventArgs e)
        {
          string userid = Session["cid"].ToString();
            //DateTime date = DateTime.Now;

            //SqlDataAdapter adptemp = new SqlDataAdapter("select datediff(mi, date , andro_date)as Datediff from OTPDetails where userid='" + userid + "'", _DBConnection._objcon);
            //DataTable dttemp = new DataTable();
            //adptemp.Fill(dttemp);
            //if (dttemp.Rows.Count > 0)
            //{
            //    int datediff = Convert.ToInt32(dttemp.Rows[0]["Datediff"]);

            //    if (datediff < 3600)
            //    {

            //        Response.Redirect("Home.aspx");
            //    }
            //    else
            //    {
            //        Response.Write("<script>alert('invalid login.');</script>");
            //    }
            //}
          
            DateTime date = DateTime.Now;

            SqlDataAdapter ad = new SqlDataAdapter("select datediff(mi, date , andro_date)as Datediff,andro_otp from OTPDetails where userid='" + userid + "'", _DBConnection._objcon);
            DataTable d = new DataTable();
            ad.Fill(d);
            if (d.Rows.Count > 0)
            {
                try
                {
                    string andro_otp = d.Rows[0]["andro_otp"].ToString();
                    SqlDataAdapter adpt = new SqlDataAdapter("select datediff(mi, date , andro_date)as Datediff,OTP from OTPDetails where otp='" + andro_otp + "'", _DBConnection._objcon);
                    DataTable dtt = new DataTable();
                    adpt.Fill(dtt);
                    if (dtt.Rows.Count > 0)
                    {
                        SqlDataAdapter adptemp = new SqlDataAdapter("select datediff(mi, date , andro_date)as Datediff,OTP from OTPDetails where userid='" + userid + "'", _DBConnection._objcon);
                        DataTable dttemp = new DataTable();
                        adptemp.Fill(dttemp);
                        if (dttemp.Rows.Count > 0)
                        {
                            int datediff = Convert.ToInt32(dttemp.Rows[0]["Datediff"]);
                            //check minutes difference
                            if (datediff < 10 && datediff > -1)
                            {
                                Response.Redirect("~/User/Home.aspx");
                        
                            }
                            else
                            {
                                Response.Write("<script>alert('invalid login.');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('invalid login.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('invalid login.');</script>");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        
        }

    
