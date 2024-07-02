<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Employment.aspx.cs" Inherits="Resource_m_RES_Employment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function fncChkSave() {
            if (document.getElementById('<%= this.ddlRES_WorkType.ClientID %>').value == "") {
                alert("고용형태를 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_WorkType.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_RBS_Lv1.ClientID %>').value == "") {
                alert("부문을 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_RBS_Lv1.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_RBS_Lv2.ClientID %>').value == "") {
                alert("부서를 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_RBS_Lv2.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_WorkGroup1.ClientID %>').value == "") {
                alert("직종을 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_WorkGroup1.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_WorkGroup2.ClientID %>').value == "") {
                alert("직급을 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_WorkGroup2.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_WorkGroup3.ClientID %>').value == "") {
                alert("직책을 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_WorkGroup3.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%= this.ddlRES_WorkType.ClientID %>').value == "") {
                alert("고용형태를 선택해 주세요.");
                document.getElementById('<%= this.ddlRES_WorkType.ClientID %>').focus();
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
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 고용정보</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:100%;"><p >고용정보</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_item_bg" style="padding:0; border-bottom:1px solid #ccc;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:80px;">이름 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em;" colspan="2">
                            <asp:Label runat="server" ID="lblRES_Name" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">고용형태 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_WorkType" runat="server" CssClass="i_f_out" AutoPostBack="true" 
                                DataTextField="CTD_NM" DataValueField="CTD_CD" Width="100%" 
                                OnSelectedIndexChanged="ddlRES_WorkType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">부문 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_RBS_Lv1" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="RES_RBS_Name" 
                                DataValueField="RES_RBS_CD" OnSelectedIndexChanged="ddlRES_RBS_Lv1_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">부서 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_RBS_Lv2" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="RES_RBS_Name" DataValueField="RES_RBS_CD" Width="100%">
                                <asp:ListItem Text="-선택-" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">직종 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_WorkGroup1" runat="server" CssClass="i_f_out" AutoPostBack="true" DataTextField="CTD_NM" DataValueField="CTD_CD" 
                            OnSelectedIndexChanged="ddlRES_WorkGroup1_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">직급 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_WorkGroup2" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="CTD_NM" DataValueField="CTD_CD" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">직책 :</th>
                        <td style="text-align:left; border-top:1px solid #ccc; padding-right:.8em;" colspan="2">
                            <asp:DropDownList ID="ddlRES_WorkGroup3" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="CTD_NM" DataValueField="CTD_CD" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" TabIndex="5" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
