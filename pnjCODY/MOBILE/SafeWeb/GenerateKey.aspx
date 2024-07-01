<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenerateKey.aspx.cs" Inherits="SafeWeb_GenerateKey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>제목 없음</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset style="padding:10px;">
            <legend><b>Generate Key</b></legend>
            <asp:DropDownList ID="ddlKeySize" runat="server">
                <asp:ListItem Selected="True" Value="512">512 bit</asp:ListItem>
                <asp:ListItem Value="1024">1024 bit</asp:ListItem>
                <asp:ListItem Value="2048">2048 bit</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate" />
            *PS. 비트수가 높아질수록 암/복호화 속도는 느려집니다.
        </fieldset>
        
        <p></p>
        
        <fieldset style="padding:10px;">
            <legend><b>Public Key (base64)</b></legend>
            <asp:TextBox ID="tbPublicKey" runat="server" Rows="7" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </fieldset>
        
        <p></p>
        
        <fieldset style="padding:10px;">
            <legend><b>Private Key (base64)</b></legend>
            <asp:TextBox ID="tbPrivateKey" runat="server" Rows="10" TextMode="MultiLine" Width="100%"></asp:TextBox>
        </fieldset>
        
        <p></p>    
    </div>
    </form>
</body>
</html>
