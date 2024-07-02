<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_BUS_Connection.aspx.cs" Inherits="m_BUS_Connection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">       
    </script>
    <input type="hidden" id="hdMODE" name="hdMODE" runat="server" />
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>            
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >업무 연락</p></td>
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
<%--                <a href="/TO/m_To_Change_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnTOChange">
                    <asp:Label ID="lblTOChange" runat="server" Text="TO 변경" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a> --%><!-- 입력된 정보가 있을땐 className = skyblue를 orange로 -->   
<%--                <p style="padding-top: 1em;">--%>
                <a href="/TO/m_To_Round_Change_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnRound">
                    <asp:Label ID="lblRound" runat="server" Text="순회/격고 매장 변경" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a> <!-- 입력된 정보가 있을땐 className = skyblue를 orange로 -->   
                <p style="padding-top: 1em;">
                <a href="/TO/m_AC_Change_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnACChange">
                    <asp:Label ID="lblACChange" runat="server" Text="사원 정보 변경" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>  
                </p>
                <p style="padding-top: 1em;">
                <a href="/TO/m_Etc_Change_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnEtcChange">
                    <asp:Label ID="lblEtcChange" runat="server" Text="기타변경" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a> 
                </p>
                <p style="padding-top: 1em;">
<%--                <a href="/TO/m_To_Submit_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnTOSubmit">
                    <asp:Label ID="lblTOSubmit" runat="server" Text="TO증원" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
                 <p style="padding-top: 1em;">--%>
                <a href="/Business/m_BUS_Accident_List.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnAccident">
                    <asp:Label ID="lblAccident" runat="server" Text="사고발생보고" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
                <p style="padding-top: 1em;">
                <a href="/Business/m_BUS_Approval_List.aspx"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnApproval">
                    <asp:Label ID="lblApproval" runat="server" Text="고령자근무요청" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
                <p style="padding-top: 1em;">
                <a href="/Business/m_BUS_Join_Confirm_List.aspx"><span class="button skyblue mepm_btn" style="width:80%;" runat="server" id="btnJoinConfirm">
                    <asp:Label ID="lblJoinConfirm" runat="server" Text="재입사요청" Font-Names="NanumGothicWeb, Verdana, Arial, sans-Serif"></asp:Label></span></a>
                </p>
               <%-- <p style="padding-top: 1em;">
                * 이전 2개월 부터 오늘까지 보고된 항목이 숫자로 표시됩니다.
                </p>--%>
            </div>
        </section>
    </article>
</asp:Content>