using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace QRCode_Banking_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate = "login/{email}/{pass}", ResponseFormat = WebMessageFormat.Json)]
        Login_ login(string email, string pass);

        [OperationContract]
        [WebGet(UriTemplate = "setdata/{qr_data}/{u_id}/{imei}", ResponseFormat = WebMessageFormat.Json)]
        Scan_data setdata(string qr_data, string u_id, string imei);
        // TODO: Add your service operations here


        //[OperationContract]
        //[WebInvoke(Method ="POST",UriTemplate ="setdata",ResponseFormat=WebMessageFormat.Json)]
        //Scan_data setdata(otp_data otpdata);
    }

    [DataContract]
    public class Login_
    {
        [DataMember]
        public string Msg
        {
            get;
            set;
        }
    }

    [DataContract]
    public class Scan_data
    {
        [DataMember]
        public string Msg2
        {
            get;
            set;
        }
    }

    [DataContract]
    public class otp_data
    {
        [DataMember]
        public string qr_data { get; set; }
        [DataMember]
        public string u_id { get; set; }
        [DataMember]
        public string imei { get; set; }
    }
  

}
