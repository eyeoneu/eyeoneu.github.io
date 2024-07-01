<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_ATT_Parttime_Request_List.aspx.cs" Inherits="Attendance_m_ATT_Parttime_Request_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 메뉴 토글
        function toggle_display(obj) {
            var div1 = document.getElementById(obj);
            if (div1.style.display == 'table') {
                div1.style.display = 'none';
            } else {
                div1.style.display = 'table';
            }
        }

        // 텍스트박스 기본값 지정 후 클릭시 사라지면서 스타일 지정하기
        function change_Att(obj, strValue) {
            if (obj.value == strValue) {
                obj.value = "";
                obj.className = 'i_f_on'
            }
            else if (obj.value == "") {
                obj.value = strValue;
                obj.className = 'i_f_out2'
            }
        }

        function fncSearchCheck() {
            if (document.getElementById('<%=this.txtAttMonth.ClientID %>').value != "") {
                var month = document.getElementById('<%=this.txtAttMonth.ClientID %>').value.substring(5, 7);

                if (month > 12 || month <= 0) {
                    alert("근무월을 확인해 주세요.");
                    document.getElementById('<%=this.txtAttMonth.ClientID %>').focus();
                    return false;
                }
            }
        }
    </script>
    <style type="text/css">
        dl, dt, dd, ul, ol, li {
            margin: 0;
            padding: 0;
            cursor: pointer;
        }
        ul {
            width: 100%;
            float: initial;
            margin: 0;
            padding: 0;
        }
        li {
            margin-left: 0px;
            float: initial;
        }
        ul .ListStyle {
            color: #ff0000;
        }
        li .ModalPopup {
            table-layout: fixed;
            word-break: break-all;
            line-height: 2.5em;
            border: 1px solid #ffd800;
            padding: 5px 10px 5px 10px;
            background-color: #fff5a9;
            box-shadow: 2px 2px 5px #999;
            font-size: 1.1em;
            position: fixed;
            text-align: left;
            width: 300px;
            height: 50%;
            bottom: 25px;
            left: 50%;
            margin-left:-162px;
        }
    </style>
     <input type="hidden" id="hdDate" name="hdDate" runat="server" />
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
               <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Attendance/m_ATT_Requset_Today.aspx" ><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">근태 수정요청 현황</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:75%;"><p>
                        계약 근태 수정요청 현황</p></td>
                </tr>
            </table>
               <!-- 공지사항 기간 시작 -->
             <div class="mepm_menu_item" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width:50px;text-align:left; padding-left:.5em;">
                            근무월 :
                        </td>
                        <td style="width:200px;padding-right:.5em; text-align:left;" colspan="2">
                           <asp:TextBox ID="txtAttMonth" width="75px" runat="server" value="YYYY-MM" onfocus="change_Att(this,'YYYY-MM')" onblur="change_Att(this,'YYYY-MM')" class="i_f_out2" ></asp:TextBox>
                        </td>
                        <td align="right" style="padding-right:0.5em; width:50px;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" 
                                    runat="server" OnClientClick="javascript:return fncSearchCheck();"  OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <!-- 공지사항 기간 종료 -->
            <div>
               <table>
                    <tr class="mepm_menu_title_bg" style="height:3em;">
                            <th style="border-top:1px solid #ccc;"></th>
                            <td style="text-align:left;border-top:1px solid #ccc;">
                                요청 중 <asp:Label runat="server" ID="lblAR_REQ_CNT" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;border-top:1px solid #ccc;">
                                완료 <asp:Label runat="server" ID="lblAR_CNF_CNT" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="text-align:left;border-top:1px solid #ccc;">
                                반려 <asp:Label runat="server" ID="lblAR_RTN_CNT" Font-Bold="true">-</asp:Label>
                            </td>
                        </tr>
                </table>
            </div>
            <div ID="divAttClosed" runat="server" class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;" visible="false">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td style="text-align:center;border-top:1px solid #ccc; background-color:#880000; color:#ffd800;">
                            <h3><asp:Label runat="server" ID="lblAttClosed" Text="" Fore-Color="#ffd800"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                <asp:Panel ID="pnlAttReqList" CssClass="wenitepm_div_fix_layout1" runat="server" Width="100%" ScrollBars="none">
                    <asp:GridView ID="gvAttReqList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                            CssClass="table_border" OnRowDataBound="gvAttReqList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="이름" DataField="RES_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="일자" DataField="ATT_DATE">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="수정내용" DataField="ADR_Edit_Text">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                              <asp:boundfield HeaderText="승인상태" DataField="ADR_Status">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
                </asp:Panel>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="< 이전 달" ID="btnBottomPreDate" 
                    runat="server" OnClick="btnPreDate_Click" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="다음 달 >" ID="btnBottomNextDate" 
                    runat="server" OnClick="btnNextDate_Click" />
            </div>                  
        </section>
    </article>
<script type="text/javascript">
    fncSetScroll();
</script>
</asp:Content>