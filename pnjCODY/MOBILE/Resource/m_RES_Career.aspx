﻿<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Career.aspx.cs" Inherits="Resource_m_RES_Career" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncDetail(strRES_ID, strRES_CAR_ID){
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  strRES_ID;
            document.getElementById('<%= this.hdRES_CAR_ID.ClientID %>').value =  strRES_CAR_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_CAR_ID" name="hdRES_CAR_ID" runat="server" />
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
        <h2 class="mepm_title">사원 등록 정보 > 경력사항</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:100%;"><p >경력사항</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                    <asp:GridView ID="gvCareerList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvCareerList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:templatefield HeaderText="번호">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
							</asp:templatefield>
                            <asp:boundfield HeaderText="회사명" DataField="RES_CAR_Company">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="근무시작" DataField="RES_CAR_Start">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="근무종료" DataField="RES_CAR_Finish">
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
                <a href="/Resource/m_RES_Career_Add.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">경력사항 추가</span></a>
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
