<%@ Page Language="C#" MasterPageFile="~/m_MasterPage.master" AutoEventWireup="true" CodeFile="m_RES_MyList.aspx.cs" Inherits="Resource_m_RES_MyList" Debug="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        // 사원 클릭 시
        function fncDetail(srtID){
//            var parm = new Array();
//            parm.push("RES_ID=" + srtID);

//            location.href("/Resource/m_RES_Mng.aspx?" + encodeURI(parm.join('&')));

            document.getElementById('<%= this.hdRES_ID.ClientID %>').value =  srtID;

            <%= Page.GetPostBackEventReference(this.btnDetail) %>    
        }
    </script>
    <input type="hidden" id="hdRES_ID" name="hdRES_ID" runat="server" />
    <asp:LinkButton ID="btnDetail" runat="server" OnClick="btnDetail_Click"></asp:LinkButton>
    <header>
        <h1 class="mepm_lg">
            <a href="/" class="mepm_lga">
                <img src="/images/m/mepm_logo.png" alt="코디서비스" width="24" height="20" class="mepm_lgm" /><span class="mepm_lgi">CODY<br />SERVICE</span>
            </a>
        </h1>
        <p class="mepm_lg mepm_lgt">
            | <strong>사원정보 관리</strong></p>
        <p class="mepm_lg mepm_lgback">
            <a href="/"><span class="button blue">이전단계</span></a></p>
    </header>
    <div class="title">
        <h2 class="mepm_title">내 사원 목록</h2>
    </div>
    <article style="padding-bottom: 1em;">
        <section>
            <table class="mepm_icon_title">
                    <tr>
                        <td style="width:90%;"><p ><%= Session["sRES_RBS_NAME"].ToString()%>  <%= Session["sRES_RBS_AREA_NAME"].ToString() %> 사원 목록</p></td>
                        <td align="right" style="width:10%; font-weight:normal; padding-right:0.5em;">
                        </td>
                    </tr>
                </table>
             <div class="mepm_menu_item" style="padding:0; font-size:small;">
                    <asp:GridView ID="gvResList" runat="server" CellPadding="0"  Width="100%"  EmptyDataText="일치하는 정보가 없습니다." ShowHeaderWhenEmpty="True"
                         CssClass="table_border" OnRowDataBound="gvResList_RowDataBound" AutoGenerateColumns="false">
                        <Columns>
                            <asp:boundfield HeaderText="이름" DataField="RES_Name">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border" />
						    </asp:boundfield>
                            <asp:boundfield HeaderText="직종" DataField="RES_WorkGroup1_NAME">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
						    <asp:boundfield HeaderText="최초입사일" DataField="RES_Birthday">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="사번" DataField="RES_Number">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
						    <asp:boundfield HeaderText="핸드폰번호" DataField="RES_CP">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                            <asp:boundfield HeaderText="상태" DataField="RES_WorkState">
							    <HeaderStyle CssClass="tr_border"/>
							    <ItemStyle HorizontalAlign="Center" CssClass="tr_border"/>
						    </asp:boundfield>
                        </Columns>
                        <EmptyDataRowStyle Height="100px" HorizontalAlign="Center" CssClass="empty_border" />
					    <RowStyle CssClass="mepm_menu_item_bg" />
					    <HeaderStyle CssClass="mepm_menu_title_bg"/>
                    </asp:GridView>
            </div>
        </section>
    </article>
</asp:Content>
