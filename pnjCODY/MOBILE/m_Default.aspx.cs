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

public partial class m_Default : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {       
		//Response.Write(Request.ServerVariables["REMOTE_ADDR"]);

        if (Session["sRES_WorkGroup1"] != null)
        {
            if (Session["sRES_WorkGroup1"].ToString() == "001" || Session["sRES_WorkGroup1"].ToString() == "008" || Session["sRES_WorkGroup1"].ToString() == "005")
            {
                this.divUser.Visible = false;
            }
            else
            {
                this.divSupporter.Visible = false;
            }
        }

        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

        txtNotice_Count.Text = "<li><a href='/Notice/m_NOT_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지 사항 </span></a></li>"; // 읽지 않은 공지 카운트 영역 ex:8

        txtNotice_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Notice/m_NOT_List.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp01\"></span><span class=\"mepm_icon_txt\">공지사항</span></a></li>"; // 읽지 않은 공지 카운트 영역 ex:999

         
		txtLeave_Count.Text = "<li><a href='/Attendance/m_ATT_Closed_Leave_Mng.aspx'><span><b style=\"color:#bbb;\">-</b> 연차 신청서 관리 </span></a></li>";

        txtLeave_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Attendance/m_ATT_Closed_Leave_Mng.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp05\"></span><span class=\"mepm_icon_txt\">연차신청서</span></a></li>";


        //권한체크
        if (Session["sRES_RBS_CD"].ToString() == "1111")//관리팀
        {
            RES_GB = "A";
        }
        else if (Session["sRES_WorkGroup2"].ToString() == "220")//팀장 
        {
            RES_GB = "T";
        }
        else if (Session["sRES_WorkGroup1"].ToString() == "008" || Session["sRES_WorkGroup1"].ToString() == "005") //서포터, 매니저
        {
            RES_GB = "S";
            SetNoticeCount();
            SetLeaveCount();
            SetNoticeWorkerCount();
            this.divGv.Visible = true;
            SetList();
        }
        else   
        {
          
           SetNoticeWorkerCount();
        }
        
