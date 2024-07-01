<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_REP_Toll_Price.aspx.cs" Inherits="m_REP_Toll_Price" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkSave() {
            var num, year, month, day;
            num = document.getElementById('<%= this.txtCash.ClientID %>').value;

             if (isNaN(num)) {
                 num = "";
                 alert("숫자만 입력 가능합니다.");
                 document.getElementById('<%= this.txtCash.ClientID %>').focus();
                 return false;
             }
             else {
                 document.getElementById('<%= this.txtCash.ClientID %>').value = num;
             }

            num = document.getElementById('<%= this.txtCard.ClientID %>').value;

            if (isNaN(num)) {
                num = "";
                alert("숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtCard.ClientID %>').focus();
                 return false;
             }
             else {
                 document.getElementById('<%= this.txtCard.ClientID %>').value = num;
             }
         }
    </script>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>통행료</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Report/m_REP_Daily_Mng_Drive.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">기타 업무 관리 > 자차 운영 일지</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"> 통행료 <asp:Label ID="lblStart" runat="server"></asp:Label> </td>

                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; padding-right:.8em; width:70px; text-align:right;">현금(원) : </th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtCash" runat="server" MaxLength="30" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; padding-right:.8em; width:70px; text-align:right;">법인(원) : </th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtCard" runat="server" MaxLength="30" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Report/m_REP_Daily_Mng_Drive.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
