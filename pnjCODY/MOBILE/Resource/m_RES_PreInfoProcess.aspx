<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_PreInfoProcess.aspx.cs" Inherits="Resource_m_RES_PreInfoProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        if (!confirm('<%= this.Page.Request["RES_JOINDATE"].ToString() %>'  + " 에 등록된 이전 사원정보가 있습니다. 사원정보를 가져오시겠습니까?")) {
            <%= Page.GetPostBackEventReference(this.btnCancel) %>    
        }
        else
        {
            <%= Page.GetPostBackEventReference(this.btnPreInfoSave) %>    
        }
    </script>
    <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"></asp:LinkButton>
    <asp:LinkButton ID="btnPreInfoSave" runat="server" OnClick="btnPreInfoSave_Click"></asp:LinkButton>
</asp:Content>