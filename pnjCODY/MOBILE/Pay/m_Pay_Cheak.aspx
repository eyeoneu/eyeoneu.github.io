<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_Pay_Cheak.aspx.cs" Inherits="MOBILE_Pay_m_Pay_Cheak" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function fncDetailView() {            
            document.getElementById('<%= this.dvDetailView.ClientID %>').style.display = "";
        }
    </script>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />
                    SERVICE</span> </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>급여명세 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">
            급여 명세 관리 > 급여 명세서 확인</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>
                            급여 명세서 확인</p>
                    </td>
                </tr>
            </table>
            <!-- 검색 분류 선택 시작 -->
            <div class="mepm_menu_title" style="padding: 0;">
                <table style="table-layout: fixed; word-break: break-all;">
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="width: 75px; text-align: left; padding-left: .5em;">
                            조회년월 :
                        </td>
                        <td style="width: auto; text-align: left; padding-right: .5em;">
                            <asp:DropDownList ID="ddlYYYYMM" runat="server" Width="90px" CssClass="i_f_out" 
                                AutoPostBack="True" onselectedindexchanged="ddlYYYYMM_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 90px; text-align: left; padding-left: .5em;">
                            급/상여구분 :
                        </td>
                        <td style="width: auto; text-align: left; padding-right: .5em;">
                            <asp:DropDownList ID="ddlTYPE" runat="server" Width="80px" CssClass="i_f_out" 
                                AutoPostBack="True" onselectedindexchanged="ddlTYPE_SelectedIndexChanged">
                                <%--<asp:ListItem>-선택-</asp:ListItem>--%>
                                <asp:ListItem Text="1. 급여" Value="100"></asp:ListItem>
                                <asp:ListItem Text="2. 상여" Value="200"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 검색 분류 선택 종료 -->
            <!-- 사원 정보 시작 -->
            <table style="table-layout: fixed; word-break: break-all; border-bottom: 1px solid #ccc;">
                <tr class="mepm_menu_item_bg " style="height: 3em;">
                    <td style="width: 47.5px; text-align: left; padding-left: .5em;">
                        사번 :
                    </td>
                    <td style="width: auto; padding-right: .5em; text-align: left;">
                        <asp:Label ID="lbIEMP_CD" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 47.5px; border-left: 1px solid #ccc; text-align: left; padding-left: .5em;">
                        이름 :
                    </td>
                    <td style="width: auto; padding-right: .5em; text-align: left;">
                        <asp:Label ID="lbIKOR_NM" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="mepm_menu_item_bg" style="height: 3em;">
                    <td style="border-top: 1px solid #ccc; text-align: left; padding-left: .5em;">
                        직급 :
                    </td>
                    <td style="border-top: 1px solid #ccc; padding-right: .5em; text-align: left;">
                        <asp:Label ID="lbIWORKGROUP2" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="border-top: 1px solid #ccc; border-left: 1px solid #ccc; text-align: left; padding-left: .5em;">
                        부서 :
                    </td>
                    <td style="width: auto; border-top: 1px solid #ccc; padding-right: .5em; text-align: left;">
                        <asp:Label ID="lbIAREA" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="mepm_menu_item_bg" style="height: 3em;">
                    <td style="border-top: 1px solid #ccc; text-align: left; padding-left: .5em;">
                        은행 :
                    </td>
                    <td style="border-top: 1px solid #ccc; padding-right: .5em; text-align: left;">
                        <asp:Label ID="lbIBANK_NM" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="border-top: 1px solid #ccc; border-left: 1px solid #ccc; text-align: left; padding-left: .5em;">
                        계좌 :
                    </td>
                    <td style="width: auto; border-top: 1px solid #ccc; padding-right: .5em; text-align: left;">
                        <asp:Label ID="lbIACCT_NO" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
            <!-- 사원 정보 종료 -->
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 40%;">
                        <p>
                            급여 실 지급금액</p>
                    </td>
                    <td align="right" style="width: 60%; padding-right: 0.5em;">
                        <span class="button gray mepm_btn_5.5em" onclick="javascript:fncDetailView()" />상세보기</span>
                    </td>
                </tr>
            </table>
            <!-- 급여 내역 시작 -->
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 105px;">
                            총지급금액 :
                        </td>
                        <td style="width: auto; text-align: right; padding-right: .5em;">
                            <asp:Label ID="lblSum1" runat="server" Text="" Style="color: Red; font-weight: bold;"></asp:Label>
                            원
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 105px;">
                            총공제금액 :
                        </td>
                        <td style="width: auto; text-align: right; padding-right: .5em;">
                            <asp:Label ID="lblSum2" runat="server" Text="" Style="color: Red; font-weight: bold;"></asp:Label>
                            원
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 105px;">
                            차인지급액 :
                        </td>
                        <td style="width: auto; text-align: right; padding-right: .5em;">
                            <asp:Label ID="lblSum3" runat="server" Text="" Style="color: Red; font-weight: bold;"></asp:Label>
                            원
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 급여 내역 종료 -->
            
            <!-- 급여 상세 시작 -->
            <div class="mepm_menu_item" style="padding: 0; display:none;" runat="server" id="dvDetailView">
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>
                            항목별 상세 내역</p>
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item" style="padding:0;">
                <asp:GridView ID="gvPayDetailList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 항목이 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvPayDetailList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="NO" DataField="">
							    <HeaderStyle CssClass="tr_border" Width="50px" />
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="항목" DataField="PRIT_NM">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle CssClass="tr_border_left"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="금액" DataField="PAY_AM" DataFormatString="{0:N0}">
							    <HeaderStyle CssClass="tr_border" Width="100px" />
							    <ItemStyle CssClass="tr_border_right"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
            </div>
            <div class="mepm_menu_item" style="padding:0;">
                <asp:GridView ID="gvPayDetailList2" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 항목이 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvPayDetailList2_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="NO" DataField="">
							    <HeaderStyle CssClass="tr_border" Width="50px" />
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="항목" DataField="PRIT_NM">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle CssClass="tr_border_left"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="금액" DataField="PAY_AM" DataFormatString="{0:N0}">
							    <HeaderStyle CssClass="tr_border" Width="100px" />
							    <ItemStyle CssClass="tr_border_right"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation100" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 10px 10px 10px 10px; font-size:smaller;">
                            * 급여지급일 : 매월 11일에 지급한다. 단, 11일이 공휴일인 경우 전일 지급한다.
                        </td>
                    </tr>
                </table>    
            </div>   
            <div style="padding: 0;" runat="server" id="dvExplanation110" visible="false">
                <table>
                    <tr>    
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 휴직 및 월중 입퇴사를 할 경우 월 만근일수를 기준으로 일할계산 한다. (기본급, 시간외수당)
                        </td>
                    </tr>
                </table>    
            </div> 
            <div style="padding: 0;" runat="server" id="dvExplanation101" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 계산방법
                            <br />&nbsp;&nbsp;&nbsp;- 기본급: 근로계약서에 명시된 금액
                            <br />&nbsp;&nbsp;&nbsp;- 시간외수당: 근로계약서에 명시된 금액
                            <br />&nbsp;&nbsp;&nbsp;- 임시조정수당: 별도의 근로계약서를 작성(월 또는 일)의 합계, 또는 재고조사
                            <br />&nbsp;&nbsp;&nbsp;- 특근수당: 통상시급 * 150% * 휴일근로시간
                            <br />&nbsp;&nbsp;&nbsp;- 장애수당: 복지카드 소지자에 한해 당사 지급규정에 의한 금액
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation102" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 계산방법
                            <br />&nbsp;&nbsp;&nbsp;- 기본급: 근로계약서에 명시된 금액
                            <br />&nbsp;&nbsp;&nbsp;- 임시조정수당: 별도의 근로계약서를 작성(월 또는 일)의 합계, 또는 재고조사
                            <br />&nbsp;&nbsp;&nbsp;- 특근수당: 통상시급 * 150% * 휴일근로시간
                            <br />&nbsp;&nbsp;&nbsp;- 유사급여1(년차): 근로계약서에 명시된 금액
                            <br />&nbsp;&nbsp;&nbsp;- 장애수당: 복지카드 소지자에 한해 당사 지급규정에 의한 금액
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation103" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 계산방법
                            <br />&nbsp;&nbsp;&nbsp;- 기본급: 근로계약서에 명시된 금액 * 근무일수
                            <br />&nbsp;&nbsp;&nbsp;- 유사급여1(년차): 근로계약서에 명시된 금액 * 근무일수
                            <br />&nbsp;&nbsp;&nbsp;- 주휴유급수당: 근로계약서에 명시된 금액 * 근무일수
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation104" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 계산방법
                            <br />&nbsp;&nbsp;&nbsp;- 기본급: 근로계약서에 명시된 금액 * 근무일수 (복지카드 소지자에 한해 당사 지급규정에 의한 금액은 기본급에 포함)
                            <br />&nbsp;&nbsp;&nbsp;- 유사급여1(년차): 근로계약서에 명시된 금액 * 근무일수
                            <br />&nbsp;&nbsp;&nbsp;- 주휴유급수당: 근로계약서에 명시된 금액 * 근무일수
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation105" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 계산방법
                            <br />&nbsp;&nbsp;&nbsp;- 기본급: 통상시급 * 소정근로일수 * 일 근무시간
                            <br />&nbsp;&nbsp;&nbsp;- 시간외수당: 통상시급 * 소정근로일수 * 일 근무시간 * 150%
                            <br />&nbsp;&nbsp;&nbsp;- 특근수당: 통상시급 * 150% * 휴일근로시간
                            <br />&nbsp;&nbsp;&nbsp;- 주휴유급수당: 통상시급 * 주휴일 * 일 근무시간
                            <br />&nbsp;&nbsp;&nbsp;- 장애수당: 복지카드 소지자에 한해 당사 지급규정에 의한 금액
                            <br />&nbsp;&nbsp;&nbsp;- 야간근로수당: (통상시급 * (소정근로일수 * 야간근로시간)) * 50%
                            <br />&nbsp;&nbsp;&nbsp;- 휴일연장근로수당: 통상시급 * 200% * 휴일근로일수
                            <br />&nbsp;&nbsp;&nbsp;- 휴일야간근로수당: 통상시급 * 50% * 휴일근무일수 * 야간근로시간
                        </td>
                    </tr>
                </table>
                <table class="mepm_icon_title">
                    <tr>
                        <td style="width: 100%;">
                            <p>
                                월 근무일수</p>
                        </td>
                    </tr>
                </table>
                <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView ID="gvAttendanceInfomation" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="데이터가 없습니다." ShowHeaderWhenEmpty="True"
                             CssClass="table_border" AutoGenerateColumns="false">
                            <Columns>
                                <asp:boundfield HeaderText="근태종류" DataField="ATT_TYPE">
							        <HeaderStyle CssClass="tr_border"/>
							        <ItemStyle CssClass="tr_border_left"/>
						        </asp:boundfield>
                                <asp:boundfield HeaderText="근무일수" DataField="ATT_DAYS">
							        <HeaderStyle CssClass="tr_border" />
							        <ItemStyle CssClass="tr_border_right"/>
						        </asp:boundfield>
                            </Columns>
                            <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					        <RowStyle CssClass="mepm_menu_item_bg" />
					        <HeaderStyle CssClass="mepm_menu_title_bg"/>
                        </asp:GridView>
                </div>
                <table>
                    <tr>
                        <td style="width: 100%; padding: 10px 10px 10px 10px; font-size:smaller;">
                            * 정상근무일수가 해당월의 월~금 까지 일수보다(법정공휴일 제외) 많을 경우 휴일근무 일수로 본다.
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation200" visible="false">
                <table>
                    <tr>
                        <td style="width: 100%; padding: 10px 10px 0px 10px; font-size:smaller;">
                            * 급여지급일 : 매월 11일에 지급한다. 단, 11일이 공휴일인 경우 전일 지급한다.
                        </td>
                    </tr>
                    <tr>    
                        <td style="width: 100%; padding: 10px 10px 10px 10px; font-size:smaller;">
                            * 미사용연차는 전년도 연차사용분 미차감 개수 및 직종변경으로 지급완료된 연차 개수(금액)를 반영함
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 0;" runat="server" id="dvExplanation201" visible="false">
                <table id="Table5">
                    <tr>
                        <td style="width: 100%; padding: 0px 10px 10px 10px; font-size:smaller;">
                            * 계산방법
                            <br />&nbsp;&nbsp;&nbsp;- 상여: 별도 기준
                            <br />&nbsp;&nbsp;&nbsp;- 년차: 통상시급 * 미사용연차 * 일 근무시간
                        </td>
                    </tr>
                </table>
            </div>
            </div>
            <!-- 급여 상세 종료 -->          
        </section>
    </article>
</asp:Content>
