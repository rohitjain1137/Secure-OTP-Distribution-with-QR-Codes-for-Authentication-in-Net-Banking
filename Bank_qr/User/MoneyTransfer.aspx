<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage1.master" AutoEventWireup="true" CodeFile="MoneyTransfer.aspx.cs" Inherits="User_MoneyTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .dd{
            width:100%;
            height:30px;
        }   
        .table{
            margin-left:30%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <h3 align="center" style="margin-top:2%"> Transfer Money</h3>
    <div>
        <table style="margin-left:40%;margin-top:1%;height:300px">
      
           
            <tr>
               
                 <td>    <asp:Label ID="Label3" runat="server" Text="Select Accont No :" Font-Bold="True" ForeColor="Black"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlaccountno" runat="server"  OnSelectedIndexChanged="ddlaccountno_SelectedIndexChanged" AutoPostBack="True" CssClass="dd">
                       
                    </asp:DropDownList>
                </td>
            </tr>
             <tr>
               
              <td>    <asp:Label ID="Label1" runat="server" Text="Customer Name :" Font-Bold="True" ForeColor="Black"></asp:Label></td>
              
               
           
                    <td>   <asp:Label ID="lblname" runat="server" Text="" Font-Bold="True" ForeColor="Black"></asp:Label>    </td>
               
            </tr>

             <tr>
                    <td>  <asp:Label ID="Label2" runat="server" Text="Amount : " Font-Bold="True" ForeColor="Black"></asp:Label>   </td>
                <td><asp:TextBox ID="txtamount" runat="server"></asp:TextBox></td>
                  <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtamount" Text="Enter User name" ForeColor="Red"> </asp:RequiredFieldValidator></td>
            </tr>


          
             <tr>
                
                <td> <asp:Label ID="Label5" runat="server" Text="Balance :" Font-Bold="True" ForeColor="Black"></asp:Label></td>
               <td>   <asp:Label ID="lblamount" runat="server" Text="" Font-Bold="True" ForeColor="Black"></asp:Label></td> 
              
            </tr>




            
           <%-- <tr>
                 <td>Image</td>
                <td><asp:FileUpload ID="FileUpload1" runat="server"/></td>
            </tr>--%>
            <tr>
                <td colspan="2" align="center"><asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="button" OnClick="Button1_Click" /></td>
            </tr>
           <%--  <tr>
                <td colspan="3" align="center"><asp:Button ID="Cancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnsubmit_Click" /></td>
            </tr>--%>
        </table>
    </div>
     <div style=" text-align: center; padding: 3px; margin-top:15%;
  background-color: black;
  color: white;font-size:20px">

        <p style ="margin-left:10%">
   
</p>
    </div>
</asp:Content>


