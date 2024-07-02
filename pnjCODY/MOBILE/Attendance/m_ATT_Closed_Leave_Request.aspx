<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_ATT_Closed_Leave_Request.aspx.cs" Inherits="Attendance_m_ATT_Closed_Leave_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        // 신정기간 시작(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckStartDate() {
            if (document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value == "YYYYMMDD") {
                alert("시작일을 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("시작일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("시작일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value = num;
            }
        }

        // 신정기간 종료(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {
            if(document.getElementById('<%= this.ddlREQ_TYPE.ClientID %>').value == "012"){
                return true;
            }
            else{
                if (document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').value == "YYYYMMDD") {
                    alert("종료일을 입력해 주세요.");
                    document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').focus();
                    return false;
                }

                var num, year, month, day;
                num = document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').value;

                while (num.search("-") != -1) {
                    num = num.replace("-", "");
                }

                if (isNaN(num)) {
                    num = "";
                    alert("종료일은 숫자만 입력 가능합니다.");
                    document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').focus();
                    return false;
                }
                else {

                    if (num != 0 && num.length == 8) {
                        year = num.substring(0, 4);
                        month = num.substring(4, 6);
                        day = num.substring(6);

                        if (isValidDay(year, month, day) == false) {
                            num = "";
                            alert("유효하지 않은 일자 입니다.");
                            document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').focus();
                            return false;
                        }
                        num = year + "-" + month + "-" + day;
                    }
                    else {
                        num = "";
                        alert("종료일 입력 형식은 YYYYMMDD 입니다.");
                        document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').focus();
                        return false;
                    }

                    document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').value = num;
                }
            }
        }

        // 유효한(존재하는) 일(日) 인지 체크
        function isValidDay(yyyy, mm, dd) {
            var m = parseInt(mm, 10) - 1;
            var d = parseInt(dd, 10);
            var end = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

            if ((yyyy % 4 == 0 && yyyy % 100 != 0) || yyyy % 400 == 0) {
                end[1] = 29;
            }
            return (d >= 1 && d <= end[m]);
        }
  

        // 검색 시 확인
        function fncChkSearch() {
            if (document.getElementById('<%= this.txtRES_Name.ClientID %>').value.length < 2) {
                alert("정확한 이름을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }
            return true;
        }

        // 연차 신청서 대상 선택 시
        function fncSelectRes(strRES_ID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;

            <%= Page.GetPostBackEventReference(this.btnSelectRes) %>    
        }

        // 연차 신청서 대상 선택 시
        function fncSelectResSub(strRES_ID, strGB, strASSCON_ID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;
            document.getElementById('<%= this.hdnGB.ClientID %>').value = strGB;
            document.getElementById('<%= this.hdnASSCONID.ClientID %>').value = strASSCON_ID;

            <%= Page.GetPostBackEventReference(this.btnSelectResSub) %>
        }

        //승인 시 필수확인
        function fncChkConfirm() {
            var num, year, month, day;
            var numExp = /^[0-9]*$/; //숫자 정규식

            // if (!numExp.test(num)) { // . (소숫점) 입력 제외
            // if (isNaN(num)) { // . (소숫점) 입력 허용

            if (document.getElementById('<%= this.ddlREQ_TYPE.ClientID %>').value == "") {
                alert("승인항목을 선택해 주세요.");
                document.getElementById('<%= this.ddlREQ_TYPE.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.hdRES_ID.ClientID %>').value == "") {
                alert("신청사원을 선택해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }

            //신청기간
            if (CheckStartDate() == false) return false;

            if (CheckFinishDate() == false) return false;          


            if (document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value > document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').value) {
                alert("신청기간 종료일이 시작일 보다 이전 일 수 없습니다.");
                document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.txtATT_REQ_Tel.ClientID %>').value == "") {
                alert("연락처를 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_Tel.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.txtATT_REQ_Reason.ClientID %>').value == "") {
                alert("신청사유를 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_Reason.ClientID %>').focus();
                return false;
            }

            // 시청기간이 오늘을 포함하거나 오늘보다 이전 일자를 포함할 경우 지연사유를 반드시 입력한다. (2016-10-06 정창화 수정)
            var today = getToday();
            var dday = getDiffDay(today, document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value);


            if (dday <= 0 && document.getElementById('<%= this.txtATT_REQ_Delay.ClientID %>').value == "") {
                alert("지연사유를 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_Delay.ClientID %>').focus();
                return false;
            }
            
            if (document.getElementById('<%= this.ddlConfirm.ClientID %>').value == "") {
                alert("승인결과를 선택해 주세요. \n\n(신청기간의 시작일자가 오늘 날짜와 같거나 이전일 경우에만 승인결과 항목을 선택할 수 있습니다.)");
                document.getElementById('<%= this.ddlConfirm.ClientID %>').focus();
                return false;
            }
            
            // 승인 시 첨부파일을 반드시 첨부하도록 확인
            if (document.getElementById('<%= this.ddlConfirm.ClientID %>').value == "A" && document.getElementById('<%=this.hdPhotoPath.ClientID %>').value == "") {
                alert("승인 시 첨부파일 입력은 필수 항목 입니다.");
                document.getElementById('<%=this.upPicture.ClientID %>').focus();
                return false;
            }
            
            if (document.getElementById('<%= this.ddlConfirm.ClientID %>').value == "R" && document.getElementById('<%= this.txtRejectReason.ClientID %>').value == "") {
                alert("반려사유를 입력해 주세요.");
                document.getElementById('<%= this.txtRejectReason.ClientID %>').focus();
                return false;
            }
            
            return true;
        }
        
        function setFileName(strfile) {
            document.getElementById('<%= this.hdPhotoPath.ClientID %>').value = strfile;
        }

        // 저장 시 필수 확인
        function fncChkSave() {
            var num, year, month, day;
            var numExp = /^[0-9]*$/; //숫자 정규식

            // if (!numExp.test(num)) { // . (소숫점) 입력 제외
            // if (isNaN(num)) { // . (소숫점) 입력 허용

            if (document.getElementById('<%= this.ddlREQ_TYPE.ClientID %>').value == "") {
                alert("승인항목을 선택해 주세요.");
                document.getElementById('<%= this.ddlREQ_TYPE.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.hdRES_ID.ClientID %>').value == "") {
                alert("신청사원을 선택해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }

            //신청기간
            if (CheckStartDate() == false) return false;

            if (CheckFinishDate() == false) return false;          


            if (document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value > document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').value) {
                alert("신청기간 종료일이 시작일 보다 이전 일 수 없습니다.");
                document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.txtATT_REQ_Tel.ClientID %>').value == "") {
                alert("연락처를 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_Tel.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.txtATT_REQ_Reason.ClientID %>').value == "") {
                alert("신청사유를 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_Reason.ClientID %>').focus();
                return false;
            }

            // 신청 시작날짜가 오늘을 포함하거나 오늘보다 이전 일자를 포함할 경우 지연사유를 반드시 입력한다. (2016-10-06 정창화 수정)
            var today = getToday();
            var dday = getDiffDay(today, document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value);
            
            // 신청 시작날짜를 기준으로 5일 뒤의 날짜가 오늘보다 이후 일경우에만 지연사유를 입력한다. (2019-06-12 정창화 수정)
            if (dday > 5 && document.getElementById('<%= this.txtATT_REQ_Delay.ClientID %>').value == "") {
                alert("지연사유를 입력해 주세요.");
                document.getElementById('<%= this.txtATT_REQ_Delay.ClientID %>').focus();
                return false;
            }
            // 지연사유 항목을 삭제하고 비활성 한다.
            else if (dday <= 5 && document.getElementById('<%= this.txtATT_REQ_Delay.ClientID %>').value != "")
            {
                alert("신청 시작일자를 기준으로 5일이 지나지 않았을 경우 지연사유 항목을 입력하지 않습니다.");
                document.getElementById('<%= this.txtATT_REQ_Delay.ClientID %>').value = "";
                return false
            }

            return true;
        }

        // 날짜 기간 계산
        function fncDuration() {
            if (document.getElementById('<%= this.txtATT_REQ_StartDate.ClientID %>').value != "YYYYMMDD" && document.getElementById('<%= this.txtATT_REQ_FinishDate.ClientID %>').value != "YYYYMMDD")
            {
                if (CheckStartDate() == false) return false;

                if (CheckFinishDate() == false) return false;

                var start_date = document.getElementById('<%=this.txtATT_REQ_StartDate.ClientID %>').value.split('-');
                var finish_date = document.getElementById('<%=this.txtATT_REQ_FinishDate.ClientID %>').value.split('-');

                var startDt = new Date(Number(start_date[0]), Number(start_date[1]) - 1, Number(start_date[2]));
                var endDt = new Date(Number(finish_date[0]), Number(finish_date[1]) - 1, Number(finish_date[2]));
                //if (startDt > endDt) {
                //    alert("신청기간 종료일이 시작일 보다 이전 일 수 없습니다.");
                //    document.getElementById('<%=this.txtATT_REQ_StartDate.ClientID %>').focus();
                //    return false;
                //}
                var Duration = Math.floor(endDt.valueOf() / (24 * 60 * 60 * 1000) - startDt.valueOf() / (24 * 60 * 60 * 1000)) + 1;

                document.getElementById('<%=this.lblATT_REQ_Duration.ClientID %>').innerHTML = Duration;
                document.getElementById('<%=this.hdATT_REQ_Duration.ClientID %>').value = Duration
            }
        }

        // 텍스트박스 기본값 지정 후 클릭시 사라지면서 스타일 지정하기
        function change(obj, strValue) {
            date = document.getElementById('<%= this.hdDate.ClientID %>').value.substring(0, 4); //현재목록의 날짜를 4자리만 가져온다 ex. 2013

            if (obj.value == strValue) {
                obj.value = date;
                obj.className = 'i_f_on'
            }
            else if (obj.value == date) {
                obj.value = strValue;
                obj.className = 'i_f_out2'
            }
        }

        // 오늘 날짜 구하기
        function getToday()
        {
            var date = new Date();

            yyyy = date.getYear();
            if (yyyy < 1000) {
                if (yyyy < 70) {
                    yyyy = 2000 + yyyy;
                }
                else yyyy = 1900 + yyyy;
            }

            mm = date.getMonth() + 1;
            if (mm < 10) {
                mm = '0' + mm;
            }

            dd = date.getDate();
            if (dd < 10) {
                dd = '0' + dd;
            }

            return yyyy + "-" + mm + "-" + dd;

        }

        // 두 날짜의 차이 구하기
        function getDiffDay(startDate, endDate) {
            var diffDay = 0;
            var start_yyyy = startDate.substring(0, 4);
            var start_mm = startDate.substring(5, 7);
            var start_dd = startDate.substring(8, startDate.length);
            var sDate = new Date(start_yyyy, start_mm - 1, start_dd);
            var end_yyyy = endDate.substring(0, 4);
            var end_mm = endDate.substring(5, 7);
            var end_dd = endDate.substring(8, endDate.length);
            var eDate = new Date(end_yyyy, end_mm - 1, end_dd);

            diffDay = Math.ceil((eDate.getTime() - sDate.getTime()) / (1000 * 60 * 60 * 24));

            return diffDay;
        }
    </script>

    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdDate" name="hdDate" runat="server" />
    <input type="hidden" id="hdATT_REQ_Duration" name="hdATT_REQ_Duration" runat="server" />
    <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>


    <input type="hidden" id="hdnGB" name="hdnGB" runat="server" />
    <input type="hidden" id="hdnASSCONID" name="hdnASSCONID" runat="server" />
    <asp:LinkButton ID="btnSelectResSub" runat="server" OnClick="btnSelectResSub_Click"></asp:LinkButton>
    <input type="hidden" id="hdPhotoPath" name="hdPhotoPath" runat="server" />

    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span> </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">연차 신청서 작성</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>연차 신청서</p>
                    </td>
                </tr>
            </table> 
            
            <div ID="divMessage" runat="server" class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;" visible="false">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td style="text-align:center;border-top:1px solid #ccc; background-color:#880000; color:#ffd800;">
                            <h3><asp:Label runat="server" ID="lblMessage" Text="" Fore-Color="#ffd800">익월 3일까지만 연차신청 등록이 가능합니다.</asp:Label></h3>
                        </td>
                    </tr>
                </table>
            </div>
            <div ID="divMessage2" runat="server" class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;" visible="false">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td style="text-align:center;border-top:1px solid #ccc; background-color:#880000; color:#ffd800;">
                            <h3><asp:Label runat="server" ID="Label1" Text="" Fore-Color="#ffd800">중복된 기간의 연차 신청서가 존재합니다.</asp:Label></h3>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 연차신청서 테이블 시작 -->
            <!-- 승인항목선택 항목 시작 -->
             <div class="mepm_menu_title" style="padding: 0;">

                 <table>
                     <tr style="height: 3em;">
                         <td style="width: 65px; text-align: left; padding-left:.5em;">
                             승인항목 :
                         </td>
                         <td style="text-align: left; padding-right:.5em;">
                             <asp:DropDownList ID="ddlREQ_TYPE" runat="server" CssClass="i_f_out" AutoPostBack="true" Width="100%">
                              <asp:ListItem Text="유급_연차" Value ="011"></asp:ListItem>
                            </asp:DropDownList>
                         </td>
                     </tr>
                 </table>

             </div>
             <!-- 승인항목선택 항목 종료 -->
            <!-- 신청사원 조회 시작 -->
            <div class="mepm_menu_item" style="padding: 0;" runat="server" id="dvResSearch">
                <table>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="width: 65px; text-align: left; padding-left:.5em;">
                        신청사원 :
                        </td>
                        <td style="text-align:center; padding-right:.5em;" colspan="2">
                            <asp:TextBox ID="txtRES_Name" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="text-align:center;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvResList" visible="false">
                    <table class="mepm_icon_title">
                        <tr>
                            <td style="width: 100%;">
                                <p>연차 신청서 대상 선택</p>
                            </td>
                        </tr>
                    </table> 
                    <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="이름" DataField="RES_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="주민번호" DataField="RES_PersonNumber">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="사번" DataField="RES_Number">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="상태" DataField="RES_WorkState">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
                    <table>
                        <tr>
                            <td style="width: 100%; height: 15px; border-top:1px solid #ccc; background-color: #fff;">
                            </td>
                        </tr>
                    </table> 
            </div>


            <!-- ASS / CON 상세 시작 -->
            <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvResSubList" visible="false">
                <table class="mepm_icon_title">
                    <tr>
                        <td>
                            <p>연차 신청서 대상 배정/계약 선택</p>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvResSubList" runat="server" CellPadding="0" Width="100%" CssClass="table_border" 
                    OnRowDataBound="gvResSubList_RowDataBound" AutoGenerateColumns="false">
                    <Columns>
                        <asp:boundfield HeaderText="지원사" DataField="RES_Vender">
							<HeaderStyle CssClass="tr_border"/>
							<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						</asp:boundfield>
                        <asp:boundfield HeaderText="소속" DataField="RES_VEN_Area_Name">
							<HeaderStyle CssClass="tr_border"/>
							<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						</asp:boundfield>
                        <asp:boundfield HeaderText="근무부서" DataField="RES_VEN_Office_Name">
							<HeaderStyle CssClass="tr_border"/>
							<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						</asp:boundfield>
                        <asp:boundfield HeaderText="매장" DataField="RES_Store">
							<HeaderStyle CssClass="tr_border"/>
							<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						</asp:boundfield>
                    </Columns>
					<RowStyle CssClass="mepm_menu_item_bg" />
					<HeaderStyle CssClass="mepm_menu_title_bg"/>
                </asp:GridView>
                <table>
                    <tr>
                        <td style="width: 100%; height: 15px; border-top:1px solid #ccc; background-color: #fff;">
                        </td>
                    </tr>
                </table> 
            </div>
            <!-- ASS / CON 상세 종료 -->


            <!-- 신청사원 조회 종료 -->
            <!-- 신청사원 정보 시작 -->
            <div class="mepm_menu_item" style="padding: 0; border-bottom: 0px;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width: 75px; text-align:left; padding-left:.5em;">
                            사번 :
                        </td>
                        <td style="width: auto; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblREQ_RES_ID" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 75px; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            이름 :
                        </td>
                        <td style="width: auto; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_Name" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부문 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_RBS_NAME" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            직종 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_WorkGroup1_NAME" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부서 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_RBS_AREA_NAMEE" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            입사일 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_JoinDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            지원사 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_Vender" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            매장명 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_Store" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 신청사원 정보 종료 -->
            <!-- 신청기간 항목 시작 -->
            <div class="mepm_menu_item" style="padding: 0;">

                <table>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="width: 75px; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            신청기간 :                             
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;" colspan="3">
                           <asp:TextBox ID="txtATT_REQ_StartDate" width="105px" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); fncDuration();" class="i_f_out2"></asp:TextBox> - 
                           <asp:TextBox ID="txtATT_REQ_FinishDate" width="105px" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); fncDuration();" class="i_f_out2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            신청일수 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;" colspan="3">
                            <asp:Label ID="lblATT_REQ_Duration" runat="server" Text="0"></asp:Label> 일
                        </td>
                    </tr>
                    <!-- 신청기간 항목 종료 -->
                </table>

            </div>
            <!-- 연차신청서 테이블 종료 -->


            <!-- 기타 입력 사항 항목 시작 -->
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:100%;">
                        <p>기타 입력 사항</p>
                    </td>
                </tr>
            </table> 
            <div class="mepm_menu_item" style="padding: 0;">
                 <table>
                     <tr style="height: 3em;">
                         <td style="width: 65px; text-align: left; padding-left:.5em;">
                             연락처 :
                         </td>
                         <td style="width:auto; text-align: left; padding-right:.5em;"  colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Tel" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr style="height: 3em;">
                         <td style="border-top: 1px solid #ccc; text-align: left; padding-left:.5em;">
                             신청사유 :
                         </td>
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-right:.5em;"  colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Reason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr style="height: 3em;" runat="server" visible="false">
                         <td style="border-top: 1px solid #ccc; text-align: left; padding-left:.5em;">
                             첨부서류 :
                         </td>
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-right:.5em;"  colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Attatch" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr style="height: 3em;">
                         <td style="border-top: 1px solid #ccc; text-align: left; padding-left:.5em;">
                             지연사유 :
                         </td>
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-right:.5em;" colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Delay" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr class="mepm_menu_title_bg" style="height: 3em;" runat="server" id="trApprove" visible="false">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인일자 :
                        </td>
                        <td style="width:auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblATT_REQ_Approve2Date" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width:65px; border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인번호 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblATT_REQ_ApproveNumber" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                 </table>
             </div>
             <!-- 기타 입력 사항 항목 종료 -->
             
            <!-- 서포터 승인 항목 시작 -->
            
            
            
            <div id="ConfirmReq" runat="server" style="padding: 0;" visible="false">
                <table class="mepm_icon_title">
                    <tr>
                        <td style="width: 100%;">
                            <p>서포터 승인</p>
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0;">
                <table>
                    <tr style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:65px; text-align:left;">첨부파일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:Label ID="lblRES_Picture" runat="server"></asp:Label>
                            <asp:Image ID="imgRES_Picture" ImageUrl="" runat="server" Visible="false"  width="180px" height="240px" />
                            <asp:FileUpload CssClass="i_f_out" ID="upPicture" runat="server" Width="100%" BackColor="white" onchange="setFileName(this.value);" />
                    </tr>
                </table>
            </div>
                <div class="mepm_menu_title" style="padding: 0;"> 
                    <table>
                         <tr style="height: 3em;">
                             <td style="width: 65px; text-align: left; padding-left:.5em;">
                                 승인결과 :
                             </td>
                             <td style="text-align: left; padding-right:.5em;">
                                 <asp:DropDownList ID="ddlConfirm" runat="server" CssClass="i_f_out" Width="100%">
                                    <asp:ListItem Text="-선택-" Value =""></asp:ListItem>
                                    <asp:ListItem Text="승인" Value ="A"></asp:ListItem>
                                    <asp:ListItem Text="반려" Value ="R"></asp:ListItem>
                                </asp:DropDownList>
                             </td>
                         </tr>
                    </table>
               </div>
               <div class="mepm_menu_title" style="padding: 0;">      
                    <table>
                         <tr style="height: 3em;">
                             <td style="width: 65px; text-align: left; padding-left:.5em;">
                                 반려사유 :
                             </td>
                             <td style="text-align: left; padding-right:.5em;">
                                 <asp:TextBox ID="txtRejectReason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                             </td>
                         </tr>
                     </table>
                </div>
            </div>
            <!-- 서포터 승인 항목 종료 -->
             
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="확인" ID="btnConfirm" 
                    runat="server" OnClientClick="javascript:return fncChkConfirm();" OnClick="btnConfirm_Click" Visible="false" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="작성완료" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Attendance/m_ATT_Closed_Leave_Mng.aspx"><span class="button gray mepm_btn">취소</span></a>
            </div>            
        </section>
    </article>
</asp:Content>
