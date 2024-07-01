<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Assignment_New.aspx.cs" Inherits="Resource_m_RES_Assignment_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 현재날짜 생성 (yyyy-mm-dd)
        function getTimeStamp() {
            var d = new Date();

            var s =
            leadingZeros(d.getFullYear(), 4) + '-' +
            leadingZeros(d.getMonth() + 1, 2) + '-' +
            leadingZeros(d.getDate(), 2); // + ' ' +

            //            leadingZeros(d.getHours(), 2) + ':' +
            //            leadingZeros(d.getMinutes(), 2) + ':' +
            //            leadingZeros(d.getSeconds(), 2);

            return s;
        }

        function leadingZeros(n, digits) {
            var zero = '';
            n = n.toString();

            if (n.length < digits) {
                for (i = 0; i < digits - n.length; i++)
                    zero += '0';
            }
            return zero + n;
        }

        // 배정시작(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckStartDate() {
            if (document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value == "YYYYMMDD") {
                alert("배정시작일을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("배정시작일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
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
                        document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("배정시작일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value = num;
            }
        }

        // 배정종료(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {
            if (document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').value == "YYYYMMDD") {
//                alert("배정종료일을 입력해 주세요.");
//                document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').focus();
//                return false;
            }
            else {
                var num, year, month, day;
                num = document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').value;

                while (num.search("-") != -1) {
                    num = num.replace("-", "");
                }

                if (isNaN(num)) {
                    num = "";
                    alert("배정종료일은 숫자만 입력 가능합니다.");
                    document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').focus();
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
                            document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').focus();
                            return false;
                        }
                        num = year + "-" + month + "-" + day;
                    }
                    else {
                        num = "";
                        alert("배정종료일 입력 형식은 YYYYMMDD 입니다.");
                        document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').focus();
                        return false;
                    }

                    document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').value = num;
                }
            }
        }

        // 현재 시각을 Time 형식으로 리턴
        function getCurrentTime() {
            return toTimeString(new Date());
        }

        // YYYY-MM-01 형식으로 리턴
        function toFirstDay(num) {
            year = num.substring(0, 4);
            month = num.substring(4, 6);

            num = year + "-" + month + "-01";

            return num;
        }

        // 주어진 Time 과 y년 m월 d일 차이나는 Time을 리턴
        function shiftTime(time, y, m, d) { //moveTime(time,y,m,d,h)
            var date = toTimeObject(time);

            date.setFullYear(date.getFullYear() + y); //y년을 더함
            date.setMonth(date.getMonth() + m);       //m월을 더함
            date.setDay(date.getDay() + d);         //d일을 더함

            return toTimeString(date);
        }

        // Time 스트링을 자바스크립트 Date 객체로 변환
        function toTimeObject(time) { //parseTime(time)
            var year = time.substr(0, 4);
            var month = time.substr(4, 2) - 1; // 1월=0,12월=11
            var day = time.substr(6, 2);

            return new Date(year, month, day);
        }

        // 자바스크립트 Date 객체를 Time 스트링으로 변환
        function toTimeString(date) { //formatTime(date)
            var year = date.getFullYear();
            var month = date.getMonth() + 1; // 1월=0,12월=11이므로 1 더함
            var day = date.getDate();

            if (("" + month).length == 1) { month = "0" + month; }
            if (("" + day).length == 1) { day = "0" + day; }

            return ("" + year + month + day)
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
  
        function fncChkSave() {
            if(document.getElementById('<%= this.hdRES_WorkGroup1.ClientID %>').value == "002")
            {
                if (document.getElementById('<%= this.ddlToList.ClientID %>').value == "") {
                    alert("TO를 선택해 주세요.");
                    document.getElementById('<%= this.ddlToList.ClientID %>').focus();
                    return false;
                }
            }
           if (document.getElementById('<%= this.ddlRES_ASS_VENDER_CD.ClientID %>').value == "") {
               alert("지원사를 선택해 주세요.");
               document.getElementById('<%= this.ddlRES_ASS_VENDER_CD.ClientID %>').focus();
               return false;
           }
           if (document.getElementById('<%= this.ddlRES_ASS_VEN_AREA_CD.ClientID %>').value == "") {
               alert("소속을 선택해 주세요.");
               document.getElementById('<%= this.ddlRES_ASS_VEN_AREA_CD.ClientID %>').focus();
               return false;
           }
           if (document.getElementById('<%= this.ddlRES_ASS_VEN_OFFICE_CD.ClientID %>').value == "") {
               alert("근무부서를 선택해 주세요.");
               document.getElementById('<%= this.ddlRES_ASS_VEN_OFFICE_CD.ClientID %>').focus();
               return false;
           }
           if (document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE_L2.ClientID %>').value == "") {
               alert("매장을 선택해 주세요.");
               document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE_L2.ClientID %>').focus();
               return false;
           }

           // 배정시작일 날짜형식 체크
           if (CheckStartDate() == false) return false;

           // 배정종료일 날짜형식 체크
           if (CheckFinishDate() == false) return false;

           if (document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value > document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').value) {
               alert("배정종료일이 배정시작일 보다 이전 일 수 없습니다.");
               document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
               return false;
           }



           //// 매월 1일까지 이전 달 수정권한 OPEN 시작 ///////////////////////////////////////////////////////////////////////////
           // 신규 등록 시

           var today = "";
           var referenceday = ""; // 기준일
           today = getTimeStamp()

           // 현재일이 1일 일 경우
           if (today == toFirstDay(shiftTime(getCurrentTime(), 0, 0, 0)))
               referenceday = toFirstDay(shiftTime(getCurrentTime(), 0, -1, -1)); // 이전 달의 1일
           else
               referenceday = toFirstDay(shiftTime(getCurrentTime(), 0, 0, -1)); // 이번 달의 1일


           if ('<%= this.Page.Request["RES_ID"]%>' != "" && '<%= this.Page.Request["RES_ASS_ID"]%>' == "") {
               if (referenceday > document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value) {
                   alert(" 배정시작일이 " + referenceday + " 보다 이전 일 수 없습니다.");
                   document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
                   return false;
               }
           }

           // 수정 시
           if ('<%= this.Page.Request["RES_ID"]%>' != "" && '<%= this.Page.Request["RES_ASS_ID"]%>' != "") {
               if ('<%= this.txtRES_ASS_STARTDATE.Enabled %>' == 'True') {
                   if (referenceday > document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value) {
                       alert(" 배정시작일이 " + referenceday + " 보다 이전 일 수 없습니다.");
                       document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
                       return false;
                   }
               }

               if ('<%= this.txtRES_ASS_FINISHDATE.Enabled %>' == 'True') {
                   if (referenceday > document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').value) {
                       alert(" 배정종료일이 " + referenceday + " 보다 이전 일 수 없습니다.");
                       document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').focus();
                       return false;
                   }
               }
           }
           //// 매월 1일까지 이전 달 수정권한 OPEN 끝 ///////////////////////////////////////////////////////////////////////////

////  임시 허용  ///////////////////////////////////////////////////////////////////////////////////////////////////////
//           var today = "";
//           today = getTimeStamp()
//            // 신규 등록 시
//           if ('<%= this.Page.Request["RES_ID"]%>' != "" && '<%= this.Page.Request["RES_ASS_ID"]%>' == "")
//            {
//                if (today > document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value) {
//                    alert(" 배정시작일이 현재일 보다 이전 일 수 없습니다.");
//                    document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
//                    return false;
//                }
//            }

//            // 수정 시
//            if ('<%= this.Page.Request["RES_ID"]%>' != "" && '<%= this.Page.Request["RES_ASS_ID"]%>' != "")
//            {
//                if ('<%= this.txtRES_ASS_STARTDATE.Enabled %>' == 'True') {
//                    if (today > document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').value) {
//                        alert(" 배정시작일이 현재일 보다 이전 일 수 없습니다.");
//                        document.getElementById('<%= this.txtRES_ASS_STARTDATE.ClientID %>').focus();
//                        return false;
//                    }
//                }

//                if ('<%= this.txtRES_ASS_FINISHDATE.Enabled %>' == 'True') {
//                    if (today > document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').value) {
//                        alert(" 배정종료일이 현재일 보다 이전 일 수 없습니다.");
//                        document.getElementById('<%= this.txtRES_ASS_FINISHDATE.ClientID %>').focus();
//                        return false;
//                    }
//                }
//            }
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
    </script>    
    <input type="hidden" id="hdDate" name="hdDate" runat="server" />
    <input type="hidden" id="hdRES_WorkType" name="hdRES_WorkType" runat="server" />
    <input type="hidden" id="hdRES_WorkGroup1" name="hdRES_WorkGroup1" runat="server" />
    <input type="hidden" id="hdRES_WorkGroup2" name="hdRES_WorkGroup2" runat="server" />
    <input type="hidden" id="hdRES_WorkGroup3" name="hdRES_WorkGroup3" runat="server" />
    <input type="hidden" id="hdRES_RBS_CD" name="hdRES_RBS_CD" runat="server" />
    <input type="hidden" id="hdRES_RBS_AREA_CD" name="hdRES_RBS_AREA_CD" runat="server" />
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Assignment.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 관리 > 배정정보</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >신규배정</p></td>

                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="삭제" ID="btnDel" 
                                runat="server" OnClientClick="return confirm('삭제 하시겠습니까?');" OnClick="btnDel_Click" />
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3em;">
                        <th style="width:70px;">대상 :</th>
                        <td style="width:auto; text-align:left; padding-right:0.8em;" colspan="2">
                            <asp:Label runat="server" ID="lblRES_Name" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">서포터 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:Label runat="server" ID="lblRES_ASS_SUPPORTER" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">TO 선택 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlToList" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="TO_Name" 
                                DataValueField="TO_NUM" OnSelectedIndexChanged="ddlToList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">지원사 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_ASS_VENDER_CD" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_Name" 
                                DataValueField="COD_CD" OnSelectedIndexChanged="ddlRES_ASS_VENDER_CD_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">소속 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">                            
                            <asp:DropDownList ID="ddlRES_ASS_VEN_AREA_CD" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD" OnSelectedIndexChanged="ddlRES_ASS_VEN_AREA_CD_SelectedIndexChanged">
                                <asp:ListItem Text="-선택-" Value=""></asp:ListItem>
                            </asp:DropDownList>  
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">근무부서 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_ASS_VEN_OFFICE_CD" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD">
                                <asp:ListItem Text="-선택-" Value=""></asp:ListItem>
                            </asp:DropDownList>  
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">거래처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlEPM_CUSTOMER_STORE" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD" OnSelectedIndexChanged="ddlEPM_CUSTOMER_STORE_SelectedIndexChanged">
                            </asp:DropDownList>                    
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">매장 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlEPM_CUSTOMER_STORE_L2" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD">
                            </asp:DropDownList>                   
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">영업담당 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_ASS_Sales" runat="server" MaxLength="20" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">배정시작 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_ASS_STARTDATE" width="100%" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">배정종료 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_ASS_FINISHDATE" width="100%" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out"></asp:TextBox>
                        </td>
                    </tr>
                </table>                
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Assignment.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>                                
            </div>
        </section>
    </article>
</asp:Content>