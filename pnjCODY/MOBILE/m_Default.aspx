<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_Default.aspx.cs" Inherits="m_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--2016-10-07 김기훈 추가--%>   
    <script type="text/javascript" >
        function fncDetail(strETC_NOT_ID){
            document.getElementById('<%= this.hdETC_NOT_ID.ClientID %>').value = strETC_NOT_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
        
        function fncChkConfirm() {
        //if (confirm('로그인 시 더이상 공고문이 화면에 나타나지 않기를 원하실 경우 확인 버튼을 클릭하세요.') == true) {
        //    var divNotice = $get('<%= this.divNotice.ClientID %>');
        //    return true;
        //}
        //else {
        //    return false;
        //}
    }
       window.onload = function(){
          if('<%= Session["sRES_WorkState"].ToString() %>' == "004")
          {
             document.getElementById("div_Notice").style.display = "none"
          }
          
       }
    </script>

    <input type="hidden" id="hdETC_NOT_ID" name="hdETC_NOT_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <%--2016-10-07 김기훈 추가--%>   
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
    
    <div id="divGv" class="mepm_menu_item" style="padding:0;" runat="server" visible="false">
        <div>
            <p class="mepm_icon_title" style="background-color:white;">기한일이 다가오는 공지사항</p>
        </div>
        
        <asp:GridView ID="gvNoticeList_set" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
            CssClass="table_border" OnRowDataBound="gvNoticeList_RowDataBound" AutoGenerateColumns="false">
        <Columns>
            <asp:boundfield HeaderText="ID" DataField="ETC_NOT_ID" Visible="False">
				<HeaderStyle CssClass="tr_border"/>
				<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
			</asp:boundfield> 
            <asp:boundfield HeaderText="제목" DataField="ETC_NOT_Title">
				<HeaderStyle CssClass="tr_border"/>
				<ItemStyle HorizontalAlign="left" CssClass="tr_border_left"/>
			</asp:boundfield>                    
            <asp:boundfield HeaderText="공지자" DataField="RES_Name">
				<HeaderStyle CssClass="tr_border" Width="60px" />
				<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
			</asp:boundfield>
            <asp:boundfield HeaderText="공지일(기한일)" DataField="CREATED_DATE">
				<HeaderStyle CssClass="tr_border" Width="100px" />
				<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
			</asp:boundfield>
        </Columns>
        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
		<RowStyle CssClass="mepm_menu_item_bg" />
		<HeaderStyle CssClass="mepm_menu_title_bg"/>
        </asp:GridView>            

        <!-- 공지사항 목록 종료 -->
    </div>
    
    <div id="divSupporter" runat="server">
        <p class="mepm_icon_title" style="background-color:white;">관리 메뉴</p>
        <article style="padding-bottom: 1em;">
            <!-- 토글 메뉴 시작 -->
            <section>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle1')"><span><b style="color: #bbb;">+</b> 사원 정보 관리</span></a></div>
                <ul id="toggle1" class="mepm_menu cursor_pointer">
                    <li><a href="/Resource/m_RES_Basic.aspx"><span><b style="color: #bbb;">-</b> 신규 등록</span></a></li>
                    <li><a href="/Resource/m_RES_MyList.aspx"><span><b style="color: #bbb;">-</b> 내 사원 목록</span></a></li>
                    <li><a href="/Resource/m_RES_Serch.aspx"><span><b style="color: #bbb;">-</b> 사원 검색</span></a></li>
                     <li><a href="/Business/m_BUS_Connection.aspx"><span><b style="color: #bbb;">-</b> 업무 연락</span></a></li>
                </ul>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle2')"><span><b style="color: #bbb;">+</b> 근태 정보 관리</span></a></div>
                <ul id="toggle2" class="mepm_menu cursor_pointer">
                    <li><a href="/Attendance/m_ATT_Information.aspx"><span><b style="color: #bbb;">-</b> 일근태 관리</span></a></li>
		    <li><a href="/Attendance/m_ATT_Requset_Today.aspx"><span><b style="color: #bbb;">-</b> 근태 수정요청 현황</span></a></li>
                    <li><a href="/Attendance/m_ATT_Closed_Request.aspx"><span><b style="color: #bbb;">-</b> 휴무 신청서 작성</span></a></li>
                    <li><a href="/Attendance/m_ATT_Closed_Mng.aspx"><span><b style="color: #bbb;">-</b> 휴무 신청서 관리</span></a></li>
                    <li><a href="/Attendance/m_ATT_Closed_Leave_Request.aspx"><span><b style="color: #bbb;">-</b> 연차 신청서 작성</span></a></li>
                    <asp:Label ID="txtLeave_Count" runat="server" Text=""></asp:Label>
                </ul>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle3')"><span><b style="color: #bbb;">+</b> 업무 정보 관리</span></a></div>
                <ul id="toggle3" class="mepm_menu cursor_pointer">
                    <li><a href="/Business/m_BUS_Expend_Request.aspx"><span><b style="color: #bbb;">-</b> 지출 신청서 작성</span></a></li>
                    <li><a href="/Business/m_BUS_Expend_Mng.aspx"><span><b style="color: #bbb;">-</b> 지출 신청서 관리</span></a></li>
                </ul>
<%--                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle4')"><span><b style="color: #bbb;">+</b> 급여 명세 관리</span></a></div>
                <ul id="toggle4" class="mepm_menu cursor_pointer">
                    <li><a href="/Pay/m_Pay_Cheak.aspx"><span><b style="color: #bbb;">-</b> 급여 명세서 확인</span></a></li>
                </ul>--%>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle6')"><span><b style="color: #bbb;">+</b> 일일 업무 관리</span></a></div>
                <ul id="toggle6" class="mepm_menu cursor_pointer">
                    <asp:Label ID="txtNotice_Count" runat="server" Text=""></asp:Label>
                    <!-- 공지사항 <li>부분 -->
                    <%--<li><a href="/Notice/m_NOT_List.aspx"><span><b style="color:#bbb;">-</b> 공지 사항</span></a></li>--%>
                    <%--                <li><a href="/Report/m_REP_Oneday_List.aspx"><span><b style="color:#bbb;">-</b> 일일 업무 보고</span></a></li>       --%>
                    <asp:Label ID="txtNotice_Worker_Support_Count" runat="server" Text=""></asp:Label>
                    <li><a href="/Report/m_REP_Daily_Report_Main.aspx"><span><b style="color: #bbb;">-</b> 업무 일지</span></a></li>
                </ul>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle5')"><span><b style="color: #bbb;">+</b> 개인 신청 관리</span></a></div>
                <ul id="toggle5" class="mepm_menu cursor_pointer">
                    <li><a href="/Document/m_EXP_Employment_List.aspx"><span><b style="color: #bbb;">-</b> 제증명 신청</span></a></li>
                    <li><a href="/Document/m_EXP_Dependents_List.aspx"><span><b style="color: #bbb;">-</b> 피부양자 신청</span></a></li>
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
                    
                    <li class="mepm_icon_li"><a href="/Attendance/m_ATT_Closed_Mng.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp03"></span><span class="mepm_icon_txt">휴무신청관리</span></a></li>
                    <%--<li class="mepm_icon_li"><a href="/Pay/m_Pay_Cheak.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp08"></span><span class="mepm_icon_txt">급여확인</span></a></li>--%>
                    <%--<li class="mepm_icon_li"><a href="javascript:alert('서비스 이용이 제한되었습니다.')" class="mepm_icon_a"><span class="mepm_icon_img bp08"></span><span class="mepm_icon_txt">급여확인</span></a></li>--%>
                    <asp:Label ID="txtLeave_Icon" runat="server" Text=""></asp:Label>
                    <li class="mepm_icon_li"><a href="/Business/m_BUS_Expend_Mng.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp08"></span><span class="mepm_icon_txt">지출신청관리</span></a></li>
                    <%--<li class="mepm_icon_li"><a href="/Report/m_REP_Oneday_List.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp07"></span><span class="mepm_icon_txt">일일업무보고</span></a></li>--%>
                    <li class="mepm_icon_li"><a href="/Report/m_REP_Daily_Report_Main.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp07"></span><span class="mepm_icon_txt">일일업무보고</span></a></li>
                </ul>
            </section>
        </article>
    </div>
    <div id="divUser" runat="server">
        <article style="padding-bottom: 1em;">
            <!-- 토글 메뉴 시작 -->
            <section>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle1')"><span><b style="color: #bbb;">+</b> 급여 명세 관리</span></a></div>
                <ul id="toggle1" class="mepm_menu cursor_pointer">
                    <li><a href="/Pay/m_Pay_Cheak.aspx"><span><b style="color: #bbb;">-</b> 급여 명세서 확인</span></a></li>
                </ul>
                <div class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle2')"><span><b style="color: #bbb;">+</b> 개인 신청 관리</span></a></div>
                <ul id="toggle2" class="mepm_menu cursor_pointer">
                    <li><a href="/Document/m_EXP_Employment_List.aspx"><span><b style="color: #bbb;">-</b> 제증명신청</span></a></li>
                    <li><a href="/Document/m_EXP_Dependents_List.aspx"><span><b style="color: #bbb;">-</b> 피부양자신청</span></a></li>
                    <li><a href="/Attendance/m_ATT_Closed_Leave_Request.aspx"><span><b style="color: #bbb;">-</b> 연차신청서 작성</span></a></li>
                    <li><a href="/Attendance/m_ATT_Closed_Leave_Mng.aspx"><span><b style="color: #bbb;">-</b> 연차신청서 관리</span></a></li>
                </ul>
                <div id="div_Notice" class="mepm_menu_title cursor_pointer"><a onclick="toggle_display('toggle3')"><span><b style="color: #bbb;">+</b> 공지사항 관리</span></a></div>
                  <ul id="toggle3" class="mepm_menu cursor_pointer">
                    <asp:Label ID="txtNotice_Worker_Count" runat="server" Text=""></asp:Label>
                   
                </ul>
            </section>
            <section>
                <p class="mepm_icon_title">코디서비스 바로가기</p>
                <ul class="mepm_icon_ul">
                    <li class="mepm_icon_li"><a href="/Pay/m_Pay_Cheak.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp08"></span><span class="mepm_icon_txt">급여확인</span></a></li>
                    <li class="mepm_icon_li"><a href="/Attendance/m_ATT_Closed_Leave_Request.aspx" class="mepm_icon_a"><span class="mepm_icon_img bp03"></span><span class="mepm_icon_txt">연차신청</span></a></li>
                     <asp:Label ID="txtNotice_Worker_Icon" runat="server" Text=""></asp:Label>
                </ul>
            </section>
        </article>
    </div>
    <div id="divNotice" class="title" runat="server" visible="false">
        <img src="/Notice.PNG" alt="공고문" width="100%" class="mepm_lgm" />
        <br />
        <br />
        <asp:Button CssClass="button gray mepm_asp_btn" Text="공고문 확인" ID="btnConfirm" 
            runat="server" OnClientClick="javascript:return fncChkConfirm();" OnClick="btnConfirm_Click"  />
        <br />
        <br />    
    </div>
</asp:Content>
