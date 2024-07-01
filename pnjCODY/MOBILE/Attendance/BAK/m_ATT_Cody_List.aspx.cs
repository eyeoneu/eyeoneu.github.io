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

public partial class Attendance_m_ATT_Cody_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.Page.Request["scroll"] != null)
                this.hdscrollTop.Value = this.Page.Request["scroll"].ToString();

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
            //if (this.hdDate.Value.ToString().Equals(DateTime.Today.ToShortDateString()))
            //    btnBatch.Visible = true;
            //else
            //    btnBatch.Visible = false;

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
            DateTime t1 = DateTime.Today; // 오늘 날짜
            DateTime t2 = Convert.ToDateTime(this.hdDate.Value); // 목록 기준일자
            TimeSpan t3 = t2.Subtract(t1); // 날짜 차이값 구하기

            string t7 = t2.ToString().Substring(5, 2); // 목록기준월 구하기
            string t8 = t1.ToString().Substring(8, 2); // 접속일 구하기
            string t9 = t1.ToString().Substring(5, 2); // 접속월 구하기

            string t4 = t1.ToString().Substring(8, 2); // 일자 구하기
            int t5 = t3.Days; // 날짜 차이값 int 변환
            int t6 = -6; // 날짜 수정 가능일 제한 (당일 포함하여 7일까지만 수정 가능)         
            if (t4 == "02") // 2일일 경우 1일전까지만 수정 가능(이전 달 수정방지)
                t6 = -1;
            else if (t4 == "03") // 3일일 경우 2일전까지만 수정 가능(이전 달 수정방지)
                t6 = -2;
            else if (t4 == "04") // 4일일 경우 3일전까지만 수정 가능(이전 달 수정방지)
                t6 = -3;
            else if (t4 == "05") // 5일일 경우 4일전까지만 수정 가능(이전 달 수정방지)
                t6 = -4;
            else if (t4 == "06") // 6일일 경우 5일전까지만 수정 가능(이전 달 수정방지)
                t6 = -5;
            else
                t6 = -6;

            if (t5 >= t6)
            {
                divAttClosed.Visible = true;
                e.Row.Attributes["onClick"] = "fncDetail('"
                                              + ((DataRowView)e.Row.DataItem)["ATT_DAY_ID"].ToString() + "','"
                                              + this.hdDate.Value.ToString()
                                              + "');";
                e.Row.Attributes["style"] = "cursor: pointer;";
                if (t4 == "02" || t4 == "03" || t4 == "04" || t4 == "05" && t7 == t9) // 월 초 마감일 계산식 변경(+4) 추가 (2014-06-03 : 김재영) 
                {
                    if ((t5 - t6 + 4) <= 3) // 마감일 3일 이전부터 메시지 호출
                    {
                        divAttClosed.Visible = true;
                        lblAttClosed.Text = "일근태 입력 마감이 " + (t5 - t6 + 4).ToString() + "일 남았습니다.";
                    }
                    else
                        divAttClosed.Visible = false;
                }
                if ((t5 - t6 + 1) <= 3) // 마감일 3일 이전부터 메시지 호출
                {
                    divAttClosed.Visible = true;
                    lblAttClosed.Text = "일근태 입력 마감이 " + (t5 - t6 + 1).ToString() + "일 남았습니다.";
                }
                else
                    divAttClosed.Visible = false;
            }
            else
            {
                divAttClosed.Visible = true;
                e.Row.Attributes["onClick"] = "fncDetail('"
                                              + ((DataRowView)e.Row.DataItem)["ATT_DAY_ID"].ToString() + "','"
                                              + this.hdDate.Value.ToString()
                                              + "');";
                e.Row.Attributes["style"] = "cursor: pointer;";
                lblAttClosed.Text = "일근태 입력 마감, 승인 후 반영";
            }

            //// 임시 코드 시작
            ////if (t7 == "04" && t8 == "31" || t7 == "03" && t8 == "01")
            //if (t7 == "04")
            //{
            //    e.Row.Attributes["onClick"] = "fncDetail('"
            //                               + ((DataRowView)e.Row.DataItem)["ATT_DAY_ID"].ToString() + "','"
            //                               + this.hdDate.Value.ToString()
            //                               + "');";
            //    e.Row.Attributes["style"] = "cursor: pointer;";
            //    btnBatch.Visible = true;
            //}
            //else if (t5 >= t6)
            //{
            //    e.Row.Attributes["onClick"] = "fncDetail('"
            //                                  + ((DataRowView)e.Row.DataItem)["ATT_DAY_ID"].ToString() + "','"
            //                                  + this.hdDate.Value.ToString()
            //                                  + "');";
            //    e.Row.Attributes["style"] = "cursor: pointer;";
            //    btnBatch.Visible = true;
            //}
            //else
            //    btnBatch.Visible = false;
            //// 임시 코드 종료

            if (((DataRowView)e.Row.DataItem)["ATT_DAY_Code"].ToString() != "001")
                e.Row.CssClass = "mepm_menu_active_bg";
                //e.Row.Attributes["Css"] = "mepm_menu_active_bg";

            if (((DataRowView)e.Row.DataItem)["ADR_Status"].ToString() == "승인요청")
            {
                e.Row.CssClass = "mepm_menu_stone_bg";
                e.Row.Cells[3].Text = "승인요청";
            }
            if (((DataRowView)e.Row.DataItem)["ADR_Status"].ToString() == "승인반려")
            {
                e.Row.CssClass = "mepm_menu_stone_bg";
                e.Row.Cells[3].Text = "승인반려";
            }

            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        DateTime t1 = DateTime.Today; // 오늘 날짜
        DateTime t2 = Convert.ToDateTime(this.hdDate.Value); // 목록 기준일자
        TimeSpan t3 = t2.Subtract(t1); // 날짜 차이값 구하기

        string t4 = t1.ToString().Substring(8, 2); // 일자 구하기
        int t5 = t3.Days; // 날짜 차이값 int 변환
        int t6 = -6; // 날짜 수정 가능일 제한 (당일 포함하여 7일까지만 수정 가능)         
        if (t4 == "03") // 3일일 경우 2일전까지만 수정 가능(이전 달 수정방지)
            t6 = -2;
        else if (t4 == "04") // 4일일 경우 3일전까지만 수정 가능(이전 달 수정방지)
            t6 = -3;
        else if (t4 == "05") // 5일일 경우 4일전까지만 수정 가능(이전 달 수정방지)
            t6 = -4;
        else if (t4 == "06") // 6일일 경우 5일전까지만 수정 가능(이전 달 수정방지)
            t6 = -5;
        else
            t6 = -6;

        if (t5 >= t6)
        {
            Response.Redirect("/Attendance/m_ATT_Cody_Modify.aspx?ATT_DAY_ID=" + this.hdATT_DAY_ID.Value.ToString()
                            + "&DATE=" + this.hdDate.Value.ToString()
                            + "&scroll=" + this.hdscrollTop.Value.ToString());
        }
        else
        {
            Response.Redirect("/Attendance/m_ATT_Cody_Request_Modify.aspx?ATT_DAY_ID=" + this.hdATT_DAY_ID.Value.ToString()
                            + "&DATE=" + this.hdDate.Value.ToString()
                            + "&scroll=" + this.hdscrollTop.Value.ToString());
        }
    }

    /// <summary>
    /// 날짜 변경 버튼 클릭 시
    /// </summary>
    protected void btnChange_Click(object sender, EventArgs e)
    {
        this.txtDate.Text = "YYYYMMDD";

        this.lblDate.Visible = false;
        this.lblWeek.Visible = false;
        this.btnChange.Visible = false;

        this.txtDate.Visible = true;
        this.btnSubmit.Visible = true;
    }

    /// <summary>
    /// 날짜 확인 버튼 클릭 시
    /// </summary>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.lblDate.Visible = true;
        this.lblWeek.Visible = true;
        this.btnChange.Visible = true;

        this.txtDate.Visible = false;
        this.btnSubmit.Visible = false;

        if (this.txtDate.Text != "YYYYMMDD")
        {
            this.hdDate.Value = this.txtDate.Text.ToString();
            this.lblDate.Text = DateTime.Parse(this.txtDate.Text.ToString()).ToString("yyyy년 MM월 dd일");
            this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.txtDate.Text.ToString()));

            SetList();
        }
    }

    /// <summary>
    /// 이전 날짜 버튼 클릭 시
    /// </summary>
    protected void btnPreDate_Click(object sender, EventArgs e)
    {
        this.lblDate.Visible = true;
        this.lblWeek.Visible = true;
        this.btnChange.Visible = true;

        this.txtDate.Visible = false;
        this.btnSubmit.Visible = false;

        this.hdDate.Value = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(-1).ToString("yyyy-MM-dd");
        this.lblDate.Text = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(-1).ToString("yyyy년 MM월 dd일");
        this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.lblDate.Text.ToString()));

        SetList();
    }

    /// <summary>
    /// 다음 날짜 버튼 클릭 시
    /// </summary>
    protected void btnNextDate_Click(object sender, EventArgs e)
    {
        this.lblDate.Visible = true;
        this.lblWeek.Visible = true;
        this.btnChange.Visible = true;

        this.txtDate.Visible = false;
        this.btnSubmit.Visible = false;

        this.hdDate.Value = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(1).ToString("yyyy-MM-dd");
        this.lblDate.Text = DateTime.Parse(this.lblDate.Text.ToString()).AddDays(1).ToString("yyyy년 MM월 dd일");
        this.lblWeek.Text = GetStrDayOfWeek(DateTime.Parse(this.lblDate.Text.ToString()));

        SetList();
    }
    protected void btnBatch_Click(object sender, EventArgs e)
    {
        DateTime t1 = DateTime.Today; // 오늘 날짜
        DateTime t2 = Convert.ToDateTime(this.hdDate.Value); // 목록 기준일자
        TimeSpan t3 = t2.Subtract(t1); // 날짜 차이값 구하기

        string t4 = t1.ToString().Substring(8, 2); // 일자 구하기
        int t5 = t3.Days; // 날짜 차이값 int 변환
        int t6 = -6; // 날짜 수정 가능일 제한 (당일 포함하여 7일까지만 수정 가능)         
        if (t4 == "03") // 3일일 경우 2일전까지만 수정 가능(이전 달 수정방지)
            t6 = -2;
        else if (t4 == "04") // 4일일 경우 3일전까지만 수정 가능(이전 달 수정방지)
            t6 = -3;
        else if (t4 == "05") // 5일일 경우 4일전까지만 수정 가능(이전 달 수정방지)
            t6 = -4;
        else if (t4 == "06") // 6일일 경우 5일전까지만 수정 가능(이전 달 수정방지)
            t6 = -5;
        else
            t6 = -6;

        if (t5 >= t6)
        {
            Response.Redirect("/Attendance/m_ATT_Cody_List_Batch.aspx?DATE=" + this.hdDate.Value.ToString());
        }
        else
        {
            Response.Redirect("/Attendance/m_ATT_Cody_List_Request_Batch.aspx?DATE=" + this.hdDate.Value.ToString());
        }
    }
}