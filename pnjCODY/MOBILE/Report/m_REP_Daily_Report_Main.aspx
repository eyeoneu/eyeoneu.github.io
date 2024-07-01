<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_REP_Daily_Report_Main.aspx.cs" Inherits="m_REP_Daily_Report_Main" %>
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
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 일지</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>            
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >업무 일지 현황</p></td>
                    </tr>
                </table>

            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 65px; text-align:right;">
                            일자 :
                        </td>
                        <td style="width: auto; text-align: left; padding-left: 1em;">
                            <asp:Label ID="lblDate" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 65px; text-align:right;">
                            서포터 :
                        </td>
                        <td style="width: auto; text-align: left; padding-left: 1em;">
                            <asp:Label ID="lblRES_Name" runat="server" Text="" Style="font-weight: bold;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <p style="padding-top: .5em; width:100%;">
                <a href="/Report/m_REP_Attendance.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" runat="server" id="btnAttendance">
                    <asp:Label ID="lblAttendance" runat="server" Text="출근" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>   
                <a href="/Report/m_REP_Leave.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" runat="server" id="btnLeave">
                    <asp:Label ID="lblLeave" runat="server" Text="퇴근" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>             
                <p style="padding-top: 1.5em; width:100%;">
                <a href="/Report/m_REP_Oneday_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn_big" runat="server" id="btnDailyReport">
                    <asp:Label ID="lblDailyReport" runat="server" Text="일일업무보고" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
                <p style="padding-top: 1.5em;">
                <a href="/Report/m_REP_Daily_Mng_Drive.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" runat="server" id="btnDailyDrive">
                    <asp:Label ID="lblDrv" runat="server" Text="자차운영일지" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a> 
                <a href="/Report/m_REP_Transportation_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" runat="server" id="btnDrvieCost">
                    <asp:Label ID="lblTrn" runat="server" Text="교통비정산서" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
                <p style="padding-top: 1.5em;">
                <a href="/Report/m_REP_Vacancy_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" runat="server" id="btnVacancy">
                    <asp:Label ID="lblVacancy" runat="server" Text="결원매장" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>                
                <a href="/Report/m_REP_Market_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" runat="server" id="btnMarketResearch"> 
                    <asp:Label ID="lblMarket" runat="server" Text="시장조사" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
            </div>

        </section>
    </article>
</asp:Content>