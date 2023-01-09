<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="User" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login</title>
    <style>


        body{
             /*background-image: url('images/background.jpg');*/
             
        }

        .heading{
				border:2px solid none;
				width:100%;
			}
			.contactTable{
				position:absolute;
				top:2%;
				right:1%;
			}
			.section{
				border:2px solid none;
				width:520px;
				margin-left:32%;
				margin-top:15%;
				box-shadow: 5px 10px 5px 10px #888888;
				border-radius: 5px 5px;				
				
			}
			input[type=text], input[type=password],select {
			  width: 100%;
			  padding: 12px 0px;
			  display: inline-block;
			  border: 1px solid #ccc;
			  box-sizing: border-box;
			}
			.button {
			  background-color: teal; /* Red */
			  border: none;
			  color: white;
			  text-align: center;
			  padding:12px 12px;
			  width:30%;
			  text-decoration: none;
			  display: inline-block;
			  font-size: 16px;
			  transition-duration: 0.4s;
			  cursor: pointer;
              width:50%;
              margin-left:38%
		}
		.button:hover {
			  background-color:transparent;
			  border:2px solid teal;
			  color:black;
		}
        .lbl{
            margin-left:30%;
            cursor:pointer;
        }
        tr{
           height:55px
        }
        b,h2{
            font-family:'Arial Rounded MT'
        }
        .div{
            box-shadow:  15px 15px 10px #888888;	 
        }
        .lblMsg{
            margin-left:35%;
        }
    </style>
       
</head>
<body>
    <form id="form1" runat="server">
      
            <div style="width: 30%; height:500px; background-color:RGB(242, 187, 36); float:left;margin-left:10%;margin-top:5%" class="div">
               <table border="0" style="width:100%;margin-top:25%">
                   <tr>
                       <td>
                           <img class="rounded-circle" src="Images/Image1.jpg" alt="" height="200px" style="margin-left:15%"/>
                       </td>
                   </tr>
                   <tr>
                       <td> <h2 style="color:white" align="center"> Welcome Back ! </h2></td>
                   </tr>
           </table>
              
            </div>
            <div style="width: 50%; height:500px; background-color:whitesmoke; float:right;margin-right:10%;margin-top:5%" class="div">

                <h2 align="center" style="font-family:'Arial Rounded MT';font-size:29px">User Login</h2>
                <table border="0" style="margin-left:1%;margin-top:1%;width:98%">
                 <%--   <tr">
                        <td><b>Login as</b></td>
                        <td>
                            <asp:DropDownList ID="ddType" runat="server">
                                <asp:ListItem>Admin</asp:ListItem>
                                <asp:ListItem>Mentor</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
					<tr>
						<td><b>Email</b></td>
						<td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername" Text="Please fill" ForeColor="Red"></asp:RequiredFieldValidator></td>
					</tr>
                  
				
                   
					<tr>
						<td colspan="2"><asp:Button ID="btn_OTP" runat="server" Text="Generate QR" CssClass="button" OnClick="btn_OTP_Click"></asp:Button></td>
					</tr>

<%--                    <tr>
                        <td colspan="3"><asp:Label ID="lblMsg" runat="server" Text="Invalid email id " ForeColor="Red" Visible="false" CssClass="lblMsg" /></td>
                    </tr>--%>

                      <tr>

                    <td colspan="3">  <asp:Image ID="ImgQrCode" runat="server" Height="200" style="margin-left:30%"
                                  Width="250" /></td>


                    </tr>



                    <tr>

                     <td colspan="2"> <asp:Button ID="btn_login" CssClass="button" runat="server" 
                                 Text="Login" OnClick="btn_login_Click1" /></td>

                    </tr>
                     <tr>
						<td colspan="2"><a href="Login.aspx" style="margin-left:48%;color:black">Go to User Login</a></td>
					</tr>
                    
				</table>
            </div>
    </form>
</body>
</html>
