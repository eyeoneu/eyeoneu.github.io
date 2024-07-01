<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_AC_Change.aspx.cs" Inherits="m_AC_Change" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 검색 시 확인
        function fncChkSearch() {
            if (document.getElementById('<%= this.txtRES_Name.ClientID %>').value.length < 2) {
                alert("정확한 이름을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }
            return true;
        }

        // 정보변경대상 선택 시
        function fncSelectRes(strRES_ID) {
            document.getElementById('<%= this.hdnAssConRES.ClientID %>').value = strRES_ID;

            <%= Page.GetPostBackEventReference(this.btnSelectRes) %>
        }

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

            if (document.getElementById('<%=this.hdnResName.ClientID %>').value == "") {
                alert("사원을 선택하세요.");
                document.getElementById('<%=this.hdnResName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.ddlResType.ClientID %>').value == "") {
                alert("변경항목을 선택하세요.");
                document.getElementById('<%=this.ddlResType.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%=this.ddlResType.ClientID %>').value == "매장이동") {
                if (document.getElementById('<%=this.ddlResAfter3.ClientID %>').value == "") {
                    alert("변경 후 거래처 항목을 선택하세요.");
                    document.getElementById('<%=this.ddlResAfter3.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.ddlResAfter2.ClientID %>').value == "") {
                    alert("변경 후 매장 항목을 선택하세요.");
                    document.getElementById('<%=this.ddlResAfter2.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.hdnAssConStore.ClientID %>').value == document.getElementById('<%= this.ddlResAfter3.ClientID %>').options[document.getElementById('<%= this.ddlResAfter3.ClientID %>').selectedIndex].text + " " + document.getElementById('<%= this.ddlResAfter2.ClientID %>').options[document.getElementById('<%= this.ddlResAfter2.ClientID %>').selectedIndex].text) {
                    alert("같은 내용은 저장할 수 없습니다.");
                    document.getElementById('<%=this.ddlResAfter2.ClientID %>').focus();
                    return false;
                }
                else
                    return true;
            }
            if (document.getElementById('<%=this.ddlResType.ClientID %>').value == "서포터지역변경") {
                if (document.getElementById('<%=this.ddlResAfter4.ClientID %>').value == "") {
                    alert("변경 후 부문 항목을 선택하세요.");
                    document.getElementById('<%=this.ddlResAfter4.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.ddlResAfter2.ClientID %>').value == "") {
                    alert("변경 후 부서 항목을 선택하세요.");
                    document.getElementById('<%=this.ddlResAfter2.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.ddlResBefore.ClientID %>').value == document.getElementById('<%= this.ddlResAfter2.ClientID %>').value) {
                    alert("같은 내용은 저장할 수 없습니다.");
                    document.getElementById('<%=this.ddlResAfter2.ClientID %>').focus();
                    return false;
                }
                else
                    return true;
            }
            if (document.getElementById('<%=this.ddlResType.ClientID %>').value == "지원사변경") {
                if (document.getElementById('<%=this.ddlResAfter.ClientID %>').value == "") {
                    alert("변경 후 항목을 선택하세요.");
                    document.getElementById('<%=this.ddlResAfter.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.ddlResBefore.ClientID %>').value == document.getElementById('<%= this.ddlResAfter.ClientID %>').value) {
                    alert("같은 내용은 저장할 수 없습니다.");
                    document.getElementById('<%=this.ddlResAfter.ClientID %>').focus();
                    return false;
                }
                else
                    return true;
            }
            if (document.getElementById('<%=this.ddlResType.ClientID %>').value == "월계약정보변경") {
                if (document.getElementById('<%=this.ddlRES_CON_TIME.ClientID %>').value == "") {
                    alert("변경 후 일근무시간 항목을 선택하세요.");
                    document.getElementById('<%=this.ddlRES_CON_TIME.ClientID %>').focus();
                    return false;
                }
                if (document.getElementById('<%=this.txtRES_CON_PAY.ClientID %>').value == "") {
                    alert("변경 후 월급여 항목을 입력하세요.");
                    document.getElementById('<%=this.txtRES_CON_PAY.ClientID %>').focus();
                }
                else
                    return true;
            }

            if (document.getElementById('<%=this.txtResReason.ClientID %>').value == "") {
                alert("변경사유를 입력하세요.");
                document.getElementById('<%=this.txtResReason.ClientID %>').focus();
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
        <input type="hidden" id="hdnResName" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnAssConID" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnAssConStore" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnAssConGB" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnAssConRES" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnAssConVender" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnWorkGroup2" value="" style='width:1px;' runat="server" />
        <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Business/m_BUS_Connection.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 사원정보 변경</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >사원정보 변경</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>

            <div ID="divMsg" runat="server" class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;" visible="false">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td style="text-align:center;border-top:1px solid #ccc; background-color:#880000; color:#ffd800;">
                            <h3><asp:Label runat="server" ID="lblMsg" Text="" Fore-Color="#ffd800"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
            </div>

             <!-- 신청사원 조회 시작 -->
            <div class="mepm_menu_item" style="padding: 0;" runat="server" id="dvResSearch">
                <table>
                   <tr class="mepm_menu_title_bg" style="height:3em;">
                       <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">사원선택 :</th>
                        <td style="border-top:1px solid #ccc; text-align:center; padding-right:.5em;">
                            <asp:TextBox ID="txtRES_Name" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="text-align:center; border-top:1px solid #ccc; ">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
              <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvResList" visible="false">
                    <table class="mepm_icon_title">
                        <tr>
                            <td style="width: 100%;">
                                <p>사원정보 변경 대상 선택</p>
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
                            <asp:boundfield HeaderText="매장" DataField="RES_Store">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="사번" DataField="RES_Number">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>                            
                            <asp:BoundField HeaderText="GB" DataField="GB" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ASSCON_ID" DataField="ASSCON_ID" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataRowStyle Height="50px" HorizontalAlign="Center" CssClass="empty_border" />
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
       
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;"> 선택정보 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtResName" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false" />
                        </td>
                    </tr>
                      <tr class="mepm_menu_item_bg" style="height:3em;">
                         <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">작성일자 :</th>
                         <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtDate" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">변경일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >                            
                            <asp:TextBox ID="txtDueDate" width="100%" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); CheckDueDate();" class="i_f_out2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">변경항목 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlResType" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="SetddlResBefore">                        
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div runat="server" visible="false" id="divConInfo" >
                <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                    <table>
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <th style="width:80px; text-align:right; padding-right:.8em;">TO 선택 :</th>
                            <td style="text-align:left; border-top:0px solid #ccc; padding-right:.8em;" colspan="2">
                                <asp:DropDownList ID="ddlToList" style="width:100%;" runat="server"
                                    CssClass="i_f_out" AutoPostBack="true" DataTextField="TO_Name" 
                                    DataValueField="TO_NUM" OnSelectedIndexChanged="ddlToList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>            
                <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                    <table>
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <th style="width:80px; text-align:right; padding-right:.8em;">변경 전 :</th>
                            <td style="text-align:left; padding-right:.8em;">
                                <asp:Label ID="lblConInfoBefore" runat="server">일근무시간(0시간), 월급여(0원)</asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>            
                <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                    <table>
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <th style="width:80px; text-align:right; padding-right:.8em;">변경 후 :</th>
                            <td style="text-align:left; padding-right:.8em;">
                                <asp:DropDownList ID="ddlRES_CON_TIME" style="width:65px;" runat="server" CssClass="i_f_out">
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
                                </asp:DropDownList> 시간, 
                                <asp:TextBox ID="txtRES_CON_PAY" runat="server" MaxLength="20" style="width:75px;" onfocus="this.className='i_f_on';" onblur="fncComma(this.value); this.className='i_f_out'" class="i_f_out" /> 원
                            </td>
                        </tr>
                     </table>
                </div>            
            </div>

            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="divBefore">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;">변경 전 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                            <asp:DropDownList ID="ddlResBefore" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="false">                                
                            </asp:DropDownList>
                              <asp:Label ID="txtResBefore2" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>    
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="divAfter">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;">변경 후 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                            <asp:DropDownList ID="ddlResAfter" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="false">                                
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlResAfter3" runat="server" CssClass="i_f_out" Width="49%" AutoPostBack="true" Enabled="true" Visible="false" OnSelectedIndexChanged="SetddlStore">                              
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlResAfter4" runat="server" CssClass="i_f_out" Width="49%" AutoPostBack="true" Enabled="true" Visible="false" OnSelectedIndexChanged="SetddlAssArea">                                
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlResAfter2" runat="server" CssClass="i_f_out" Width="49%" AutoPostBack="false" Enabled="true" Visible="false" >                                
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
                             <asp:TextBox ID="txtResReason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>        
           
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/TO/m_AC_Change_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
