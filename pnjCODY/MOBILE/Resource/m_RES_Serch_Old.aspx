<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_Serch_Old.aspx.cs" Inherits="Resource_m_RES_Serch_Old" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <header class="header">
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
        <h2 class="mepm_title">사원 정보 조회 > 기존 정보 조회</h2>
    </div>
    <article style="padding-bottom: 1em;">

        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:40%;"><p >기존 정보 조회</p></td>
                    </tr>
                </table>
            <div class="mepm_menu_title" style="padding:0;">
                <table>
                   <tr style="height:3em;">
                        <th style="width:75px;">주민번호 :</th>
                        <td style="width:auto; text-align:left;">
                        <input type="text" style="width:115px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text5" /> -
                        <input type="text" style="width:115px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text6" />
                        </td>                        
                    </tr>
                </table>
            </div>
                <div class="mepm_btn_div">
                <a href="#"><span class="button gray mepm_btn">조회</span></a>
                <a href="#"><span class="button gray mepm_btn">취소</span></a>
            </div>
             <div class="mepm_menu_item" style="border-top:1px solid #ccc; padding:0;">
                <table>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="width:75px;">성명 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em;">
                            <input type="text" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text1" />
                        </td>
                        <th style="width:75px; border-left:1px solid #ccc;">입사일 :</th>
                        <td style="width:auto; text-align:left; padding-right:.8em;">
                            <input type="text" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text2" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">상태 :</th>
                        <td style="border-top:1px solid #ccc; text-align:left; padding-right:.8em;">
                            <input type="text" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text3" />
                        </td>
                         <th style="border-top:1px solid #ccc; border-left:1px solid #ccc;">퇴사일 :</th>
                        <td  style="border-top:1px solid #ccc; text-align:left; padding-right:.8em">
                            <input type="text" style="width:100%;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text4" />
                        </td>
                    </tr>
                    <tr class="mepm_menu_item_bg" style="height:3em;">
                        <th style="border-top:1px solid #ccc;">주민번호 :</th>
                        <td style="border-top:1px solid #ccc; text-align:left;" colspan="3">
                        <input type="text" style="width:115px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text17" /> -
                        <input type="text" style="width:115px;" onfocus="this.className='i_f_on';" onblur="this.className='i_f_out'" class="i_f_out" id="text18" />
                        </td>                        
                    </tr>
                </table>
            </div>
            <div class="mepm_btn_div">
                <a href="#"><span class="button gray mepm_btn">신규등록</span></a>
                <a href="#"><span class="button gray mepm_btn">정보수정</span></a>
                <p style="padding-top: 0.5em;">
                <a href="#"><span class="button gray mepm_btn">코디배정</span></a>
                <a href="#"><span class="button gray mepm_btn">아르바이트계약</span></a>
                </p>
            </div>
        </section>
    </article>
</asp:Content>

