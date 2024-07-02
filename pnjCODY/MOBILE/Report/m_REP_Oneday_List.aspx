<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_REP_Oneday_List.aspx.cs" Inherits="MOBILE_Report_m_REP_Oneday_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
        // 시작일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckStartDate() {
            var num, year, month, day;
            num = document.getElementById('<%= this.txtFROMDATE.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("시작일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtFROMDATE.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtFROMDATE.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("시작일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtFROMDATE.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtFROMDATE.ClientID %>').value = num;
            }
        }

        // 종료일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {
            var num, year, month, day;
            num = document.getElementById('<%= this.txtTODATE.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("종료일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtTODATE.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtTODATE.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("종료일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtTODATE.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtTODATE.ClientID %>').value = num;
            }
        }

        // 유효한(존재하는) 일(日) 인지 체크
        function isValidDay(yyyy, mm, dd) {
            var m = parseInt(mm, 10) - 1;
            var d = parseInt(dd, 10);
            var end = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

            if ((yyyy % 4 == 0 && yyyy % 100 != 0) || yyyy % 400 == 0) {
                end[1] = 29;
            }
            return (d >= 1 && d <= end[m]);
        }

        function fncChkSearch() {
            // 시작일 날짜형식 체크
            if (CheckStartDate() == false) return false;

            // 종료일 날짜형식 체크
            if (CheckFinishDate() == false) return false;

            if (document.getElementById('<%= this.txtFROMDATE.ClientID %>').value > document.getElementById('<%= this.txtTODATE.ClientID %>').value) {
                alert("기간의 종료일이 시작일 보다 이전 일 수 없습니다.");
                document.getElementById('<%= this.txtFROMDATE.ClientID %>').focus();
                return false;
            }
        } 

        function fncDetail(strREP_DLY_ID, strRES_ID, strREAL_YYYYMMDD){
            document.getElementById('<%= this.hdREP_DLY_ID.ClientID %>').value = strREP_DLY_ID;
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;
            document.getElementById('<%= this.hdREAL_YYYYMMDD.ClientID %>').value = strREAL_YYYYMMDD;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }

        function fncInsertNewReport() {
            var start = document.getElementById('<%= this.txtFROMDATE.ClientID %>').value,
                end = document.getElementById('<%= this.txtTODATE.ClientID %>').value;
            
            if(start != end) {
                alert("새로운 매장 업무일지 작성은 시작일과 종료일이 같아야 합니다.");
                return false;
            }

            // window.location='m_REP_Oneday_Report.aspx';

            window.location='m_REP_Oneday_Report.aspx?VISIT_DATE=' + start;
        }

    </script>
    <input type="hidden" id="hdREP_DLY_ID" name="hdREP_DLY_ID" runat="server" />
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdREAL_YYYYMMDD" name="hdRES_ID" runat="server" />
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
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 일지 > 일일업무보고</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:100%;"><p >일일업무보고 목록</p></td>
                    </tr>
                </table>
             <!-- 일일업무보고 기간 시작 -->
             <div class="mepm_menu_item" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width:50px;text-align:left; padding-left:.5em;">
                            기간 :
                        </td>
                        <td style="width:200px;padding-right:.5em; text-align:left;" colspan="2">
                           <asp:TextBox ID="txtFROMDATE" width="85px" runat="server" value="YYYYMMDD" class="i_f_out"></asp:TextBox> - 
                           <asp:TextBox ID="txtTODATE" width="85px" runat="server" value="YYYYMMDD" class="i_f_out"></asp:TextBox>
                        </td>
                        <td align="right" style="padding-right:0.5em; width:50px;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" /> 
                        </td>
                    </tr>
                    </table>
                </div>
                <!-- 일일업무보고 기간 종료 -->
                <div style="height:12px"></div>                
                <!-- 일일업무보고 목록 시작 -->
                <div class="mepm_menu_item" style="padding:0;">
                 <asp:GridView ID="gvReportList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                    CssClass="table_border" OnRowDataBound="gvReportList_RowDataBound" AutoGenerateColumns="false">
                <Columns>
                    <asp:boundfield HeaderText="번호" DataField="REP_DLY_ID" Visible="False">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield> 
                    <asp:boundfield HeaderText="방문일시" DataField="REAL_YYYYMMDD">
						<HeaderStyle CssClass="tr_border" Width="100px"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                    <asp:boundfield HeaderText="서포터" DataField="RES_NAME">
						<HeaderStyle CssClass="tr_border" Width="80px" />
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>                    
                    <asp:boundfield HeaderText="매장명" DataField="STORE_NAME">
						<HeaderStyle CssClass="tr_border"  />
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                </Columns>
                <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
				<RowStyle CssClass="mepm_menu_item_bg" />
				<HeaderStyle CssClass="mepm_menu_title_bg"/>
            </asp:GridView>
            </div>                                      
            <!-- 일일업무보고 목록 종료 -->            
        </section>
            <div style="height:12px"></div> 
             <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="입력" ID="btnWrite" 
                    runat="server" OnClientClick="javascript:return fncInsertNewReport();" UseSubmitBehavior="False" />


                    <%--  <asp:Button CssClass="button gray mepm_asp_btn" Text="입력" ID="btnWrite" 
                    runat="server" OnClientClick="javascript:return window.location='m_REP_Oneday_Report.aspx';" UseSubmitBehavior="False" />--%>
                <asp:Button CssClass="button gray mepm_asp_btn" Text="취소" ID="btnCancel" 
                    runat="server" OnClientClick="javascript:return window.location='m_REP_Daily_Report_Main.aspx';" UseSubmitBehavior="False" />
            </div> 
    </article>
</asp:Content>
