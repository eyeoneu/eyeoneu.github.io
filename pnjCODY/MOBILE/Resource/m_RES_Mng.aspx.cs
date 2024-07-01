using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;

public partial class Resource_m_RES_Mng : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetPage();
        }
    }

    // 사원 기본 정보 세팅
    private void SetPage()
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        if (rd.Read())
        {
            this.lblRES_Name.Text = rd["RES_Name"].ToString();
            this.lblRES_PersonNumber.Text = rd["RES_PersonNumber"].ToString();
            this.lblRES_Number.Text = rd["RES_Number"].ToString();
            this.lblRES_WorkState.Text = rd["RES_WorkState"].ToString();

            // 퇴사요청 또는 퇴사확인 상태가 아닐 경우
            if (rd["RES_WorkState_Code"].ToString() != "003" && rd["RES_WorkState_Code"].ToString() != "004")
            {
                // 선택 사원이 코디직 일 경우
                if (rd["RES_WorkGroup1"].ToString() == "002" || rd["RES_WorkGroup1"].ToString() == "003")
                {
                    this.btnAssign.CssClass = "button skyblue mepm_asp_btn";
                    this.btnAssign.Visible = true;
                    this.btnAssign_disabled.Visible = false;
                    
                    this.btnRetire.Visible = true;
                    this.btnRetire_disabled.Visible = false;
                    this.btnPay.Visible = true;
                    this.btnPay_disabled.Visible = false;
                }

                // 선택 사원이 코디 또는 AR직 일 경우
                if (rd["RES_WorkGroup1"].ToString() == "002" || rd["RES_WorkGroup1"].ToString() == "004" || rd["RES_WorkGroup1"].ToString() == "009" || rd["RES_WorkGroup1"].ToString() == "012")
                {
                    this.btnCont.CssClass = "button skyblue mepm_asp_btn";
                    this.btnCont.Visible = true;
                    this.btnCont_disabled.Visible = false;
                    
                    this.btnRetire.Visible = true;
                    this.btnRetire_disabled.Visible = false;
                    this.btnPay.Visible = true;
                    this.btnPay_disabled.Visible = false;
                }
            }

            // 입사 요청 이전 (값이 Null) 일 때만 입사 요청을 할 수 있다.
            if (rd["RES_WorkState_Code"].ToString() == "")
            {
                
                this.btnJoin.CssClass = "button skyblue mepm_asp_btn";
                this.btnJoin.Visible = true;
                this.btnJoin_disabled.Visible = false;
            }
            else
            {
                // 입사 승인 상태 일 때만 퇴사요청을 할 수 있다.
                if (rd["RES_WorkState_Code"].ToString() == "002")
                {
                    this.btnResign.CssClass = "button skyblue mepm_asp_btn";
                    this.btnResign.Visible = true;
                    this.btnResign_disabled.Visible = false;
                }
            }


            // 입사승인 이전 단계일 경우 서포터가 사원을 삭제 할 수 있다.
            if (rd["RES_WorkState_Code"].ToString() == "" || rd["RES_WorkState_Code"].ToString() == "001")
                this.btnDel.Visible = true;
        }

        rd.Close();
    }

    // 코디배정 버튼 클릭 시
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Assignment.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }

    // AR계약 버튼 클릭 시
    protected void btnCont_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Contract.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }

    // 입사요청 버튼 클릭 시
    protected void btnJoin_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Join.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }

    // 퇴사요청 버튼 클릭 시
    protected void btnResign_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Resign.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }

    // 퇴직금 대상 버튼 클릭 시
    protected void btnRetire_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Resource/m_RES_Retire_Detail.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }

    // 제증명 신청 버튼 클릭 시
    protected void btnEmployment_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Document/m_EXP_Employment_Mng_List.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }


    // 급여 명세서 버튼 클릭 시
    protected void btnPay_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Pay/m_Pay_Cheak.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
    }

    // 삭제 버튼 클릭 시
    protected void btnDel_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        resource.EPM_RES_DETAIL_SUBMIT_BASIC_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

        Common.scriptAlert(this.Page, "삭제되었습니다.", "/Resource/m_RES_MyList.aspx");
    }
}