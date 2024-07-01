<%@ Page Language="C#" MasterPageFile="~/m_MasterPage_PostBackBreak.master" AutoEventWireup="true" CodeFile="m_RES_Photo_Add.aspx.cs" Inherits="Resource_m_RES_Photo_Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        //userAgent iPhone, iPad, Apple iOS 전용 페이지로
        if (isios) {
            document.location.replace('/Resource/m_RES_Photo_Add_iPhone.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>');
        }

        //--- 저장시 필수 확인
        function fncChkSave() {
            if (document.getElementById('<%= this.upPicture.ClientID%>').value == "") {
                alert("사진을 첨부해 주세요.");
                return false;
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
            <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">사원 등록 정보 > 사진</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >사진</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_title_bg" style="padding:0; border-bottom:1px solid #ccc;">
                
                <table>
                    <tr style="height:3em;">
                        <td style="width:20%;"></td>
                        <td  style="width:60%; text-align:center; padding:1em;">
                            <asp:Label ID="lblRES_Picture" runat="server"></asp:Label>
                            <asp:Image ID="imgRES_Picture" ImageUrl="" runat="server" Visible="false"  width="155px" height="185px" />
                        </td>
                        <td style="width:20%;"></td>
                    </tr>
                </table>                
            </div>
            <div class="mepm_btn_div">
                <asp:FileUpload CssClass="i_f_out" ID="upPicture" runat="server" Width="90%" BackColor="white" onchange="this.className='i_f_on';" />                                               
            </div>
            <div class="mepm_btn_div">
                <asp:Button CssClass="button gray mepm_asp_btn" Text="사진 등록하기" ID="btnUploadFile" runat="server" OnClientClick="javascript:return fncChkSave();" OnClick="btnUploadFile_Click" />
                <a href="/Resource/m_RES_Register.aspx?RES_ID=<%= this.Page.Request["RES_ID"]%>"><span class="button gray mepm_btn">취소</span></a>
            </div>
        </section>
    </article>
</asp:Content>