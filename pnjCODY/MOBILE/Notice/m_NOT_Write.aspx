﻿<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_NOT_Write.aspx.cs" Inherits="MOBILE_Notice_m_NOT_Write" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />
                    SERVICE</span> </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <script type="text/javascript">
        
        // 추가 클릭
        function fncChkAdd() {

            if (document.getElementById('<%= this.ddlRES_RBS_Lv1.ClientID %>').value == "") {
                alert("대상 서포터 지역을 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_RBS_Lv1.ClientID %>').focus();
                return false;
            }

            var e1 = document.getElementById('<%= this.ddlRES_RBS_Lv1.ClientID %>');
            var e2 = document.getElementById('<%= this.ddlRES_RBS_Lv2.ClientID %>');

            var ddlRES_RBS_Lv1 = document.getElementById('<%= this.ddlRES_RBS_Lv1.ClientID %>').value;
            var ddlRES_RBS_Lv2 = document.getElementById('<%= this.ddlRES_RBS_Lv2.ClientID %>').value;
            var hdETC_NOT_Member = document.getElementById('<%= this.hdETC_NOT_Member.ClientID %>').value;
            var txtETC_NOT_Member = document.getElementById('<%= this.txtETC_NOT_Member.ClientID %>').value;

            //배열 생성
            if (hdETC_NOT_Member == "") {
                hdETC_NOT_Member = ddlRES_RBS_Lv1 + "|" + ddlRES_RBS_Lv2 + ",";
                if (ddlRES_RBS_Lv2 == "A")
                    txtETC_NOT_Member = e1.options[e1.selectedIndex].text + ",";
                else
                    txtETC_NOT_Member = e2.options[e2.selectedIndex].text + ",";
            } else {
                hdETC_NOT_Member = hdETC_NOT_Member + ddlRES_RBS_Lv1 + "|" + ddlRES_RBS_Lv2 + ",";
                if (ddlRES_RBS_Lv2 == "A")
                    txtETC_NOT_Member = txtETC_NOT_Member + e1.options[e1.selectedIndex].text + ",";
                else
                    txtETC_NOT_Member = txtETC_NOT_Member + e2.options[e2.selectedIndex].text + ",";
            }

            
            //배열 중복 제거
            var arrItem = new Array();
            var arrText = new Array();

            var i = 0;
            var s = "";
            if (hdETC_NOT_Member != "") {
                arrItem = hdETC_NOT_Member.split(",");

                //중복은 배열에서 제거
                arrItem = fextractDupArr(arrItem);

                if (arrItem.length > 0) {
                    for (i = 0; i < arrItem.length - 1; i++) {
                        s = s + arrItem[i] + ",";
                    }

                    hdETC_NOT_Member = s;
                } else {
                    hdETC_NOT_Member = "";
                }
            }

            i = 0;
            s = "";
            if (txtETC_NOT_Member != "") {
                arrText = txtETC_NOT_Member.split(",");

                //중복은 배열에서 제거
                arrText = fextractDupArr(arrText);

                if (arrText.length > 0) {
                    for (i = 0; i < arrText.length - 1; i++) {
                        s = s + arrText[i] + ",";
                    }

                    txtETC_NOT_Member = s;
                } else {
                    txtETC_NOT_Member = "";
                }
            }

            document.getElementById('<%= this.hdETC_NOT_Member.ClientID %>').value = hdETC_NOT_Member;
            document.getElementById('<%= this.txtETC_NOT_Member.ClientID %>').value = txtETC_NOT_Member;
            //alert(hdETC_NOT_Member);
            return true;
        }


        // 제거 클릭
        function fncChkDel() {
            var hdETC_NOT_Member = document.getElementById('<%= this.hdETC_NOT_Member.ClientID %>').value;
            var txtETC_NOT_Member = document.getElementById('<%= this.txtETC_NOT_Member.ClientID %>').value;

            var arrItem = new Array();
            var arrText = new Array();
            var remItem = new Array();

            var i = 0;
            var s = "";
            if (hdETC_NOT_Member != "") {
                arrItem = hdETC_NOT_Member.split(",");

                //마지막 배열에서 제거 
                remItem = arrItem.splice(arrItem.length - 1, 1);

                if (arrItem.length > 0) {
                    for (i = 0; i < arrItem.length - 1; i++) {
                        s = s + arrItem[i] + ",";
                    }

                    hdETC_NOT_Member = s;
                } else {
                    hdETC_NOT_Member = "";
                }
            }

            i = 0;
            s = "";
            if (txtETC_NOT_Member != "") {
                arrText = txtETC_NOT_Member.split(",");

                //마지막 배열에서 제거 
                remItem = arrText.splice(arrText.length - 1, 1);

                if (arrText.length > 0) {
                    for (i = 0; i < arrText.length - 1; i++) {
                        s = s + arrText[i] + ",";
                    }

                    txtETC_NOT_Member = s;
                } else {
                    txtETC_NOT_Member = "";
                }
            }

            document.getElementById('<%= this.hdETC_NOT_Member.ClientID %>').value = hdETC_NOT_Member;
            document.getElementById('<%= this.txtETC_NOT_Member.ClientID %>').value = txtETC_NOT_Member;

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

            //2016-10-06 김기훈 추가. 카테고리 선택
            if (document.getElementById('<%=this.ddl_Category.ClientID %>').value == "-선택-") {
                alert("카테고리를 선택해 주세요.");
                document.getElementById('<%=this.ddl_Category.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.txtETC_NOT_Member.ClientID %>').value.length < 1) {
                alert("대상을 입력해 주세요.");
                document.getElementById('<%= this.txtETC_NOT_Member.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%= this.txtETC_NOT_Title.ClientID %>').value.length < 1) {
                alert("제목을 입력해 주세요.");
                document.getElementById('<%= this.txtETC_NOT_Title.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%= this.txtETC_NOT_Contents.ClientID %>').value.length < 1) {
                alert("내용을 입력해 주세요.");
                document.getElementById('<%= this.txtETC_NOT_Contents.ClientID %>').focus();
                return false;
            }
            if (CheckFinishDate() == false) return false;
            return true;
        }

        // 수정 저장시 필수 확인
        function fncChkUpdate() {            
            if (document.getElementById('<%= this.txtETC_NOT_Title.ClientID %>').value.length < 1) {
                alert("제목을 입력해 주세요.");
                document.getElementById('<%= this.txtETC_NOT_Title.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%= this.txtETC_NOT_Contents.ClientID %>').value.length < 1) {
                alert("내용을 입력해 주세요.");
                document.getElementById('<%= this.txtETC_NOT_Contents.ClientID %>').focus();
                return false;
            }
            if (CheckFinishDate() == false) return false;
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
            file.setAttribute("onfocus", "this.className = 'i_f_on';");
            file.setAttribute("onblur", "this.className = 'i_f_out';");
            file.setAttribute("class", "i_f_out");
            file.setAttribute("onchange", "onFileChange(this, " + divIdx + ");");
            fileDiv.appendChild(file);
            document.getElementById("moreUploads").appendChild(fileDiv);

            divIdx++;

        } 

        //-- 첨부파일 끝    

        // 댓글 페이지 이동
        function fncComment() {
            //alert(document.getElementById('<%= this.hdETC_NOT_ID.ClientID %>').value);
            location.replace("m_NOT_Comment_Write.aspx?ETC_NOT_ID=" + document.getElementById('<%= this.hdETC_NOT_ID.ClientID %>').value);
        }


        // 신정기간 종료(YYYYMMDD)의 유효성을 체크하고 표준 날자 포맷(YYYY-MM-DD)으로 변환하여 리턴
        function CheckFinishDate() {
            if (document.getElementById('<%= this.txtDeadlineDate.ClientID %>').value == "YYYYMMDD") {
                alert("기한일자을 입력해 주세요.");
                document.getElementById('<%= this.txtDeadlineDate.ClientID %>').focus();
                    return false;
                }

                var num, year, month, day;
                num = document.getElementById('<%= this.txtDeadlineDate.ClientID %>').value;

                while (num.search("-") != -1) {
                    num = num.replace("-", "");
                }

                if (isNaN(num)) {
                    num = "";
                    alert("기한일자은 숫자만 입력 가능합니다.");
                    document.getElementById('<%= this.txtDeadlineDate.ClientID %>').focus();
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
                            document.getElementById('<%= this.txtDeadlineDate.ClientID %>').focus();
                            return false;
                        }
                        num = year + "-" + month + "-" + day;
                    }
                    else {
                        num = "";
                        alert("기한일자 입력 형식은 YYYYMMDD 입니다.");
                        document.getElementById('<%= this.txtDeadlineDate.ClientID %>').focus();
                        return false;
                    }

                    document.getElementById('<%= this.txtDeadlineDate.ClientID %>').value = num;
                }
            }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_NAME" name="hdRES_NAME" runat="server" />    
    <input type="hidden" id="hdGB" name="hdGB" runat="server" />
    <input type="hidden" id="hdETC_NOT_ID" name="hdETC_NOT_ID" runat="server" />  
    <input type="hidden" id="hdETC_NOT_Member" name="hdETC_NOT_Member" runat="server" />    
    <input type="hidden" id="hdnAttDelete" name="hdnAttDelete" runat="server" />
    <div class="title">
        <h2 class="mepm_title">
            공지 사항</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>
                            공지 사항</p>
                    </td>
                </tr>
            </table>
            <div class="mepm_menu_item" style="padding: 0;" runat="server" id="dvResSearch">
                <table style="table-layout: fixed; word-break: break-all;">
                    <tr class="mepm_menu_item_bg" style="height: 6em;">
                        <td style="width: 80px; text-align: left; border-top: 1px solid #ccc; padding-left: .5em; vertical-align: top; padding-top: .8em;">서포터 지역 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; vertical-align: middle" colspan="2">
                            <asp:DropDownList ID="ddlRES_RBS_Lv1" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="SECT_NM"
                                DataValueField="SECT_CD" OnSelectedIndexChanged="ddlRES_RBS_Lv1_SelectedIndexChanged" Width="45%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlRES_RBS_Lv2" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="DEPT_NM" DataValueField="DEPT_CD" Width="45%">
                                <asp:ListItem Text="-선택-" Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <div style="height: 1em;"></div>
                            <asp:TextBox ID="txtETC_NOT_Member" runat="server" Style="width: 100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="width: 70px; text-align: center; border-top: 1px solid #ccc; vertical-align: top; padding-top: .5em;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="추가" ID="btnAdd" runat="server" OnClientClick="javascript:return fncChkAdd();" UseSubmitBehavior="False" />
                            <div style="height: .5em;"></div>
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="제거" ID="btnDel" runat="server" OnClientClick="javascript:return fncChkDel();" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                </table>
            </div>
              <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvDetail" >
                 <table style="table-layout: fixed; word-break: break-all;">
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width: 80px; text-align: left; padding-left: .5em;">
                            작성자 :
                        </td>
                        <td style="width: auto; text-align: left; padding-right: .5em;">
                            <asp:Label ID="lblRES_Name" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 80px; text-align: left; border-left:1px solid #ccc; padding-left: .5em;">
                            공지일 :
                        </td>
                        <td style="width: auto; text-align: left; padding-right: .5em;">
                            <asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em;">
                            카테고리 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .5em;" colspan="3">
                            <asp:Label ID="lb_Category" runat="server" style="width:100%;" />
                            <asp:DropDownList ID="ddl_Category" runat="server" CssClass="textbox1" ToolTip="카테고리" Width="45%" ></asp:DropDownList> <%--2016-10-05 김기훈 추가--%>
                        </td>
                    </tr>                 
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em;">
                            제목 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .5em;" colspan="3">
                            <asp:Label ID="lblETC_NOT_Title" runat="server" style="width:100%;" />
                            <asp:TextBox ID="txtETC_NOT_Title" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>                     
                    <tr class="mepm_menu_item_bg" style="height: 12.5em;">
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em; vertical-align:top; padding-top:.8em;">
                            내용 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .5em; vertical-align:top; padding-top: .5em; padding-bottom: .5em;" colspan="3">
                            <asp:Label ID="lblETC_NOT_Contents" runat="server" style="width:100%;" />
                            <asp:TextBox ID="txtETC_NOT_Contents" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" TextMode="MultiLine" Rows="10" />
                        </td>
                    </tr>
                      <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="text-align: left; border-top:1px solid #ccc; padding-left: .5em;">
                            기한일 :
                        </td>
                        <td style="text-align: left; border-top:1px solid #ccc; padding-right: .5em;" colspan="3">
                            <asp:Label ID="lblDeadlineDate" runat="server" style="width:100%;" />
                            <asp:TextBox ID="txtDeadlineDate" width="105px" runat="server" value="YYYYMMDD" onfocus="change(this,'YYYYMMDD')" onblur="change(this,'YYYYMMDD'); fncDuration();" class="i_f_out2"></asp:TextBox>
                        </td>
                    </tr>
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
                     <tr id="trComment" class="mepm_menu_item_bg" style="height: 3em;" runat="server"> 
                        <td style="text-align: center; border-top:1px solid #ccc;" colspan="4">
                            <asp:Label class="button gray mepm_asp_btn" ID="btnComment" runat="server" OnClick="fncComment();" Width="280px" />
                        </td>
                    </tr>
                    <!-- 첨부 파일 부분 종료 -->
                </table>
            </div>
        </section>
        <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="편집" ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                <asp:Button CssClass="button gray_disabled mepm_asp_btn" Text="편집" ID="btnEditFake" runat="server" Visible="false" Enabled="false" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnEditComplate" runat="server" OnClientClick="javascript:return fncChkUpdate();" OnClick="btnSave_Click" Visible="false" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="취소" ID="btnCancel" 
                    runat="server" OnClientClick="javascript:return window.location='/m_Default.aspx';" UseSubmitBehavior="False" />
        </div> 
    </article>
</asp:Content>