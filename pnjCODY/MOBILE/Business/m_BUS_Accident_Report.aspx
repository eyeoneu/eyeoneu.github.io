<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_BUS_Accident_Report.aspx.cs" Inherits="m_BUS_Accident_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 사고일자(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckDate() {            
            if (document.getElementById('<%= this.txtAccDate.ClientID %>').value == "YYYYMMDD") {
                alert("사고일자을 입력해 주세요.");
                document.getElementById('<%= this.txtAccDate.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtAccDate.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("사고일자은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtAccDate.ClientID %>').focus();
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
                        document.getElementById('<%= this.txtAccDate.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("사고일자 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtAccDate.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtAccDate.ClientID %>').value = num;
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
            var regExp = /^\s*$/;
            var dateExp = /\d{4}\-\d{2}\-\d{2}/; //날짜 정규식

            // 사고일자
            if (CheckDate() == false) return false;

            else if (regExp.test(document.getElementById('<%=this.hdRES_ID.ClientID %>').value)
                || document.getElementById('<%=this.hdRES_ID.ClientID %>').value == "") {
                alert("사고자를 검색하여 입력하여 주세요.");
                document.getElementById('<%=this.txtResName.ClientID %>').focus();
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
            else if (regExp.test(document.getElementById('<%=this.txtAccPlace.ClientID %>').value)
                || document.getElementById('<%=this.txtAccPlace.ClientID %>').value == "") {
                alert("사고 장소를 입력하여 주세요.");
                document.getElementById('<%=this.txtAccPlace.ClientID %>').focus();
                return false;
            }

            else if (regExp.test(document.getElementById('<%=this.txtAccWhen.ClientID %>').value)
                || document.getElementById('<%=this.txtAccWhen.ClientID %>').value == "") {
                alert("사고 일시를 입력하여 주세요.");
                document.getElementById('<%=this.txtAccWhen.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtAccWhere.ClientID %>').value)
                || document.getElementById('<%=this.txtAccWhere.ClientID %>').value == "") {
                alert("사고 장소를 입력하여 주세요.");
                document.getElementById('<%=this.txtAccWhere.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtAccWho.ClientID %>').value)
                || document.getElementById('<%=this.txtAccWho.ClientID %>').value == "") {
                alert("사고 행위자를 입력하여 주세요.");
                document.getElementById('<%=this.txtAccWho.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtAccWhat.ClientID %>').value)
                || document.getElementById('<%=this.txtAccWhat.ClientID %>').value == "") {
                alert("사고 내용을 입력하여 주세요.");
                document.getElementById('<%=this.txtAccWhat.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtAccHow.ClientID %>').value)
                || document.getElementById('<%=this.txtAccHow.ClientID %>').value == "") {
                alert("사고 경위를 입력하여 주세요.");
                document.getElementById('<%=this.txtAccHow.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtAccWhy.ClientID %>').value)
                || document.getElementById('<%=this.txtAccWhy.ClientID %>').value == "") {
                alert("사고 이유를 입력하여 주세요.");
                document.getElementById('<%=this.txtAccWhy.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtAccReason.ClientID %>').value)
                || document.getElementById('<%=this.txtAccReason.ClientID %>').value == "") {
                alert("사고 발생원인을 입력하여 주세요.");
                document.getElementById('<%=this.txtAccReason.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.txtRemark.ClientID %>').value)
            || document.getElementById('<%=this.txtRemark.ClientID %>').value == "") {
                    alert("서포터의견을 입력하여 주세요.");
                    document.getElementById('<%=this.txtRemark.ClientID %>').focus();
                return false;
            }
            else if (regExp.test(document.getElementById('<%=this.hdPhotoPath.ClientID %>').value)
                || document.getElementById('<%=this.hdPhotoPath.ClientID %>').value == "") {
                alert("사진을 첨부 해주세요.");
                document.getElementById('<%=this.upFile.ClientID %>').focus();
                return false;
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

        function fncSelectRes(strRES_ID, strGB, strASSCON_ID, strCUSTOMER_ID, strASSCON_STOREID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value = strRES_ID;
            document.getElementById('<%= this.hdGB.ClientID %>').value = strGB;
            document.getElementById('<%= this.hdASSCON_ID.ClientID %>').value = strASSCON_ID;
            document.getElementById('<%= this.hdCUSTOMER_ID.ClientID %>').value = strCUSTOMER_ID;
            document.getElementById('<%= this.hdASSCON_STOREID.ClientID %>').value = strASSCON_STOREID;

            <%= Page.GetPostBackEventReference(this.btnSelectRes) %>
        }

        function setFileName(strfile) {
            document.getElementById('<%= this.hdPhotoPath.ClientID %>').value = strfile;
        }

        //-----------첨부파일 부분
        var divIdx = 2;
        function onFileChange(obj, idx) {
            //document.getElementById("fileDiv" + idx).style.display = "none";
            //obj.style.display = "none";
            var fileDiv = document.createElement("div");
            fileDiv.id = "fileDiv" + divIdx;
            var file = document.createElement("input");
            file.setAttribute("type", "file");
            file.setAttribute("name", "attachment[]");
            file.setAttribute("style", "width:90%;");
            file.setAttribute("onfocus", "this.className = 'i_f_on';");
            file.setAttribute("onblur", "this.className = 'i_f_out';");
            file.setAttribute("class", "i_f_out");
            file.setAttribute("onchange", "onFileChange(this, " + divIdx + ");");
            fileDiv.appendChild(file);
            document.getElementById("moreUploads").appendChild(fileDiv);

            divIdx++;

            document.getElementById('<%= this.hdPhotoPath.ClientID %>').value += obj.value;

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
    <input type="hidden" id="hdGB" name="hdGB" runat="server" />
    <input type="hidden" id="hdASSCON_ID" name="hdASSCON_ID" runat="server" />
    <input type="hidden" id="hdCUSTOMER_ID" name="hdCUSTOMER_ID" runat="server" />
    <input type="hidden" id="hdASSCON_STOREID" name="hdASSCON_STOREID" runat="server" />
    <input type="hidden" id="hdPhotoPath" name="hdPhotoPath" runat="server" />
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 사고발생보고</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >사고발생보고</p></td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">사고일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtAccDate" runat="server" MaxLength="10" style="width:45%;" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD')" class="i_f_out2" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0;" runat="server" id="divExtra">

                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">사원 :</th>
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
                            <asp:boundfield HeaderText="매장" DataField="RES_Store">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="사번" DataField="RES_Number">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>                            
                            <asp:BoundField HeaderText="GB" DataField="GB" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ASSCON_ID" DataField="ASSCON_ID" Visible="False">
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
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">이름 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2" >
                            <asp:Label ID="lblResName" runat="server" Text=""></asp:Label>    <input type="hidden" id="hdResIdExtra" name="hdResIdExtra" runat="server" />

                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">직급 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2" >
                            <asp:Label ID="lblPosition" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">입사일자 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:Label ID="lblJoinDate" runat="server"></asp:Label>
                        </td>
                    </tr>                 
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">거래처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">매장명 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlStore" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">사고장소 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccPlace" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">사고시간(언제) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccWhen" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">장 소(어디서) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccWhere" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">행위자(누가) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccWho" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">행위내용(무엇을) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccWhat" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">경 위(어떻게) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccHow" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">이 유(왜) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccWhy" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">발생원인 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAccReason" runat="server" style="width:100%;" MaxLength="1000" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                 TextMode="MultiLine" Rows="3"/>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">서포터의견 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtRemark" runat="server" style="width:100%;" MaxLength="1000" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                 TextMode="MultiLine" Rows="3"/>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <!-- 첨부 파일 부분 시작 -->
                    <tr class="mepm_menu_item_bg" style="height: 3em;"> 
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em; vertical-align:top; padding-top:.8em;" rowspan="2">
                            첨부 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .5em;" colspan="3">
                            <span style="color:red;">* iPhone 에서는 첨부 할 수 없습니다.</span><br />
                            <asp:Label ID="lbAttFile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 6em;"> 
                        <td style="text-align: left; padding-right: .5em;" colspan="3">
                            <div id="moreUploads">
                                <div id="fileDiv1" >
                                    <asp:FileUpload ID="upFile" CssClass="i_f_out" name="attachment[]" runat="server" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" onchange="onFileChange(this,1);" Width="90%" />
                                </div>
                            </div>
                            <%--<asp:FileUpload CssClass="i_f_out" ID="upFile" runat="server" Width="90%"  BackColor="white" onchange="this.className='i_f_on';" />--%>
                        </td>
                    </tr>
                    <!-- 첨부 파일 부분 종료 -->

                </table>
            </div>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="Acc_SubScript" visible="false">
                <table>
                    <!-- 첨부 파일 부분 시작 -->
                    <tr class="mepm_menu_item_bg" style="height: 3em;"> 
                        <td style="text-align:center; border-top:1px solid #ccc; padding-right: .5em;" colspan="3">
                            <span style="color:red;">* 담당자 승인 후 수정할 수 없습니다. 담당자에게 연락하세요.</span><br />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Business/m_BUS_Accident_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
                <%--<a href="/Business/m_REP_Accident_List.aspx"><span class="button gray mepm_btn">취소</span></a>--%>
            </div>
        </section>
    </article>
</asp:Content>
