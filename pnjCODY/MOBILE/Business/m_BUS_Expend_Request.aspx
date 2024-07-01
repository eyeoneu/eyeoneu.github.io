<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_BUS_Expend_Request.aspx.cs" Inherits="MOBILE_Business_M_BUS_Expend_Request" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

 <!--   <asp:LinkButton ID="btnSelectRes" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>-->
    
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span> </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>업무 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <script type="text/javascript">
        //--- 필수 확인
        function fncChkSearch() {
            if (document.getElementById('<%= this.txtRES_Name.ClientID %>').value.length < 2) {
                alert("정확한 이름을 입력해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }
            return true;
        }

        // 지출 신청서 대상 선택 시
        function fncSelectRes(strRES_ID, strGB, strASSCON_ID) {
            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  strRES_ID;
            document.getElementById('<%= this.hdGB.ClientID %>').value =  strGB;
            document.getElementById('<%= this.hdASSCON_ID.ClientID %>').value =  strASSCON_ID;

            <%= Page.GetPostBackEventReference(this.btnSelectRes2) %>    
        } 
        
        // 추가 클릭
        function fncChkAdd() {
            if (document.getElementById('<%= this.hdRES_ID.ClientID %>').value == "") {
                alert("신청사원을 선택해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%= this.txtEXP_TAR_Amount.ClientID %>').value.length < 1) {
                alert("정확한 승인금액을 입력해 주세요.");
                document.getElementById('<%= this.txtEXP_TAR_Amount.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%= this.txtEXP_TAR_Tax.ClientID %>').value.length < 1) {
                alert("정확한 부가세를 입력해 주세요.");
                document.getElementById('<%= this.txtEXP_TAR_Tax.ClientID %>').focus();
                return false;
            }

            var hdRES_ID = document.getElementById('<%= this.hdRES_ID.ClientID %>').value;
            var hdRES_NAME = document.getElementById('<%= this.hdRES_NAME.ClientID %>').value;
            var hdGB = document.getElementById('<%= this.hdGB.ClientID %>').value;
            var hdASSCON_ID = document.getElementById('<%= this.hdASSCON_ID.ClientID %>').value;
            var txtEXP_TAR_Amount = document.getElementById('<%= this.txtEXP_TAR_Amount.ClientID %>').value;
            var txtEXP_TAR_Tax = document.getElementById('<%= this.txtEXP_TAR_Tax.ClientID %>').value;
            var txtEXP_TAR_Memo = document.getElementById('<%= this.txtEXP_TAR_Memo.ClientID %>').value;            
            var hdReq_List = document.getElementById('<%= this.hdREQ_LIST.ClientID %>').value;

            if (hdReq_List == "")
            {
                hdReq_List =  hdRES_NAME + "|" + txtEXP_TAR_Amount + "|" + txtEXP_TAR_Tax + "|" + txtEXP_TAR_Memo + "|" + hdRES_ID + "|" + hdGB + "|" + hdASSCON_ID +";";
            }else
            {
                hdReq_List =  hdReq_List + hdRES_NAME + "|" + txtEXP_TAR_Amount + "|" + txtEXP_TAR_Tax + "|" + txtEXP_TAR_Memo + "|" + hdRES_ID + "|" + hdGB + "|" + hdASSCON_ID +";";
            }

            document.getElementById('<%= this.hdREQ_LIST.ClientID %>').value = hdReq_List;
            //alert(hdReq_List);
            return true;
        }

        // 대상목록 아이템 제거
        function fncDelitem(delidx) {            
            var hdReq_List = document.getElementById('<%= this.hdREQ_LIST.ClientID %>').value;
            
            var arrItem = new Array();
            var remItem = new Array();
            
            var i = 0; 
            var s = "";
            if (hdReq_List != "")
            {
                arrItem = hdReq_List.split(";");

                //선택행 배열에서 제거 
                remItem = arrItem.splice(delidx,1);

                if (arrItem.length > 0)
                {
                    for(i=0; i < arrItem.length-1 ; i++)
                    {
                        s = s + arrItem[i] + ";";
                    }

                    hdReq_List = s;
                }else
                {
                    hdReq_List = "";
                }
                
            }

            document.getElementById('<%= this.hdREQ_LIST.ClientID %>').value = hdReq_List;
            //alert(hdReq_List);
            <%= Page.GetPostBackEventReference(this.btnRefresh) %>  
            //return true;
        }  

        // 저장 시 필수 확인
        function fncChkSave() {
            if (document.getElementById('<%= this.ddlReqType.ClientID %>').value == "") {
                alert("승인항목을 선택해 주세요.");
                document.getElementById('<%= this.ddlReqType.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.hdREQ_LIST.ClientID %>').value == "") {
                alert("지출 신청 대상이 없습니다. 신청 대상 목록을 작성해 주세요.");
                document.getElementById('<%= this.txtRES_Name.ClientID %>').focus();
                return false;
            }
            
            /* 순회 교통비 일경우 사용연월의 형식 맞춤 YYYYMM 2016-07-29 박병진 */
            if (document.getElementById('<%= ddlReqType.ClientID %>').value == '021') {
                var value = document.getElementById('<%= txtText2.ClientID %>').value;
 
                if (value == '') {
                    alert('사용 연월 입력해 주세요.');
                    return false;
                } else {
                    if (!parse(value.substr(0, 6) + '01')) {
                        alert("올바른 날짜 형식이 아닙니다. YYYYMM 형식으로 입력해 주세요.");
                        return false;
                    }
                }
            }
            
            return true;
        }  
        
        function parse(str) {
            // validate year as 4 digits, month as 01-12, and day as 01-31 
            if ((str = str.match(/^(\d{4})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$/))) {
                // make a date
                str[0] = new Date(+str[1], +str[2] - 1, +str[3]);
                // check if month stayed the same (ie that day number is valid)
                if (str[0].getMonth() === +str[2] - 1)
                    return str[0];
            }
            return false;
        }     
    </script>
   
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <input type="hidden" id="hdRES_NAME" name="hdRES_NAME" runat="server" />    
    <input type="hidden" id="hdGB" name="hdGB" runat="server" />
    <input type="hidden" id="hdASSCON_ID" name="hdASSCON_ID" runat="server" />
    <input type="hidden" id="hdREQ_LIST" name="hdREQ_LIST" runat="server" />    
    <input type="hidden" id="hdEXP_REQ_ID" name="hdEXP_REQ_ID" runat="server" />  
    <asp:LinkButton ID="btnSelectRes2" runat="server" OnClick="btnSelectRes_Click"></asp:LinkButton>
    <asp:LinkButton ID="btnRefresh" runat="server" OnClick="btnRefresh_Click"></asp:LinkButton>
    <div class="title">
        <h2 class="mepm_title">지출 신청서 작성</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>지출신청서</p>
                    </td>
                </tr>
            </table> 
            <!-- 지출신청서 테이블 시작 -->
            <!-- 승인항목선택 항목 시작 -->
             <div class="mepm_menu_title" style="padding: 0;">
                 <table>
                     <tr style="height: 3em;">
                         <td style="width: 65px; text-align: left; padding-left:.5em;">
                             승인항목 :
                         </td>
                         <td style="text-align: left; padding-right:.5em;">
                             <asp:DropDownList ID="ddlReqType" runat="server" CssClass="i_f_out" OnSelectedIndexChanged="ddlReqType_SelectedIndexChanged"
                                DataTextField="COD_Name" DataValueField="COD_CD" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                         </td>
                     </tr>
                 </table>
             </div>
             <!-- 승인항목선택 항목 종료 -->
            <!-- 신청사원 조회 시작 -->
            <div class="mepm_menu_item" style="padding: 0;" runat="server" id="dvResSearch">
                <table>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="width: 65px; text-align: left; padding-left:.5em;">
                        신청사원 :
                        </td>
                        <td style="text-align:center; padding-right:.5em;" colspan="2">
                            <asp:TextBox ID="txtRES_Name" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="text-align:center;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="검색" ID="id2" runat="server" OnClientClick="javascript:return fncChkSearch();" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding:0;" runat="server" id="dvResList" visible="false">
                    <table class="mepm_icon_title">
                        <tr>
                            <td style="width: 100%;">
                                <p>지출 신청서 대상 선택</p>
                            </td>
                        </tr>
                    </table> 
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
            <!-- 신청사원 조회 종료 -->
            <!-- 신청사원 정보 시작 -->
            <div class="mepm_menu_item" style="table-layout: fixed; word-break: break-all; padding:0;" runat="server" id="dvResInfo">
                <table>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="width: 60px; text-align:left; padding-left:.5em;">
                            사번 :
                        </td>
                        <td style="width: auto; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblREQ_RES_ID" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 50px; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            이름 :
                        </td>
                        <td style="width: auto; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_Name" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부문 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_RBS_NAME" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            직종 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_WorkGroup1_NAME" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부서 :
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_RBS_AREA_NAMEE" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            입사일 :
                        </td>
                        <td style="width: auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:Label ID="lblRES_JoinDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <!-- 신청사원 정보 종료 -->
                    <!-- 승인금액 항목 시작 -->
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인금액 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-left:.2em; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtEXP_TAR_Amount" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="border-left:1px solid #ccc; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            부가세 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtEXP_TAR_Tax" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            비고 :                             
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-left:.2em; padding-right:.5em; text-align:left;" colspan="2">
                            <asp:TextBox ID="txtEXP_TAR_Memo" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:center;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="추가" ID="btnAdd" runat="server" OnClientClick="javascript:return fncChkAdd();" OnClick="btnAdd_Click"/>
                        </td>
                    </tr>
                    <!-- 승인금액 항목 종료 -->
                </table>
            </div>
            <!-- 지출신청서 테이블 종료 -->
            <!-- 지출신청서 대상 리스트 명단 시작 -->
            <div class="mepm_menu_item" style="border-bottom:0; padding:0;" runat="server" id="Div1" visible="true">
                    <table class="mepm_icon_title" style="border-bottom:0;">
                        <tr>
                            <td style="width: 100%;">
                                <p>지출 신청 대상 목록</p>
                            </td>
                        </tr>
                    </table> 
                    <asp:Panel ID="pnlReqList" CssClass="wenitepm_div_fix_layout1" runat="server"
                                    Height="100%" Width="100%" ScrollBars="Auto">
                    <asp:GridView ID="gvReqList" runat="server" CellPadding="0"  Width="100%"  Height="100px" EmptyDataText="내용이 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvReqList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="이름" DataField="이름">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="승인금액" DataField="승인금액">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="부가세" DataField="부가세">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:BoundField HeaderText="비고" DataField="비고">
                                <HeaderStyle CssClass="tr_border" Width="100" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="EXP_TAR_RES_ID" DataField="EXP_TAR_RES_ID" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="GB" DataField="GB" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ASSCON_ID" DataField="ASSCON_ID" Visible="False">
                                <HeaderStyle CssClass="tr_border" Width="0" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="삭제" DataField="">
                                <HeaderStyle CssClass="tr_border" Width="30" />
                                <ItemStyle HorizontalAlign="Center" CssClass="tr_border xbtn"/>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataRowStyle Height="50px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
                    </asp:Panel>
                    <table>
                        <tr>
                            <td style="width: 100%; height: 15px; border-top:1px solid #ccc; background-color: #fff;">
                            </td>
                        </tr>
                    </table> 
            </div>
            <!-- 지출신청서 대상 리스트 명단 끝 -->
            <!-- 기타 입력 사항 항목 시작 -->
            <div class="mepm_menu_item" style="padding:0;" runat="server" id="divRemark" visible="false">
            <table class="mepm_icon_title" style="border-bottom:0;">
                <tr>
                    <td style="width:100%;">
                        <p>기타 입력 사항</p>
                    </td>
                </tr>
            </table>            
            <table width="100%">                        
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="width:10px;border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="seq1" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width:70px; border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="txtLine1" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnLine1" runat="server" Value="" />
                        </td>
                        <td style="width:auto; border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtText1" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="seq2" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="txtLine2" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnLine2" runat="server" Value="" />
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtText2" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="seq3" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="txtLine3" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnLine3" runat="server" Value="" />
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtText3" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="seq4" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="txtLine4" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnLine4" runat="server" Value="" />
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtText4" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="seq5" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="border-top: 1px solid #ccc; text-align:left; padding-left:.5em;">
                            <asp:Label ID="txtLine5" runat="server"></asp:Label>
                            <asp:HiddenField ID="hdnLine5" runat="server" Value="" />
                        </td>
                        <td style="border-top: 1px solid #ccc; padding-right:.5em; text-align:left;">
                            <asp:TextBox ID="txtText5" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" style="padding: 0;">
                 <table>                     
                     <tr class="mepm_menu_title_bg" style="height: 3em;">
                        <td style="width:65px; text-align:left; padding-left:.5em;">
                            승인일자 :
                        </td>
                        <td style="width:auto; padding-right:.5em; text-align:left; font-size:.7em;">
                            <asp:Label ID="lblApproveDate" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width:65px; border-left:1px solid #ccc; text-align:left; padding-left:.5em;">
                            승인번호 :
                        </td>
                        <td style="width: auto; padding-right:.5em; text-align:left; font-size:.7em;">
                            <asp:Label ID="lblApproveNumber" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                 </table>
             </div>
             <!-- 기타 입력 사항 항목 종료 -->
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="작성완료" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="취소" ID="btnCancel" 
                    runat="server" OnClientClick="javascript:return window.location='/m_Default.aspx';" UseSubmitBehavior="False" />
                <%--<a href="/m_Default.aspx"><span class="button gray mepm_btn">취소</span></a>--%>
            </div>            
        </section>
    </article>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .i_f_out {}
    </style>
</asp:Content>

