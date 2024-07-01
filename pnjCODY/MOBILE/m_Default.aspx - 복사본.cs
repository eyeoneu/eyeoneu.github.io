using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class m_Default : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

        txtNotice_Count.Text = "<li><a href='/Notice/m_NOT_List.aspx'><span><b style=\"color:#bbb;\">-</b> 공지 사항 </span></a></li>"; // 읽지 않은 공지 카운트 영역 ex:8

        txtNotice_Icon.Text = "<li class=\"mepm_icon_li\"><a href=\"/Notice/m_NOT_List.aspx\" class=\"mepm_icon_a\"><span class=\"mepm_icon_img bp01\"></span><span class=\"mepm_icon_txt\">공지사항</span></a></li>"; // 읽지 않은 공지 카운트 영역 ex:999

        //권한체크
        if (Session["sRES_RBS_CD"] == "1111")//관리팀
        {
            RES_GB = "A";
        }
        else if (Session["sRES_WorkGroup2"].ToString() == "220")//팀장 
        {
            RES_GB = "T";
        }
        else if (Session["sRES_WorkGroup1"].ToString() == "008")//서포터
        {
            RES_GB = "S";
            SetNoticeCount();
        }

    }

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
}