<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_ATT_Cody_List_Batch.aspx.cs" Inherits="m_ATT_Cody_List_Batch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<META http-equiv="Expires" content="-1"> 
<META http-equiv="Pragma" content="no-cache"> 
<META http-equiv="Cache-Control" content="No-Cache"> 
    <script type="text/javascript">

        
        // 오늘 날짜 구하기
        function getToday()
        {
            var date = new Date();

            yyyy = date.getYear();
            if (yyyy < 1000) {
                if (yyyy < 70) {
                    yyyy = 2000 + yyyy;
                }
                else yyyy = 1900 + yyyy;
            }

            mm = date.getMonth() + 1;
            if (mm < 10) {
                mm = '0' + mm;
            }

            dd = date.getDate();
            if (dd < 10) {
                dd = '0' + dd;
            }

            return yyyy + "-" + mm + "-" + dd;

        }

        // 두 날짜의 차이 구하기
        function getDiffDay(startDate, endDate) {
            var diffDay = 0;
            var start_yyyy = startDate.substring(0, 4);
            var start_mm = startDate.substring(5, 7);
            var start_dd = startDate.substring(8, startDate.length);
            var sDate = new Date(start_yyyy, start_mm - 1, start_dd);
            var end_yyyy = endDate.substring(0, 4);
            var end_mm = endDate.substring(5, 7);
            var end_dd = endDate.substring(8, endDate.length);
            var eDate = new Date(end_yyyy, end_mm - 1, end_dd);

            diffDay = Math.ceil((eDate.getTime() - sDate.getTime()) / (1000 * 60 * 60 * 24));

            return diffDay;
        }

        function fncDetail(strATT_DAY_ID, strDate) {
            var today = getToday();
            var dday = getDiffDay(strDate, today);

            // 토요일이 아니거나 근태일자가 7일이상 지난경우 수정 할 수 없음
            if (new Date().getDay() != 6 && eval(dday) > 6)
            {
                alert("근태일자가 6일이상 지났을 경우 근태를 수정할 수 없습니다. \n\n(매주 토요일은 해당 월의 모든 근태 수정 가능)");  // 일요일=0, 토요일=6
                return;
            }

            document.getElementById('<%= this.hdATT_DAY_ID.ClientID %>').value =  strATT_DAY_ID;
            document.getElementById('<%= this.hdDate.ClientID %>').value =  strDate;
        }

        // 날짜(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckDate() {
            if (document.getElementById('<%= this.txtDate.ClientID %>').value != "YYYYMMDD") {
               var num, year, month, day;
               num = document.getElementById('<%= this.txtDate.ClientID %>').value;

               while (num.search("-") != -1) {
                   num = num.replace("-", "");
               }

               if (isNaN(num)) {
                   num = "";
                   alert("날짜는 숫자만 입력 가능합니다.");
                   document.getElementById('<%= this.txtDate.ClientID %>').focus();
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
                           document.getElementById('<%= this.txtDate.ClientID %>').focus();
                           return false;
                       }
                       num = year + "-" + month + "-" + day;
                   }
                   else {
                       num = "";
                       alert("날짜의 입력 형식은 YYYYMMDD 입니다.");
                       document.getElementById('<%= this.txtDate.ClientID %>').focus();
                       return false;
                   }

                   document.getElementById('<%= this.txtDate.ClientID %>').value = num;
               }
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

        function fncDateSubmit() {
            if (CheckDate() == false) return false;

            return true;
        }

        // 텍스트박스 기본값 지정 후 클릭시 사라지면서 스타일 지정하기
        function change_Att(obj, strValue) {
            if (obj.value == strValue) {
                obj.value = "";
                obj.className = 'text_nm i_f_on'
            }
            else if (obj.value == "") {
                obj.value = strValue;
                obj.className = 'text_nm i_f_out2'
            }
        }

        function checkAll(obj) {
            var blChecked = obj.checked;
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                if (document.forms[0].elements[i].id.indexOf("cbCheck") > -1) {
                    document.forms[0].elements[i].checked = blChecked;
                }
            }
        }

        var toggle = false;

        function ChangeRowColor(rowID) {
            alert(rowID);
            document.getElementById(rowID).style.backgroundColor = "#939393";
            //var color = document.getElementById(rowID).style.backgroundColor;
            ////alert(color);

            ////if (color != "#939393")
            ////    document.getElementById("hiddenColor").value = "#939393";
            //document.getElementById("hiddenColor").value = color;

            ////alert(oldColor);

            //if (color == "#939393")
            //    document.getElementById(rowID).style.backgroundColor = document.getElementById("hiddenColor").value;
            //else
            //    document.getElementById(rowID).style.backgroundColor = "#f6f6f6";

        }
    </script>
    <input type="hidden" id="hiddenColor" name="hiddenColor" />
    <input type="hidden" id="hdATT_DAY_ID" name="hdATT_DAY_ID" runat="server" />
    <input type="hidden" id="hdDate" name="hdDate" runat="server" />
    <%--<input type="hidden" id="hdscrollTop" name="hdscrollTop" value="0" runat="server" />--%>
