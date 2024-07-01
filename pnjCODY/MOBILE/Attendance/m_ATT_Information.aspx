<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_ATT_Information.aspx.cs" Inherits="Attendance_m_ATT_Information" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function fncDetail(srtMode){
            document.getElementById('<%= this.hdMODE.ClientID %>').value =  srtMode
            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdMODE" name="hdMODE" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
               <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">일근태 관리</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>            
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >오늘의 근태 현황</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px;">담당자 :</th>
                        <td style="width:auto; padding-right:0.5em;" colspan="4">
                            <asp:Label runat="server" ID="lblRES_Name"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">부문 :</th>
                        <td style="border-top:1px solid #ccc; padding-right:0.5em;" colspan="3">
                            <asp:Label runat="server" ID="lblRES_RBS_NAME"></asp:Label>
                        </td>
                    </tr>                    
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">부서 :</th>
                        <td style="border-top:1px solid #ccc; padding-right:0.5em;" colspan="3">
                            <asp:Label runat="server" ID="lblRES_RBS_AREA_NAME"></asp:Label>
                        </td>
                    </tr>
                    <tr onclick="fncDetail('C');" class="mepm_menu_title_bg" style="height:4.5em; cursor: pointer;">
                        <th style="border-top:1px solid #ccc;">코디 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;">
                            출근 <asp:Label runat="server" ID="lblCODY_ATT_CNT" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="text-align:left;border-top:1px solid #ccc;">
                            결근 <asp:Label runat="server" ID="lblCODY_ABS_CNT" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="text-align:left;border-top:1px solid #ccc;">
                            휴가(무) <asp:Label runat="server" ID="lblCODY_HOL_CNT" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr onclick="fncDetail('A');" class="mepm_menu_title_bg" style="height:4.5em; cursor: pointer;">
                        <th style="border-top:1px solid #ccc;">계약 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;">
                            출근 <asp:Label runat="server" ID="lblAR_ATT_CNT" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="text-align:left;border-top:1px solid #ccc;">
                            결근 <asp:Label runat="server" ID="lblAR_ABS_CNT" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="text-align:left;border-top:1px solid #ccc;">
                            휴가(무) <asp:Label runat="server" ID="lblAR_HOL_CNT" Font-Bold="true">-</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </section>
    </article>
</asp:Content>