<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage1.master" AutoEventWireup="true" CodeFile="QRGeneration.aspx.cs" Inherits="User_QRGeneration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
       
         
        .button
        {
            font-family: Georgia;
            font-size: 18px;
            color: White;
            padding: 4px 45px;
            margin: 0 20px;
            text-decoration: none;
            border: solid 1px #720000;
            background-color: #A52A2A;
            -webkit-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -webkit-border-radius: 7px;
            -moz-border-radius: 50px;
            text-align: left;
        }
        .button1
        {
            font-family: Georgia;
            font-size: 18px;
            padding: 5px 14px;
            margin: 0 20px;
            -moz-box-shadow: 0px 0px 1px #FF3300, inset 0px 0px 1px #FFFFFF;
            -webkit-border-radius: 5px;
            -moz-border-radius: 50px;
            text-align: left;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div>
        <table >

            <tr>
                <td colspan="2" align="center"><asp:Button ID="btn_OTP" runat="server" Text="Add" CssClass="button" OnClick="btn_OTP_Click" /></td>
                  <td colspan="3" align="center"><asp:Button ID="btn_genrateQR" runat="server" Text="" CssClass="button"/></td>
                
                      <td><asp:TextBox ID="txtencrypt" runat="server"></asp:TextBox></td>

                  <td colspan="4" align="center"><asp:Button ID="btndecrypt" runat="server" Text="Match OTP" OnClick="btndecrypt_Click" CssClass="button"/></td>
                
            </tr>




      
        </table>

        <asp:Label ID="lbldecrypted" runat="server" CssClass="button" Text=""></asp:Label><br  />
         <asp:Image ID="ImgQrCode" runat="server" Height="300" Style="margin-left: 75px" Width="350" />
    </div>
</asp:Content>