<%--    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>--%>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Attendance/m_ATT_Cody_Summary.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">일근태 관리 > 코디 근태 일괄 편집</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:75%;"><p>
                        <asp:Label runat="server" ID="lblDate" Text="" Font-Bold="true"></asp:Label>
                        <asp:Label runat="server" ID="lblWeek" Text="" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtDate" runat="server" MaxLength="10" style="width:100px;" value="" onfocus="change_Att(this,'YYYYMMDD')" onblur="change_Att(this,'YYYYMMDD')" class="text_nm i_f_out2" Visible="false" />
                         근태 목록</p></td> 
                    <td align="right" style="width:25%; font-weight:normal; padding-right:0.5em;">
                        <asp:Button CssClass="button_nm gray mepm_btn_4em" Text="취소" ID="btnList" 
                                runat="server" OnClick="btnList_Click" />
                    </td>
                </tr>
            </table>
            <div  style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td style="text-align:right;border-top:1px solid #ccc;">
                            출근 
                        </td>
                        <td style="width: 16%;text-align:center;border-top:1px solid #ccc;">
                            <asp:Label runat="server" ID="lblCODY_ATT_CNT" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 19%;text-align:right;border-top:1px solid #ccc;">
                            결근 
                        </td>
                        <td style="width: 16%;text-align:center;border-top:1px solid #ccc;">
                            <asp:Label runat="server" ID="lblCODY_ABS_CNT" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 19%;text-align:right;border-top:1px solid #ccc;">
                            휴가(무)
                        </td>
                        <td style="width: 16%;text-align:center;border-top:1px solid #ccc;">
                            <asp:Label runat="server" ID="lblCODY_HOL_CNT" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr  class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td colspan="5" style="border-top:1px solid #ccc;padding-left:.8em;">
                            <asp:DropDownList ID="ddlATT_DAY_Code" style="width:120px;" runat="server"
                                CssClass="i_f_out" DataTextField="COD_Name" 
                                DataValueField="COD_CD">
            <%--                    <asp:ListItem Text="-선택-" Value=""></asp:ListItem>--%>
                            </asp:DropDownList>  
                        </td>
                        <td style="border-top:1px solid #ccc; padding-top:.8em;padding-right:0.5em;">
                            <asp:Button CssClass="button_nm gray mepm_btn_4em" Text="저장" ID="btnSave" runat="server" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg">
                        <td colspan="6" style="height: 5px; border-top:1px solid #ccc;""></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                <asp:Panel ID="pnlCodyAttList" CssClass="wenitepm_div_fix_layout1" runat="server" Height="330px" Width="100%" ScrollBars="Auto">
                    <asp:GridView ID="gvCodyAttList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="등록된 정보가 없습니다." ShowHeaderWhenEmpty="True"
                            CssClass="table_border" OnRowDataBound="gvCodyAttList_RowDataBound" AutoGenerateColumns="false" OnRowCreated="gvCodyAttList_RowCreated1">
                        <Columns>
                            <asp:TemplateField Visible="true">
                                <HeaderTemplate >
                                    <asp:CheckBox ID="cbCheckAll" runat="server" OnCheckedChanged="cbCheckAll_CheckedChanged" AutoPostBack="true"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbCheck" runat="server"/>
                                    <asp:HiddenField ID="hdDayId" runat="server" Value='<%# Eval("ATT_DAY_ID")%>' />
                                </ItemTemplate>
                                <HeaderStyle CssClass="tr_border" />
                                <ItemStyle HorizontalAlign="Center" Height="30px" CssClass="tr_border" Wrap="False" Width="50px"/>
                            </asp:TemplateField>
                            <asp:boundfield HeaderText="이름" DataField="RES_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="매장" DataField="RES_STORE_NAME">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="근무형태" DataField="ATT_DAY_Code_Name">
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
        </section>
    </article>
</asp:Content>
