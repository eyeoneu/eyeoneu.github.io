<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Contract.aspx.cs" Inherits="Resource_m_RES_Contract" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncDetail(strRES_ID, strRES_CON_ID){
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  strRES_ID;
            document.getElementById('<%= this.hdRES_CON_ID.ClientID %>').value =  strRES_CON_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_CON_ID" name="hdRES_CON_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원정보 > 계약정보</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p>선택 사원 정보</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_item" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="width:70px; text-align:left; text-align:left; padding-left:.5em;">사원번호 :</td>
                        <td style="width:auto; text-align:left; font-weight:bold;"><asp:Label runat="server" ID="lblRES_Number" Text=""></asp:Label></td>  
                        <td style="width:40px; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">이름 :</td>
                        <td style="width:auto; text-align:left;"><asp:Label runat="server" ID="lblRES_Name" Text=""></asp:Label></td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top:1px solid #ccc; text-align:left; padding-left:.5em;">주민번호 :</td>
                        <td style="border-top:1px solid #ccc; text-align:left;"><asp:Label runat="server" ID="lblRES_PersonNumber" Text=""></asp:Label></td>    
                        <td style="border-left:1px solid #ccc; border-top:1px solid #ccc; text-align:left; padding-left:.5em;">상태 :</td>
                        <td style="border-top:1px solid #ccc; text-align:left;"><asp:Label runat="server" ID="lblRES_WorkState" Text=""></asp:Label></td>
                    </tr>                
                </table>
            </div>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >사원 계약 목록</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                    <asp:GridView ID="gvContractList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvContractList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:templatefield HeaderText="번호">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
							</asp:templatefield>
                            <asp:boundfield HeaderText="매장" DataField="RES_CON_Store_NAME">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="계약시작일" DataField="RES_CON_STARTDATE" DataFormatString="{0:d}">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="상태" DataField="RES_CON_STATE_NAME">
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
                <a href="/Resource/m_RES_Contract_New.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">신규계약</span></a>
                <a href="/Resource/m_RES_Mng.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>         
        </section>
    </article>
</asp:Content>