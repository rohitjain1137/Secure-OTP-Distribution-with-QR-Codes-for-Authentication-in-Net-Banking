<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="ManageCustomer.aspx.cs" Inherits="Admin_ManageCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .gv{
            margin-left:5%;
            width:85%;
            margin-top:2%;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <h4 style="text-align:center">Manage User</h4>
       <h6 style="margin-top:3%;margin-left:1%"><a href="User_Registration.aspx" style="color:black"><u>Add New User</u></a></h6>
 <%--   OnRowCommand="gvCourse_RowCommand" DataKeyNames="c_id"--%>
        <asp:GridView ID="gvCourse" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="cid" HeaderStyle-HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="firstname" HeaderText="First Name" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="lastname" HeaderText="last name" ItemStyle-HorizontalAlign="Center"/>
                 <asp:BoundField DataField="username" HeaderText="user name" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="emailid" HeaderText="email id" ItemStyle-HorizontalAlign="Center"/>
                 <asp:BoundField DataField="mobileno" HeaderText="mobile no" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="IMEIno" HeaderText="IMEI no" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="balance" HeaderText="balance" ItemStyle-HorizontalAlign="Center"/>
                    <asp:HyperLinkField HeaderText="Action" DataNavigateUrlFields="cid" DataNavigateUrlFormatString="User_Registration.aspx?msg=edit&amp;cid={0}"
                                        Text="Edit" />
             <%--      <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                       <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditTable" CausesValidation="false" CommandArgument='<%# Eval("c_id") %>' BackColor="#2c92e6" ForeColor="White" CssClass="btn"/>
                   </ItemTemplate>
                </asp:TemplateField>   
                  <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                   <ItemTemplate>
                       <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="DeleteTable" CausesValidation="false" CommandArgument='<%# Eval("c_id") %>' BackColor="Red" ForeColor="White" CssClass="btn"/>
                   </ItemTemplate>
               </asp:TemplateField>--%>
            </Columns>
        </asp:GridView> 
     <div style=" text-align: center; padding: 3px; margin-top:1%;
  background-color: black;
  color: white;font-size:20px">

        <p style ="margin-left:10%">
 
</p>
    </div>
</asp:Content>

