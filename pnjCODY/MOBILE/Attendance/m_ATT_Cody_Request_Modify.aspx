<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_ATT_Cody_Request_Modify.aspx.cs" Inherits="Attendance_m_ATT_Cody_Request_Modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkSave() {
            if (document.getElementById('<%=this.txtAttRequest.ClientID %>').value == "") {
                alert("수정사유를 입력해 주세요.");
                document.getElementById('<%=this.txtAttRequest.ClientID %>').focus();
                return false;
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
            | <strong>근태정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="javascript:history.back();"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">근태정보 > 코디 근태 목록 > 요청</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >근태 수정요청</p></td>                        
                    </tr>
            </table>
           <%-- <div ID="divAttClosed" runat="server" class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                        <td style="text-align:center;border-top:1px solid #ccc; background-color:#880000; color:#ffd800;">
                            <h3><asp:Label runat="server" ID="lblAttClosed" Text="근태 수정 요청시 팀장 결제 후에 반영됩니다." Fore-Color="#ffd800"></asp:Label></h3>
                        </td>
                    </tr>
                </table>
            </div>--%>
            <table class="mepm_menu_title_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <tr class="mepm_menu_title_bg" style="height:3em;">
                    <th style="width:60px">
                        사번 :
                    </th>
                    <td style="width:auto; text-align:left; padding-right:0.5em">
                        <asp:Label runat="server" ID="lblRES_Number" Text=""></asp:Label>
                    </td>
                    <th style="width:45px; border-left:1px solid #ccc;">
                        이름 :
                    </th>
                    <td style="width:auto; text-align:left; padding-right:0.5em">
                        <asp:Label runat="server" ID="lblRES_Name" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="mepm_menu_title_bg" style="height:3em;">
                    <th style="border-top:1px solid #ccc;">
                        핸드폰 :
                    </th>
                    <td colspan="3" style="border-top:1px solid #ccc; text-align:left; padding-right:0.5em">
                        <asp:Label runat="server" ID="lblRES_CP" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="mepm_menu_title_bg" style="height:3em;">
                    <th style="border-top:1px solid #ccc;">
                        매장 :
                    </th>
                    <td colspan="3" style="border-top:1px solid #ccc; text-align:left; padding-right:0.5em">
                        <asp:Label runat="server" ID="lblRES_STORE_NAME" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
           
            <table class="mepm_btn_div" style="padding:5; border-bottom:1px solid #ccc;">
                    <tr>
                        <td style="width:120px;"><b>남은 대체 휴무:</b></td> 
                        <td align="left" style="width:70px; font-weight:normal;">
                            <asp:Label runat="server" ID="lblCNT_ALT_HOLIDAY" Text=""></asp:Label> 개
                        </td>
                        <td align="right" style="width:auto; padding-right:.3em;">
                        </td>                       
                    </tr>
            </table>                
             <div class="mepm_btn_div" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:100%; padding-left:.8em;">
                            <asp:RadioButtonList ID="rdoATT_DAY_Code" runat="server" RepeatDirection="Vertical" RepeatColumns="2" AutoPostBack="false" Height="230px">
                            </asp:RadioButtonList>
                        </th>
                    </tr>     
                    <tr  class="mepm_menu_title_bg" style="height:3.5em; border-bottom:1px solid #ccc;">
                         <td>
                             <table style="height:3.5em;">
                                 <tr>
                                     <td style="text-align:right;border-top:1px solid #ccc; text-align:left; padding-left:16px; width:25%;">
                                        수정사유 :</td>
                                      <td style="text-align:center;border-top:1px solid #ccc; text-align:left;">
                                        <asp:TextBox runat="server" ID="txtAttRequest" CssClass="i_f_out" width="95%" maxlength="50"></asp:TextBox>
                                      </td>
                                 </tr>
                             </table>
                        </td>
                    </tr>              
                </table>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="javascript:history.back();"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
