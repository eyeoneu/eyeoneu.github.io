﻿<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_ATT_Parttime_Modify.aspx.cs" Inherits="Attendance_m_ATT_Parttime_Modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncDDLControler(strType) {
            document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').disabled = false;
            
            if (strType == "A") {
                document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').value = "<%=conTime%>";
            }
            else {
                //
                document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').value = "0";
            }

            document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').disabled = true;
        }

        function fncChkSave() {
            if (document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').value == "") {
                alert("근무시간을 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').focus();
                return false;
            }
            
            document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').disabled = false;
        }

        function getRadioSelectedValue(objectName) {
            var col = document.getElementsByName(objectName);

        }
    </script>  
    
    <input type="hidden" id="hdATT_DAY_Icon" name="hdATT_DAY_Icon" runat="server" />
    <input type="hidden" id="hdATT_DAY_Code" name="hdATT_DAY_Code" runat="server" />
    
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>근태 정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a onClick="history.back();"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">일근태 관리 > 계약 근태</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >근태 수정</p></td>                        
                    </tr>
            </table>
            <table class="mepm_menu_title_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <tr class="mepm_menu_title_bg" style="height:3em;">
                    <th style="width:55px">
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
                <tr class="mepm_menu_title_bg" style="height:3em;">
                    <td colspan="2" style="border-top:1px solid #ccc; text-align:left; padding-left:0.5em; padding-right:0.5em">
                        계약 형태 :&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRES_CON_Type" Text=""></asp:Label>
                    </td>
                    <td colspan="2" style="border-top:1px solid #ccc; border-left:1px solid #ccc; padding-left:0.5em">
                        지원사 :&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRES_Venter" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="mepm_menu_title_bg" style="height:3em;">
                    <th style="border-top:1px solid #ccc;">
                        계약 :
                    </th>
                    <td style="border-top:1px solid #ccc; text-align:left; padding-right:0.5em">
                        <asp:Label runat="server" ID="lblRES_CON_Time" Text=""></asp:Label> 시간
                    </td>
                    <th style="border-top:1px solid #ccc; border-left:1px solid #ccc;">
                        일당 :
                    </th>
                    <td style="border-top:1px solid #ccc; text-align:left; padding-right:0.5em">
                        <asp:Label runat="server" ID="lblRES_CON_Pay" Text=""></asp:Label> 원
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="height:.5em;"></td>                    
                </tr>
            </table>               
            <div class="mepm_menu_item" style="padding:0; border-top:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:65px">
                            근무시간 :
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlRES_CON_TIME" runat="server" CssClass="i_f_out">
                                <asp:ListItem Text="-선택-" Value=""></asp:ListItem>

                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            </asp:DropDownList> 시간
                        </th>
                    </tr>                  
                </table>
            </div>
             <div runat="server" id="dvATT_DAY_Code" class="mepm_btn_div" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:100%; padding-left:.8em;">
                            <asp:RadioButtonList ID="rdoATT_DAY_Code"  runat="server" RepeatDirection="Vertical" RepeatColumns="2" AutoPostBack="false" Height="230px">
                            </asp:RadioButtonList>
                        </th>
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
    
    <script type="text/javascript">
        if ("<%=conType%>" == "M") {
            document.getElementById('<%= this.ddlRES_CON_TIME.ClientID %>').disabled = true;
        }
    </script>  
</asp:Content>