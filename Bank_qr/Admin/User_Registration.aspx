<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="User_Registration.aspx.cs" Inherits="Admin_User_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .dd{
            width:100%;
            height:30px;
        }   
        .table{
            margin-left:10%;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <h3 align="center" style="margin-top:2%"> New User Registration</h3>
    <div>
        <table style="margin-left:40%;margin-top:10%">
            <tr>
               
            </tr>
            <tr>
                <td>First Name :</td>
                <td><asp:TextBox ID="txtfirstname" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtfirstname" Text="Enter First name" ForeColor="Red"> </asp:RequiredFieldValidator></td>
            </tr>
             <tr>
                <td>Last Name :</td>
                <td><asp:TextBox ID="txtlastname" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtlastname" Text="Enter Last name" ForeColor="Red"> </asp:RequiredFieldValidator></td>
            </tr>
             <tr>
                <td>User Name :</td>
                <td><asp:TextBox ID="txtusername" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtusername" Text="Enter User name" ForeColor="Red"> </asp:RequiredFieldValidator></td>
            </tr>
             <tr>
                <td>Email Id :</td>
                <td><asp:TextBox ID="txtemailid" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtemailid" Text="Enter Email-id" ForeColor="Red"> </asp:RequiredFieldValidator></td>
            </tr>
             <tr>
                <td>Mobile IMEI No :</td>
                <td><asp:TextBox ID="txtimeino" runat="server"></asp:TextBox></td>
                <td><asp:RegularExpressionValidator runat="server" ControlToValidate="txtimeino" ValidationExpression="^[0-9]{15}$" ForeColor="Red" ErrorMessage="Invalid Imei no" /></td>
                  
            </tr>
             <tr>
                <td>Mobile No :</td>
                <td><asp:TextBox ID="txtmobileno" runat="server"></asp:TextBox></td>
                <td>   <asp:RegularExpressionValidator runat="server" ControlToValidate="txtmobileno" ValidationExpression="^[7-9][0-9]{9}$" ForeColor="Red" ErrorMessage="Invalid Mobile no" /></td>
 
            </tr>
             <tr>
                <td>Amount :</td>
                <td><asp:TextBox ID="txtamount" runat="server"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtamount" ForeColor="Red" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
            </tr>
           <%-- <tr>
                 <td>Image</td>
                <td><asp:FileUpload ID="FileUpload1" runat="server"/></td>
            </tr>--%>
            <tr>
                <td colspan="2" align="center"><asp:Button ID="btnsubmit" runat="server" Text="Add" CssClass="button" OnClick="btnsubmit_Click" /></td>
            </tr>
        </table>
    </div>
     <div style=" text-align: center; padding: 3px; margin-top:15%;
  background-color: black;
  color: white;font-size:20px">

        <p style ="margin-left:10%">
    
</p>
    </div>
</asp:Content>

