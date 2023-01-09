using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
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

public partial class User_QRGeneration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["cid"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        btn_genrateQR.Visible = false;
        ImgQrCode.Visible = false;
        btndecrypt.Visible = false;
        txtencrypt.Visible = false;
    }
    protected void btn_OTP_Click(object sender, EventArgs e)
    {

        string userid = Session["cid"].ToString();

        //OTP genration----------------

        btn_genrateQR.Visible = true;
        btndecrypt.Visible = true;
        txtencrypt.Visible = true;


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

        btn_genrateQR.Text = _strrandom.ToString();
        string original_otp = _strrandom.ToString();
        string encrypted_otp = this.Encrypt(original_otp);

        ////Insert otp and userdeatils

        try
        {
            SqlCommand _objcmd = new SqlCommand("insert into OTPDetails(userid ,otp ,encrypt_otp,date) values(@userid ,@otp ,@encrypt_otp,@date)", _DBConnection._objcon);
            _objcmd.Parameters.AddWithValue("@userid", userid);
            _objcmd.Parameters.AddWithValue("@otp", original_otp);
            _objcmd.Parameters.AddWithValue("@encrypt_otp", encrypted_otp);
            _objcmd.Parameters.AddWithValue("@date", DateTime.Now.Date);
            _DBConnection._objcon.Open();
            _objcmd.ExecuteNonQuery();
            _DBConnection._objcon.Close();
            Response.Write("<script>alert('inserted details.');</script>");
        }
        catch (Exception)
        {
            throw;
        }

        ////encrpted OTP to genrate QR code

        QRCodeEncoder encode = new QRCodeEncoder();
        Bitmap bi = encode.Encode(encrypted_otp);
        bi.Save(("E:/Sagar/Qr_Banking_S/Images/ORImage/first.jpeg"), ImageFormat.Jpeg);
        ImgQrCode.ImageUrl = "~/Images/ORImage/first.jpeg";
        ImgQrCode.Visible = true;
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

     protected void btndecrypt_Click(object sender, EventArgs e)
        {
            string Decrypted_otp = this.Decrypt(txtencrypt.Text);
            lbldecrypted.Text = Decrypted_otp;
            if (lbldecrypted.Text == btn_genrateQR.Text)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Write("<script>alert('invalid login or OTP');</script>");
            }

        }

     private string Decrypt(string p)
     {
         string userid = Session["cid"].ToString();

            SqlDataAdapter _adp = new SqlDataAdapter("Select * from Customer_registraion where cid='" + userid + "'", _DBConnection._objcon);
            DataTable dt = new DataTable();
            _adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string IMEIdentity = dt.Rows[0]["IMEIno"].ToString();
                string EncryptionKey = IMEIdentity.ToString();
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            return cipherText;
        }


    
public  string cipherText { get; set; }}
