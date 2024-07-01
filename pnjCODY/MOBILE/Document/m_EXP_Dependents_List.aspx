<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_EXP_Dependents_List.aspx.cs" Inherits="m_EXP_Dependents_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncDetail(strRES_ID, strRES_CAR_ID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;
            document.getElementById('<%= this.hdRES_FAM_ID.ClientID %>').value = strRES_CAR_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_FAM_ID" name="hdRES_FAM_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>개인 신청 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">피부양자 신청</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:100%;"><p >피부양자 신청 목록</p></td>
                </tr>
            </table>
            <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView ID="gvFamilyList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                        CssClass="table_border" OnRowDataBound="gvFamilyList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="번호" DataField="RES_FAM_ID">
                                <HeaderStyle CssClass="tr_border" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="성명" DataField="RES_FAM_Name">
                                <HeaderStyle CssClass="tr_border" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="관계">
                                <HeaderStyle CssClass="tr_border" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="의료보험" DataField="RES_FAM_Health_NAME">
                                <HeaderStyle CssClass="tr_border" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="관리자확인">
                                <HeaderStyle CssClass="tr_border" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                            </asp:BoundField>
                        </Columns>
                    <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					<RowStyle CssClass="mepm_menu_item_bg" />
					<HeaderStyle CssClass="mepm_menu_title_bg"/>
                </asp:GridView>
            </div>
            <br />
                   
            <div class="mepm_btn_div">
                <a href="/Document/m_EXP_Dependents.aspx?RES_ID=<%= Session["sRES_ID"].ToString() %>"><span class="button gray mepm_btn">입력</span></a>
                <a href="/m_Default.aspx"><span class="button gray mepm_btn">취소</span></a>
            </div>
            
        </section>
    </article>
</asp:Content>