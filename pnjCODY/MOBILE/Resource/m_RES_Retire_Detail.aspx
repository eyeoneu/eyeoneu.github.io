<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Retire_Detail.aspx.cs" Inherits="m_RES_Retire_Detail" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />
                    SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong>
        </p>
        <p class="mepm_lg mepm_lgback">
            <a onclick="history.back();"><span class="button blue">이전단계</span></a>
        </p>
    </header>
    <div class="title">
        <h2 class="mepm_title">퇴직금 대상 여부</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <div style="padding: 0;">
                <table class="mepm_icon_title">
                    <tr>
                        <td style="width: 75%;">
                            <p>선택 사원 정보</p>
                        </td>
                        <td align="right" style="width: 25%; font-weight: normal; padding-right: 0.5em;"></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding: 0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width: 70px; text-align: left; text-align: left; padding-left: .5em;">사원번호 :</td>
                        <td style="width: auto; text-align: left; font-weight: bold;">
                            <asp:Label runat="server" ID="lblRES_Number" Text=""></asp:Label></td>
                        <td style="width: 60px; border-left: 1px solid #ccc; text-align: left; padding-left: .5em;">이름 :</td>
                        <td style="width: auto; text-align: left;">
                            <asp:Label runat="server" ID="lblRES_Name" Text=""></asp:Label></td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align: left; padding-left: .5em;">주민번호 :</td>
                        <td style="border-top: 1px solid #ccc; text-align: left;">
                            <asp:Label runat="server" ID="lblRES_PersonNumber" Text=""></asp:Label></td>
                        <td style="border-left: 1px solid #ccc; border-top: 1px solid #ccc; text-align: left; padding-left: .5em;">상태 :</td>
                        <td style="border-top: 1px solid #ccc; text-align: left;">
                            <asp:Label runat="server" ID="lblRES_WorkState" Text=""></asp:Label></td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align: left; padding-left: .5em;">입사일 :</td>
                        <td style="border-top: 1px solid #ccc; text-align: left;">
                            <asp:Label runat="server" ID="lblJoin" Text=""></asp:Label></td>
                        <td style="border-left: 1px solid #ccc; border-top: 1px solid #ccc; text-align: left; padding-left: .5em;">퇴사일 :</td>
                        <td style="border-top: 1px solid #ccc; text-align: left;">
                            <asp:Label runat="server" ID="lblRetire" Text=""></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;">
				<table class="mepm_icon_title">
                    <tr>
                        <td style="width: 75%;">
                            <p>60시간 이상 개월수</p>
                        </td>
                        <td align="right" style="width: 25%; font-weight: normal; padding-right: 0.5em;"></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding: 0;">
                <asp:ListView ID="ListView2" runat="server" ItemPlaceholderID="itemPlaceHolder1">
                    <layouttemplate>
                        <table style="width:100%">
                            <tr class="mepm_menu_item_bg" style="height:3em;">
                                <th style="border-top:1px solid #ccc; width:30%;text-align:center;">
                                    지급월
                                </th>
                                <th style="border-top:1px solid #ccc; width:35%;text-align:right;padding-right:.6em;">
                                    근무시간
                                </th>
                                <th style="border-top:1px solid #ccc; width:35%;text-align:right;padding-right:.6em;">
                                    금액
                                </th>
                            </tr>
                        </table>
                        <asp:PlaceHolder ID="itemPlaceHolder1" runat="server"></asp:PlaceHolder>
                     </layouttemplate>
                    <itemtemplate>
                        <div class="mepm_menu_title cursor_pointer" style="border-top:1px solid #ccc;">
                            <a onclick="toggle_display('<%# ((ListViewDataItem)Container).DataItemIndex + 1 %>')">
                                <table  border="0">
                                    <tr>
                                        <td style="width:30%;">
                                            <span><b style="color: #bbb;">+</b><%#Eval("YYYY")%>( <%#Eval("COUNT")%>건)</span>
                                        </td>
                                        <td style="width:35%;text-align:right;padding-right:.8em;">
                                            <span><%# Eval("TOTAL_WORK").ToString() %></span>
                                        </td>
                                        <td style="width:35%;text-align:right;padding-right:.8em;">
                                            <span> &#8361;<%# string.Format("{0:N0}", decimal.Parse(Eval("TOTAL_SALARY").ToString())) %></span>
                                        </td>
                                    </tr>
                                </table>
                            </a>
                        </div>
                        <ul id="<%# ((ListViewDataItem)Container).DataItemIndex + 1 %>" class="mepm_menu cursor_pointer">
                            <asp:ListView ID="ListView3" runat="server" DataSource='<%# Eval("Sub")%>' ItemPlaceholderID="itemPlaceHolder2" onitemdatabound="ListView3_ItemDataBound">
                                <LayoutTemplate>
                                   <table style="width:100%" border="0">
                                        <asp:PlaceHolder ID="itemPlaceHolder2" runat="server" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr style="height:3em;" id="MainTableRow" runat="server">
                                        <td style="border-top:1px solid #ccc;width:30%;text-align:center;">
                                            <%#Eval("MM")%>월
                                        </td>
                                        <td style="border-top:1px solid #ccc;width:35%;padding-right:.6em;text-align:right;">
                                            <%#Eval("WORK_TIME_INT").ToString() %>
                                        </td>
                                        <td style="border-top:1px solid #ccc;width:35%;padding-right:.8em;text-align:right;">
                                            &#8361;<%# string.Format("{0:N0}", decimal.Parse(Eval("SALARY").ToString())) %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                        </ul>
                    </ItemTemplate>
                    <emptydatatemplate>
                    <table class="emptyTable" cellpadding="5" cellspacing="5">
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <td align="center">
                                데이터가 없습니다.
                            </td>
                        </tr>
                    </table>
                    </emptydatatemplate>
                </asp:ListView>
            </div>
            <div style="padding: 0;">
				<table class="mepm_icon_title">
                    <tr>
                        <td style="width: 75%;">
                            <p>퇴직금 납입 금액</p>
                        </td>
                        <td align="right" style="width: 25%; font-weight: normal; padding-right: 0.5em;"></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding: 0;">
                <table style="width:100%">
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:50%;text-align:center;">
                            운영기관
                        </th>
                        <th style="border-top:1px solid #ccc; width:50%;text-align:center;">
                            퇴직연금(DC) 납입금액
                        </th>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top: 1px solid #ccc;text-align:center;"><asp:Label runat="server" ID="lblBank"></asp:Label></td>
                        <td style="border-top: 1px solid #ccc;text-align:center;"><asp:Label runat="server" ID="lblAmount"></asp:Label></td>
                    </tr>

                </table>
            </div>
            <div style='width:100%; text-align:center;'>
				위 금액은 운영기관에 납입된 금액이며, 퇴직시 지급 금액과 다를 수 있습니다.
            </div>
			 

        </section>
    </article>
</asp:Content>
