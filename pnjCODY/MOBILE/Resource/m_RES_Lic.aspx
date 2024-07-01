<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Lic.aspx.cs" Inherits="Resource_m_RES_Lic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncDetail(strRES_ID, strRES_LIC_ID){
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  strRES_ID;
            document.getElementById('<%= this.hdRES_LIC_ID.ClientID %>').value =  strRES_LIC_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_LIC_ID" name="hdRES_LIC_ID" runat="server" />
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
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 자격정보</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:100%;"><p >자격정보</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                    <asp:GridView ID="gvLicList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvLicList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:templatefield HeaderText="번호">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
							</asp:templatefield>
                            <asp:boundfield HeaderText="자격명" DataField="RES_LIC_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="취득일" DataField="RES_LIC_Date">
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
                <a href="/Resource/m_RES_Lic_Add.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">자격정보 추가</span></a>
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
