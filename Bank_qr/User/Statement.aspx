<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage1.master" AutoEventWireup="true" CodeFile="Statement.aspx.cs" Inherits="User_Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .gv{
            margin-left:5%;
            width:85%;
            margin-top:5%;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <h4 style="text-align:center">Bank Statement</h4>
     <%--  <h6 style="margin-top:7%;margin-left:1%"><a href="User_Registration.aspx" style="color:black"><u>Add New User</u></a></h6>--%>
 <%--   OnRowCommand="gvCourse_RowCommand" DataKeyNames="c_id"--%>
        <asp:GridView ID="gvCourse" runat="server" CssClass="gv" AutoGenerateColumns="false" DataKeyNames="cid" HeaderStyle-HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="Name" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="FromAccountNo" HeaderText="Debit Account_No" ItemStyle-HorizontalAlign="Center"/>
                 <asp:BoundField DataField="balance" HeaderText=" Account balance" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="ToAccountNo" HeaderText="Account" ItemStyle-HorizontalAlign="Center"/>
                 <asp:BoundField DataField="transn_type" HeaderText="transaction type" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="creditamount" HeaderText="credit amount" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="date" HeaderText="date" ItemStyle-HorizontalAlign="Center"/>
                   <%-- <asp:HyperLinkField HeaderText="Action" DataNavigateUrlFields="cid" DataNavigateUrlFormatString="User_Registration.aspx?msg=edit&amp;cid={0}"
                                        Text="Edit" />--%>
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

      <div style=" text-align: center; padding: 3px; margin-top:15%;
  background-color: black;
  color: white;font-size:20px">

        <p style ="margin-left:10%">
  
</p>
    </div>
</asp:Content>

