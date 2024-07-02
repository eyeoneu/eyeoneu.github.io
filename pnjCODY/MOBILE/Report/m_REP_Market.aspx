<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_REP_Market.aspx.cs" Inherits="m_REP_Market" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 날짜(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckStartDate() {
            if (document.getElementById('<%= this.txtEventStart.ClientID %>').value != "YYYYMMDD") {
                var num, year, month, day;
                num = document.getElementById('<%= this.txtEventStart.ClientID %>').value;

               while (num.search("-") != -1) {
                   num = num.replace("-", "");
               }

               if (isNaN(num)) {
                   num = "";
                   alert("날짜는 숫자만 입력 가능합니다.");
                   document.getElementById('<%= this.txtEventStart.ClientID %>').focus();
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
                           document.getElementById('<%= this.txtEventStart.ClientID %>').focus();
                           return false;
                       }
                       num = year + "-" + month + "-" + day;
                   }
                   else {
                       num = "";
                       alert("날짜의 입력 형식은 YYYYMMDD 입니다.");
                       document.getElementById('<%= this.txtEventStart.ClientID %>').focus();
                       return false;
                   }

                   document.getElementById('<%= this.txtEventStart.ClientID %>').value = num;
               }
           }
        }

        // 날짜(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinsihDate() {
            if (document.getElementById('<%= this.txtEventFinsih.ClientID %>').value != "YYYYMMDD") {
                 var num, year, month, day;
                 num = document.getElementById('<%= this.txtEventFinsih.ClientID %>').value;

                while (num.search("-") != -1) {
                    num = num.replace("-", "");
                }

                if (isNaN(num)) {
                    num = "";
                    alert("날짜는 숫자만 입력 가능합니다.");
                    document.getElementById('<%= this.txtEventFinsih.ClientID %>').focus();
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
                           document.getElementById('<%= this.txtEventFinsih.ClientID %>').focus();
                           return false;
                       }
                       num = year + "-" + month + "-" + day;
                   }
                   else {
                       num = "";
                       alert("날짜의 입력 형식은 YYYYMMDD 입니다.");
                       document.getElementById('<%= this.txtEventFinsih.ClientID %>').focus();
                       return false;
                   }

                   document.getElementById('<%= this.txtEventFinsih.ClientID %>').value = num;
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

        function fncChkSave() {
            var ddlType = document.getElementById('<%=this.ddlType.ClientID %>').value;
            var regExp = /^\s*$/;
            var dateExp = /\d{4}\-\d{2}\-\d{2}/; //날짜 정규식
            var num;

            //alert(ddlType);

            if (regExp.test(ddlType) || ddlType == "") {
                alert("구분은 필수 선택 항목입니다.");
                document.getElementById('<%=this.ddlType.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.ddlCustomer.ClientID %>').value)
                    || document.getElementById('<%=this.ddlCustomer.ClientID %>').value == "") {
                alert("거래처는 필수 선택 항목입니다.");
                document.getElementById('<%=this.ddlCustomer.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.ddlStore.ClientID %>').value)
                    || document.getElementById('<%=this.ddlStore.ClientID %>').value == "") {
                alert("매장명은 필수 선택 항목입니다.");
                document.getElementById('<%=this.ddlStore.ClientID %>').focus();
                return false;
            }
            else {
                if (ddlType == 'S' || ddlType == 'R') {
                    var date1 = new Date(document.getElementById('<%=this.txtEventStart.ClientID %>').value);
                    var date2 = new Date(document.getElementById('<%=this.txtEventFinsih.ClientID %>').value);

                    if (CheckStartDate() == false) return false;
                    if (CheckFinsihDate() == false) return false;

                    if (date1 != null && date2 != null) {
                        if (date1 > date2) {
                            alert("행사 종료일이 행사 시작일 보다 빠릅니다.");
                            document.getElementById('<%=this.txtEventFinsih.ClientID %>').focus();
                            return false;
                        }
                    }

                    if (regExp.test(document.getElementById('<%=this.txtEventProperty.ClientID %>').value)
                        || document.getElementById('<%=this.txtEventProperty.ClientID %>').value == "") {
                        alert("행사 특성을 입력하여 주세요.");
                        document.getElementById('<%=this.txtEventProperty.ClientID %>').focus();
                        return false;
                    }
                }
                else if (ddlType == "N") {
                    if (regExp.test(document.getElementById('<%=this.txtProduct.ClientID %>').value)
                        || document.getElementById('<%=this.txtProduct.ClientID %>').value == "") {
                        alert("제품명을 입력하여 주세요.");
                        document.getElementById('<%=this.txtProduct.ClientID %>').focus();
                        return false;
                    }

                    if (regExp.test(document.getElementById('<%=this.ddlCompany.ClientID %>').value)
                        || document.getElementById('<%=this.ddlCompany.ClientID %>').value == "") {
                        alert("회사는 필수 선택 항목입니다.");
                        document.getElementById('<%=this.ddlCompany.ClientID %>').focus();
                        return false;
                    }

                    //                    if (regExp.test(document.getElementById('<%=this.ddlClassify.ClientID %>').value)
                    //                       || document.getElementById('<%=this.ddlClassify.ClientID %>').value == "") {
                    //                       alert("종류는 필수 선택 항목입니다.");
                    //                       document.getElementById('<%=this.ddlClassify.ClientID %>').focus();
                    //                       return false;
                    //                   }

                    num = document.getElementById('<%= this.txtRetailPrice.ClientID %>').value;
                    if (isNaN(num)) {
                        num = "";
                        alert("소비자가(원)는 숫자만 입력 가능합니다.");
                        document.getElementById('<%= this.txtRetailPrice.ClientID %>').focus();
                        return false;
                    }
                    else {
                        document.getElementById('<%= this.txtRetailPrice.ClientID %>').value = num;
                    }

                    if (regExp.test(document.getElementById('<%=this.txtRetailPrice.ClientID %>').value)
                        || document.getElementById('<%=this.txtRetailPrice.ClientID %>').value == "") {
                        alert("소비자가(원)을 입력하여 주세요.");
                        document.getElementById('<%=this.txtRetailPrice.ClientID %>').focus();
                        return false;
                    }

                    num = document.getElementById('<%= this.txtPrice.ClientID %>').value;
                    if (isNaN(num)) {
                        num = "";
                        alert("판매가(원)는 숫자만 입력 가능합니다.");
                        document.getElementById('<%= this.txtPrice.ClientID %>').focus();
                        return false;
                    }
                    else {
                        document.getElementById('<%= this.txtPrice.ClientID %>').value = num;
                    }

                    if (regExp.test(document.getElementById('<%=this.txtPrice.ClientID %>').value)
                        || document.getElementById('<%=this.txtPrice.ClientID %>').value == "") {
                        alert("판매가(원)을 입력하여 주세요.");
                        document.getElementById('<%=this.txtPrice.ClientID %>').focus();
                        return false;
                    }

                    num = document.getElementById('<%= this.txtWeight.ClientID %>').value;
                    if (isNaN(num)) {
                        num = "";
                        alert("중량(g)은 숫자만 입력 가능합니다.");
                        document.getElementById('<%= this.txtWeight.ClientID %>').focus();
                        return false;
                    }
                    else {
                        document.getElementById('<%= this.txtWeight.ClientID %>').value = num;
                    }

                    if (regExp.test(document.getElementById('<%=this.txtWeight.ClientID %>').value)
                        || document.getElementById('<%=this.txtWeight.ClientID %>').value == "") {
                        alert("중량(g)을 입력하여 주세요.");
                        document.getElementById('<%=this.txtWeight.ClientID %>').focus();
                        return false;
                    }

                    if (regExp.test(document.getElementById('<%=this.txtRemark.ClientID %>').value)
                        || document.getElementById('<%=this.txtRemark.ClientID %>').value == "") {
                        alert("시장반응을 입력하여 주세요.");
                        document.getElementById('<%=this.txtRemark.ClientID %>').focus();
                        return false;
                    }
                }
                else if (ddlType == "E") {
                    if (regExp.test(document.getElementById('<%=this.lblResName.ClientID %>').innerHTML)
                        || document.getElementById('<%=this.lblResName.ClientID %>').innerHTML == "") {
                        alert("이름을 검색하여 정보를 입력하여 주세요.");
                        document.getElementById('<%=this.txtResName.ClientID %>').focus();
                        return false;
                    }

                    if (regExp.test(document.getElementById('<%=this.lblPosition.ClientID %>').innerHTML)
                        || document.getElementById('<%=this.lblPosition.ClientID %>').innerHTML == "") {
                        alert("이름을 검색하여 정보를 입력하여 주세요.");
                        document.getElementById('<%=this.txtResName.ClientID %>').focus();
                        return false;
                    }

                    if (regExp.test(document.getElementById('<%=this.lblJoinDate.ClientID %>').innerHTML)
                        || document.getElementById('<%=this.lblJoinDate.ClientID %>').innerHTML == "") {
                        alert("이름을 검색하여 정보를 입력하여 주세요.");
                        document.getElementById('<%=this.txtResName.ClientID %>').focus();
                        return false;
                    }

                    if (regExp.test(document.getElementById('<%=this.txtRemarks2.ClientID %>').value)
                        || document.getElementById('<%=this.txtRemarks2.ClientID %>').value == "") {
                        alert("운영사유를 입력하여 주세요.");
                        document.getElementById('<%=this.txtRemarks2.ClientID %>').focus();
                        return false;
                    }

                }

                if (regExp.test(document.getElementById('<%=this.hdPhotoPath.ClientID %>').value)
                    || document.getElementById('<%=this.hdPhotoPath.ClientID %>').value == "") {
                    alert("사진을 입력하여 주세요.");
                    document.getElementById('<%=this.upPicture.ClientID %>').focus();
                    return false;
                }
            }
        }

        function fncChkSearch() {
            if (document.getElementById('<%= this.txtResName.ClientID %>').value.length < 2) {
                alert("정확한 이름을 입력해 주세요.");
                document.getElementById('<%= this.txtResName.ClientID %>').focus();
                return false;
            }
            return true;
        }

        function fncSelectRes(strRES_ID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;

            <%= Page.GetPostBackEventReference(this.btnSelectRes) %>
        }

        function setFileName(strfile) {
            document.getElementById('<%= this.hdPhotoPath.ClientID %>').value = strfile;
        }

        // 텍스트박스 기본값 지정 후 클릭시 사라지면서 스타일 지정하기
        function change(obj, strValue) {
            date = document.getElementById('<%= this.hdDate.ClientID %>').value.substring(0, 4); //현재목록의 날짜를 4자리만 가져온다 ex. 2013

            if (obj.value == strValue) {
                obj.value = date;
                obj.className = 'i_f_on'
            }
            else if (obj.value == date) {
                obj.value = strValue;
                obj.className = 'i_f_out2'
            }
        }
    </script>
    <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdDate" name="hdDate" runat="server" />
    <input type="hidden" id="hdPhotoPath" name="hdPhotoPath" runat="server" />
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 일지 > 시장조사</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >시장조사</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">구분 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                <asp:ListItem Value="">-선택-</asp:ListItem>
                                <%--<asp:ListItem Value="S">지원사</asp:ListItem>
                                <asp:ListItem Value="R">경쟁사</asp:ListItem>--%>
                                <asp:ListItem Value="N">신제품</asp:ListItem>
                                <asp:ListItem Value="E">별도매대</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">거래처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">매장명 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlStore" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_menu_item_bg" style="padding:0;" runat="server" id="divEvent">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">행사기간 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtEventStart" runat="server" MaxLength="10" style="width:45%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out2" /> -
                            <asp:TextBox ID="txtEventFinsih" runat="server" MaxLength="10" style="width:45%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">행사특성 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtEventProperty" runat="server" style="width:100%;" TextMode="MultiLine" Rows="5" MaxLength="500" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_menu_item_bg" style="padding:0;" runat="server" id="divNew">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">제품명 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtProduct" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">회사 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                                <asp:ListItem Value="">-선택-</asp:ListItem>
                                <asp:ListItem Value="C">크라운</asp:ListItem>
                                <asp:ListItem Value="H">해태</asp:ListItem>
                                <asp:ListItem Value="L">롯데</asp:ListItem>
                                <asp:ListItem Value="O">오리온</asp:ListItem>                                
                                <asp:ListItem Value="U">아워홈</asp:ListItem>
                                <asp:ListItem Value="J">CJ</asp:ListItem>
                                <asp:ListItem Value="N">농심</asp:ListItem>
                                <asp:ListItem Value="G">청우식품</asp:ListItem>
                                <asp:ListItem Value="E">청정원</asp:ListItem>
                                <asp:ListItem Value="B">백설</asp:ListItem>
                                <asp:ListItem Value="D">동원</asp:ListItem>
                                <asp:ListItem Value="P">풀무원</asp:ListItem>
                                
                                <asp:ListItem Value="A">하림</asp:ListItem>
                                <asp:ListItem Value="Z">엄지식품</asp:ListItem>
                                <asp:ListItem Value="F">기타</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">류별 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:DropDownList ID="ddlClassify" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">소비자가(원) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtRetailPrice" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">판매가(원) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtPrice" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">중량(g) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtWeight" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">시장반응 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtRemark" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                 TextMode="MultiLine" Rows="5"/>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_menu_item_bg" style="padding:0;" runat="server" id="divExtra">

                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">성명 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtResName" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="text-align:right; border-top:1px solid #ccc;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="btnSearch" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" />

                        </td>
                    </tr>
                </table>

                <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvResList" visible="false">
                    <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                            CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="이름" DataField="RES_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="사번" DataField="RES_Number">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>    
                            <asp:boundfield HeaderText="직급" DataField="RES_WorkGroup2_NAME">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>                                                        
                            <asp:BoundField HeaderText="입사일" DataField="RES_JoinDate" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataRowStyle Height="50px" HorizontalAlign="Center" CssClass="empty_border" />
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

                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">이름 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2" >
                            <asp:Label ID="lblResName" runat="server" Text=""></asp:Label>    <input type="hidden" id="hdResIdExtra" name="hdResIdExtra" runat="server" />

                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">직급 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2" >
                            <asp:Label ID="lblPosition" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">입사일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">운영사유 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtRemarks2" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                     TextMode="MultiLine" Rows="8"/>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:5em;">
                        <th style="border-top:1px solid #ccc; width:90px; text-align:right; padding-right:.8em;">사진 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:Label ID="lblRES_Picture" runat="server"></asp:Label>
                            <asp:Image ID="imgRES_Picture" ImageUrl="" runat="server" Visible="false"  width="185px" height="155px" />
                            <asp:FileUpload CssClass="i_f_out" ID="upPicture" runat="server" Width="100%" BackColor="white" onchange="setFileName(this.value);" />
                    </tr>
                </table>
            </div>

            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Report/m_REP_Market_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
