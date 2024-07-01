<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_To_Round_Change.aspx.cs" Inherits="m_To_Round_Change" %>
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
            if (document.getElementById('<%=this.ddlRepresent.ClientID %>').value == "") {
                alert("대표 매장을 선택해야 합니다.");
                document.getElementById('<%=this.ddlRepresent.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%=this.ddlTO.ClientID %>').value == "") {
                alert("TO를 선택하세요.");
                document.getElementById('<%=this.ddlTO.ClientID %>').focus();
                return false;
            }
            if (CheckDueDate() == false) return false;

            if (document.getElementById('<%=this.txtTOReason.ClientID %>').value == "") {
                alert("변경사유를 입력하세요.");
                document.getElementById('<%=this.txtTOReason.ClientID %>').focus();
                return false;
            }
            return true;
        }

        function fncChkVisible() {
            if (document.getElementById('<%=this.ddlTO.ClientID %>').value == "") {
                        alert("TO를 선택하세요.");
                        document.getElementById('<%=this.ddlTO.ClientID %>').focus();
                return false;
            }
            if (CheckDueDate() == false) return false;

            if (document.getElementById('<%=this.txtTOReason.ClientID %>').value == "") {
                alert("변경사유를 입력하세요.");
                document.getElementById('<%=this.txtTOReason.ClientID %>').focus();
                return false;
            }
            return true;
        }


        function fncChkConfirm() {

            if (fncChkStore()) {
                var ddlObject = document.getElementById('<%=this.ddlWork.ClientID %>');
                var selectedText = ddlObject.options[ddlObject.selectedIndex].text;

                if (ddlObject.options[ddlObject.selectedIndex].value != "")
                    return confirm(selectedText + "하시겠습니까?");
                else
                    return false;
            }
            else
                return false;
        }

        function fncChkStore() {
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

            if (document.getElementById('<%=this.txtRemark.ClientID %>').value == "") {
                alert("비고를 입력하세요.");
                document.getElementById('<%=this.txtRemark.ClientID %>').focus();
                return false;
            }
            return true;
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

        function fncChange(roundUid) {
            document.getElementById('<%= this.hdRoundUid.ClientID %>').value = roundUid;
            <%= Page.GetPostBackEventReference(this.btnChange) %>
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
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Business/m_BUS_Connection.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <asp:LinkButton ID="btnChange" runat="server" OnClick="btnChange_Click"></asp:LinkButton>
    <asp:HiddenField ID="hdRoundUid" runat="server" />
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 순회/격고 매장 변경</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:60%;"><p >순회/격고 매장 변경 <asp:Button ID="btnVisible" runat="server" Text="숨기기" OnClick="btnVisible_Click" CssClass="button gray mepm_btn_5.5em" OnClientClick="return fncChkVisible();" /></p> </td>
                    <td align="right" style="width:40%; font-weight:normal; padding-right:0.5em;">
                        <asp:Label ID="lblRoundType" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlBasicInfo" CssClass="wenitepm_div_fix_layout1" runat="server" Width="100%">
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
                            <asp:DropDownList ID="ddlTO" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlTO_SelectedIndexChanged">
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
                        <th style="width:80px; text-align:right; padding-right:.8em;">변경사유 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtTOReason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>        
            </asp:Panel>

            <asp:Panel ID="pnlStores" runat="server" Visible="false">
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >매장 변경</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">거래처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">                  
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
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;">비고 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtRemark" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>

                </table>
            </div>  
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
               <table>
                    <tr class="mepm_menu_item_bg" style="height:2em;">
<%--                        <th style="width:40px; text-align:right; padding-right:.8em;" rowspan="2">근무 :</th>--%>
                        <td colspan="2" style=" text-align:right; border-bottom:1px solid #ccc;">근무 :</td>
                        <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           월
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlMonday" runat="server" CssClass="i_f_out" Border="0" Width="100%" AutoPostBack="false" Enabled="true">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                         <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           화
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlTuesday" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem> 
                            </asp:DropDownList>
                        </td>
                         <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           수
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlWednesday" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">         
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem>      
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           목
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlThursday" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem>   
                            </asp:DropDownList>
                        </td>
                         <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           금
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlFriday" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem>  
                            </asp:DropDownList>
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           토
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlSaturday" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem>  
                            </asp:DropDownList>
                         <td style="text-align:center; border-left:1px solid #ccc; border-bottom:1px solid #ccc; width:20px;">
                           일
                        </td>
                        <td style="text-align:center; border-left:1px solid #ccc;">
                            <asp:DropDownList ID="ddlSunday" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="오전" Text="오전"></asp:ListItem>
                                <asp:ListItem Value="오후" Text="오후"></asp:ListItem>          
                                <asp:ListItem Value="종일" Text="종일"></asp:ListItem>  
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>    
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr>
                        <td style="text-align:left;height:40px;">      
                            <asp:Button ID="btnNew" runat="server"  CssClass="button gray mepm_btn_4em"  Text="신규" Visible="false" OnClick="btnNew_Click" />
                        </td>
                        <td style="text-align:right;height:40px;">   
                            <asp:DropDownList ID="ddlWork" runat="server" CssClass="i_f_out" Width="30%" AutoPostBack="true" Enabled="true" Visible="false" OnSelectedIndexChanged="ddlWork_SelectedIndexChanged">          
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                                <asp:ListItem Value="E" Text="수정"></asp:ListItem>
                                <asp:ListItem Value="D" Text="삭제"></asp:ListItem>          
                            </asp:DropDownList>      
                            <asp:Button ID="btnOk" runat="server"  CssClass="button gray mepm_btn_4em"  Text="확인" Visible="false"  OnClientClick="javascript:return fncChkConfirm();" OnClick="btnOk_Click"/>                                                   
                            <asp:Button ID="btnInsert" runat="server"  CssClass="button gray mepm_btn_4em"  Text="추가" OnClick="btnInsert_Click" OnClientClick="javascript:return fncChkStore();" />
                        </td>
                    </tr>
                </table>
            </div>  
            <div class="mepm_menu_item" style="padding:0;">
                <asp:GridView   ID="gvList" 
                                runat="server" 
                                CellPadding="0"  
                                Width="100%"  
                                EmptyDataText="일치하는 정보가 없습니다." 
                                ShowHeaderWhenEmpty="True"
                                CssClass="table_border" 
                                OnRowDataBound="gvList_RowDataBound"                                                                 
                                AutoGenerateColumns="false">
                    <Columns>                    
                        <asp:boundfield HeaderText="거래처">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>
                        <asp:boundfield HeaderText="매장">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>
                        <asp:boundfield HeaderText="오전">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>          
                        <asp:boundfield HeaderText="오후">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>                                         
                        <asp:boundfield HeaderText="종일">
						    <HeaderStyle CssClass="tr_border"/>
						    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					    </asp:boundfield>  
<%--                      <asp:boundfield HeaderText="s" DataField="TO_Status">
							<HeaderStyle CssClass="tr_border"/>
							<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						</asp:boundfield>  
                      <asp:boundfield HeaderText="s" DataField="TO_Status2">
							<HeaderStyle CssClass="tr_border"/>
							<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						</asp:boundfield>  --%>
                    </Columns>
                    <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					<RowStyle CssClass="mepm_menu_item_bg" />
					<HeaderStyle CssClass="mepm_menu_title_bg"/>
                </asp:GridView>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">대표매장 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlRepresent" runat="server" CssClass="i_f_out" Enabled="false" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlRepresent_SelectedIndexChanged">                  
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/TO/m_To_Round_Change_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
