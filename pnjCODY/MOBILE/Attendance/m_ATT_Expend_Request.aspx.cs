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

public partial class MOBILE_Attendance_M_ATT_Expend_Request : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();

        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        // 고용형태
        Code code = new Code();
        DataSet dsREQ_TYPE = code.EPM_CODE("9");

        this.ddlREQ_TYPE.DataSource = dsREQ_TYPE;
        this.ddlREQ_TYPE.DataBind();
    }

    // 검색 버튼 클릭
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetgvResList();
        this.dvResList.Visible = true;
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            Resource resource = new Resource();
            DataSet ds = resource.EPM_RES_LIST_MOBILE(this.txtRES_Name.Text.ToString(), "005", "", "");

            this.gvResList.DataSource = ds;
            this.gvResList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncSelectRes('"
                                        + ((DataRowView)e.Row.DataItem)["RES_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }


    /// <summary>
    /// 휴무 신청서 대상 선택 시
    /// </summary>
    protected void btnSelectRes_Click(object sender, EventArgs e)
    {
        Resource resource = new Resource();

        SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.hdRES_ID.Value.ToString()), "REQ");

        if (rd.Read())
        {
            this.lblREQ_RES_ID.Text = rd["RES_ID"].ToString();
            this.lblRES_RBS_NAME.Text = rd["RES_RBS_NAME"].ToString();
            this.lblRES_WorkGroup1_NAME.Text = rd["RES_WorkGroup1_NAME"].ToString();
            this.lblRES_RBS_AREA_NAMEE.Text = rd["RES_RBS_AREA_NAME"].ToString();
            this.lblRES_JoinDate.Text = DateTime.Parse(rd["RES_JoinDate"].ToString()).ToString("yyyy-MM-dd");
        }


        this.dvResList.Visible = false;
    }
}
