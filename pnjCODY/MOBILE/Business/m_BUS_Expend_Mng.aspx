<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_BUS_Expend_Mng.aspx.cs" Inherits="Business_m_BUS_Expend_Mng" %>
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

        function fncDetail(strEXP_REQ_ID){
            document.getElementById('<%= this.hdEXP_REQ_ID.ClientID %>').value = strEXP_REQ_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdEXP_REQ_ID" name="hdEXP_REQ_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>업무 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">지출 신청서 관리</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:100%;"><p >지출 신청서 관리</p></td>
                    </tr>
                </table>
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
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" 
                                    runat="server" OnClientClick="javascript:return fncChkSearch();"  OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    </table>
                    <table style="table-layout: fixed; word-break: break-all;">
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width:50px; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            상태 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="i_f_out"
                                DataTextField="COD_Name" DataValueField="COD_CD" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td style="width:53px; border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            항목 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:DropDownList ID="ddlReqType" runat="server" CssClass="i_f_out"
                                DataTextField="COD_Name" DataValueField="COD_CD" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                 </table>
            
            <asp:GridView ID="gvReqList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                    CssClass="table_border" OnRowDataBound="gvReqList_RowDataBound" AutoGenerateColumns="false">
                <Columns>
                    <asp:templatefield HeaderText="번호">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
					</asp:templatefield>
                    <asp:boundfield HeaderText="대상자" DataField="TARGET">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>                    
                    <asp:boundfield HeaderText="항목" DataField="항목">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                    <asp:boundfield HeaderText="등록일" DataField="작성일">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                    <asp:boundfield HeaderText="상태" DataField="진행상태">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                </Columns>
                <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
				<RowStyle CssClass="mepm_menu_item_bg" />
				<HeaderStyle CssClass="mepm_menu_title_bg"/>
            </asp:GridView>
            </div>
                            
        </section>
    </article>
</asp:Content>