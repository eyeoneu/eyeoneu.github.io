<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_EXP_Employment_Mng_List.aspx.cs" Inherits="m_EXP_Employment_Mng_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        // 사원 클릭 시
        function fncDetail(srtID, empID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  srtID;
            document.getElementById('<%= this.htEMP_ID.ClientID %>').value = empID;
            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="htEMP_ID" name="hdTrn_ID" runat="server" />
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
            <a href="/Resource/m_RES_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a>
        </p>
    </header>
    <div class="title">
        <h2 class="mepm_title">제증명 신청</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:90%;"><p >제증명신청 목록</p></td>
                        <td align="right" style="width:10%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; padding-left:2em; width:65px;">기준일 : </th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtDate" runat="server" MaxLength="10" width="105px" value="YYYY-MM-DD" onfocus="change(this,'YYYY-MM-DD')" onblur="change(this,'YYYY-MM-DD')" class="i_f_out2" />
                        </td>
                        <td style="width: 75px; border-top: 1px solid #ccc; text-align:left; padding-right:.5em; ">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
             <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView ID="gvMkList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="신청항목">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="진행상태">
							    <HeaderStyle CssClass="tr_border" Width="55px" />
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
            </div>

            <div class="mepm_btn_div">
                <a href="/Document/m_EXP_Employment_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>&EMP_ID=0"><span class="button gray mepm_btn">입력</span></a>
                <a href="/Resource/m_RES_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>

            <div style="padding-top: .25em; text-align:center;">
            * 기준일 이전 2개월 부터 기준일까지 보고된 목록이 조회됩니다.
            </div>
         
        </section>
    </article>
</asp:Content>
