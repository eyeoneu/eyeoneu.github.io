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

public partial class m_ATT_Cody_List_Batch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (this.Page.Request["scroll"] != null)
            //    this.hdscrollTop.Value = this.Page.Request["scroll"].ToString();

            SetControl();
            SetList();
        }
    }

    // 기본 정보 세팅
    private void SetControl()
    {
        if (this.Page.Request["DATE"] != null)
        {
            this.lblDate.Text = DateTime.Parse(this.Page.Request["DATE"].ToString()).ToString("yyyy년 MM월 dd일");
            this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.Page.Request["DATE"].ToString()));
            this.hdDate.Value = DateTime.Parse(this.Page.Request["DATE"].ToString()).ToString("yyyy-MM-dd");
        }
        else
        {
            this.lblDate.Text = DateTime.Now.ToString("yyyy년 MM월 dd일");
            this.lblWeek.Text = GetStrDayOfWeek(DateTime.Now);
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        }

        Code code = new Code();
        DataSet ds = code.EPM_CODE("7");

        int intindex = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string CodeName = dr["COD_Name"].ToString();
            string Code = dr["COD_CD"].ToString();

            if (Code != "")
            {
                ListItem tempItem = new ListItem(CodeName, Code);
                this.ddlATT_DAY_Code.Items.Add(tempItem);

                // 코드 값이 100 이상인 경우 해당 항목을 비활성화 한다.
                if (int.Parse(Code) > 100)
                    this.ddlATT_DAY_Code.Items[intindex].Enabled = false;

                intindex = intindex + 1;
            }
        }
    }

    // 한글 요일 변환
    public static string GetStrDayOfWeek(DateTime a_odtDateTime)
    {
        switch (a_odtDateTime.DayOfWeek)
        {
            case DayOfWeek.Monday:
                return "(월)";
            case DayOfWeek.Tuesday:
                return "(화)";
            case DayOfWeek.Wednesday:
                return "(수)";
            case DayOfWeek.Thursday:
                return "(목)";
            case DayOfWeek.Friday:
                return "(금)";
            case DayOfWeek.Saturday:
                return "(토)";
            case DayOfWeek.Sunday:
                return "(일)";
        }
        return "";
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            Attendance attendance = new Attendance();
            DataSet ds = attendance.EPM_ATT_BY_DAY_SELECT_MOBILE(this.hdDate.Value.ToString(), "C", Session["sRES_RBS_CD"].ToString(), Session["sRES_RBS_AREA_CD"].ToString());

            this.gvCodyAttList.DataSource = ds;
            this.gvCodyAttList.DataBind();

            DataRow[] drCODY_ATT_CNT = ds.Tables[0].Select("RES_ASS_ID IS NOT NULL AND ATT_DAY_Type = 'A'");
            DataRow[] drCODY_ABS_CNT = ds.Tables[0].Select("RES_ASS_ID IS NOT NULL AND ATT_DAY_Type = 'B'");
            DataRow[] drCODY_HOL_CNT = ds.Tables[0].Select("RES_ASS_ID IS NOT NULL AND ATT_DAY_Type = 'C'");

            this.lblCODY_ATT_CNT.Text = drCODY_ATT_CNT.Length.ToString();
            this.lblCODY_ABS_CNT.Text = drCODY_ABS_CNT.Length.ToString();
            this.lblCODY_HOL_CNT.Text = drCODY_HOL_CNT.Length.ToString();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvCodyAttList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            if (((DataRowView)e.Row.DataItem)["ATT_DAY_Code"].ToString() != "001")
                e.Row.CssClass = "mepm_menu_active_bg";
            if (((DataRowView)e.Row.DataItem)["ATT_DAY_Code_Name"].ToString() == "경조휴가"
                || ((DataRowView)e.Row.DataItem)["ATT_DAY_Code_Name"].ToString() == "유급휴가"
                || ((DataRowView)e.Row.DataItem)["ATT_DAY_Code_Name"].ToString() == "무급휴가"
                || ((DataRowView)e.Row.DataItem)["ATT_DAY_Code_Name"].ToString() == "연차")
            {
                CheckBox chk = (CheckBox)(e.Row.FindControl("cbCheck"));
                chk.Enabled = false;
            }
        }
    }

    //protected void gvCodyAttList_List_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        CheckBox cbCheckAll = (CheckBox)e.Row.FindControl("cbCheckAll");
    //        cbCheckAll.Attributes.Add("onClick", "checkAll(this);");
    //    }

    //    //gvRowColor(e);
    //}


    protected void btnList_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Attendance/m_ATT_Cody_List.aspx?DATE=" + this.hdDate.Value.ToString());
    }

    protected void cbCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;
        CheckBox chk;

        foreach (GridViewRow rowItem in gvCodyAttList.Rows)
        {
            if (chkAll.Checked == true)
            {   
                chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));
                    if (chk.Enabled == true)
                        chk.Checked = true;
            }
            else
            {
                chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));
                if (chk.Enabled == true)
                    chk.Checked = false;
            }
        }
    }

    protected void cbCheck_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk;

        foreach (GridViewRow rowItem in gvCodyAttList.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));
            if (chk.Checked == true)
                rowItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#939393");
            else
                rowItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
        }
    }

    //protected void gvCodyAttList_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    foreach (GridViewRow row in gvCodyAttList.Rows)
    //    {
    //        if (row.RowIndex == gvCodyAttList.SelectedIndex)
    //        {
    //            row.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
    //        }
    //        else
    //        {
    //            row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
    //        }
    //    }
    //}
    protected void gvCodyAttList_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        //string rowID = String.Empty;

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    rowID = "row" + e.Row.RowIndex;

        //    e.Row.Attributes.Add("id", "row" + e.Row.RowIndex);
        //    e.Row.Attributes.Add("onclick", "ChangeRowColor(" + "'" + rowID + "'" + ")");
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CheckBox chk;
        HiddenField hdfield;

        Attendance attendance = new Attendance();

        int cnt = 0;

        foreach (GridViewRow rowItem in gvCodyAttList.Rows)
        {
            chk = (CheckBox)(rowItem.Cells[0].FindControl("cbCheck"));

            if (chk.Checked == true)
            {
                hdfield = (HiddenField)(rowItem.Cells[0].FindControl("hdDayId"));

                attendance.EPM_ATT_BY_DAY_ITEM_UPDATE_MOBILE("C", this.ddlATT_DAY_Code.SelectedValue.ToString(), "", int.Parse(hdfield.Value.ToString()));
                cnt++;
            }
        }

        if (cnt > 0)
        {
            Common.scriptAlert(this.Page, "저장되었습니다.");
            SetList();
        }
        else
            Common.scriptAlert(this.Page, "변경할 항목을 선택하세요.");
    }
}