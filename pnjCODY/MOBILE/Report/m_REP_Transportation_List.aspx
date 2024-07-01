<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_REP_Transportation_List.aspx.cs" Inherits="m_REP_Transportation_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        // 사원 클릭 시
        function fncDetail(srtID, trnID) {
//            var parm = new Array();
//            parm.push("RES_ID=" + srtID);

//            location.href("/Resource/m_RES_Mng.aspx?" + encodeURI(parm.join('&')));

            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  srtID;
            document.getElementById('<%= this.htTRN_ID.ClientID %>').value = trnID;
            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="htTRN_ID" name="hdTrn_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 일지 > 교통비정산서</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:90%;"><p >교통비 정산서 목록</p></td>
                        <td align="right" style="width:10%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
             <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="거래처" DataField="TRN_CUSTOMER">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="방문매장" DataField="TRN_STORE">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="금액" DataField="TRN_PRICE">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="증빙" DataField="TRN_EVIDENCE">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
            </div>

            <div class="mepm_btn_div">
                <a href="/Report/m_REP_Transportation.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>&TRN_ID=0"><span class="button gray mepm_btn">입력</span></a>
                <a href="/Report/m_REP_Daily_Report_Main.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
