<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Join.aspx.cs" Inherits="Resource_m_RES_Join" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 입사일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckJoinDate() {
            if (document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').value == "YYYYMMDD") {
                alert("입사일을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("입사일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("입사일이 유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("입사일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtRES_JoinDate.ClientID %>').value = num;
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

        //--- 저장시 필수 확인
        function fncChkSave() {
            if (CheckJoinDate() == false) return false;

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
            <a href="/Resource/m_RES_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 관리 > 입사 요청</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >사원 정보</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_item" style="border-top: 1px solid #ccc; padding: 0;">
                <table>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <th style="width:110px; border-left: 1px solid #ccc;">
                            사진
                        </th>
                        <th style="width:70px; border-left: 1px solid #ccc;">
                            사번 :
                        </th>
                        <td style="width:auto; text-align: left; padding-right:.8em;">
                            <b>[발급요청]</b><%--<asp:Label id="lblRES_Number" width="100%" runat="server" Font-Size=".85em"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; border-left: 1px solid #ccc;" rowspan="4" align="center" valign="middle">
                             <asp:Image ID="imgRES_Picture" ImageUrl="" runat="server" Visible="false" width="99px" height="134px" />
                        </td>
                        <th style="border-top: 1px solid #ccc; border-left: 1px solid #ccc;">
                            성명 :
                        </th>
                        <td style="border-top: 1px solid #ccc; text-align: left; padding-right: .8em;">
                            <asp:Label id="lblRES_Name" width="100%" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; border-left: 1px solid #ccc;">
                            주민번호 :
                        </th>
                        <td style="border-top: 1px solid #ccc; text-align: left; padding-right: .8em;">                            
                            <asp:Label id="lblRES_PersonNumber" width="100%" runat="server" Font-Size=".85em"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; border-left: 1px solid #ccc;">
                            핸드폰 :
                        </th>
                        <td style="border-top: 1px solid #ccc; text-align: left; padding-right: .8em;">
                            <asp:Label id="lblRES_CP" width="100%" runat="server" Font-Size=".8em"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; border-left: 1px solid #ccc;">
                            입사일 :
                        </th>
                        <td style="border-top: 1px solid #ccc; text-align: left; padding-right: .8em;">
                            <asp:Textbox id="txtRES_JoinDate" width="100%" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out2"></asp:Textbox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="입사요청" ID="btnJoin" 
                                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnJoin_Click" />
                <a href="/Resource/m_RES_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>