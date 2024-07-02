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


public partial class Resource_m_RES_Employment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
            SetPage();
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        // 고용형태
        Code code = new Code();
        DataSet dsRES_WorkType = code.DZICUBE_CODE("WT");

        this.ddlRES_WorkType.DataSource = dsRES_WorkType;
        this.ddlRES_WorkType.DataBind();

        // 부문 LV1
        DataSet dsRES_RBS_Lv1 = code.EPM_RES_RBS_LIST("DEPT", "");

        this.ddlRES_RBS_Lv1.DataSource = dsRES_RBS_Lv1;
        this.ddlRES_RBS_Lv1.DataBind();

        // 직종
        DataSet dsRES_WorkGroup1 = code.DZICUBE_CODE_BY_WORKTYPE(this.ddlRES_WorkType.SelectedValue.ToString());

        this.ddlRES_WorkGroup1.DataSource = dsRES_WorkGroup1;
        this.ddlRES_WorkGroup1.DataBind();

        //// 직급
        //DataSet dsRES_WorkGroup2 = code.DZICUBE_CODE("G4");

        //this.ddlRES_WorkGroup2.DataSource = dsRES_WorkGroup2;
        //this.ddlRES_WorkGroup2.DataBind();

        // 직책
        DataSet dsRES_WorkGroup3 = code.DZICUBE_CODE("G3");

        this.ddlRES_WorkGroup3.DataSource = dsRES_WorkGroup3;
        this.ddlRES_WorkGroup3.DataBind();
    }

    // 부서 LV1 변경 시
    protected void ddlRES_RBS_Lv1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlRES_RBS_Lv2.Items.Clear();

        Code code = new Code();
        DataSet ds = code.EPM_RES_RBS_LIST("AREA", this.ddlRES_RBS_Lv1.SelectedValue.ToString());

        this.ddlRES_RBS_Lv2.DataSource = ds;
        this.ddlRES_RBS_Lv2.DataBind();
    }

	// 고용형태 변경 시
	protected void ddlRES_WorkType_SelectedIndexChanged(object sender, EventArgs e)
	{
        this.ddlRES_WorkGroup1.Items.Clear();

        Code code = new Code();
        DataSet ds = code.DZICUBE_CODE_BY_WORKTYPE(this.ddlRES_WorkType.SelectedValue.ToString());

        this.ddlRES_WorkGroup1.DataSource = ds;
        this.ddlRES_WorkGroup1.DataBind();
        
        this.ddlRES_WorkGroup2.Items.Clear();

        DataSet ds1 = code.DZICUBE_CODE_BY_WORKGROUP1(this.ddlRES_WorkGroup1.SelectedValue.ToString());

        this.ddlRES_WorkGroup2.DataSource = ds1;
        this.ddlRES_WorkGroup2.DataBind();
	}

    // 직급 변경 시
    protected void ddlRES_WorkGroup1_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlRES_WorkGroup2.Items.Clear();

        Code code = new Code();
        DataSet ds = code.DZICUBE_CODE_BY_WORKGROUP1_EMPLOYMENT(this.ddlRES_WorkGroup1.SelectedValue.ToString(), this.ddlRES_WorkType.SelectedValue.ToString());

        this.ddlRES_WorkGroup2.DataSource = ds;
        this.ddlRES_WorkGroup2.DataBind();
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "EMP");

            if (rd.Read())
            {
                this.lblRES_Name.Text = rd["RES_Name"].ToString();
                this.ddlRES_WorkType.SelectedValue = rd["RES_WorkType"].ToString();

                if (rd["RES_RBS_CD"].ToString() != "")
                    this.ddlRES_RBS_Lv1.SelectedValue = rd["RES_RBS_CD"].ToString();
                else
                    this.ddlRES_RBS_Lv1.SelectedValue = Session["sRES_RBS_CD"].ToString();

                this.ddlRES_WorkGroup1.ClearSelection();
                Code code = new Code();
                DataSet ds = code.EPM_RES_RBS_LIST("AREA", this.ddlRES_RBS_Lv1.SelectedValue.ToString());

                this.ddlRES_RBS_Lv2.DataSource = ds;
                this.ddlRES_RBS_Lv2.DataBind();

                if (rd["RES_RBS_AREA_CD"].ToString() != "")
                    this.ddlRES_RBS_Lv2.SelectedValue = rd["RES_RBS_AREA_CD"].ToString();
                else
                    this.ddlRES_RBS_Lv2.SelectedValue = Session["sRES_RBS_AREA_CD"].ToString();

                this.ddlRES_RBS_Lv1.Enabled = false;
                this.ddlRES_RBS_Lv2.Enabled = false;

                this.ddlRES_WorkGroup1.SelectedValue = rd["RES_WorkGroup1"].ToString();

                this.ddlRES_WorkGroup2.Items.Clear();

                DataSet dsG4 = code.DZICUBE_CODE_BY_WORKGROUP1(this.ddlRES_WorkGroup1.SelectedValue.ToString());

                this.ddlRES_WorkGroup2.DataSource = dsG4;
                this.ddlRES_WorkGroup2.DataBind();

                this.ddlRES_WorkGroup2.SelectedValue = rd["RES_WorkGroup2"].ToString();
                this.ddlRES_WorkGroup3.SelectedValue = rd["RES_WorkGroup3"].ToString();
            }

            rd.Close();
        }
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        try
        {
            // 삽입
            if (this.Page.Request["RES_ID"] != null)
            {
                resource.EPM_RES_DETAIL_UPDATE_EPMLOYMENT_MOBILE
                                                            (int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                            this.ddlRES_WorkType.SelectedValue.ToString(),
                                                            this.ddlRES_RBS_Lv1.SelectedValue.ToString(),
                                                            this.ddlRES_RBS_Lv2.SelectedValue.ToString(),
                                                            this.ddlRES_WorkGroup1.SelectedValue.ToString(),
                                                            this.ddlRES_WorkGroup2.SelectedValue.ToString(),
                                                            this.ddlRES_WorkGroup3.SelectedValue.ToString()
                                                            );

                Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Register.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());

                SetPage();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}