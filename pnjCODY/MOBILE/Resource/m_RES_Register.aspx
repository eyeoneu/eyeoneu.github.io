<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Register.aspx.cs" Inherits="Resource_m_RES_Register" %>
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
            <asp:Button CssClass="button blue" Text="이전단계" ID="btnPageMove" 
                                runat="server" OnClick="btnPageMove_Click" />
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p>선택 사원 정보</p></td>
                    </tr>
                </table>
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
                    <td style="width:40%;"><p >사원 등록 정보 카테고리</p></td>  
                </tr>
            </table>
            <div class="mepm_btn_div">
                <a href="/Resource/m_RES_Basic.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button orange mepm_btn">기본정보</span></a> <!-- 입력된 정보가 있을땐 className = skyblue를 orange로 -->   
                <a href="/Resource/m_RES_Address.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnADD">주소</span></a>             
                <p style="padding-top: 1.5em;">
                <a href="/Resource/m_RES_Employment.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnEMP">고용정보</span></a> 
                <a href="/Resource/m_RES_Career.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnCAR">경력사항</span></a>
                <p style="padding-top: 1.5em;">
                <a href="/Resource/m_RES_Edu.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnEDU">학력정보</span></a> 
                <a href="/Resource/m_RES_Lic.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnLIC">자격정보</span></a>
                <p style="padding-top: 1.5em;">
                <a href="/Resource/m_RES_Family.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnFAM">가족관계</span></a>                
                <a href="/Resource/m_RES_Photo_Add.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button skyblue mepm_btn" runat="server" id="btnPHO">사진</span></a>
                </p>
            </div>
        </section>
    </article>
</asp:Content>