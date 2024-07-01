<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_EXP_Employment.aspx.cs" Inherits="m_EXP_Employment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkSave() {
            var regExp = /^\s*$/;
            var dateExp = /\d{4}\-\d{2}\-\d{2}/; //날짜 정규식
            var num;            
        
            if (regExp.test(document.getElementById('<%=this.txtDescription.ClientID %>').value)
                    || document.getElementById('<%=this.txtDescription.ClientID %>').value == "") {
                alert("신청내용을 입력하여 주세요.");
                document.getElementById('<%=this.txtDescription.ClientID %>').focus();
                return false;
            }
        }
    </script>

    <header>
        <asp:HiddenField ID="hdType" runat="server" />
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>개인 신청 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Document/m_EXP_Employment_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">제증명 신청</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 40%;">
                        <p>제증명신청</p>
                    </td>
                    <td align="right" style="width: 60%; font-weight: normal; padding-right: 0.5em;"></td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding: 0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">일자 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc; padding-right: .8em;">
                            <asp:TextBox ID="txtDate" runat="server" MaxLength="10" style="width:100%;" Enabled="false" value="YYYY-MM-DD" onfocus="change(this,'YYYY-MM-DD')" onblur="change(this,'YYYY-MM-DD')" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">신청항목 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc;">
                            <asp:Panel ID="pnlEmploymentList" CssClass="wenitepm_div_fix_layout1" runat="server" Height="" Width="100%" ScrollBars="none">
                                <asp:GridView ID="gvEmploymentList" runat="server" CellPadding="0" Width="100%" EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                                    CssClass="table_border" OnRowDataBound="gvEmploymentList_RowDataBound" AutoGenerateColumns="false" OnRowCreated="gvEmploymentList_RowCreated1">
                                    <Columns>
                                        <asp:TemplateField Visible="true">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbCheckAll" runat="server" OnCheckedChanged="cbCheckAll_CheckedChanged" AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbCheck" runat="server" OnCheckedChanged="cbCheck_CheckedChanged" AutoPostBack="true" />
                                                <asp:HiddenField ID="hdType" runat="server" Value='<%# Eval("CONTENTS")%>' />
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="tr_border" />
                                            <ItemStyle HorizontalAlign="Center" Height="30px" CssClass="tr_border" Wrap="False" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="신청항목" DataField="CONTENTS">
                                            <HeaderStyle CssClass="tr_border" />
                                            <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
                                    <RowStyle CssClass="mepm_menu_item_bg" />
                                    <HeaderStyle CssClass="mepm_menu_title_bg" />
                                </asp:GridView>
                            </asp:Panel>                      
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">제출처 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc; padding-right: .8em;">
                            <asp:DropDownList ID="ddlPresentation" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <th style="border-top: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">발송구분 :</th>
                        <td style="text-align: left; border-top: 1px solid #ccc; padding-right: .8em;">
                            <asp:DropDownList ID="ddlSend" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlSend_SelectedIndexChanged">
                                <asp:ListItem Value="우편" Selected></asp:ListItem>
                                <asp:ListItem Value="팩스"></asp:ListItem>
                                <asp:ListItem Value="직접수령"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 8em;">
                        <th style="border-top: 1px solid #ccc; border-bottom: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">
                            <asp:label ID="lblTitle" runat="server" Text="주소 :"></asp:label>
                        </th>
                        <td style="text-align: left; border-top: 1px solid #ccc; border-bottom: 1px solid #ccc; padding-right: .8em;">
                            <asp:TextBox ID="txtDescription" runat="server" Style="width: 100%;" TextMode="MultiLine" Rows="5" MaxLength="500" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr id="trStatus" class="mepm_menu_item_bg" style="height: 3em;" runat="server" visible="false">
                        <th style="border-bottom: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">진행상태 :</th>
                        <td style="text-align: left; border-bottom: 1px solid #ccc; padding-right: .8em;">
                            <asp:TextBox ID="txtStatus" runat="server" Style="width: 100%;" Enabled="false" class="i_f_out" />
                        </td>
                    </tr>
                    <tr id="trDeny" class="mepm_menu_item_bg" style="height: 3em;" runat="server" visible="false">
                        <th style="border-bottom: 1px solid #ccc; width: 90px; text-align: right; padding-right: .8em;">반려사유 :</th>
                        <td style="text-align: left; border-bottom: 1px solid #ccc; padding-right: .8em;">
                             <asp:TextBox ID="txtDeny" runat="server" MaxLength="25" style="width:100%;" Enabled="false" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Document/m_EXP_Employment_List.aspx"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
