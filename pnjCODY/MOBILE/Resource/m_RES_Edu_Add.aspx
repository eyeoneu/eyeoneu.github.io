<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Edu_Add.aspx.cs" Inherits="Resource_m_RES_Edu_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/javascript">
       //--- 저장시 필수 확인
       function fncChkSave() {
           if (document.getElementById('<%= this.txtRES_EDU_School.ClientID %>').value == "") {
               alert("학교명을 입력해 주세요.");
               document.getElementById('<%= this.txtRES_EDU_School.ClientID %>').focus();
               return false;
           }

           if (document.getElementById('<%= this.ddlRES_EDU_Graduation.ClientID %>').value == "") {
               alert("졸업여부를 입력해 주세요.");
               document.getElementById('<%= this.ddlRES_EDU_Graduation.ClientID %>').focus();
               return false;
           }

           // 졸업여부가 졸업 일 경우
           if (document.getElementById('<%= this.ddlRES_EDU_Graduation.ClientID %>').value == "100") {
               if (document.getElementById('<%= this.ddlRES_EDU_GraduationDate.ClientID %>').value == "") {
                   alert("졸업년도를 입력해 주세요.");
                   document.getElementById('<%= this.ddlRES_EDU_GraduationDate.ClientID %>').focus();
                   return false;
               }
           }

           return true;
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
            <a href="/Resource/m_RES_Edu.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 학력정보 > 학력정보 추가</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p>학력정보 추가</p></td>
                        <td align="right" style="width:60%; font-weight:normal; padding-right:0.5em;">
                            <asp:Button CssClass="button gray mepm_btn_4em" Text="삭제" ID="btnDel" 
                                runat="server" OnClientClick="return confirm('삭제 하시겠습니까?');" OnClick="btnDel_Click" />
                        </td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0; border-bottom:1px solid #ccc;">
                
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:70px;">학교명 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em;">
                            <asp:TextBox ID="txtRES_EDU_School" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">졸업여부 :</th>
                        <td style="text-align:left; padding-right:.8em; border-top:1px solid #ccc;">
                            <asp:DropDownList ID="ddlRES_EDU_Graduation" runat="server" CssClass="i_f_out" AutoPostBack="false" DataTextField="CTD_NM" DataValueField="CTD_CD" Enabled="true" Width="100%">
                                <asp:ListItem Text="-선택-" Value=""></asp:ListItem>
                                <asp:ListItem Text="졸업" Value="100"></asp:ListItem>
                                <asp:ListItem Text="졸업예정" Value="400"></asp:ListItem>
                                <asp:ListItem Text="재학" Value="500"></asp:ListItem>
                                <asp:ListItem Text="중퇴" Value="200"></asp:ListItem>
                                <asp:ListItem Text="휴학" Value="300"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">졸업연도 :</th>
                        <td style="text-align:left; padding-right:.8em; border-top:1px solid #ccc;">
                            <asp:DropDownList ID="ddlRES_EDU_GraduationDate" runat="server" CssClass="i_f_out" AutoPostBack="false" Enabled="true">
                            </asp:DropDownList> 년
                        </td>
                    </tr>                   
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">전공 :</th>
                        <td style="text-align:left; padding-right:.8em; border-top:1px solid #ccc;">
                            <asp:TextBox ID="txtRES_EDU_Major" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">소재지 :</th>
                        <td style="text-align:left; padding-right:.8em; border-top:1px solid #ccc;">
                            <asp:TextBox ID="txtRES_EDU_Area" runat="server" MaxLength="10" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" />
                        </td>
                    </tr>
                </table>                
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="저장" ID="btnSave" 
                    runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnSave_Click" />
                <a href="/Resource/m_RES_Edu.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>

