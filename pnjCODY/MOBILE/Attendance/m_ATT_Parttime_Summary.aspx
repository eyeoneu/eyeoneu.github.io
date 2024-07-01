<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_ATT_Parttime_Summary.aspx.cs" Inherits="Attendance_m_ATT_Parttime_Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Attendance/m_ATT_Information.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">
            근태정보 > 아르바이트 근태 현황</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:70%;"><p >오늘의 아르바이트 근태 개요</p></td>
                        <td align="right" style="width:30%; font-weight:normal; padding-right:0.5em;">
                            <a href="/Attendance/m_ATT_Parttime_List.aspx"><span class="button gray mepm_btn_4em">목록보기</span></a>
                        </td>
                    </tr>
                </table>
             <table class="mepm_menu_title" style="padding:0;">
                <tr style="height:3em;">
                    <th style="width:70px;">
                        대상 :
                    </th>
                    <td style="width:auto; text-align:right; padding-right:.5em">
                        <asp:Label runat="server" ID="lblTOT_CNT" Text=""></asp:Label>
                    </td>
                    <th style="width:70px; border-left:1px solid #ccc;">
                        출근 :
                    </th>
                    <td style="width:auto; text-align:right; padding-right:.5em">
                        <asp:Label runat="server" ID="lblATT_CNT" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <p style="height:.7em;">&nbsp;</p>
             <div class="mepm_menu_item" style="padding:0; border-top:1px solid #ccc; border-bottom:0px;">
                <table>
                     <tr>
                        <td valign="top" style="background-color:#fff;">
                             <table>
                                 <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="width:70px;">
                                        정상근무 :
                                    </th>
                                    <td style="width:auto; text-align:right; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblA1_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc;">
                                        대체근무 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblA2_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc;">
                                        대체휴무 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblA3_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc;">
                                        특근 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblA4_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc;">
                                        결근 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblA5_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc; border-bottom:1px solid #ccc;">
                                        하계휴가 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; border-bottom:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblA6_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="top" style="background-color:#fff;">
                            <table>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="width:70px; border-left:1px solid #ccc;">
                                        경조휴가 :
                                    </th>
                                    <td style="width:auto; text-align:right; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblB1_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc; border-left:1px solid #ccc;">
                                        유급휴가 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblB2_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc; border-left:1px solid #ccc;">
                                        무급휴가 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblB3_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc; border-left:1px solid #ccc;">
                                        연차 :
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="lblB4_CNT" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc; border-left:1px solid #ccc;">
                                        
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
                                    </td>
                                </tr> 
                                <tr class="mepm_menu_item_bg" style="height:3em;">
                                    <th style="border-top:1px solid #ccc; border-left:1px solid #ccc; border-bottom:1px solid #ccc;">
                                        
                                    </th>
                                    <td style="text-align:right; border-top:1px solid #ccc; border-bottom:1px solid #ccc; padding-right:.5em">
                                        <asp:Label runat="server" ID="Label2" Text=""></asp:Label>
                                    </td>
                                </tr> 
                            </table>
                        </td>
                    </tr>
                </table>                
            </div>
        </section>
    </article>
</asp:Content>
