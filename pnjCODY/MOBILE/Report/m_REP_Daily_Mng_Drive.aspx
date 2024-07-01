<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_REP_Daily_Mng_Drive.aspx.cs" Inherits="m_REP_Daily_Mng_Drive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function fncChkSave() {
            var num, year, month, day;
            num = document.getElementById('<%= this.txtStoreCnt.ClientID %>').value;

            if (isNaN(num)) {
                num = "";
                alert("숫자만 입력 가능합니다.");
                document.getElementById('<%= this.txtStoreCnt.ClientID %>').focus();
                 return false;
             }
             else {
                 document.getElementById('<%= this.txtStoreCnt.ClientID %>').value = num;
             }
        }
    </script>
    <input type="hidden" id="hdMODE" name="hdMODE" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>일일 업무 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/m_Default.aspx"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">업무 일지 > 자차운영일지</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>            
            <table class="mepm_icon_title">
                <tr>
                    <td style="width:40%;"><p >자차운영일지</p></td>
                </tr>
            </table>

            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 65px; text-align:left;  padding-left: 1em;">
                            <b>차량계기판 숫자</b>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 75px; text-align:left;padding-left: 1.5em;">
                            출근/퇴근 :
                        </td>
                        <td style="width: auto; text-align: left; padding-left: 1em;">
                            <asp:Label ID="lblAtt" runat="server" Text="" Style="font-weight: bold;"></asp:Label> 
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="mepm_menu_item" style="border-top:1px solid #ccc; ">
                <table>
                    <tr>
                        <td style="width: 75px; text-align:left;padding-left: 1em;">
                            <b>방문매장</b>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 75px; text-align:left;padding-left: 1.5em;">
                            매장수 :
                        </td>
                        <td style="width: auto; text-align: left; padding-left: .5em;">
                            <asp:TextBox ID="txtStoreCnt" runat="server"  onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 75px; text-align:left;padding-left: 1.5em;">
                            이동경로 : <br /><br />
                            <asp:TextBox ID="txtRoute" runat="server" MaxLength="500" Rows="5" Height="100px" Width="95%" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="mepm_menu_item" style="border-top:1px solid #ccc; ">
                <table>
                    <tr>
                        <td style="width: 75px; text-align:left;padding-left: 1em; cursor:pointer;" onClick="location.href='/Report/m_REP_Oil_Price.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>'" >
                           <span><b> 주유비 : </b><asp:Label ID="lblOil" runat="server" Text="0"></asp:Label> </span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width: 75px; text-align:left;padding-left: 1em; cursor:pointer;" onClick="location.href='/Report/m_REP_Toll_Price.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>'" >
                            <span><b> 통행료 : </b><asp:Label ID="lblToll" runat="server" Text="0"></asp:Label> </span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
<%--                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />--%>
                <a href="/Report/m_REP_Daily_Report_Main.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>