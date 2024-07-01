<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_REP_Vacancy.aspx.cs" Inherits="m_REP_Vacancy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkSave() {
            var num, year, month, day;

            var numExp = /^[0-9]*$/; //숫자 정규식

            // if (!numExp.test(num)) { // . (소숫점) 입력 제외
            // if (isNaN(num)) { // . (소숫점) 입력 허용
            var regExp = /^\s*$/;

            if (regExp.test(document.getElementById('<%=this.ddlCause.ClientID %>').value)) {
                alert("결원사유를 선택해주세요. ");
                document.getElementById('<%=this.ddlCause.ClientID %>').focus();
                return false;
            }

            if (regExp.test(document.getElementById('<%=this.ddlInstead.ClientID %>').value)) {
                alert("대체투입을 선택해주세요. ");
                document.getElementById('<%=this.ddlInstead.ClientID %>').focus();
                return false;
            }

            // 대체사원 선택 필수 -- 2014-03-04 추가 (김재영)
            if (document.getElementById('<%=this.ddlInstead.ClientID %>').value == "Y") {
                if (regExp.test(document.getElementById('<%=this.txtResName.ClientID %>').value)) {
                    alert("대체사원을 선택해주세요. ");
                    document.getElementById('<%=this.txtRES_Name.ClientID %>').focus();
                    return false;
                }
            }

            if (regExp.test(document.getElementById('<%=this.ddlJob.ClientID %>').value)) {
                alert("구인방법을 입력해주세요. ");
                document.getElementById('<%=this.ddlJob.ClientID %>').focus();
                return false;
            }

            if (regExp.test(document.getElementById('<%=this.txtInterview.ClientID %>').value)) {
                alert("면접인원을 입력해주세요. ");
                document.getElementById('<%=this.txtInterview.ClientID %>').focus();
                return false;
            }

            num = document.getElementById('<%= this.txtInterview.ClientID %>').value;
            if (!numExp.test(num)) {
                num = "";
                alert("면접인원은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtInterview.ClientID %>').focus();
                return false;
            }
            else {
                document.getElementById('<%= this.txtInterview.ClientID %>').value = num;
            }

            if (regExp.test(document.getElementById('<%=this.txtRemark.ClientID %>').value)) {
                alert("진행상황을 입력해주세요. ");
                document.getElementById('<%=this.txtRemark.ClientID %>').focus();
                return false;
            }
        }

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
    </script>

    <header>
        <input type="hidden" id="hdnResName" value="" style='width: 1px;' runat="server" />
        <input type="hidden" id="hdnAssConID" value="" style='width: 1px;' runat="server" />
        <input type="hidden" id="hdnAssConStore" value="" style='width: 1px;' runat="server" />
        <input type="hidden" id="hdnAssConGB" value="" style='width: 1px;' runat="server" />
        <input type="hidden" id="hdnAssConRES" value="" style='width: 1px;' runat="server" />
        <input type="hidden" id="hdnAssConVender" value="" style='width: 1px;' runat="server" />
        <input type="hidden" id="hdnWorkGroup2" value="" style='width: 1px;' runat="server" />
        <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 일지 > 결원매장</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >결원매장</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">TO정보 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:Label ID="lblToName" runat="server" style="width:100%;"  />
                        </td>
                    </tr>
                     <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">결원시작일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:Label ID="lblStartDate" runat="server" style="width:100%;"  />
                        </td>
                     </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 80px; text-align: right; padding-right: .8em;">결원사유 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc; padding-right: .8em;">
                            <asp:DropDownList ID="ddlCause" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 80px; text-align: right; padding-right: .8em;">대체투입 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc; padding-right: .8em;">
                            <asp:DropDownList ID="ddlInstead" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlInstead_OnSelectedIndexChanged">
                                <asp:ListItem Value="">-선택-</asp:ListItem>
                                <asp:ListItem Value="Y">예</asp:ListItem>
                                <asp:ListItem Value="N">아니오</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

             <div class="mepm_menu_item" style="padding: 0; border:0;" runat="server" id="dvResSearch" visible="false">
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
              <div class="mepm_menu_item" style="padding:0; border-top:1px solid #ccc;"  runat="server" id="dvResList" visible="false">
                    <table class="mepm_icon_title">
                        <tr>
                            <td style="width: 100%;">
                                <p>대체투입 대상 선택</p>
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
            <div class="mepm_menu_item_bg" style="padding:0; border-top:1px solid #ccc;" runat="server" id="dvResSelected" visible="false">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px; text-align:right; padding-right:.8em;"> 선택정보 :</th>
                        <td style="text-align:left; padding-right:.8em;">
                             <asp:TextBox ID="txtResName" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false" />
                        </td>
                    </tr>                   
                </table>
            </div>
                    
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">구인방법 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlJob" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">면접인원 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtInterview" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out"/>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">진행상황 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em; padding-top:.5em; padding-bottom:.5em; " >
                            <asp:TextBox ID="txtRemark" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                 TextMode="MultiLine" Rows="5"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Report/m_REP_Vacancy_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
