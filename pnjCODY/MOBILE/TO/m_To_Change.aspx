<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_To_Change.aspx.cs" Inherits="m_To_Change" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 변경일자(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckDueDate() {
            if (document.getElementById('<%= this.txtDueDate.ClientID %>').value == "YYYYMMDD") {
                alert("변경일자를 입력해 주세요.");
                document.getElementById('<%= this.txtDueDate.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtDueDate.ClientID %>').value;

        while (num.search("-") != -1) {
            num = num.replace("-", "");
        }

        if (isNaN(num)) {
            num = "";
            alert("변경일자은 숫자만 입력 가능합니다.");
            document.getElementById('<%= this.txtDueDate.ClientID %>').focus();
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
                        document.getElementById('<%= this.txtDueDate.ClientID %>').focus();
                            return false;
                        }
                        num = year + "-" + month + "-" + day;
                    }
                    else {
                        num = "";
                        alert("변경일자 입력 형식은 YYYYMMDD 입니다.");
                        document.getElementById('<%= this.txtDueDate.ClientID %>').focus();
                        return false;
                    }
                    document.getElementById('<%= this.txtDueDate.ClientID %>').value = num;
            }
        }

        function fncChkSave() {
            if (CheckDueDate() == false) return false;

            if (document.getElementById('<%=this.ddlTO.ClientID %>').value == "0") {
                    alert("TO를 선택하세요.");
                    document.getElementById('<%=this.ddlTO.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.ddlTOType.ClientID %>').value == "") {
                    alert("변경항목을 선택하세요.");
                    document.getElementById('<%=this.ddlTOType.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.txtTOReason.ClientID %>').value == "") {
                    alert("변경사유를 입력하세요.");
                    document.getElementById('<%=this.txtTOReason.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.ddlTOType.ClientID %>').value == "TO삭제") {
                    if (confirm("삭제 변경요청을 하시겠습니까?")) {
                        return true;
                    }
                    else
                        return false;
                }
                if (document.getElementById('<%=this.ddlTOType.ClientID %>').value == "TO직급변경") {
                    if (document.getElementById('<%=this.ddlTOAfter.ClientID %>').value == "") {
                        alert("변경 후 항목을 선택하세요.");
                        document.getElementById('<%=this.ddlTOAfter.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.hdnWorkGroup2.ClientID %>').value == document.getElementById('<%=this.ddlTOAfter.ClientID %>').value) {
                        alert("같은 내용은 저장할 수 없습니다.");
                        document.getElementById('<%=this.ddlTOAfter.ClientID %>').focus();
                    return false;
                }
                else
                    return true;
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
    </script>

    <header>
        <input type="hidden" id="hdnWorkGroup2" value="" style='width:1px;' runat="server" />
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>기타업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Business/m_BUS_Connection.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">기타 업무 관리 > TO 변경</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >TO 변경</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">작성일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtDate" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">TO 선택 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlTO" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="SetddlTOType">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">변경항목 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlTOType" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="SetddlTOBefore">                            
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">변경일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >                            
                            <asp:TextBox ID="txtDueDate" width="100%" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); CheckDueDate();" class="i_f_out2"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="divAfter">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;">변경 후 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                            <asp:DropDownList ID="ddlTOAfter" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>            
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;">변경사유 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtTOReason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>        
           
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/TO/m_To_Change_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
