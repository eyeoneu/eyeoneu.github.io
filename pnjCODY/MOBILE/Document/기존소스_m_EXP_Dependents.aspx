<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="기존소스_m_EXP_Dependents.aspx.cs" Inherits="m_EXP_Dependents" %>
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

            if (check_num != m) {
                alert("올바른 주민등록번호가 아닙니다.");
                return false;
            }
        }

        // 저장시 필수 확인
        function fncChkSave() {
            var dateExp = /\d{4}\-\d{2}\-\d{2}/; //날짜 정규식

            if (document.getElementById('<%= this.ddlRES_FAM_Relation.ClientID %>').value == "") {
                alert("관계를 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_FAM_Relation.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.txtRES_FAM_Name.ClientID %>').value == "") {
                alert("성명을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_FAM_Name.ClientID %>').focus();
                return false;
            }

            if (!dateExp.test(document.getElementById('<%=this.txtGetDate.ClientID %>').value)) {
                alert("올바른 날짜 형식이 아닙니다.");
                document.getElementById('<%=this.txtGetDate.ClientID %>').focus();
                return false;
            }

            if (CheckSSN() == false)
                return false

            return true;
        }
        
        // 주민번호 포커스 이동
        function fncAutofocus() {
            if (eval(document.getElementById('<%= this.txtRES_FAM_Pnumber1.ClientID %>').value.length) == 6) {
                document.getElementById('<%= this.txtRES_FAM_Pnumber2.ClientID %>').focus();
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
            | <strong>증명 신청 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a onClick="history.back();"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">증명 신청 관리 > 피부양자 신청</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td ><p >피부양자 신청</p></td>
                </tr>
            </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">일자 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc; padding-right: .8em;">
                            <asp:TextBox ID="txtDate" runat="server" MaxLength="10" style="width:100%;" Enabled="false" value="YYYY-MM-DD" onfocus="change(this,'YYYY-MM-DD')" onblur="change(this,'YYYY-MM-DD')" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;text-align:right;padding-right: .8em;">구분 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;">
                            <asp:RadioButtonList ID="rdoGB" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Text="추가" Value="I" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="삭제" Value="D"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;text-align:right;padding-right: .8em;">관계 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlRES_FAM_Relation" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="CTD_NM" DataValueField="CTD_CD" Enabled="true" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:75px;border-top:1px solid #ccc;text-align:right;padding-right: .8em;">성명 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em;">
                            <asp:TextBox ID="txtRES_FAM_Name" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>

                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;text-align:right;padding-right: .8em;">주민번호 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc;">
                            <asp:TextBox runat="server" ID="txtRES_FAM_Pnumber1" type="tel" style="width:70px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="6"  onkeyup="fncAutofocus();" TabIndex="1" />&nbsp;-
                            <asp:TextBox runat="server" ID="txtRES_FAM_Pnumber2" type="tel" style="width:70px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" MaxLength="7" TabIndex="2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;text-align:right;padding-right: .8em;">취득일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtGetDate" runat="server" MaxLength="10" style="width:100%;" value="YYYY-MM-DD" onfocus="change(this,'YYYY-MM-DD')" onblur="change(this,'YYYY-MM-DD')" class="i_f_out" />
                        </td>
                    </tr>
                </table>
                
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Document/m_EXP_Dependents_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>