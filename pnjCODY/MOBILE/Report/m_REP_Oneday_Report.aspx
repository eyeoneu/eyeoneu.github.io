<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_REP_Oneday_Report.aspx.cs" Inherits="MOBILE_Report_m_REP_Oneday_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />
                    SERVICE</span> </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>기타업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <script type="text/javascript">
        // 방문일(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckVisitDate() {
            var num, year, month, day;
            num = document.getElementById('<%= this.txtVISITDATE.ClientID %>').value;

            while (num.search("-") != -1) {
                num = num.replace("-", "");
            }

            if (isNaN(num)) {
                num = "";
                alert("방문일은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtVISITDATE.ClientID %>').focus();
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
                        document.getElementById('<%= this.txtVISITDATE.ClientID %>').focus();
                        return false;
                    }
                    num = year + "-" + month + "-" + day;
                }
                else {
                    num = "";
                    alert("방문일 입력 형식은 YYYYMMDD 입니다.");
                    document.getElementById('<%= this.txtVISITDATE.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%= this.txtVISITDATE.ClientID %>').value = num;
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
            if (CheckVisitDate() == false) return false;
        }


        // 추가 클릭
        function fncChkAdd() {
            if (document.getElementById('<%= this.ddlREP_DLY_Member.ClientID %>').value == "") {
                alert("근무자를 선택해 주세요.");
                document.getElementById('<%= this.ddlREP_DLY_Member.ClientID %>').focus();
                return false;
            }

            var e1 = document.getElementById('<%= this.ddlREP_DLY_Member.ClientID %>');

            var ddlREP_DLY_Member = document.getElementById('<%= this.ddlREP_DLY_Member.ClientID %>').value;
            var hdREP_DLY_Member_ID = document.getElementById('<%= this.hdREP_DLY_Member_ID.ClientID %>').value;
            var txtREP_DLY_Member = document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').value;

            //배열 생성
            if (hdREP_DLY_Member_ID == "") {
                hdREP_DLY_Member_ID = ddlREP_DLY_Member + ",";
                txtREP_DLY_Member = e1.options[e1.selectedIndex].text + ",";
            } else {
                hdREP_DLY_Member_ID = hdREP_DLY_Member_ID + ddlREP_DLY_Member + ",";
                txtREP_DLY_Member = txtREP_DLY_Member + e1.options[e1.selectedIndex].text + ",";
            }


            //배열 중복 제거
            var arrItem = new Array();
            var arrText = new Array();

            var i = 0;
            var s = "";
            if (hdREP_DLY_Member_ID != "") {
                arrItem = hdREP_DLY_Member_ID.split(",");

                //중복은 배열에서 제거
                arrItem = fextractDupArr(arrItem);

                if (arrItem.length > 0) {
                    for (i = 0; i < arrItem.length - 1; i++) {
                        s = s + arrItem[i] + ",";
                    }

                    hdREP_DLY_Member_ID = s;
                } else {
                    hdREP_DLY_Member_ID = "";
                }
            }

            i = 0;
            s = "";
            if (txtREP_DLY_Member != "") {
                arrText = txtREP_DLY_Member.split(",");

                //중복은 배열에서 제거
                arrText = fextractDupArr(arrText);

                if (arrText.length > 0) {
                    for (i = 0; i < arrText.length - 1; i++) {
                        s = s + arrText[i] + ",";
                    }

                    txtREP_DLY_Member = s;
                } else {
                    txtREP_DLY_Member = "";
                }
            }

            document.getElementById('<%= this.hdREP_DLY_Member_ID.ClientID %>').value = hdREP_DLY_Member_ID;
            document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').value = txtREP_DLY_Member;
            //alert(hdREP_DLY_Member_ID);
            return true;
        }


        // 제거 클릭
        function fncChkDel() {
            var hdREP_DLY_Member_ID = document.getElementById('<%= this.hdREP_DLY_Member_ID.ClientID %>').value;
            var txtREP_DLY_Member = document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').value;

            var arrItem = new Array();
            var arrText = new Array();
            var remItem = new Array();

            var i = 0;
            var s = "";
            if (hdREP_DLY_Member_ID != "") {
                arrItem = hdREP_DLY_Member_ID.split(",");

                //마지막 배열에서 제거 
                remItem = arrItem.splice(arrItem.length - 1, 1);

                if (arrItem.length > 0) {
                    for (i = 0; i < arrItem.length - 1; i++) {
                        s = s + arrItem[i] + ",";
                    }

                    hdREP_DLY_Member_ID = s;
                } else {
                    hdREP_DLY_Member_ID = "";
                }
            }

            i = 0;
            s = "";
            if (txtREP_DLY_Member != "") {
                arrText = txtREP_DLY_Member.split(",");

                //마지막 배열에서 제거 
                remItem = arrText.splice(arrText.length - 1, 1);

                if (arrText.length > 0) {
                    for (i = 0; i < arrText.length - 1; i++) {
                        s = s + arrText[i] + ",";
                    }

                    txtREP_DLY_Member = s;
                } else {
                    txtREP_DLY_Member = "";
                }
            }

            document.getElementById('<%= this.hdREP_DLY_Member_ID.ClientID %>').value = hdREP_DLY_Member_ID;
            document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').value = txtREP_DLY_Member;

            return true;
        }
        //배열 중복제거 함수
        function fextractDupArr(arr) {

            for (var i = 0; i < arr.length; i++) {

                var checkDobl = 0;

                for (var j = 0; j < arr.length; j++) {
                    if (arr[i] != arr[j]) {
                        continue;
                    } else {
                        checkDobl++;
                        if (checkDobl > 1) {
                            spliced = arr.splice(j, 1);
                        }
                    }
                }
            }
            return arr;
        }

        // 입력 저장시 필수 확인
        function fncChkSave() {
            if (document.getElementById('<%= this.txtVISITDATE.ClientID %>').value.length < 1) {
                alert("방문일을 입력해 주세요.");
                document.getElementById('<%= this.txtVISITDATE.ClientID %>').focus();
                return false;
            }

            if (CheckVisitDate() == false) return false;

            var CUSTOMER_STORE = document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').options[document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').selectedIndex].text;

            if (CUSTOMER_STORE == "공통업무 회의/교육" || CUSTOMER_STORE == "내근업무 전산/마감" || CUSTOMER_STORE == "영업소 식품" || CUSTOMER_STORE == "유통팀 제과") {
                if (document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').value == "") {
                    alert("매장을 선택해주세요.");
                    document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').focus();
                    return false;
                }
            }
            else {
                if (document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').value != "") {
                    if (document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').value.length < 1) {
                        alert("근무자를 추가해 주세요.");
                        document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').focus();
                        return false;
                    }
                }
            }

            if (document.getElementById('<%= this.txtContents.ClientID %>').value.length < 1) {
                alert("내용을 입력해 주세요.");
                document.getElementById('<%= this.txtContents.ClientID %>').focus();
                return false;
            }

            return true;
        }

        // 수정 저장시 필수 확인
        function fncChkUpdate() {
            if (document.getElementById('<%= this.txtVISITDATE.ClientID %>').value.length < 1) {
                alert("방문일을 입력해 주세요.");
                document.getElementById('<%= this.txtVISITDATE.ClientID %>').focus();
                return false;
            }

            if (CheckVisitDate() == false) return false;

            var CUSTOMER_STORE = document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').options[document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').selectedIndex].text;

            if (CUSTOMER_STORE == "공통업무 회의/교육" || CUSTOMER_STORE == "내근업무 전산/마감" || CUSTOMER_STORE == "영업소 식품" || CUSTOMER_STORE == "유통팀 제과")
            {
                if (document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').value == "") {
                    alert("매장을 선택해주세요.");
                    document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').focus();
                    return false;
                }
            }
            else {
                if (document.getElementById('<%= this.ddlEPM_CUSTOMER_STORE.ClientID %>').value != "") {
                    if (document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').value.length < 1) {
                        alert("근무자를 추가해 주세요.");
                        document.getElementById('<%= this.txtREP_DLY_Member.ClientID %>').focus();
                        return false;
                    }
                }
            }

            if (document.getElementById('<%= this.txtContents.ClientID %>').value.length < 1) {
                alert("내용을 입력해 주세요.");
                document.getElementById('<%= this.txtContents.ClientID %>').focus();
                return false;
            }

            return true;
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
            file.setAttribute("class", "i_f_out");
            file.setAttribute("onchange", "onFileChange(this, " + divIdx + ");");
            fileDiv.appendChild(file);
            document.getElementById("moreUploads").appendChild(fileDiv);

            divIdx++;

        }

        //-- 첨부파일 끝    
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_NAME" name="hdRES_NAME" runat="server" />    
    <input type="hidden" id="hdGB" name="hdGB" runat="server" />
    <input type="hidden" id="hdREP_DLY_ID" name="hdREP_DLY_ID" runat="server" />  
    <input type="hidden" id="hdREP_DLY_Member_ID" name="hdREP_DLY_Member_ID" runat="server" />    
    <div class="title">
        <h2 class="mepm_title">기타 업무 관리 > 일일업무보고</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>일일업무보고</p>
                    </td>
                    <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                             <%--<asp:Button CssClass="button gray mepm_btn_4em" Text="삭제" ID="btnDelete" runat="server" Visible="false" OnClientClick="return confirm('삭제 하시겠습니까?');" OnClick="btnDelete_Click" />--%>
                        </td>
                </tr>
            </table>
            <div class="mepm_menu_item" style="padding: 0;">
               <table style="table-layout: fixed; word-break: break-all;">                    
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width:75px; text-align: left; border-top:1px solid #ccc; padding-left: .5em;"> 거래처 :</td>
                        <td style="width: auto; border-top:1px solid #ccc; padding-right: .5em;">                                 
                            <asp:DropDownList ID="ddlEPM_CUSTOMER_STORE" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD" OnSelectedIndexChanged="ddlEPM_CUSTOMER_STORE_SelectedIndexChanged" Enabled="true">
                            </asp:DropDownList>   
                        </td>
                    </tr>                
                    <%--<tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width:75px; text-align: left; border-top:1px solid #ccc; padding-left: .5em;"> 매장 :</td>
                        <td style="width: auto; border-top:1px solid #ccc; padding-right: .5em;">                   
                            <asp:DropDownList ID="ddlEPM_CUSTOMER_STORE" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD" OnSelectedIndexChanged="ddlEPM_CUSTOMER_STORE_SelectedIndexChanged" Enabled="true">
                            </asp:DropDownList>   
                        </td>
                    </tr>--%>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding: 0;">
                <table style="table-layout: fixed; word-break: break-all;">                    
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width:75px; text-align: left; border-top:1px solid #ccc; padding-left: .5em;">
                            방문일 :
                        </td>
                        <td style="width: auto; border-top:1px solid #ccc;">
                            <asp:TextBox ID="txtVISITDATE" width="90px" runat="server" value="" class="i_f_out"></asp:TextBox>                              
                        </td>
                        <td style="width: 70px; text-align:center; border-top:1px solid #ccc;">
                        </td>
                    </tr>
                    <tr class="" style="height: 3em;">
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em; vertical-align:top; padding-top:.8em;">
                            근무자선택 :
                        </td>
                        <td style="width: auto; border-top:1px solid #ccc;">
                            <asp:DropDownList ID="ddlREP_DLY_Member" runat="server" DataTextField="RES_INFO" 
                                DataValueField="RES_ID" CssClass="i_f_out" Width="100%">
                            </asp:DropDownList>    
                            
                        </td>
                        <td style="width: 70px; text-align:center; border-top:1px solid #ccc;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="추가" ID="btnAdd" 
                                runat="server" OnClientClick="javascript:return fncChkAdd();" UseSubmitBehavior="False" />                            
                        </td>   
                    </tr>
                    <tr class="" style="height: 3em;">
                        <td style="text-align: left; padding-left: .5em;" colspan="2">
                            <asp:TextBox ID="txtREP_DLY_Member" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out"  />
                        </td>
                        <td style="text-align:center;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="제거" ID="btnDel" 
                                runat="server" OnClientClick="javascript:return fncChkDel();" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 12.5em;">
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em; vertical-align:top; padding-top:.8em;">
                            보고내용 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .7em;" colspan="2">
                            <asp:TextBox ID="txtContents" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" TextMode="MultiLine" Rows="10" />
                        </td>
                    </tr>
                     <!-- 첨부 파일 부분 시작 -->
                    <tr class="mepm_menu_item_bg" style="height: 3em;"> 
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em; vertical-align:top; padding-top:.8em;" rowspan="2">
                            첨부 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .5em;" colspan="2">
                            <font color="red">* i-Phone 에서는 첨부 할 수 없습니다.</font><br />
                            <asp:Label ID="lbAttFile" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 6em;"> 
                        <td style="text-align: left; padding-right: .5em;" colspan="2">
                            <div id="moreUploads">
                                <div id="fileDiv1" >
                                    <asp:FileUpload ID="upFile" CssClass="i_f_out" name="attachment[]" runat="server" onchange="onFileChange(this,1);" Width="90%" />
                                </div>
                            </div>
                            <%--<asp:FileUpload CssClass="i_f_out" ID="upFile" runat="server" Width="90%"  BackColor="white" onchange="this.className='i_f_on';" />--%>
                        </td>
                    </tr>
                    <!-- 첨부 파일 부분 종료 -->             
                 </table> 
              </div>
        </section>
        <div class="mepm_btn_div">
            <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave"
                runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
            <asp:Button CssClass="button gray_disabled mepm_asp_btn" Text="저장" ID="btnSaveFake" runat="server" Visible="false" Enabled="false" />
            <asp:Button CssClass="button gray mepm_asp_btn" Text="취소" ID="btnCancel"
                runat="server" OnClientClick="javascript:return window.location='m_REP_Oneday_List.aspx';" UseSubmitBehavior="False" />
        </div>
    </article>
</asp:Content>
