<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_NOT_List.aspx.cs" Inherits="MOBILE_Notice_m_NOT_List" %>
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

        function fncDetail(strETC_NOT_ID){
            document.getElementById('<%= this.hdETC_NOT_ID.ClientID %>').value = strETC_NOT_ID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdETC_NOT_ID" name="hdETC_NOT_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="W-HRM" width="24" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />
                    SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">공지 사항</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:100%;"><p >공지 사항 목록</p></td>
                    </tr>
                </table>
             <!-- 공지사항 기간 시작 -->
             <div class="mepm_menu_item" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <%--<td style="width:50px;text-align:left; padding-left:.5em;"></td>--%>
                        <td style="width:200px;padding-right:.5em; padding-top:.3em; padding-bottom:.3em; padding-left:.5em; text-align:left;" colspan="2">
                            기간 : 
                           <asp:TextBox ID="txtFROMDATE" width="85px" runat="server" value="YYYYMMDD" class="i_f_out"></asp:TextBox> - 
                           <asp:TextBox ID="txtTODATE" width="85px" runat="server" value="YYYYMMDD" class="i_f_out"></asp:TextBox>
                           <br />
                           <asp:DropDownList ID="ddl_Category" runat="server" CssClass="i_f_out" ToolTip="카테고리" width="90px" ></asp:DropDownList> <%--2016-10-05 김기훈 추가--%>
                           <asp:DropDownList ID="ddlSearchGB" runat="server" CssClass="i_f_out" Width="50px" AutoPostBack="false" Enabled="true">         
                                <asp:ListItem Value="TIT">제목</asp:ListItem>
                                <asp:ListItem Value="CON">내용</asp:ListItem>               
                           </asp:DropDownList> : 
                            <asp:TextBox ID="txtSearch_Text" runat="server" style="width:85px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </asp:DropDownList>
                        </td>
                        <td align="right" style="padding-right:0.5em; width:50px;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" 
                                    runat="server" OnClientClick="javascript:return fncChkSearch();"  OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    </table>
                </div>
                <!-- 공지사항 기간 종료 -->
                <div style="height:12px"></div>                
                <!-- 공지사항 목록 시작 -->
                <div class="mepm_menu_item" style="padding:0;">
                <asp:GridView ID="gvNoticeList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                    CssClass="table_border" OnRowDataBound="gvNoticeList_RowDataBound" AutoGenerateColumns="false">
                <Columns>
                    <asp:boundfield HeaderText="ID" DataField="ETC_NOT_ID" Visible="False">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield> 
                    <asp:boundfield HeaderText="제목" DataField="ETC_NOT_Title">
						<HeaderStyle CssClass="tr_border"/>
						<ItemStyle HorizontalAlign="left" CssClass="tr_border_left"/>
					</asp:boundfield>                    
                    <asp:boundfield HeaderText="공지자" DataField="RES_Name">
						<HeaderStyle CssClass="tr_border" Width="60px" />
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                    <asp:boundfield HeaderText="공지일(기한일)" DataField="CREATED_DATE">
						<HeaderStyle CssClass="tr_border" Width="100px" />
						<ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
					</asp:boundfield>
                </Columns>
                <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
				<RowStyle CssClass="mepm_menu_item_bg" />
				<HeaderStyle CssClass="mepm_menu_title_bg"/>
            </asp:GridView>                                      
            <!-- 공지사항 목록 종료 -->
            </div>
        </section>
            <div style="height:12px"></div> 
             <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="작성" ID="btnWrite" 
                    runat="server" OnClientClick="javascript:return window.location='m_NOT_Write.aspx';" UseSubmitBehavior="False" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="취소" ID="btnCancel" 
                    runat="server" OnClientClick="javascript:return window.location='/m_Default.aspx';" UseSubmitBehavior="False" />
            </div> 
    </article>
</asp:Content>