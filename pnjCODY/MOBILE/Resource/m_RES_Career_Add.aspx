<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Career_Add.aspx.cs" Inherits="Resource_m_RES_Career_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 근무시작(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckStartDate() {
            if (document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').value == "YYYYMMDD") {
                alert("근무시작일을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("근무시작일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').focus();
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
                        document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else if (num != 0 && num.length == 6) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);

                    num = year + "-" + month;
                }
                else {
                    num = "";
                    alert("근무시작일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtRES_CAR_START.ClientID %>').value = num;
            }
        }

        // 근무종료(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {
            if (document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').value == "YYYYMMDD") {
                alert("근무종료일을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("근무종료일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').focus();
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
                        document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else if (num != 0 && num.length == 6) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);

                    num = year + "-" + month;
                }
                else {
                    num = "";
                    alert("근무종료일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtRES_CAR_FINISH.ClientID %>').value = num;
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
  
        // 저장시 필수 확인
        function fncChkSave() {
            if (document.getElementById('<%= this.txtRES_CAR_COMPANY.ClientID %>').value == "") {
                alert("회사명을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_CAR_COMPANY.ClientID %>').focus();
                return false;
            }

            if (CheckStartDate() == false) return false;

            if (CheckFinishDate() == false) return false;

            if (document.getElementById('<%= this.txtRES_CAR_MAINJOB.ClientID %>').value == "") {
                alert("담당업무를 입력해 주세요.");
                document.getElementById('<%= this.txtRES_CAR_MAINJOB.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_CAR_RETIRE.ClientID %>').value == "") {
                alert("퇴직사유를 입력해 주세요.");
                document.getElementById('<%= this.ddlRES_CAR_RETIRE.ClientID %>').focus();
                return false;
            }

            return true;
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
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Career.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 경력사항 > 경력사항 추가</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p>경력사항 추가</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="삭제" ID="btnDel" 
                                runat="server" OnClientClick="return confirm('삭제 하시겠습니까?');" OnClick="btnDel_Click" />
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:70px;">회사명 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em">
                            <asp:TextBox ID="txtRES_CAR_COMPANY" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">근무시작 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtRES_CAR_START" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out" />
                        </td>
                    </tr>                   
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">근무종료 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtRES_CAR_FINISH" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">담당업무 :</th>
                        <td style="text-align:center; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtRES_CAR_MAINJOB" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">퇴직사유 :</th>
                        <td style="text-align:center; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlRES_CAR_RETIRE" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="COD_Name" DataValueField="COD_CD" Enabled="true" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Career.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>