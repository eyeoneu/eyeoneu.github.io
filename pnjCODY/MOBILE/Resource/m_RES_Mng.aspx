<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Mng.aspx.cs" Inherits="Resource_m_RES_Mng" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource//m_RES_MyList.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 관리</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <div style="padding:0;">
                <table class="mepm_icon_title">
                    <tr>
                        <td style="width:75%;"><p>선택 사원 정보</p></td>
                        <td align="right" style="width:25%; font-weight:normal; padding-right:0.5em;">
                            <asp:Button CssClass="button_nm gray mepm_btn_4em" Text="삭제" ID="btnDel" 
                                    runat="server" OnClientClick="return confirm('삭제 하시겠습니까?');" OnClick="btnDel_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="width:70px; text-align:left; text-align:left; padding-left:.5em;">사원번호 :</td>
                        <td style="width:auto; text-align:left; font-weight:bold;"><asp:Label runat="server" ID="lblRES_Number" Text=""></asp:Label></td>  
                        <td style="width:40px; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">이름 :</td>
                        <td style="width:auto; text-align:left;"><asp:Label runat="server" ID="lblRES_Name" Text=""></asp:Label></td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top:1px solid #ccc; text-align:left; padding-left:.5em;">주민번호 :</td>
                        <td style="border-top:1px solid #ccc; text-align:left;"><asp:Label runat="server" ID="lblRES_PersonNumber" Text=""></asp:Label></td>    
                        <td style="border-left:1px solid #ccc; border-top:1px solid #ccc; text-align:left; padding-left:.5em;">상태 :</td>
                        <td style="border-top:1px solid #ccc; text-align:left;"><asp:Label runat="server" ID="lblRES_WorkState" Text=""></asp:Label></td>
                    </tr>                
                </table>
            </div>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >선택 사원 관리 옵션</p></td>
                    </tr>
                </table>
            <p style="padding-top: .5em;"></p>
            <div class="mepm_btn_div">
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn_big">사원 등록 정보</span></a>
                <p style="padding-top: 1.5em;">
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="배정정보" ID="btnAssign" runat="server" OnClick="btnAssign_Click" Visible="false" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="배정정보" ID="btnAssign_disabled" runat="server" />
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="계약정보" ID="btnCont" runat="server" OnClick="btnCont_Click" Visible="false" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="계약정보" ID="btnCont_disabled" runat="server" />
                </p>
                <p style="padding-top: 1.5em;">
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="입사요청" ID="btnJoin" runat="server" OnClick="btnJoin_Click" Visible="false" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="입사요청" ID="btnJoin_disabled" runat="server" />
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="퇴사요청" ID="btnResign" runat="server" OnClick="btnResign_Click" Visible="false" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="퇴사요청" ID="btnResign_disabled" runat="server" />
                </p>
                <p style="padding-top: 1.5em;">
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="퇴직금 대상" ID="btnRetire" runat="server" OnClick="btnRetire_Click" Visible="false" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="급여명세서확인" ID="btnRetire_disabled" runat="server" />
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="제증명 신청" ID="btnEmployment" runat="server" OnClick="btnEmployment_Click" />
                </p>
                <p style="padding-top: 1.5em;">
                    <asp:Button CssClass="button skyblue mepm_asp_btn" Text="급여명세서확인" ID="btnPay" runat="server" OnClick="btnPay_Click" Visible="false" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="급여명세서확인" ID="btnPay_disabled" runat="server" />
                    <asp:Button CssClass="button disabled mepm_asp_btn" Text="-" ID="Button1" runat="server" />
                </p>
<%--                <p style="padding-top: 1.5em;">
                    <a href="/Pay/m_Pay_Cheak.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn_big">급여 명세서 확인</span></a>
                </p>--%>
            </div>
        </section>
    </article>
</asp:Content>

