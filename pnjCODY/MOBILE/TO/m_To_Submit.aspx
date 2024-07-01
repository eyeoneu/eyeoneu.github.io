<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_To_Submit.aspx.cs" Inherits="m_To_Submit" %>
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

            if (document.getElementById('<%=this.ddlVender.ClientID %>').value == "") {
                alert("지원사를 선택하세요.");
                document.getElementById('<%=this.ddlVender.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlVenArea.ClientID %>').value == "") {
                alert("소속을 선택하세요.");
                document.getElementById('<%=this.ddlVenArea.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlVenOffice.ClientID %>').value == "") {
                alert("근무부서를 선택하세요.");
                document.getElementById('<%=this.ddlVenOffice.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlCustomer.ClientID %>').value == "") {
                alert("거래처를 선택하세요.");
                document.getElementById('<%=this.ddlCustomer.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlStore.ClientID %>').value == "") {
                alert("매장을 선택하세요.");
                document.getElementById('<%=this.ddlStore.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlWorkgroup2.ClientID %>').value == "") {
                alert("직급을 선택하세요.");
                document.getElementById('<%=this.ddlWorkgroup2.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlGB.ClientID %>').value == "") {
                alert("고정/CP/RP를 선택하세요.");
                document.getElementById('<%=this.ddlGB.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.txtTOSubmitReason.ClientID %>').value == "") {
                alert("증원사유를 입력하세요.");
                document.getElementById('<%=this.txtTOSubmitReason.ClientID %>').focus();
                return false;
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

        // 콤마 없애기
        function replaceComma(str) {
            while (str.indexOf(",") > -1) {
                str = str.replace(",", "");
            }
            return str;
        }

        var oldText = ""
        // 일급여 콤마찍기
        function fncComma(strRES_CON_PAY) {
            var rightchar = replaceComma(strRES_CON_PAY);
            var moneychar = "";

            for (index = rightchar.length - 1; index >= 0; index--) {
                splitchar = rightchar.charAt(index);
                if (isNaN(splitchar)) {
                    alert("일급여는 숫자만 입력이 가능합니다.");
                    document.getElementById('<%= this.txtRES_CON_PAY.ClientID %>').value = "";
                    document.getElementById('<%= this.txtRES_CON_PAY.ClientID %>').focus();
                return false;
            }
            moneychar = splitchar + moneychar;
            if (index % 3 == rightchar.length % 3 && index != 0) { moneychar = ',' + moneychar; }
        }
        oldText = moneychar;
        document.getElementById('<%= this.txtRES_CON_PAY.ClientID %>').value = moneychar;
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
        <h2 class="mepm_title">기타 업무 관리 > TO증원</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >TO증원</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">근무형태 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlGB" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlGB_SelectedIndexChanged">      
                                <asp:ListItem Text="고정(진열)" Value="고정(진열)"></asp:ListItem>
                                        <asp:ListItem Text="고정(행사)" Value="고정(행사)"></asp:ListItem>
                                        <asp:ListItem Text="고정(진행)" Value="고정(진행)"></asp:ListItem>
                                        <asp:ListItem Text="순회(진열)" Value="순회(진열)"></asp:ListItem>
                                        <asp:ListItem Text="순회(행사)" Value="순회(행사)"></asp:ListItem>
                                        <asp:ListItem Text="순회(진행)" Value="순회(진행)"></asp:ListItem>
                                        <asp:ListItem Text="격고(진열)" Value="격고(진열)"></asp:ListItem>
                                        <asp:ListItem Text="격고(행사)" Value="격고(행사)"></asp:ListItem>
                                        <asp:ListItem Text="격고(진행)" Value="격고(진행)"></asp:ListItem>
                                        <asp:ListItem Text="CP" Value="CP"></asp:ListItem>
                                        <asp:ListItem Text="RP" Value="RP"></asp:ListItem>
                                        <asp:ListItem Text="고정(내근)" Value="고정(내근)"></asp:ListItem>
                                        
                                        <asp:ListItem Text="고정<미사용>" Value="고정"></asp:ListItem>
                                        <asp:ListItem Text="순회<미사용>" Value="순회"></asp:ListItem>
                                        <asp:ListItem Text="격고<미사용>" Value="격고"></asp:ListItem>               
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                         <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">작성일자 :</th>
                         <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtDate" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">지원사 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlVender" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="SetddlVenArea">                       
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">소속 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlVenArea" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="SetddlVenOffice">                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">근무부서 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlVenOffice" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="false">                     
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">거래처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="SetddlStoreTO">                  
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">매장 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlStore" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="false" >                      
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">직급 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlWorkgroup2" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlWorkGroup2_SelectedIndexChanged">                 
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <div id="divTOSelect" class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" visible="false">
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">일근무시간 :</th>
                            <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                                <asp:DropDownList ID="ddlRES_CON_TIME" style="width:65px;" runat="server" CssClass="i_f_out" >
                                    <asp:ListItem Text="-선택-" Value=""></asp:ListItem>
                                    <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="0.5" Value="0.5"></asp:ListItem>
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="1.5" Value="1.5"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="2.5" Value="2.5"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="3.5" Value="3.5"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="4.5" Value="4.5"></asp:ListItem>
                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="5.5" Value="5.5"></asp:ListItem>
                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="6.5" Value="6.5"></asp:ListItem>
                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                    <asp:ListItem Text="7.5" Value="7.5"></asp:ListItem>
                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                </asp:DropDownList> 시간
                            </td>
                        </tr>
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">일/월급여 :</th>
                            <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                                <asp:TextBox ID="txtRES_CON_PAY" runat="server" MaxLength="20" style="width:75px;" onfocus="this.className='i_f_on';" onblur="fncComma(this.value); this.className='i_f_out'" class="i_f_out" /> 원
                            </td>
                        </tr>
                    </div>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">부서 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlAssArea" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="false">                 
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
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;">증원사유 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtTOSubmitReason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>     
           
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/TO/m_To_Submit_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
