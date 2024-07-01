<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Address.aspx.cs" Inherits="Resource_m_RES_Address" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function fncSearch() {
            if (document.getElementById('<%= this.txtDong.ClientID %>').value == "") {
                alert("검색할 도로명 또는 건물명을 입력해 주세요.");
                document.getElementById('<%= this.txtDong.ClientID %>').focus();
                return false;
            }
        }

        function fncChkSave() {
            if (document.getElementById('<%= this.txtAdd1.ClientID %>').value == "") {
                alert("우편번호를 검색 해주세요.");
                //document.getElementById('<%= this.txtDong.ClientID %>').focus();
                return false;
            }

            //if (document.getElementById('<%= this.txtAdd2.ClientID %>').value == "") {
            //    alert("상세주소를 입력 해주세요.");
            //    document.getElementById('<%= this.txtAdd2.ClientID %>').focus();
            //    return false;
            //}
        }

        function fncAddressAdd(strPost, strAdd){
            document.getElementById('<%= this.hdPOST.ClientID %>').value =  strPost;
            document.getElementById('<%= this.hdADD1.ClientID %>').value =  strAdd;

            <%= Page.GetPostBackEventReference(this.btnAddressAdd) %>    
        }
    </script>
    <input type="hidden" id="hdPOST" name="hdPOST" runat="server" />
    <input type="hidden" id="hdADD1" name="hdADD1" runat="server" />
    <asp:LinkButton ID="btnAddressAdd" runat="server" OnClick="btnAddressAdd_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 주소</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title" style="display:none;">
                <tr>
                    <td style="width: 100%;">
                        <p>
                            우편번호 검색</p>
                    </td>
                </tr>
            </table>
            <div class="logindiv" style="display:none;">
                <ul>
                    <li class="input_area">                    
                    <span class="l">
                        <label>시/도 :</label>
                    </span>
                    <span class="c">
                        <span id="Span4">
                            <asp:DropDownList ID="ddlSIDO" runat="server" CssClass="i_f_out" AutoPostBack="false" 
                                DataTextField="SIDO_NM" DataValueField="SIDO_CD" Width="100%">
                            </asp:DropDownList>
                        </span>
                    </span>
                    <li class="input_area">                    
                    <span class="l">
                        <label>도로명 :</label>
                    </span>
                    <span class="c">
                        <span id="pwb" class="input_txt_area input_txt_area_v1">
                        <asp:TextBox ID="txtDong" runat="server" CssClass="input_txt input_txtc" TabIndex="1"></asp:TextBox>
                        </span>
                    </span>
                    <span class="r">
                        <asp:Button ID="ibtnSerch" runat="server" Text="조회" OnClientClick="javascript:return fncSearch();" OnClick="ibtnSerch_Click" CssClass="lbtn ls" />
                    </span></li>
                    <li class="input_area"><span class="a">도로명 또는 건물명을 입력하세요.<br />(예: 강남대로39길, 대륭포스트타워2차)</span></li>
                </ul>
            </div>
            <!-- 우편번호 조회 목록 시작 -->
            <div class="mepm_menu_item"  style="padding: 0;" runat="server" id="dvPostList" visible="false">
                        <table class="mepm_icon_title">
                <tr>
                    <td style="width: 100%;">
                        <p>우편번호 검색 결과</p>
                    </td>
                    <td align="right" style="width: 65%; font-weight: normal; padding-right: 0.5em;">
                    </td>
                </tr>
            </table>
                <asp:GridView ID="gvPostList" runat="server" CellPadding="0" Width="100%" EmptyDataText="우편번호 검색결과가 없습니다." ShowHeaderWhenEmpty="True" 
                    CssClass="table_border" OnRowDataBound="gvPostListt_RowDataBound" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="주소" DataField="전체주소">
                            <HeaderStyle CssClass="tr_border" />
                            <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="우편번호" DataField="우편번호">
                            <HeaderStyle CssClass="tr_border" Width="17%" />
                            <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
                    <RowStyle CssClass="mepm_menu_item_bg" />
                    <HeaderStyle CssClass="mepm_menu_title_bg" />
                </asp:GridView>
            </div>
            <!-- 우편번호 조회 목록 종료 -->
            <!-- 상세 주소 입력 텍스트박스 시작 -->
            <div>
            <table class="mepm_icon_title">
                <tr>
                    <td style="width: 40%;">
                        <p>
                            주소</p>
                    </td>
                </tr>
            </table>
            <div class="logindiv">
                <ul>
                    <li class="input_area">
                        <span class="l">
                            <label>우편번호 :</label>
                        </span>
                        <span class="c">
                            <span id="Span3" class="input_txt_area input_txt_area_v1">
                                <asp:TextBox ID="txtPOST" runat="server" CssClass="input_txt input_txtc" TabIndex="2" ReadOnly="true"></asp:TextBox>
                            </span>
                        </span>
                        <span class="r">
                            <input type="button" onclick="showDaumPostcode();" value="조회" class="lbtn ls" /><br>
                        </span>
                    </li>
                     <li id="layer" style="display: none; z-index: 9998; border: 5px solid; position: fixed; width: 350px; height: 500px; left: 50%; margin-left: -180px; top: 50%; margin-top: -235px; overflow: hidden; -webkit-overflow-scrolling: touch;">
                        <img src="//i1.daumcdn.net/localimg/localimages/07/postcode/320/close.png" id="btnCloseLayer" style="z-index: 9999; cursor: pointer; position: absolute; right: -3px; top: -3px" onclick="closeDaumPostcode()" alt="닫기 버튼">
                    </li>
                    <li class="input_area">
                        <span class="l">
                            <label>기본주소 :</label>
                        </span>
                        <span class="c">
                            <span id="Span2" class="input_txt_area input_txt_area_v1">
                                <asp:TextBox ID="txtAdd1" runat="server" CssClass="input_txt input_txtc" TabIndex="3" ReadOnly="true"></asp:TextBox>
                            </span>
                        </span>
                    </li>
                    <li class="input_area">
                        <span class="l">
                            <label>상세주소 :</label>
                        </span>
                        <span class="c">
                            <span id="Span1" class="input_txt_area input_txt_area_v1">
                                <asp:TextBox ID="txtAdd2" runat="server" CssClass="input_txt input_txtc" TabIndex="4"></asp:TextBox>
                            </span>
                        </span>
                    </li>
                </ul>
            </div>
            </div>
            <!-- 상세 주소 입력 텍스트박스 종료 -->            
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" TabIndex="5" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>

    <%--<script src="http://dmaps.daum.net/map_js_init/postcode.js"></script>--%>
    <script src="http://dmaps.daum.net/map_js_init/postcode.v2.js"></script>
    
    <script>
        // 우편번호 찾기 iframe을 넣을 element
        var element = document.getElementById('layer');

        function closeDaumPostcode() {
            // iframe을 넣은 element를 안보이게 한다.
            element.style.display = 'none';
        }

        function showDaumPostcode() {
            new daum.Postcode({
                oncomplete: function(data) {
                    // 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.
                    // 우편번호와 주소 및 영문주소 정보를 해당 필드에 넣는다.

                    if (data.addressType == "N") {
                        element.style.display = 'none';
                        alert("도로명 주소로 검색하여 선택해 주세요.");
                        return false
                    }
                    else {
                        var postcode = data.postcode1 + "-" + data.postcode2;
                        document.getElementById('<%= this.txtPOST.ClientID %>').value = data.zonecode;
                        document.getElementById('<%= this.txtAdd1.ClientID %>').value = data.buildingName == null ? data.roadAddress : data.roadAddress + "(" + data.buildingName + ")";
                        document.getElementById('<%= this.hdPOST.ClientID %>').value = data.zonecode;
                        document.getElementById('<%= this.hdADD1.ClientID %>').value = data.buildingName == null ? data.roadAddress : data.roadAddress + "(" + data.buildingName + ")";
                    }
                    // iframe을 넣은 element를 안보이게 한다.
                    element.style.display = 'none';
                },
                width: '100%',
                height: '100%'
            }).embed(element);

            // iframe을 넣은 element를 보이게 한다.
            element.style.display = 'block';
        }
    </script>
</asp:Content>
