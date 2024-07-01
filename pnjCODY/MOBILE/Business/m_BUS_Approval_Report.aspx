<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_BUS_Approval_Report.aspx.cs" Inherits="m_BUS_Approval_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        // 텍스트박스 기본값 지정 후 클릭시 사라지면서 스타일 지정하기
        function changeDate(obj, strValue) {
            date = document.getElementById('<%= this.hdDate.ClientID %>').value.substring(0, 4); //현재목록의 날짜를 4자리만 가져온다 ex. 2013

            if (obj.value == strValue) {
                obj.value = date;
                obj.className = 'i_f_on'
            }
            else if (obj.value == date) {
                obj.value = strValue;
                obj.className = 'i_f_out2'
            }
            else if (obj.value == "") {
                obj.value = strValue;
                obj.className = 'i_f_out2'
            }
            else {
                obj.className = 'i_f_out'
            }
        }

        function changeClear(obj, strValue) {
            if (obj.value == strValue) {
                obj.value = "";
                obj.className = 'i_f_on'
            }
            else if (obj.value == "") {
                obj.value = strValue;
                obj.className = 'i_f_out2'
            }
            else {
                obj.className = 'i_f_out'
            }
        }

        // 근무요청일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckRequestDate() {            
            if (document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').value == "YYYYMMDD 또는 YYYY-MM-DD") {
                alert("근무요청일을 입력해 주세요.");
                document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("근무요청일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("근무요청일이 유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("근무요청일의 입력 형식은 YYYYMMDD 또는 YYYY-MM-DD 입니다.");
                    document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtAPP_REQUEST_YYYYMMDD.ClientID %>').value = num;
            }
        }

        // 생년월일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckBirthDate() {
            if (document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').value == "YYYYMMDD 또는 YYYY-MM-DD") {
                alert("생년월일을 입력해 주세요.");
                document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("생년월일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("생년월일이 유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("생년월일의 입력 형식은 YYYYMMDD 또는 YYYY-MM-DD 입니다.");
                    document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtAPP_BIRTH_YYYYMMDD.ClientID %>').value = num;
            }
        }

        // 근무시작일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckStartDate() {
            if (document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').value == "YYYYMMDD 또는 YYYY-MM-DD") {
                alert("근무시작일을 입력해 주세요.");
                document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("근무시작일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("근무시작일이 유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("근무시작일의 입력 형식은 YYYYMMDD 또는 YYYY-MM-DD 입니다.");
                    document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtAPP_START_YYYYMMDD.ClientID %>').value = num;
            }
        }

        // 근무종료일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {
            if (document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').value == "YYYYMMDD 또는 YYYY-MM-DD") {
                alert("근무종료일을 입력해 주세요.");
                document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').focus();
                return false;
            }

            var num, year, month, day;
            num = document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("근무종료일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').focus();
                return false;
            }
            else {

                if (num != 0 && num.length == 8) {
                    year = num.substring(0, 4);
                    month = num.substring(4, 6);
                    day = num.substring(6);

                    if (isValidDay(year, month, day) == false) {
                        num = "";
                        alert("근무종료일이 유효하지 않은 일자 입니다.");
                        document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("근무종료일의 입력 형식은 YYYYMMDD 또는 YYYY-MM-DD 입니다.");
                    document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtAPP_FINISH_YYYYMMDD.ClientID %>').value = num;
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

            if (CheckRequestDate() == false) return false;

            if (regExp.test(document.getElementById('<%=this.txtAPP_NAME.ClientID %>').value)
                || document.getElementById('<%=this.txtAPP_NAME.ClientID %>').value == "") {
                alert("근무자이름을 입력하여 주세요.");
                document.getElementById('<%=this.txtAPP_NAME.ClientID %>').focus();
                return false;
            }

            if (CheckBirthDate() == false) return false;

            if (regExp.test(document.getElementById('<%=this.txtAPP_VENDER.ClientID %>').value)
                || document.getElementById('<%=this.txtAPP_VENDER.ClientID %>').value == "") {
                alert("지원사를 입력하여 주세요.");
                document.getElementById('<%=this.txtAPP_VENDER.ClientID %>').focus();
                return false;
            }

            if (regExp.test(document.getElementById('<%=this.txtAPP_VISIT_STORE.ClientID %>').value)
                || document.getElementById('<%=this.txtAPP_VISIT_STORE.ClientID %>').value == "") {
                alert("근무매장을 입력하여 주세요.");
                document.getElementById('<%=this.txtAPP_VISIT_STORE.ClientID %>').focus();
                return false;
            }

            if (CheckStartDate() == false) return false;

            if (CheckFinishDate() == false) return false;

            if (regExp.test(document.getElementById('<%=this.txtAPP_WORK_DAY.ClientID %>').value)
                || document.getElementById('<%=this.txtAPP_WORK_DAY.ClientID %>').value == "") {
                alert("월근무일수를 입력하여 주세요.");
                document.getElementById('<%=this.txtAPP_WORK_DAY.ClientID %>').focus();
                return false;
            }

            if (regExp.test(document.getElementById('<%=this.txtAPP_CAREER.ClientID %>').value)
                || document.getElementById('<%=this.txtAPP_CAREER.ClientID %>').value == ""
                || document.getElementById('<%=this.txtAPP_CAREER.ClientID %>').value == "예) 근무요청일 이전 코디서비스 근무 이력") {
                alert("기존근무이력을 입력하여 주세요.");
                document.getElementById('<%=this.txtAPP_CAREER.ClientID %>').focus();
                return false;
            }

            if (regExp.test(document.getElementById('<%=this.txtAPP_REASON.ClientID %>').value)
                || document.getElementById('<%=this.txtAPP_REASON.ClientID %>').value == "") {
                alert("채용사유를 입력하여 주세요.");
                document.getElementById('<%=this.txtAPP_REASON.ClientID %>').focus();
                return false;
            }
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

    </script>
    <input type="hidden" id="hdDate" name="hdDate" runat="server" />
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
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= Session["sRES_ID"] %>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 연락 > 고령자근무승인</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:95%;"><p >고령자근무승인 요청</p></td>
                    <td align="right" style="font-weight:normal; padding-right:0.5em;">
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item_bg" style="padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">근무요청일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtAPP_REQUEST_YYYYMMDD" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD 또는 YYYY-MM-DD" onfocus="changeDate(this,'YYYYMMDD 또는 YYYY-MM-DD')" onblur="changeDate(this,'YYYYMMDD 또는 YYYY-MM-DD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">근무자이름 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_NAME" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">생년월일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtAPP_BIRTH_YYYYMMDD" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD 또는 YYYY-MM-DD" onfocus="changeClear(this,'YYYYMMDD 또는 YYYY-MM-DD')" onblur="changeClear(this,'YYYYMMDD 또는 YYYY-MM-DD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">지원사 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_VENDER" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">근무매장 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_VISIT_STORE" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">근무시작일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtAPP_START_YYYYMMDD" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD 또는 YYYY-MM-DD" onfocus="changeDate(this,'YYYYMMDD 또는 YYYY-MM-DD')" onblur="changeDate(this,'YYYYMMDD 또는 YYYY-MM-DD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">근무종료일 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:TextBox ID="txtAPP_FINISH_YYYYMMDD" runat="server" MaxLength="10" style="width:100%;" value="YYYYMMDD 또는 YYYY-MM-DD" onfocus="changeDate(this,'YYYYMMDD 또는 YYYY-MM-DD')" onblur="changeDate(this,'YYYYMMDD 또는 YYYY-MM-DD')" class="i_f_out2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">월근무일수 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_WORK_DAY" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">기존근무이력 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_CAREER" runat="server" style="width:100%;" MaxLength="1000" onfocus="changeClear(this,'예) 근무요청일 이전 코디서비스 근무 이력')" onblur="changeClear(this,'예) 근무요청일 이전 코디서비스 근무 이력')" class="i_f_out2" TextMode="MultiLine" Rows="3">예) 근무요청일 이전 코디서비스 근무 이력</asp:TextBox>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">채용사유 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_REASON" runat="server" style="width:100%;" MaxLength="1000" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
                                 TextMode="MultiLine" Rows="3"/>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:110px; text-align:right; padding-right:.8em;">추가요청사항 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtAPP_REMARK" runat="server" style="width:100%;" MaxLength="1000" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" 
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
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;" runat="server" id="APP_SubScript" visible="false">
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
                <a href="/Business/m_BUS_Approval_List.aspx"><span class="button gray mepm_btn">취소</span></a>
                <%--<a href="/Business/m_REP_Approval_List.aspx"><span class="button gray mepm_btn">취소</span></a>--%>
            </div>
        </section>
    </article>
</asp:Content>