        AddReadCount();
    }

    // 공고문 페이지 접속 기록
    protected void AddReadCount()
    {

        bool bError = false;
        SqlConnection ConAtt = null;
        SqlTransaction trans = null;

        try
        {
            ConAtt = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
            ConAtt.Open();

            //Response.Write(delAtt[i].ToString());
            SqlCommand CmdAtt = new SqlCommand("ACCESS_INFO_INSERT", ConAtt);
            CmdAtt.CommandType = CommandType.StoredProcedure;
            trans = ConAtt.BeginTransaction();
            CmdAtt.Transaction = trans;

            CmdAtt.Parameters.AddWithValue("@ACCESS_RES_ID", Session["sRES_ID"]);
            CmdAtt.Parameters.AddWithValue("@ACCESS_IP", Request.ServerVariables["REMOTE_ADDR"]);

            CmdAtt.ExecuteNonQuery();

            trans.Commit();
        }
        catch (Exception ex)
        {
            bError = true;
            trans.Rollback();
            Response.Write(ex.Message);
        }

        finally
        {
            if (ConAtt != null)
            {
                ConAtt.Close();
                ConAtt = null;
            }
        }

    }

    // 공고문 확인 버튼 클릭
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
		 this.divNotice.Visible = false;
    }

	// 연차신청 카운트
    private void SetLeaveCount()
    {
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_Leave();

            string cnt = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
                {
                    cnt = ds.Tables[0].Rows[0]["CNT"].ToString();

                    txtLeave_Count.Text = "<li><a href='/Attendance/m_ATT_Closed_Leave_Mng.aspx'><span><b style=\"color:#bbb;\">-</b> 연차 신청서 관리 <b class='sDiv'>"
                                                + cnt + "</b></span></a></li>";
                    txtLeave_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Attendance/m_ATT_Closed_Leave_Mng.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp03\"></span><span class=\"mepm_icon_txt\">연차신청서</span><b class=\"sDiv\">"
                                             + cnt + "</b></a></li>";
                }
            }
        }
    }

	#region 연차신청 갯수 조회
    private DataSet Select_Leave()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ATT_REQ_LEAVE_CNT_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());

        DataSet ds = new DataSet();

        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }
    #endregion

    // 공지사항 카운트
    private void SetNoticeCount()
    {
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_List();

            string cnt = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
                {
                    cnt = ds.Tables[0].Rows[0]["CNT"].ToString();

                    txtNotice_Count.Text = "<li><a href='/Notice/m_NOT_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지 사항 <b class='sDiv'>"
                                                + cnt + "</b></span></a></li>";
                    txtNotice_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Notice/m_NOT_List.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp01\"></span><span class=\"mepm_icon_txt\">공지사항</span><b class=\"sDiv\">"
                                             + cnt + "</b></a></li>";
                }
            }
            
        }
    }
  

    #region 목록 조회
    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ETC_NOT_CNT_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());

        DataSet ds = new DataSet();

        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }
    #endregion


    // 그리드뷰 DataBound
    public void SetList()
    {
        DataSet ds = null;
        ds = Select_List_NOT();

        if(ds.Tables[0].Rows.Count != 0)
        {
            this.gvNoticeList_set.DataSource = ds;
            this.gvNoticeList_set.DataBind();
        }
        else
        {
            this.divGv.Visible = false;
        }


        
        
    }
    
      //공지사항(근로자) 카운트
   private void SetNoticeWorkerCount()
    {
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_Notice_WorkerList();

            string cnt = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (int.Parse(ds.Tables[0].Rows[0]["CNT"].ToString()) > 0)
                {
                    cnt = ds.Tables[0].Rows[0]["CNT"].ToString();
                    if(Session["sRES_WorkState"].ToString() != "004")
                    {

                    txtNotice_Worker_Count.Text = "<li><a href='/Notice/m_NOT_WORKER_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지 사항<b class='sDiv'>"
                                                + cnt + "</b></span></a></li>";
					txtNotice_Worker_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Notice/m_NOT_WORKER_List.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp01\"></span><span class=\"mepm_icon_txt\">공지사항</span><b class=\"sDiv\">"
										 + cnt + "</b></a></li>";  										 
                    txtNotice_Worker_Support_Count.Text = "<li><a href='/Notice/m_NOT_WORKER_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지사항(근로자)<b class='sDiv'>"
                                                + cnt + "</b></span></a></li>";
			        }
										                          
                   
                }
                else
                {    
                     if(Session["sRES_WorkState"].ToString() != "004")
                     {
                     txtNotice_Worker_Support_Count.Text = "<li><a href='/Notice/m_NOT_WORKER_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지사항(근로자) </span></a></li>";
                     txtNotice_Worker_Count.Text = "<li><a href='/Notice/m_NOT_WORKER_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지 사항 </span></a></li>";
                     txtNotice_Worker_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Notice/m_NOT_WORKER_List.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp01\"></span><span class=\"mepm_icon_txt\">공지사항</span></a></li>";                            
                     }
                }
            }
                                                                           
        }
    }
    
     #region 목록 조회
    private DataSet Select_Notice_WorkerList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ETC_NOT_WORKER_CNT_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());

        DataSet ds = new DataSet();

        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }
    #endregion

    #region 목록 조회
    public DataSet Select_List_NOT()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ETC_NOT_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_GB", RES_GB);
        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@SWITCH", 0);

        DataSet ds = new DataSet();
        
        try
        {
            ad.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }
    #endregion

    // 그리드뷰 DataBound 시
    public void gvNoticeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["ETC_NOT_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";

            //신규 공지사항 아이콘
            if (((DataRowView)e.Row.DataItem)["READ_YN"].ToString() == "N")
                e.Row.Cells[1].Text = ((DataRowView)e.Row.DataItem)["ETC_NOT_Title"].ToString() + " <span style=\"color:#FF0000; font-size:0.9em;\">[" + ((DataRowView)e.Row.DataItem)["ETC_NOT_Comment"].ToString() + "]</span> " + "<b class='sDiv'>N</b>";
            else
                e.Row.Cells[1].Text = ((DataRowView)e.Row.DataItem)["ETC_NOT_Title"].ToString() + " <span style=\"color:#FF0000; font-size:0.9em;\">[" + ((DataRowView)e.Row.DataItem)["ETC_NOT_Comment"].ToString() + "]</span> ";

            if (((DataRowView)e.Row.DataItem)["DEADLINE_DATE"].ToString() == "")
                e.Row.Cells[3].Text = ((DataRowView)e.Row.DataItem)["CREATED_DATE"].ToString();
            else
                e.Row.Cells[3].Text = ((DataRowView)e.Row.DataItem)["CREATED_DATE"].ToString() + "<br/><span style=\"color:#666;\">(" + ((DataRowView)e.Row.DataItem)["DEADLINE_DATE"].ToString() + ")</span>";
        }
    }

    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("Notice/m_NOT_Write.aspx?ETC_NOT_ID=" + this.hdETC_NOT_ID.Value.ToString());
    }


}
