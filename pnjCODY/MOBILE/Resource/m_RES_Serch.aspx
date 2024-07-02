<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Serch.aspx.cs" Inherits="Resource_m_RES_Serch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

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
            <a onClick="history.back();"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">
            사원 검색</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <p class="mepm_icon_title">사원 검색 조건</p>
            <div class="mepm_menu_item">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 구분 :</td>
                        <td style="width:auto; height:2em;"><span style="display:block;">                            
                            <asp:RadioButtonList ID="rdoType" runat="server" RepeatDirection="Horizontal" 
                                AutoPostBack="true" Height="30px" 
                                onselectedindexchanged="rdoType_SelectedIndexChanged">
                                <asp:ListItem Text="이름" Value="W" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="매장" Value="S"></asp:ListItem>
                            </asp:RadioButtonList></span></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_title" runat="server" id="dvRES_WORKGROUP1" visible="true">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 조건 :</td>
                        <td style="width:auto; height:2em;"><span style="display:block;">                            
                            <asp:RadioButtonList ID="rdoRES_WORKGROUP1" runat="server" RepeatDirection="Vertical" AutoPostBack="false" RepeatColumns="2" Height="100px">
                                <asp:ListItem Text="내 사원" Value="002" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="입사대기 사원" Value="" ></asp:ListItem>
                                <asp:ListItem Text="전체 사원" Value="004"></asp:ListItem>
                            </asp:RadioButtonList></span></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" runat="server" id="dvName" visible="true">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 이름 :</td>
                        <td style="width:auto; padding-right:.8em;">
                        <asp:TextBox ID="txtName" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" /></td>
                    </tr>
                </table>
            </div> 
            <div class="mepm_menu_item" runat="server" id="dvBirth" visible="true">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 생년월일 :</td>
                        <td style="width:auto; padding-right:.8em;">
                        <asp:TextBox ID="txtBirth" runat="server" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" /></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" runat="server" id="dvRES_RBS_Lv1" visible="false">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 부문 :</td>
                        <td style="width:auto; height:2em;"><span style="display:block;">                            
                            <asp:DropDownList ID="ddlRES_RBS_Lv1" runat="server" CssClass="i_f_out" Width="100%" 
                                AutoPostBack="true" OnSelectedIndexChanged="ddlRES_RBS_Lv1_SelectedIndexChanged">
                                <asp:ListItem Value="" Text="전체" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="3001" Text="지원1부문"></asp:ListItem>
                                <asp:ListItem Value="2010" Text="지원2부문"></asp:ListItem>
                                <asp:ListItem Value="2011" Text="지원3부문"></asp:ListItem>
                            </asp:DropDownList>
                        </span></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" runat="server" id="dvEPM_CUSTOMER_STORE" visible="false">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 거래처 :</td>
                        <td style="width:auto; height:2em;"><span style="display:block;">                            
                            <asp:DropDownList ID="ddlEPM_CUSTOMER_STORE" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD" OnSelectedIndexChanged="ddlEPM_CUSTOMER_STORE_SelectedIndexChanged">
                            </asp:DropDownList>   
                        </span></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_menu_item" runat="server" id="dvEPM_CUSTOMER_STORE_L2" visible="false">
                <table>
                    <tr>
                        <td style="width:85px;"><b style="color:#bbb;">+</b> 매장 :</td>
                        <td style="width:auto; height:2em;"><span style="display:block;">                            
                            <asp:DropDownList ID="ddlEPM_CUSTOMER_STORE_L2" style="width:100%;" runat="server"
                                CssClass="i_f_out" AutoPostBack="true" DataTextField="COD_NM" 
                                DataValueField="COD_CD">
                            </asp:DropDownList>   
                        </span></td>
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div" runat="server" id="dvMassege" visible="false">
                <asp:Label runat="server" ID="lblMessage" ForeColor="Red" Font-Bold="true"></asp:Label>
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="검색" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                <a href="javascript:history.back();"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>
