<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SafeTest.aspx.cs" Inherits="SafeWeb_SafeTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>코디서비스 모바일</title>
</head>
<body>
    <script language="javascript" type="text/javascript" src="/SafeWeb/SST/SST.js"></script>
    <script language="javascript" type="text/javascript">
    <!--//
    function btnSend_Click()
    {
        var tbUserID = document.getElementById("<%=tbUserID.ClientID %>");
        var hdUserID_Encrypt = document.getElementById("<%=hdUserID_Encrypt.ClientID %>");
        var tbPassword = document.getElementById("<%=tbPassword.ClientID %>");
        var hdPassword_Encrypt = document.getElementById("<%=hdPassword_Encrypt.ClientID %>");
        
        if(tbUserID.value == "" || tbPassword.value == "")
        {
            alert("암호화 전송할 아이디 혹은 패스워드를 입력해 주십시오.");
            tbUserID.focus();
            return false;
        }else{
            try
            {
                LoadPublicKey('<%=System.Configuration.ConfigurationManager.AppSettings["PublicKeyPath"] %>');
                hdUserID_Encrypt.value = Encrypt(tbUserID.value);
                hdPassword_Encrypt.value = Encrypt(tbPassword.value);
                if(hdUserID_Encrypt.value == null || hdPassword_Encrypt.value == null)
                {
                    alert("암호화에 실패했습니다.");
                    return false;
                }
                tbUserID.value = "";
                tbPassword.value = "";
            }catch(e){
                alert("암호화에 실패했습니다.");
                return false;
            }
        }
        return true;
    }
    //-->
    </script>
    
    <form id="form1" runat="server">
        <table width="600" border="0" cellpadding="10" cellspacing="1" bgcolor="#CCCCCC">
            <tr>
                <td>
              <li><strong>예제</strong><br />
              	<!--//-------------------------------------예제안내--------------------------------------------------//-->
              	<div style="background-color:#EEEEEE;border:solid 1px #CCCCCC;padding:15px;">
                    성명 
                    <asp:Label ID="lbResult" runat="server" Text=""></asp:Label><br />
                    <asp:TextBox ID="tbName" runat="server" Text="specialsix" /><br />
                    아 이 디 
                    <asp:TextBox ID="tbUserID" runat="server" />
                    <asp:HiddenField ID="hdUserID_Encrypt" runat="server" /><br />
                    
                    패스워드
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" />
                    <asp:HiddenField ID="hdPassword_Encrypt" runat="server" />
                    <br />
                    
                    <asp:Button ID="btnSend" runat="server" Text="암호화전송" OnClientClick="return btnSend_Click();" OnClick="btnSend_Click" />
        	</div>
              </li>

            </td>
          </tr>
        </table>
</form>
</body>
</html>
