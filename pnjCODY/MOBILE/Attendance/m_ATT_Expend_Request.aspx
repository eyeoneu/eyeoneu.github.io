<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_ATT_Expend_Request.aspx.cs" Inherits="MOBILE_Attendance_M_ATT_Expend_Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span> </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a onclick="history.back();"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">지출 신청서 > 신규 작성</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>지출 신청서</p>
                    </td>
                </tr>
            </table> 
            <!-- 지출신청서 테이블 시작 -->
            <!-- 승인항목선택 항목 시작 -->
             <div class="mepm_menu_title" style="padding: 0;">
                 <table>
                     <tr style="height: 3em;">
                         <td style="width: 65px; text-align: left; padding-left:.5em;">
                             승인항목 :
                         </td>
                         <td style="text-align: left; padding-right:.5em;">
                             <asp:DropDownList ID="ddlREQ_TYPE" runat="server" CssClass="i_f_out" AutoPostBack="false" 
                                DataTextField="COD_Name" DataValueField="COD_CD" Width="100%">
                            </asp:DropDownList>
                         </td>
                     </tr>
                 </table>
             </div>
             <!-- 승인항목선택 항목 종료 -->
            <!-- 신청사원 조회 시작 -->
            <div class="mepm_menu_item" style="padding: 0;">
                <table>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="width: 65px; text-align: left; padding-left:.5em;">
                        신청사원 :
                        </td>
                        <td style="text-align:center; padding-right:.5em;" colspan="2">
                            <asp:TextBox ID="txtRES_Name" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="text-align:center;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style=" padding:0;" runat="server" id="dvResList" visible="false">
                    <table class="mepm_icon_title">
                        <tr>
                            <td style="width: 100%;">
                                <p>지출 신청서 대상 선택</p>
                            </td>
                        </tr>
                    </table> 
                    <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="이름" DataField="RES_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="주민번호" DataField="RES_PersonNumber">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="사번" DataField="RES_Number">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="상태" DataField="RES_WorkState">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
                    <table>
                        <tr>
                            <td style="width: 100%; height: 15px; border-top:1px solid #ccc; background-color: #fff;">
                            </td>
                        </tr>
                    </table> 
            </div>
            <!-- 신청사원 조회 종료 -->
            <!-- 신청사원 정보 시작 -->
            <div class="mepm_menu_item" style="padding: 0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width: 15%; text-align:left; padding-left:.5em;">
                            사번 :
                        </td>
                        <td style="width: 35%; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblREQ_RES_ID" runat="server" Text="A000000"></asp:Label>
                        </td>
                        <td style="width: 15%; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            이름 :
                        </td>
                        <td style="width: 35%; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_Name" runat="server" Text="고길동"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부문 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_RBS_NAME" runat="server" Text="강북유통"></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            직종 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_WorkGroup1_NAME" runat="server" Text="코디"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부서 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_RBS_AREA_NAMEE" runat="server" Text="이마트은평"></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            입사일 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_JoinDate" runat="server" Text="2011-11-11"></asp:Label>
                        </td>
                    </tr>
                    <!-- 신청사원 정보 종료 -->
                    <!-- 승인금액 항목 시작 -->
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인금액 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                        </td>
                        <td style="border-left:1px solid #ccc; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부가세 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                        </td>
                    </tr>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            비고 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;" colspan="3">
                            &nbsp;</td>
                    </tr>
                    <!-- 승인금액 항목 종료 -->
                </table>
            </div>
            <!-- 지출신청서 테이블 종료 -->
            <!-- 기타 입력 사항 항목 시작 -->
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:100%;">
                        <p>기타 입력 사항</p>
                    </td>
                </tr>
            </table> 
            <div class="mepm_menu_item" style="padding: 0;">
                 <table>
                     <tr style="height: 3em;">
                         <td style="width: 65px; text-align: left; padding-left:.5em;">
                             연락처 :
                         </td>
                         <td style="width:auto; text-align: left; padding-right:.5em;"  colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Tel" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr style="height: 3em;">
                         <td style="border-top: 1px solid #ccc; text-align: left; padding-left:.5em;">
                             신청사유 :
                         </td>
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-right:.5em;"  colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Reason" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr style="height: 3em;">
                         <td style="border-top: 1px solid #ccc; text-align: left; padding-left:.5em;">
                             첨부서류 :
                         </td>
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-right:.5em;"  colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Attatch" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr style="height: 3em;">
                         <td style="border-top: 1px solid #ccc; text-align: left; padding-left:.5em;">
                             지연사유 :
                         </td>
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-right:.5em;" colspan="3">
                             <asp:TextBox ID="txtATT_REQ_Delay" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                         </td>
                     </tr>
                     <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인일자 :
                        </td>
                        <td style="width:auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblATT_REQ_Approve2Date" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width:65px; border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인번호 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblATT_REQ_ApproveNumber" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                 </table>
             </div>
             <!-- 기타 입력 사항 항목 종료 -->
            <div class="mepm_btn_div">
                <a href="#?RES_ID=<%=this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">작성완료</span></a>
                <a href="#=<%=this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>            
        </section>
    </article>
</asp:Content>
