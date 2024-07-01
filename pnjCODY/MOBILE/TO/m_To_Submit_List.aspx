<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_To_Submit_List.aspx.cs" Inherits="m_To_Submit_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        // 사원 클릭 시
        function fncDetail(srtID, hisID) {
//            var parm = new Array();
//            parm.push("RES_ID=" + srtID);

//            location.href("/Resource/m_RES_Mng.aspx?" + encodeURI(parm.join('&')));

            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  srtID;
            document.getElementById('<%= this.hdHIS_ID.ClientID %>').value = hisID;
            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }

        // 신정기간 종료(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {

            if (document.getElementById('<%= this.txtFinishDate.ClientID %>').value == "YYYYMMDD") {
                alert("기준일은 입력해 주세요.");
                document.getElementById('<%= this.txtFinishDate.ClientID %>').focus();
                    return false;
                }

                var num, year, month, day;
                num = document.getElementById('<%= this.txtFinishDate.ClientID %>').value;

                while (num.search("-") != -1) {
                    num = num.replace("-", "");
                }

                if (isNaN(num)) {
                    num = "";
                    alert("기준일은 숫자만 입력 가능합니다.");
                    document.getElementById('<%= this.txtFinishDate.ClientID %>').focus();
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
                            document.getElementById('<%= this.txtFinishDate.ClientID %>').focus();
                            return false;
                        }
                        num = year + "-" + month + "-" + day;
                    }
                    else {
                        num = "";
                        alert("기준일 입력 형식은 YYYYMMDD 입니다.");
                        document.getElementById('<%= this.txtFinishDate.ClientID %>').focus();
                        return false;
                    }

                    document.getElementById('<%= this.txtFinishDate.ClientID %>').value = num;
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
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdHIS_ID" name="hdHIS_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>기타업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">기타 업무 관리 > TO증원</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:90%;"><p >TO증원 목록</p></td>
                        <td align="right" style="width:10%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
              <div class="mepm_menu_item" style="padding: 0;">

                <table>
                    <tr class="mepm_menu_title_bgw" style="height: 3em;">
                        <td style="width: 55px; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            기준일 :                             
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">                           
                           <asp:TextBox ID="txtFinishDate" width="105px" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); fncDuration();" class="i_f_out2"></asp:TextBox>
                        </td>
                        <td style="width: 75px; border-top: 1px solid #ccc; text-align:left; padding-right:.5em; ">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return CheckFinishDate();" OnClick="btnSearch_Click" />                     
                        </td>
                    </tr>                  
                    <!-- 신청기간 항목 종료 -->
                </table>

            </div>
             <div class="mepm_menu_item" style="padding:0;">
                    <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>     
                            <asp:boundfield HeaderText="구분" DataField="TO_GB">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>                                           
                            <asp:boundfield HeaderText="변경항목" DataField="HIS_TYPE">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="작성일자" DataField="HIS_REQDATE">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="처리상태" DataField="HIS_STATUS_NAME">
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
                <a href="/TO/m_To_Submit.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">입력</span></a>
                <a href="/Business/m_BUS_Connection.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>

            <div style="padding-top: .25em; text-align:center;">
            * 기준일 이전 2개월 부터 기준일까지 보고된 목록이 조회됩니다.
            </div>
        </section>
    </article>
</asp:Content>
