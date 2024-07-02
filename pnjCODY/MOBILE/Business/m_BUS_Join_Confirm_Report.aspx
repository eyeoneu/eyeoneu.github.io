<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_BUS_Join_Confirm_Report.aspx.cs" Inherits="m_BUS_Join_Confirm_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 주민번호 포커스 이동
        function fncAutofocus() {
            if (eval(document.getElementById('<%= this.txtRES_PersonNumber1.ClientID %>').value.length) == 6) {
                document.getElementById('<%= this.txtRES_PersonNumber2.ClientID %>').focus();
            }
        }
        
        // 과거근무이력 조회 시
        function fncCheck() {
            if (document.getElementById('<%= this.txtRES_Name.ClientID %>').value == "") {
                alert("대상자이름을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }

            // 주민번호 확인
            if (CheckSSN() == false) return false;
        }

        // 주민등록번호 자리수 및 유효성 확인 (2000년 이후 출생자 가능)
        function CheckSSN() {
            var first = eval(document.getElementById('<%= this.txtRES_PersonNumber1.ClientID %>'));
            var second = eval(document.getElementById('<%= this.txtRES_PersonNumber2.ClientID %>'));

            // 주민번호 자릿 수 체크 추가 (주민번호 체크 관련 요구사항 반영, 2018-12-20 정창화)
            if (first.value.toString().length != 6) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }

            if (second.value.toString().length != 7) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }

            var a = first.value.substring(0, 1)
            var b = first.value.substring(1, 2)
            var c = first.value.substring(2, 3)
            var d = first.value.substring(3, 4)
            var e = first.value.substring(4, 5)
            var f = first.value.substring(5, 6)
            var g = second.value.substring(0, 1)
            var h = second.value.substring(1, 2)
            var i = second.value.substring(2, 3)
            var j = second.value.substring(3, 4)
            var k = second.value.substring(4, 5)
            var l = second.value.substring(5, 6)
            var m = second.value.substring(6, 7)

            var curDateObj = new Date(); // 날짜 Object 생성
            var tmpSSN = document.getElementById('<%= this.txtRES_PersonNumber1.ClientID %>').value; // 주민번호
            var curDate = ''; // 현재일자
            var tmpAge = 0; // 임시나이
            var yy = curDateObj.getFullYear(); // 현재년도
            var mm = curDateObj.getMonth() + 1; // 현재월
            if (mm < 10) mm = '0' + mm; // 현재 월이 10보다 작을경우 '0' 문자 합한다
            var dd = curDateObj.getDate(); // 현재일
            if (dd < 10) dd = '0' + dd; // 현재 일이 10보다 작을경우 '0' 문자 합한다
            curDate = yy + mm + dd;

            var genType = document.getElementById('<%= this.txtRES_PersonNumber2.ClientID %>').value.substring(0, 1); // 주민번호 성별구분 문자 추출

            if (genType <= 2) {
                tmpAge = yy - (1900 + parseInt(tmpSSN.substring(0, 2))); // 1, 2 일경우
            } else {
                tmpAge = yy - (2000 + parseInt(tmpSSN.substring(0, 2))); // 그 외의 경우
            }

            var tmpBirthday = tmpSSN.substring(2, 6); // 주민번호 4자리 생일문자 추출

            //if(curDate >= (yy + tmpBirthday)) {
            //    tmpAge += 1;
            //}
            //if (tmpAge >= 57) {
            //    confirm("만 56세 이상입니다. 팀장승인이 되었나요?")
            //}

            if (tmpAge >= 59) {
                confirm((yy - 58) + "년 이전의 생년 입니다. 팀장승인이 되었나요?")
            }

            if (tmpAge.toString().length > 0) {
                tmpAge += '세';
            }

            if (c > 1) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }

            if (e > 3) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }

            var sum = (a * 2) + (b * 3) + (c * 4) + (d * 5) + (e * 6) + (f * 7) + (g * 8) + (h * 9) + (i * 2) + (j * 3) + (k * 4) + (l * 5)
            var check_num = 11 - (sum % 11)
            if (check_num == 11) { check_num = 1 }
            else if (check_num == 10) { check_num = 0 }

            //alert(check_num);
            //alert(m);

            if (check_num != m) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }
        }
        
        function fncSelectRes(strRES_ID, strVender, strCustomer, strStore, strRES_WorkGroup1, strRES_WorkGroup2, strRES_JoinDate, strRES_RetireDate) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;
            document.getElementById('<%= this.hdVender.ClientID %>').value = strVender;
            document.getElementById('<%= this.hdCustomer.ClientID %>').value = strCustomer;
            document.getElementById('<%= this.hdStore.ClientID %>').value = strStore;
            document.getElementById('<%= this.hdRES_WorkGroup1.ClientID %>').value = strRES_WorkGroup1;
            document.getElementById('<%= this.hdRES_WorkGroup2.ClientID %>').value = strRES_WorkGroup2;
            document.getElementById('<%= this.hdRES_JoinDate.ClientID %>').value = strRES_JoinDate;
            document.getElementById('<%= this.hdRES_RetireDate.ClientID %>').value = strRES_RetireDate;
            
            <%= Page.GetPostBackEventReference(this.btnSelectHistory) %>
        }
        
        function CheckJoinDate() {            
            if (document.getElementById('<%= this.txtDuedate_Join.ClientID %>').value == "YYYYMMDD") {
                alert("입사예정일을 입력해 주세요.");
                document.getElementById('<%= this.txtDuedate_Join.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtDuedate_Join.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("입사예정일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtDuedate_Join.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("입사예정일이 유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtDuedate_Join.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("입사예정일의 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtDuedate_Join.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtDuedate_Join.ClientID %>').value = num;
            }
        }

        function CheckRetireDate() {            
            if (document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').value == "" || document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').value == "YYYYMMDD" || document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').value == document.getElementById('<%= this.hdDate.ClientID %>').value.substring(0, 4)) {
                document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').value = "";
            }
            else {
                var num, year, month, day;
                num = document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').value;

                while (num.search("-") != -1) {
                    num = num.replace("-", "");
                }

                if (isNaN(num)) {
                    num = "";
                    alert("퇴사예정일은 숫자만 입력 가능합니다.");
                    document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').focus();
                    return false;
                }
                else {

                    if (num != 0 && num.length == 8) {
                        year = num.substring(0, 4);
                        month = num.substring(4, 6);
                        day = num.substring(6);

                        if (isValidDay(year, month, day) == false) {
                            num = "";
                            alert("퇴사예정일이 유효하지 않은 일자 입니다.");
                            document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').focus();
                            return false;
                        }
                        num = year + "-" + month + "-" + day;
                    }
                    else {
                        num = "";
                        alert("퇴사예정일의 입력 형식은 YYYYMMDD 입니다.");
                        document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').focus();
                        return false;
                    }

                    document.getElementById('<%= this.txtDuedate_Retire.ClientID %>').value = num;
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
        
        // 저장 시
        function fncChkSave() {
            var regExp = /^\s*$/;

            if (regExp.test(document.getElementById('<%=this.txtRES_Name.ClientID %>').value)
                || document.getElementById('<%=this.txtRES_Name.ClientID %>').value == "") {
                alert("대상자이름을 입력해주세요.");
                document.getElementById('<%=this.txtRES_Name.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.txtRES_PersonNumber1.ClientID %>').value)
                || document.getElementById('<%=this.txtRES_PersonNumber1.ClientID %>').value == "") {
                alert("주민번호를 입력해주세요.");
                document.getElementById('<%=this.txtRES_PersonNumber1.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.txtRES_PersonNumber2.ClientID %>').value)
                || document.getElementById('<%=this.txtRES_PersonNumber2.ClientID %>').value == "") {
                alert("주민번호를 입력해주세요.");
                document.getElementById('<%=this.txtRES_PersonNumber2.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.lblStore.ClientID %>').innerHTML)
                || document.getElementById('<%=this.lblStore.ClientID %>').innerHTML == "") {
                alert("과거근무이력을 선택해주세요.");
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.ddlRES_CAR_RETIRE.ClientID %>').value)
                || document.getElementById('<%=this.ddlRES_CAR_RETIRE.ClientID %>').value == "") {
                alert("입사확인 사유를 선택해주세요.");
                document.getElementById('<%=this.ddlRES_CAR_RETIRE.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.ddlVender.ClientID %>').value)
                || document.getElementById('<%=this.ddlVender.ClientID %>').value == "") {
                alert("지원사를 선택해주세요.");
                document.getElementById('<%=this.ddlVender.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.ddlCustomer.ClientID %>').value)
                || document.getElementById('<%=this.ddlCustomer.ClientID %>').value == "") {
                alert("거래처를 선택해주세요.");
                document.getElementById('<%=this.ddlCustomer.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.ddlStore.ClientID %>').value)
                || document.getElementById('<%=this.ddlStore.ClientID %>').value == "") {
                alert("매장을 선택해주세요.");
                document.getElementById('<%=this.ddlStore.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.ddlRES_WorkGroup1.ClientID %>').value)
                || document.getElementById('<%=this.ddlRES_WorkGroup1.ClientID %>').value == "") {
                alert("직종을 선택해주세요.");
                document.getElementById('<%=this.ddlRES_WorkGroup1.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.ddlRES_WorkGroup2.ClientID %>').value)
                || document.getElementById('<%=this.ddlRES_WorkGroup2.ClientID %>').value == "") {
                alert("직급을 선택해주세요.");
                document.getElementById('<%=this.ddlRES_WorkGroup2.ClientID %>').focus();
                return false;
            }

            // 입사예정일, 퇴사예정일
            if (CheckJoinDate() == false) return false;

            
            if (regExp.test(document.getElementById('<%=this.txtRemarks.ClientID %>').value)
                || document.getElementById('<%=this.txtRemarks.ClientID %>').value == ""
                || document.getElementById('<%=this.txtRemarks.ClientID %>').value.length < 30 ) {
                alert("채용사유는 30자 이상으로 작성해주세요.");
                document.getElementById('<%=this.txtRemarks.ClientID %>').focus();
                return false;
            }
            
            if (CheckRetireDate() == false) return false;
        }
    </script>
    <asp:LinkButton ID="btnSelectHistory" runat="server" OnClick="btnSelectHistory_Click"></asp:LinkButton>
    <input type="hidden" id="hdDate" name="hdDate" runat="server" />
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdVender" name="hdVender" runat="server" />
    <input type="hidden" id="hdCustomer" name="hdCustomer" runat="server" />
    <input type="hidden" id="hdStore" name="hdStore" runat="server" />
    <input type="hidden" id="hdRES_WorkGroup1" name="hdRES_WorkGroup1" runat="server" />
    <input type="hidden" id="hdRES_WorkGroup2" name="hdRES_WorkGroup2" runat="server" />
    <input type="hidden" id="hdRES_JoinDate" name="hdRES_JoinDate" runat="server" />
    <input type="hidden" id="hdRES_RetireDate" name="hdRES_RetireDate" runat="server" />
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= Session["sRES_ID"] %>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 재입사요청</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:95%;"><p >과거 근무 이력</p></td>
                    <td align="right" style="font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:110px; text-align:right; padding-right:.8em;">대상자이름 :</th>
                        <td style="text-align:left; padding-right:.8em;" >
                            <asp:TextBox ID="txtRES_Name" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">주민번호 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox runat="server" ID="txtRES_PersonNumber1"  type="tel" style="width:80px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="6" onkeyup="fncAutofocus();" />&nbsp;-
                            <asp:TextBox runat="server" ID="txtRES_PersonNumber2" type="tel" style="width:100px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="7"  />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">과거근무이력 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnCheck" runat="server" OnClientClick="javascript:return fncCheck();" OnClick="btnCheck_Click" />
                            <asp:Label runat="server" ID="lblStore"></asp:Label>
                        </td>
                    </tr>
                    
            </table>   
            </div>
            
            <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvResList" visible="false">
                <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                        CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                    <Columns>
                        <asp:boundfield HeaderText="지원사" DataField="Vender">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
					    </asp:boundfield>
                        <asp:boundfield HeaderText="매장" DataField="Store_DP">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>
                        <asp:boundfield HeaderText="종료일" DataField="FinishDate">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>                            
                    </Columns>
                    <EmptyDataRowStyle Height="50px" HorizontalAlign="Center" CssClass="empty_border" />
				    <RowStyle CssClass="mepm_menu_item_bg" />
				    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                </asp:GridView>
                <table>
                    <tr>
                        <td style="width: 100%; height: 10px; border-top:1px solid #ccc; background-color: #fff;">
                        </td>
                    </tr>
                </table> 
            </div>
            
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:110px; text-align:right; padding-right:.8em;">입사확인 사유 :</th>
                        <td style="text-align:left; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlRES_CAR_RETIRE" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="COD_Name" DataValueField="COD_CD" Enabled="true" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>   
            </div>
            
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:95%;"><p >근무 예정 현황</p></td>
                    <td align="right" style="font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:110px; text-align:right; padding-right:.8em;">지원사 :</th>
                        <td style="text-align:left; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlVender" style="width:100%;" runat="server"
                                CssClass="i_f_out" DataTextField="COD_Name" DataValueField="COD_CD">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">거래처 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;  border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">매장 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;  border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlStore" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">직종 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;  padding-right:.8em;" >
                            <asp:DropDownList ID="ddlRES_WorkGroup1" runat="server" CssClass="i_f_out" AutoPostBack="true" DataTextField="CTD_NM" DataValueField="CTD_CD" 
                            OnSelectedIndexChanged="ddlRES_WorkGroup1_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">직급 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;  padding-right:.8em;" >
                            <asp:DropDownList ID="ddlRES_WorkGroup2" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="CTD_NM" DataValueField="CTD_CD" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">입사예정일 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;  padding-right:.8em;" >
                            <asp:TextBox ID="txtDuedate_Join" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">퇴사예정일 :</th>
                        <td style="text-align:left;border-top:1px solid #ccc;  padding-right:.8em;" >
                            <asp:TextBox ID="txtDuedate_Retire" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">채용사유 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtRemarks" runat="server" style="width:100%;" MaxLength="1000" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                 TextMode="MultiLine" Rows="3"/>
                        </td>
                    </tr>
            </table>   
            </div>
            
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="APP_SubScript" visible="false">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;"> 
                        <td style="text-align:center; border-top:1px solid #ccc; padding-right: .5em;" colspan="3">
                            <span style="color:red;">* 담당자 승인 후 수정할 수 없습니다. 담당자에게 연락하세요.</span><br />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Business/m_BUS_Join_Confirm_List.aspx"><span class="button gray mepm_btn">취소</span></a>
                <%--<a href="/Business/m_REP_Approval_List.aspx"><span class="button gray mepm_btn">취소</span></a>--%>
            </div>
        </section>
    </article>
</asp:Content>
