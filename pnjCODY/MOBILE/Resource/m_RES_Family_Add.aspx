<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Family_Add.aspx.cs" Inherits="Resource_m_RES_Family_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 외국인 등록번호 유효성 확인
        function CheckFrgSSN() {
            var first = eval(document.getElementById('<%= this.txtRES_FAM_Pnumber1.ClientID %>'));
            var second = eval(document.getElementById('<%= this.txtRES_FAM_Pnumber2.ClientID %>'));
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
            var first = eval(document.getElementById('<%= this.txtRES_FAM_Pnumber1.ClientID %>'));
            var second = eval(document.getElementById('<%= this.txtRES_FAM_Pnumber2.ClientID %>'));
            //var a = first.value.substring(0, 1)
            //var b = first.value.substring(1, 2)
            //var c = first.value.substring(2, 3)
            //var d = first.value.substring(3, 4)
            //var e = first.value.substring(4, 5)
            //var f = first.value.substring(5, 6)
            //var g = second.value.substring(0, 1)
            //var h = second.value.substring(1, 2)
            //var i = second.value.substring(2, 3)
            //var j = second.value.substring(3, 4)
            //var k = second.value.substring(4, 5)
            //var l = second.value.substring(5, 6)
            //var m = second.value.substring(6, 7)

            //if (c > 1) {
            //    alert("올바른 주민등록번호가 아닙니다.");
            //    return false;
            //}
            //if (e > 3) {
            //    alert("올바른 주민등록번호가 아닙니다.");
            //    return false;
            //}

            //var sum = (a * 2) + (b * 3) + (c * 4) + (d * 5) + (e * 6) + (f * 7) + (g * 8) + (h * 9) + (i * 2) + (j * 3) + (k * 4) + (l * 5)
            //var check_num = 11 - (sum % 11)
            //if (check_num == 11) { check_num = 1 }
            //else if (check_num == 10) { check_num = 0 }

            //if (check_num != m) {
            //    alert("올바른 주민등록번호가 아닙니다.");
            //    return false;
            //}
            
            
            var strjumin = first + "-" + second;

            // 주민번호 체크: 2020년 10월 이후 출생자는 더이상 이전 주민번호 규칙을 따르지 않아 값의 형식만 체크 (2023-02-14 정창화 수정)
            var juminRule=/^(?:[0-9]{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[1,2][0-9]|3[0,1]))-[1-8][0-9]{6}$/;

            if(!juminRule.test(rowID.value)) {
	            alert("[주민등록번호] 형식에 맞게 입력하세요!");
	            return false;
            }
        }

        // 저장시 필수 확인
        function fncChkSave() {
            if (document.getElementById('<%= this.txtRES_FAM_Name.ClientID %>').value == "") {
                alert("성명을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_FAM_Name.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_FAM_Relation.ClientID %>').value == "") {
                alert("관계를 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_FAM_Relation.ClientID %>').focus();
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

            //if (document.getElementById('<%= this.txtRES_FAM_CP.ClientID %>').value == "") {
            //    alert("연락처를 입력해 주세요.");
            //    document.getElementById('<%= this.txtRES_FAM_CP.ClientID %>').focus();
            //    return false;
            //}
            
            if (document.getElementById('<%= this.txtRES_FAM_Work.ClientID %>').value == "") {
                alert("직업을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_FAM_Work.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.rdoRES_FAM_Support.ClientID %>').value == "") {
                alert("부양여부를 입력해 주세요.");
                document.getElementById('<%= this.rdoRES_FAM_Support.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.rdoRES_FAM_Together.ClientID %>').value == "") {
                alert("동거여부를 입력해 주세요.");
                document.getElementById('<%= this.rdoRES_FAM_Together.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.rdoRES_FAM_Health.ClientID %>').value == "") {
                alert("의료보험여부를 입력해 주세요.");
                document.getElementById('<%= this.rdoRES_FAM_Health.ClientID %>').focus();
                return false;
            }

            return true;
        }
        
        // 주민번호 포커스 이동
        function fncAutofocus() {
            if (eval(document.getElementById('<%= this.txtRES_FAM_Pnumber1.ClientID %>').value.length) == 6) {
                document.getElementById('<%= this.txtRES_FAM_Pnumber2.ClientID %>').focus();
            }
        }

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
    <style type="text/css">
        dl, dt, dd, ul, ol, li {
            margin: 0;
            padding: 0;
            cursor: pointer;
        }
        ul {
            width: 100%;
            float: initial;
            margin: 0;
            padding: 0;
        }
        li {
            margin-left: 0px;
            float: initial;
        }
        ul .ListStyle {
            color: #ff0000;
        }
        li .ModalPopup {
            table-layout: fixed;
            word-break: break-all;
            line-height: 2.5em;
            border: 1px solid #ffd800;
            padding: 5px 10px 5px 10px;
            background-color: #fff5a9;
            box-shadow: 2px 2px 5px #999;
            font-size: 1.3em;
            position: fixed;
            text-align: left;
            width: 300px;
            height: 50%;
            bottom: 25px;
            left: 50%;
            margin-left:-162px;
        }
    </style>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a onClick="history.back();"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 가족관계 > 가족관계 추가</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >가족관계 추가</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="삭제" ID="btnDel" 
                                runat="server" OnClientClick="return confirm('삭제 하시겠습니까?');" OnClick="btnDel_Click" />
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:75px;">성명 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_FAM_Name" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">관계 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_FAM_Relation" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="CTD_NM" DataValueField="CTD_CD" Enabled="true" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">주민번호 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;">
                            <asp:TextBox runat="server" ID="txtRES_FAM_Pnumber1" type="tel" style="width:70px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="6"  onkeyup="fncAutofocus();" TabIndex="1" />&nbsp;-
                            <asp:TextBox runat="server" ID="txtRES_FAM_Pnumber2" type="tel" style="width:70px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="7" TabIndex="2" />
                        </td>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <input id="chkIsForeigner" name="chkIsForeigner" type="checkbox" />외국인
                        </td>                     
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;" visible="false">
                        <th style="border-top:1px solid #ccc;">연락처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_FAM_CP" runat="server" MaxLength="13" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" onkeyup="javascript:return NumberDash(this);" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">직업 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRES_FAM_Work" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">부양여부 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;" colspan="2">
                            <asp:RadioButtonList ID="rdoRES_FAM_Support" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Text="예" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="아니오" Value="N" Selected></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">동거여부 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;" colspan="2">
                            <asp:RadioButtonList ID="rdoRES_FAM_Together" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Text="예" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="아니오" Value="N" Selected></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">의료보험 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;" colspan="2">
                            <asp:RadioButtonList ID="rdoRES_FAM_Health" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Text="예" Value="Y"></asp:ListItem>
                                <asp:ListItem Text="아니오" Value="N" Selected></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Family.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >피부양자 신청 이력</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView ID="gvLogList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvLogList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="처리일시" >
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="처리상태">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="승인자">
							<HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
            </div>
        </section>
    </article>
</asp:Content>