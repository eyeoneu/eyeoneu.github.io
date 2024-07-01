<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_Etc_Change.aspx.cs" Inherits="m_Etc_Change" %>
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
            document.getElementById('<%= this.hdnResName.ClientID %>').value = strRES_ID;

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

            if (document.getElementById('<%=this.ddlEtcType.ClientID %>').value == "") {
                alert("변경항목을 선택하세요.");
                document.getElementById('<%=this.ddlEtcType.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.hdnResName.ClientID %>').value == "") { <!-- document.getElementById('<%=this.ddlEtcType.ClientID %>').value == "계좌번호변경" && document.getElementById('<%=this.hdnResName.ClientID %>').value == "") { -->
                alert("사원을 선택하세요.");
                document.getElementById('<%=this.hdnResName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.txtEtcContent.ClientID %>').value == "") {
                alert("변경내용을 입력하세요.");
                document.getElementById('<%=this.txtEtcContent.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=this.txtEtcReason.ClientID %>').value == "") {
                alert("변경사유를 입력하세요.");
                document.getElementById('<%=this.txtEtcReason.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%=this.ddlEtcType.ClientID %>').value == "계좌번호변경" &&
                document.getElementById('<%=this.upFile.ClientID %>').value == "") {
                alert("사진을 첨부 해주세요.");
                document.getElementById('<%=this.upFile.ClientID %>').focus();
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
    </script>

    <header>
        <input type="hidden" id="hdnResName" value="" style='width:1px;' runat="server" />
        <input type="hidden" id="hdnWorkGroup2" value="" style='width:1px;' runat="server" />
        <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="W-HRM" width="24" class="mepm_lgm" /><span class="mepm_lgi">W-HRM<br />MOBILE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Business/m_BUS_Connection.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 기타 변경</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >기타 변경</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                         <th style="border-top:1px solid #ccc; width:100px; text-align:right; padding-right:.8em;">작성일자 :</th>
                         <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtDate" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:100px; text-align:right; padding-right:.8em;">변경항목 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlEtcType" runat="server" CssClass="i_f_out" Width="100%" 
                                OnSelectedIndexChanged="ddlEtcType_SelectedIndexChanged" AutoPostBack="true" Enabled="true">      
                                <asp:ListItem Value="" Text="-선택-"></asp:ListItem>
                              <%--  <asp:ListItem Value="주요코디추천" Text="주요코디추천"></asp:ListItem>--%>
<%--                                <asp:ListItem Value="특근수당청구" Text="특근수당청구"></asp:ListItem> 해당 메뉼ㄹ 휴무승인으로 이동 2016-07-12 정창화 --%>  
                                <asp:ListItem Value="연락처변경" Text="연락처변경"></asp:ListItem>
                                <asp:ListItem Value="주소지변경" Text="주소지변경"></asp:ListItem> 
                                <asp:ListItem Value="개명신청" Text="개명신청"></asp:ListItem>
                                <asp:ListItem Value="계좌번호변경" Text="계좌번호변경"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:100px; text-align:right; padding-right:.8em;">변경일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >                            
                            <asp:TextBox ID="txtDueDate" width="100%" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); CheckDueDate();" class="i_f_out2"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="dvResSearch" visible="true">
                <table>
                   <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:100px; text-align:right; padding-right:.8em;">사원선택 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                            <asp:TextBox ID="txtRES_Name" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="text-align:left; padding-right:.8em;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0;" runat="server" id="dvResList" visible="false">
                <table class="mepm_icon_title">
                    <tr>
                        <td style="width: 100%;">
                            <p>변경 대상 선택</p>
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
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="divAfter">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:100px; text-align:right; padding-right:.8em;">변경내용 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtEtcContent" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>            
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:100px; text-align:right; padding-right:.8em;">변경사유 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtEtcReason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>        
           <div id="bank" class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" visible="false">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:100px; text-align:right; padding-left: .5em; vertical-align:top; padding-top:.8em;" rowspan="2"><asp:Label ID="lbTitle" runat="server"></asp:Label> :</th>
                        <!-- 첨부 파일 부분 시작 -->
                        <td style="text-align: left; padding-right: .5em; padding-left: .5em;">
                            <span style="color:red;">* iPhone 에서는 첨부 할 수 없습니다.</span><br />
                            <asp:Label ID="lbAttFile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 6em;"> 
                        <td style="text-align: left; padding-right: .5em;">
                            <asp:FileUpload CssClass="i_f_out" ID="upFile" runat="server" Width="90%"  BackColor="white" onchange="this.className='i_f_on';" />
                        </td>
                    </tr>
                    <!-- 첨부 파일 부분 종료 -->
                </table>
            </div>    
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/TO/m_Etc_Change_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
