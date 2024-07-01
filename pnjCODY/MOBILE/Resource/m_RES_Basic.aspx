<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_RES_Basic.aspx.cs" Inherits="Resource_m_RES_Basic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 외국인 등록번호 유효성 확인
        function CheckFrgSSN() {
            var first = eval(document.getElementById('<%= this.txtRES_PersonNumber1.ClientID %>'));
            var second = eval(document.getElementById('<%= this.txtRES_PersonNumber2.ClientID %>'));
            var fgnno = first.value + second.value
            var sum = 0;
            var odd = 0;
            buf = new Array(13);

            for (i = 0; i < 13; i++) buf[i] = parseInt(fgnno.charAt(i));

            odd = buf[7] * 10 + buf[8];

            if (odd % 2 != 0) {
                alert("올바른 주민등록번호가 아닙니다."); 
                return false;
            }
            if ((buf[11] != 6) && (buf[11] != 7) && (buf[11] != 8) && (buf[11] != 9)) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }
            multipliers = [2, 3, 4, 5, 6, 7, 8, 9, 2, 3, 4, 5];

            for (i = 0, sum = 0; i < 12; i++) sum += (buf[i] *= multipliers[i]);

            sum = 11 - (sum % 11);

            if (sum >= 10) sum -= 10;

            sum += 2;
            if (sum >= 10) sum -= 10;

            if (sum != buf[12]) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }
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
            if(mm < 10) mm = '0' + mm; // 현재 월이 10보다 작을경우 '0' 문자 합한다
            var dd = curDateObj.getDate(); // 현재일
            if(dd < 10) dd = '0' + dd; // 현재 일이 10보다 작을경우 '0' 문자 합한다
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

        // 휴대전화번호 형식 체크
        function CheckMobile() {
            if (document.getElementById('<%= this.txtRES_CP.ClientID %>').value == "") {
                alert("핸드폰번호를 입력해 주세요.");
                document.getElementById('<%= this.txtRES_CP.ClientID %>').focus();
                return false;
            }

            var bValid = true;
            var strMobile = document.getElementById('<%= this.txtRES_CP.ClientID %>').value;
            strMobile = strMobile.replace(/\-/gi, "");

            if ((strMobile.length < 10) || (strMobile.length > 11)) bValid = false;
            var filter = /\d/g
            if (!filter.test(strMobile)) bValid = false;
            if (!strMobile.match("^[0][1][0,1,6,7,8,9]")) bValid = false;

            if (!bValid) {
                alert("휴대전화번호를 올바르게 입력해 주세요.");
                document.getElementById('<%= this.txtRES_CP.ClientID %>').focus();
                return false;
            }
        }

        // 전화번호 형식 체크
        function CheckTel() {
//            if (document.getElementById('<%= this.txtRES_TEL.ClientID %>').value == "") {
//                alert("전화번호를 입력해 주세요.");
//                document.getElementById('<%= this.txtRES_TEL.ClientID %>').focus();
//                return false;
//            }

            var num;
            num = document.getElementById('<%= this.txtRES_TEL.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("전화번호를 올바르게 입력해 주세요.");
                document.getElementById('<%= this.txtRES_TEL.ClientID %>').focus();
                return false;
            }

        }

//        //생일 자동입력
//        function fncSetBirth() {
//            var fld1 = eval(document.getElementById('<%= this.txtRES_PersonNumber1.ClientID %>'));

//            if (fld1.value.length == 6) {
//                var yyyy = "";
//                var yy = fld1.value.substring(0, 2);
//                var mm = fld1.value.substring(2, 4);
//                var dd = fld1.value.substring(4, 6);

//                if (yy > "30")
//                    yyyy = "19" + yy;
//                else
//                    yyyy = "20" + yy;

//                document.getElementById('<%= this.txtRES_Birthday.ClientID %>').value = yyyy + "-" + mm + "-" + dd;
//            }
//            else {
//                document.getElementById('<%= this.txtRES_Birthday.ClientID %>').value = "";
//                return false;
//            }
//        }

//        // 년월일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
//        function CheckBirth() {
////             if (document.getElementById('<%= this.txtRES_Birthday.ClientID %>').value == "") {
////                 alert("생년월일을 입력해 주세요.");
////                 document.getElementById('<%= this.txtRES_Birthday.ClientID %>').focus();
////                 return false;
////             }

