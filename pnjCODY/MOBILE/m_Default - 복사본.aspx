<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_Default.aspx.cs" Inherits="m_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>모바일 웹</strong></p>
    </header>    
    <div class="title">
        <h2 class="mepm_title">이용하시려는 메뉴를 터치하세요</h2>
    </div> 
    <article style="padding-bottom:1em;">
        <!-- 토글 메뉴 시작 -->
        <section>        
        <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle1')"><span><b style="color:#bbb;">+</b> 사원 정보 관리</span></a></div>
            <ul id="toggle1" class="mepm_menu cursor_pointer">
                <li><a href="/Resource/m_RES_Basic.aspx"><span><b style="color:#bbb;">-</b> 신규 등록</span></a></li>
                <li><a href="/Resource/m_RES_MyList.aspx"><span><b style="color:#bbb;">-</b> 내 사원 목록</span></a></li>
                <li><a href="/Resource/m_RES_Serch.aspx"><span><b style="color:#bbb;">-</b> 사원 검색</span></a></li>
            </ul>
        <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle2')"><span><b style="color:#bbb;">+</b> 근태 정보 관리</span></a></div>
            <ul id="toggle2" class="mepm_menu cursor_pointer">
                <li><a href="/Attendance/m_ATT_Information.aspx"><span><b style="color:#bbb;">-</b> 근태 정보 관리</span></a></li>
                <li><a href="/Attendance/m_ATT_Closed_Request.aspx"><span><b style="color:#bbb;">-</b> 휴무 신청서 작성</span></a></li>
                <li><a href="/Attendance/m_ATT_Closed_Mng.aspx"><span><b style="color:#bbb;">-</b> 휴무 신청서 관리</span></a></li>
            </ul>
        <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle3')"><span><b style="color:#bbb;">+</b> 업무 정보 관리</span></a></div>
            <ul id="toggle3" class="mepm_menu cursor_pointer">
                <li><a href="/Business/m_BUS_Expend_Request.aspx"><span><b style="color:#bbb;">-</b> 지출 신청서 작성</span></a></li>
                <li><a href="/Business/m_BUS_Expend_Mng.aspx"><span><b style="color:#bbb;">-</b> 지출 신청서 관리</span></a></li>
            </ul>
        <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle4')"><span><b style="color:#bbb;">+</b> 급여 명세 관리</span></a></div>
            <ul id="toggle4" class="mepm_menu cursor_pointer">
                <li><a href="/Pay/m_Pay_Cheak.aspx"><span><b style="color:#bbb;">-</b> 급여 명세서 확인</span></a></li>
            </ul>
        <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle5')"><span><b style="color:#bbb;">+</b> 기타 업무 관리</span></a></div>
            <ul id="toggle5" class="mepm_menu cursor_pointer">
                <asp:Label ID="txtNotice_Count" runat="server" Text=""></asp:Label> <!-- 공지사항 <li>부분 -->
                <%--<li><a href="/Notice/m_NOT_List.aspx"><span><b style="color:#bbb;">-</b> 공지 사항</span></a></li>--%>
                <li><a href="/Report/m_REP_Oneday_List.aspx"><span><b style="color:#bbb;">-</b> 일일 업무 보고</span></a></li>                
            </ul>
        </section>
        <!-- 토글 메뉴 종료 -->
        <section>
            <p class="mepm_icon_title">코디서비스 바로가기</p>
            <ul class="mepm_icon_ul">
                <asp:Label ID="txtNotice_Icon" runat="server" Text=""></asp:Label> 
                <%--<li class="mepm_icon_li"><a href="/Notice/m_NOT_List.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp01"></span><span class="mepm_icon_txt">공지사항</span></a></li>--%>
                <li class="mepm_icon_li"><a href="/Resource/m_RES_Basic.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp06"></span><span class="mepm_icon_txt">사원신규등록</span></a></li>
                <li class="mepm_icon_li"><a href="/Resource/m_RES_MyList.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp02"></span><span class="mepm_icon_txt">사원관리</span></a></li>
                <li class="mepm_icon_li"><a href="/Attendance/m_ATT_Information.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp04"></span><span class="mepm_icon_txt">근태정보</span></a></li>
                <li class="mepm_icon_li"><a href="/Pay/m_Pay_Cheak.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp08"></span><span class="mepm_icon_txt">급여확인</span></a></li>
                <li class="mepm_icon_li"><a href="/Attendance/m_ATT_Closed_Mng.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp03"></span><span class="mepm_icon_txt">휴무신청관리</span></a></li>
                <li class="mepm_icon_li"><a href="/Business/m_BUS_Expend_Mng.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp05"></span><span class="mepm_icon_txt">지출신청관리</span></a></li>
                <li class="mepm_icon_li"><a href="/Report/m_REP_Oneday_List.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp07"></span><span class="mepm_icon_txt">일일업무보고</span></a></li>
            </ul>
        </section>
    </article>
</asp:Content>
