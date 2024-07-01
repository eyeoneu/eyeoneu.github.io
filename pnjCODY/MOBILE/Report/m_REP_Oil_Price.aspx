<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true"  CodeFile="m_REP_Oil_Price.aspx.cs" Inherits="m_REP_Oil_Price" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkPrice() {
            if (document.getElementById('<%=this.txtSum.ClientID %>').value != "" && document.getElementById('<%=this.txtPrice.ClientID %>').value != "") {
                PriceCal = parseFloat(document.getElementById('<%=this.txtSum.ClientID %>').value / document.getElementById('<%=this.txtPrice.ClientID %>').value).toFixed(2)
                document.getElementById('<%=this.txtLiter.ClientID %>').value = PriceCal
                document.getElementById('<%=this.hdLiter.ClientID %>').value = PriceCal
            }
            else if (document.getElementById('<%=this.txtSum.ClientID %>').value == "" || document.getElementById('<%=this.txtPrice.ClientID %>').value == "") {
                PriceCal = ""
                document.getElementById('<%=this.txtLiter.ClientID %>').value = PriceCal
                document.getElementById('<%=this.hdLiter.ClientID %>').value = PriceCal
            }
        }

        function fncChkSave() {
            var num, year, month, day;

            var regExp = /^\s*$/;
            if (document.getElementById('<%=this.ddlOilSelector.ClientID %>').value == "") {
                alert("주유 품목은 필수 선택 사항입니다. ");
                document.getElementById('<%=this.ddlOilSelector.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.txtLiter.ClientID %>').value)) {
                alert("용량은 공백을 허용하지 않습니다. ");
                document.getElementById('<%=this.txtLiter.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.txtPrice.ClientID %>').value)) {
                alert("단가는 공백을 허용하지 않습니다. ");
                document.getElementById('<%=this.txtPrice.ClientID %>').focus();
                return false;
            }
            if (regExp.test(document.getElementById('<%=this.txtSum.ClientID %>').value)) {
                alert("금액은 공백을 허용하지 않습니다. ");
                document.getElementById('<%=this.txtSum.ClientID %>').focus();
                return false;
            }

            num = document.getElementById('<%= this.txtLiter.ClientID %>').value;
            if (isNaN(num)) {
                num = "";
                alert("용량은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtLiter.ClientID %>').focus();
                return false;
            }
            else {
                document.getElementById('<%= this.txtLiter.ClientID %>').value = num;
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

            num = document.getElementById('<%= this.txtSum.ClientID %>').value;
            if (isNaN(num)) {
                num = "";
                alert("금액은 숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtSum.ClientID %>').focus();
                return false;
            }
            else {
                document.getElementById('<%= this.txtSum.ClientID %>').value = num;
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
            | <strong>주유비</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Report/m_REP_Daily_Mng_Drive.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">기타 업무 관리 > 자차 운영 일지</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >주유비 </p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">종류 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;">
                            <asp:DropDownList ID="ddlOilSelector" runat="server" CssClass="i_f_out" Width="100%" AutoPostBack="false" Enabled="true">
                                <asp:ListItem Value="">- 선택 -</asp:ListItem>
                                <asp:ListItem Value="G">휘발유</asp:ListItem>
                                <asp:ListItem Value="D">경유</asp:ListItem>
                                <asp:ListItem Value="L">LPG</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">리터 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" >
                            <asp:TextBox ID="txtLiter" runat="server" style="width:100%;" class="i_f_out" BackColor="#EEEEEE" Enabled="false"/>
                              <asp:HiddenField ID="hdLiter" runat="server" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">단가(원) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtPrice" runat="server" style="width:100%;" onfocus="this.className='i_f_on'; fncChkPrice();" onblur="this.className='i_f_out'; fncChkPrice();" class="i_f_out" />                            
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc; width:80px; text-align:right; padding-right:.8em;">금액(원) :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:TextBox ID="txtSum" runat="server" style="width:100%;" onfocus="this.className='i_f_on'; fncChkPrice();" onblur="this.className='i_f_out'; fncChkPrice();" class="i_f_out" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Report/m_REP_Daily_Mng_Drive.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