//             var num, year, month, day;
//             num = document.getElementById('<%= this.txtRES_Birthday.ClientID %>').value;

//             while (num.search("-") != -1) {
//                 num = num.replace("-", "");
//             }

//             if (isNaN(num)) {
//                 num = "";
//                 alert("생년월일은 숫자만 입력 가능합니다.");
//                 document.getElementById('<%= this.txtRES_Birthday.ClientID %>').focus();
//                 return false;
//             }
//             else {

//                 if (num != 0 && num.length == 8) {
//                     year = num.substring(0, 4);
//                     month = num.substring(4, 6);
//                     day = num.substring(6);

//                     if (isValidDay(year, month, day) == false) {
//                         num = "";
//                         alert("유효하지 않은 일자 입니다.");
//                         document.getElementById('<%= this.txtRES_Birthday.ClientID %>').focus();
//                         return false;
//                     }
//                     num = year + "-" + month + "-" + day;
//                 }
//                 else {
//                     num = "";
//                     alert("생년월일 입력 형식은 YYYYMMDD 입니다.");
//                     document.getElementById('<%= this.txtRES_Birthday.ClientID %>').focus();
//                     return false;
//                 }

//                 document.getElementById('<%= this.txtRES_Birthday.ClientID %>').value = num;
//             }
//         }

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

         function fncCheck() {
             if (document.getElementById('<%= this.txtRES_Name.ClientID %>').value == "") {
                 alert("성명을 입력해 주세요.");
                 document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                 return false;
             }

             // 주민번호 확인
             var chkIsForeigner = document.getElementsByName('chkIsForeigner');
             if (chkIsForeigner[0].checked == true) {
                 if (CheckFrgSSN() == false) return false;
             }
             else {
                 if (CheckSSN() == false) return false;
             }
         }


         //저장시 필수 확인
         function fncChkSave() {
             if (document.getElementById('<%= this.txtRES_Name.ClientID %>').value == "") {
                 alert("성명을 입력해 주세요.");
                 document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                 return false;
             }

             // 주민번호 확인
             var chkIsForeigner = document.getElementsByName('chkIsForeigner');
             if (chkIsForeigner[0].checked == true) {
                 if (CheckFrgSSN() == false) return false;
             }
             else {
                 if (CheckSSN() == false) return false;
             }

             if (CheckMobile() == false) return false;

             if (CheckTel() == false) return false;

//             if (CheckBirth() == false) return false;

             if (document.getElementById('<%= this.rdoRES_Marry.ClientID %>').value == "") {
                 alert("결혼여부를 입력해 주세요.");
                 document.getElementById('<%= this.rdoRES_Marry.ClientID %>').focus();
                 return false;
             }

             if (document.getElementById('<%= this.ddlRES_Bank.ClientID %>').value == "") {
                 alert("계좌의 은형명을 선택해 주세요.");
                 document.getElementById('<%= this.ddlRES_Bank.ClientID %>').focus();
                 return false;
             }

             if (document.getElementById('<%= this.txtRES_BankNumber.ClientID %>').value == "") {
                 alert("계좌번호를 입력해 주세요.");
                 document.getElementById('<%= this.txtRES_BankNumber.ClientID %>').focus();
                 return false;
             }

             return true;
         }

         // 주민번호 포커스 이동
         function fncAutofocus() {
             if (eval(document.getElementById('<%= this.txtRES_PersonNumber1.ClientID %>').value.length) == 6) {
                 document.getElementById('<%= this.txtRES_PersonNumber2.ClientID %>').focus();
             }
         }
    </script>

    <!--연락처 관련 스크립트 -->
    <script type="text/javascript">
        //-- 전화번호 (-) 넣기
        function NumberDash(obj) {
            if (obj.value != null && obj.value != '') {
                var RegNotNum = /[^0-9]/g;
                var RegPhonNum = "";
                var DataForm = "";
                var str = obj.value;
                // return blank     
                if (str == "" || str == null) return "";

                // delete not number
                str = str.replace(RegNotNum, '');

                if (str.length < 4) return str;

                if (str.substring(0, 2) == "02" && str.length > 10)
                    str = str.substring(0, 10);
                if (str.substring(0, 1) == "1" && str.length > 8)
                    str = str.substring(0, 8);
                else
                    str = str.substring(0, 11);

                if (str.length > 3 && str.length < 7) {
                    DataForm = "$1-$2";
                    if (str.substring(0, 2) == "02") {
                        DataForm = "$1-$2-$3";
                        RegPhonNum = /([0-9]{2})([0-9]{3})([0-9]+)/;
                    }
                    else
                        RegPhonNum = /([0-9]{3})([0-9]+)/;

                } else if (str.length == 7) {
                    DataForm = "$1-$2";
                    if (str.substring(0, 2) == "02") {
                        DataForm = "$1-$2-$3";
                        RegPhonNum = /([0-9]{2})([0-9]{3})([0-9])/;
                    }
                    else
                        RegPhonNum = /([0-9]{3})([0-9]{4})/;
                } else if (str.length == 8 && str.substring(0, 1) == "1") {
                    DataForm = "$1-$2";
                    RegPhonNum = /([0-9]{4})([0-9]{4})/;
                } else if (str.length == 9 && str.substring(0, 2) == "02") {
                    DataForm = "$1-$2-$3";
                    RegPhonNum = /([0-9]{2})([0-9]{3})([0-9]+)/;
                } else if (str.length > 7 && str.length < 10) {
                    DataForm = "$1-$2-$3";
                    if (str.substring(0, 2) == "02")
                        RegPhonNum = /([0-9]{2})([0-9]{3})([0-9]+)/;
                    else
                        RegPhonNum = /([0-9]{3})([0-9]{3})([0-9]+)/;
                } else if (str.length == 10) {
                    if (str.substring(0, 2) == "02") {
                        DataForm = "$1-$2-$3";
                        RegPhonNum = /([0-9]{2})([0-9]{4})([0-9]+)/;
                    } else {
                        DataForm = "$1-$2-$3";
                        RegPhonNum = /([0-9]{3})([0-9]{3})([0-9]+)/;
                    }
                } else if (str.length > 10) {
                    DataForm = "$1-$2-$3";
                    RegPhonNum = /([0-9]{3})([0-9]{4})([0-9]+)/;
                }

                while (RegPhonNum.test(str)) {
                    str = str.replace(RegPhonNum, DataForm);
                }

                obj.value = str;
            }
        }

    </script>

    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 기본정보</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >기본정보</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px;">성명 :</th>
                        <td style="text-align:left; padding-right:.8em" colspan="2">
                            <asp:TextBox ID="txtRES_Name" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">주민번호 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;">
                            <%--OnChange="javascript:return fncSetBirth(); return false;"--%>
                            <asp:TextBox runat="server" ID="txtRES_PersonNumber1"  type="tel" style="width:70px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="6" onkeyup="fncAutofocus();" TabIndex="1" />&nbsp;-
                            <asp:TextBox runat="server" ID="txtRES_PersonNumber2" type="tel" style="width:70px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="7"  tabindex="2" />
                        </td>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <input id="chkIsForeigner" name="chkIsForeigner" type="checkbox" />외국인</td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">상태확인 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="확인" ID="btnCheck" runat="server" OnClientClick="javascript:return fncCheck();" OnClick="btnCheck_Click" />
                            <asp:Label runat="server" ID="lblMSG"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">핸드폰번호 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_CP" runat="server" MaxLength="13" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" onkeyup="javascript:return NumberDash(this);" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">전화번호 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_TEL" runat="server" MaxLength="13" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" onkeyup="javascript:return NumberDash(this);"  />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">최초입사일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_Birthday" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false"  />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">결혼여부 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;" colspan="2">
                            <asp:RadioButtonList ID="rdoRES_Marry" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Text="미혼" Value="N" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="기혼" Value="Y"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;" runat="server" id="trRES_WorkState" visible="false">
                        <th style="border-top:1px solid #ccc;">근무상태 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_WorkState" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="COD_NAME" DataValueField="COD_CD" Enabled="true" Width="100%">
                                <asp:ListItem Text="-선택=" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">계좌 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_Bank" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="COD_NAME" DataValueField="COD_CD" Enabled="true">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtRES_BankNumber" runat="server" style="width:45%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">참고사항 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_Disabled" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="10" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
