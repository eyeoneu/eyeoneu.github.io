<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_BUS_Connection_View.aspx.cs" Inherits="m_BUS_Connection_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    

    <header>
        <input type="hidden" id="hdnWorkGroup2" value="" style='width:1px;' runat="server" />
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Business/m_BUS_Connection.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 업무연락 상세</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >업무연락 상세</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">작성일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:Label ID="txtHIS_REQDATE" runat="server" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">요청일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:Label ID="txtHIS_DUEDATE" runat="server" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">진행상태 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                             <asp:Label ID="txtSTATUS" runat="server" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">구분 :</th>
                       <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:Label ID="txtHIS_GB" runat="server" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">요청항목 :</th>
                       <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                             <asp:Label ID="txtHIS_TYPE" runat="server" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">서포터 :</th>
                       <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                             <asp:Label ID="txtSPT_RES_NAME" runat="server" />
                        </td>
                    </tr>                    
                     <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">근무형태 :</th>
                       <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                             <asp:Label ID="txtWorkType" runat="server"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">요청내용 :</th>
                       <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:Label ID="txtHIS_TEXT" runat="server"></asp:Label><br />
                            <asp:Label ID="txtTO_TimeNPay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">요청사유 :</th>
                       <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                             <asp:Label ID="txtHIS_Reason" runat="server"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <div id="divFile" runat="server" visible="false">
                        <tr class="mepm_menu_item_bg" style="height:3em;">
                            <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">첨부파일 :</th>
                           <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                                 <asp:Image ID="imgRES_Picture" ImageUrl="" runat="server" width="185px" height="155px" />
                            </td>
                        </tr>
                    </div>
                </table>
            </div>         
            <asp:Panel ID="pnlStores" runat="server" Visible="false">
                <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >요청 매장 정보</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
                <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView   ID="gvList" 
                                    runat="server" 
                                    CellPadding="0"  
                                    Width="100%"  
                                    EmptyDataText="일치하는 정보가 없습니다." 
                                    ShowHeaderWhenEmpty="True"
                                    CssClass="table_border" 
                                    OnRowDataBound="gvList_RowDataBound"                                                                 
                                    AutoGenerateColumns="false">
                        <Columns>                    
                            <asp:boundfield HeaderText="거래처">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="매장">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="오전">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>          
                            <asp:boundfield HeaderText="오후">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>                                         
                            <asp:boundfield HeaderText="종일">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>  
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <div class="mepm_btn_div">
               <%-- <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />--%>
         <%--       <a href="/TO/m_Etc_Change_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>--%>
            </div>
        </section>
    </article>
</asp:Content>
