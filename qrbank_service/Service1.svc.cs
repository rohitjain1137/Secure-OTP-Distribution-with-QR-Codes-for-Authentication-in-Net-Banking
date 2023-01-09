using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Configuration;

namespace QRCode_Banking_Service
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string cs = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adp;
        SqlDataReader dr;
        DataSet ds;

        public Login_ login(string email, string pass)
        {
            conn = new SqlConnection(cs);
            conn.Open();
            comm = new SqlCommand("Select * from Customer_registraion where emailid=@mail and password=@pass", conn);
            comm.Parameters.AddWithValue("@mail", email);
            comm.Parameters.AddWithValue("@pass", pass);
            using (dr = comm.ExecuteReader())
            {
                if (dr.Read())
                {
                    return new Login_
                    {
                        Msg = dr.GetValue(0).ToString()
                    };

                }
                else
                {
                    return new Login_();
                }
            }
        }

        public Scan_data setdata(string qr_data, string u_id, string imei)
        {
            conn = new SqlConnection(cs);
            conn.Open();
            try
            {
                SqlDataAdapter adp4 = new SqlDataAdapter();
                adp4.SelectCommand = new SqlCommand("select * from Customer_registraion where cid=@id", conn);
                adp4.SelectCommand.Parameters.AddWithValue("@id", u_id);
                DataTable dt7 = new DataTable();
                adp4.Fill(dt7);
                if (dt7.Rows[0]["IMEIno"].ToString() == imei)
                {
                    using (adp = new SqlDataAdapter())
                    {
                        adp.SelectCommand = new SqlCommand();
                        adp.SelectCommand.Connection = conn;
                        adp.SelectCommand.CommandText = "insert_temp_barcode";
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@otp", qr_data);
                        adp.SelectCommand.Parameters.AddWithValue("@u_id", u_id);
                        adp.SelectCommand.ExecuteNonQuery();
                        conn.Close();
                        return new Scan_data
                        {
                            Msg2 = "Data_Inserted"
                        };
                    }
                }
                else
                {
                    return new Scan_data();
                }
            }
            catch (Exception ex)
            {
                return new Scan_data();
            }
        }

      
    }
}
