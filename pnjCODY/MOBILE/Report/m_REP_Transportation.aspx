<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_REP_Transportation.aspx.cs" Inherits="m_REP_Transportation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkSave() {
            var num, year, month, day;

            var regExp = /^\s*$/;

            if (regExp.test(document.getElementById('<%=this.txtPrice.ClientID %>').value)) {
                alert("단가는 공백을 허용하지 않습니다. ");
                document.getElementById('<%=this.txtPrice.ClientID %>').focus();
                return false;
            }


            num = document.getElementById('<%= this.txtPrice.ClientID %>').value;
            if (isNaN(num)) {
                num = "";
                alert("단가는 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtPrice.ClientID %>').focus();
                return false;
            }
            else {
                document.getElementById('<%= this.txtPrice.ClientID %>').value = num;
            }
        }
    </script>

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
        <h2 class="mepm_title">업무 일지 > 교통비정산서</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >교통비 정산서</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">거래처 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtCustomer" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">사용형태 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="True" Enabled="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                <asp:ListItem Value="">- 선택 -</asp:ListItem>
                                <asp:ListItem Value="통행료">통행료</asp:ListItem>
                                <asp:ListItem Value="택시">택시</asp:ListItem>
                                <asp:ListItem Value="지하철">지하철</asp:ListItem>
                                <asp:ListItem Value="시내버스">시내버스</asp:ListItem>
                                <asp:ListItem Value="시외버스">시외버스</asp:ListItem>
                                <asp:ListItem Value="기차">기차</asp:ListItem>
                                <asp:ListItem Value="주유비">주유비</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">방문매장 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtStore" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">금액 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtPrice" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out"/>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">증빙 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlEvidence" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                                <asp:ListItem Value="">- 선택 -</asp:ListItem>
                                <asp:ListItem Value="법인카드">법인카드</asp:ListItem>
                                <asp:ListItem Value="현금">현금</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>                
            </div>            
            <div class="mepm_btn_div">
                * 택시 이용 시 출발지와 도착지를 기재하세요.<br /><br />
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Report/m_REP_Transportation_List.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
