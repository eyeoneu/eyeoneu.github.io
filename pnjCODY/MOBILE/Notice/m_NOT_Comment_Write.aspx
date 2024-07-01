<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_NOT_Comment_Write.aspx.cs" Inherits="MOBILE_Notice_m_NOT_Comment_Write" %>

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
        //-- 중복저장 방지 변수
        var sendFlag = true;
        // 입력 저장시 필수 확인
        function fncChkSave() {  
            if (document.getElementById('<%= this.txtETC_NOT_Comment.ClientID %>').value.length < 1) {
                alert("내용을 입력해 주세요.");
                document.getElementById('<%= this.txtETC_NOT_Comment.ClientID %>').focus();
                return false;
            }

            if (sendFlag == true) {
                //go_Action() //액션 펑션으로 가고..변수에 false값을 넣습니다.
                sendFlag = false;
            } else { //다음에 버튼을 클릭하면.. 이리로 옵니다. 버튼을 두번 눌렀을때 생기는 문제 방지
                alert("저장중 입니다");
                sendFlag = false;
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
            file.setAttribute("onfocus", "this.className = 'i_f_on';");
            file.setAttribute("onblur", "this.className = 'i_f_out';");
            file.setAttribute("class", "i_f_out");
            file.setAttribute("onchange", "onFileChange(this, " + divIdx + ");");
            fileDiv.appendChild(file);
            document.getElementById("moreUploads").appendChild(fileDiv);

            divIdx++;

        }
        //-- 첨부파일 끝    

        //-- 덧글 삭제
        function fncCommentDelete(CommentID) {
            if (confirm("삭제하시겠습니까?")) {
                document.getElementById('<%=this.hdnCommentDeleteID.ClientID %>').value = CommentID;
                <%= Page.GetPostBackEventReference(this.btnCommentDelete) %>;
                return true;
            }
            else
                return false;
        }

        // 메뉴 토글
        function toggle_display(obj) {
            var div1 = document.getElementById(obj);
            if (div1.style.display == 'table') {
                div1.style.display = 'none';
            } else {
                div1.style.display = 'table';
            }
        }

        // Value 토글
        function toggle_value(obj, pvalue, nvalue) {
            var input1 = document.getElementById(obj);
            if (input1.value == pvalue) {
                input1.value = nvalue;
            } else {
                input1.value = pvalue;
            }
        }

        // 공지사항 본문 페이지 이동
        function fncComment() {
            //alert(document.getElementById('<%= this.hdETC_NOT_ID.ClientID %>').value);
            location.replace("m_NOT_Write.aspx?ETC_NOT_ID=" + document.getElementById('<%= this.hdETC_NOT_ID.ClientID %>').value);
        }
    </script>
    <style type="text/css">        
        dl, dt, dd, ul, ol, li {
            margin: 0;
            padding: 0;
        }
        ul {
            float: left;
            margin: 0;
            padding: 0;
        }
        li {
            margin-left: 0px;
            float: left;
        }
        ul .btnList {
            padding-left: 1.3em;
        }
        li .fileList {
            table-layout: fixed; word-break: break-all;
            line-height: 2em;
            border: 1px solid #ffd800;
            padding: 2px 4px 2px 4px;
            background-color: #fff5a9;
            font-size:.9em;
            position: absolute;
            width: 215px;
        }
    </style>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_NAME" name="hdRES_NAME" runat="server" />    
    <input type="hidden" id="hdGB" name="hdGB" runat="server" />
    <input type="hidden" id="hdETC_NOT_ID" name="hdETC_NOT_ID" runat="server" />  
    <input type="hidden" id="hdETC_NOT_Member" name="hdETC_NOT_Member" runat="server" />    
    <input type="hidden" id="hdnAttDelete" name="hdnAttDelete" runat="server" />
    <input type="hidden" id="hdnCommentDeleteID" name="hdnCommentDeleteID" runat="server" />
    <input type="hidden" id="hdnAttDeleteComment" name="hdnAttDeleteComment" runat="server" />
    <asp:LinkButton ID="btnCommentDelete" runat="server" OnClick="btnCommentDelete_Click"></asp:LinkButton>
    <div class="title">
        <h2 class="mepm_title">
            공지 사항 > 덧글</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>
                           <asp:Label ID="lblNoticeTitle" runat="server"></asp:Label>
                        </p>
                    </td>
                </tr>
            </table>
              <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvDetail" >
                  <table style="table-layout: fixed; word-break: break-all;">
                      <tr class="mepm_menu_item_bg" style="height: 3em;">
                          <td style="text-align: center; border-bottom: 1px solid #ccc;">
                              <asp:Label class="button gray mepm_asp_btn" Text="본문 보기" ID="btnComment" runat="server" OnClick="fncComment();" Width="90%" />
                          </td>
                      </tr>
                      <tr class="mepm_menu_item_bg" style="height: 3em;">
                          <td style="text-align: left; padding-left: .5em; border-bottom: 1px solid #ccc; font-weight:bold;">덧글 작성
                          </td>
                      </tr>
                    <tr class="mepm_menu_item_bg" style="height: 2em;">
                        <td style="width:80px; text-align: left; padding-left: .5em; border-bottom:0;">
                          <asp:Label ID="lblRES_Name" runat="server" Text=""></asp:Label><br />
                        </td>
                    </tr>
                     <tr class="mepm_menu_item_bg" style="height: 6em;">
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-left: .5em; padding-right: .5em; vertical-align: top; border-top: 0;">
                             <asp:TextBox ID="txtETC_NOT_Comment" runat="server" Style="width: 100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" TextMode="MultiLine" Rows="4" />
                         </td>
                     </tr>
                     <!-- 첨부 파일 부분 시작 -->
                     <tr class="mepm_menu_item_bg" style="height: 5.5em;">
                         <td style="text-align: left; border-top: 1px solid #ccc; padding-left: .5em; padding-right: .5em;">
                             <div style="height: 1em; padding: 0; vertical-align: bottom;">
                                 첨부
                                 <span style="color: red;">* iPhone 에서는 첨부 할 수 없습니다.</span>
                             </div>
                             <br />
                             <div id="moreUploads">
                                 <div id="fileDiv1">
                                     <asp:FileUpload ID="upFile" CssClass="i_f_out" name="attachment[]" runat="server" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" onchange="onFileChange(this,1);" Width="90%" />
                                 </div>
                             </div>
                         </td>
                     </tr>                   
                     <!-- 첨부 파일 부분 종료 -->
                 </table>
              </div>
        </section>
        <div class="mepm_btn_div">
            <asp:Button CssClass="button gray mepm_asp_btn" Text="덧글 작성" ID="btnSave" ForeColor="#000066" Font-Bold="true"
                runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnCommentSave_Click" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="취소" ID="btnCancel" 
                    runat="server" OnClientClick="javascript:return fncComment();" UseSubmitBehavior="False" />
        </div>
        <section>
            <div class="mepm_menu_item" style="padding: 0;" runat="server" id="dvComment">
                <table style="table-layout: fixed; word-break: break-all;">
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="text-align: left; padding-left: .5em; border-top: 1px solid #ccc;">덧글 목록
                            <asp:Label ID="lblCommentCnt" runat="server" ForeColor="#00cc00"></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 6em;">
                        <td>
                            <asp:GridView ID="gvNoticeList" runat="server" CellPadding="0" Width="100%" EmptyDataText="작성된 덧글이 없습니다."
                                OnRowDataBound="gvNoticeList_RowDataBound"
                                CssClass="table_border" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="작성자" DataField="RES_Name" SortExpression="RES_Name">
                                        <HeaderStyle CssClass="tr_border" Width="100px" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Height="90px" CssClass="tr_border" Wrap="False" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="내용" DataField="ETC_NOT_COMMENT_CONTENT" SortExpression="ETC_NOT_COMMENT_CONTENT">
                                        <HeaderStyle CssClass="tr_border" Width="" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Height="90px" CssClass="tr_border_left" Wrap="true" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" />
                                <RowStyle CssClass="mepm_menu_item_bg" />
                                <HeaderStyle CssClass="mepm_menu_title_bg" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                          <td style="text-align: center; border-top: 1px solid #ccc;">
                              <asp:Label class="button gray mepm_asp_btn" Text="본문 보기" ID="Label1" runat="server" OnClick="fncComment();" Width="90%" />
                          </td>
                      </tr>
                </table>
            </div>
        </section>
    </article>
</asp:Content>
