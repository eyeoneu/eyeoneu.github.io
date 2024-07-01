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

public partial class m_BUS_Connection : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetSupporterInfo();
        }
    }

    // 로그인 된 서포터 정보 세팅
    private void SetSupporterInfo()
    {
        if (Session["sRES_ID"] != null)
        {
            this.lblDate.Text = DateTime.Today.ToString("yyyy년 MM월 dd일");
            this.lblRES_Name.Text = Session["sRES_Name"].ToString() + " (" + Session["sRES_Number"].ToString() + ")";
            //this.lblRES_RBS_NAME.Text = Session["sRES_RBS_NAME"].ToString();
            //this.lblRES_RBS_AREA_NAME.Text = Session["sRES_RBS_AREA_NAME"].ToString();

            DataTable dt = Select_BUS_CONNECTION_IS_CNT_MOBILE();
            if (dt.Rows.Count > 0)
            {
                //if (!string.IsNullOrEmpty(dt.Rows[0]["TO_CHANGE_CNT"].ToString()))
                //{
                //    if (dt.Rows[0]["TO_CHANGE_CNT"].ToString().Trim() != "0")
                //    {
                //        this.btnTOChange.Attributes["class"] = "button orange mepm_btn";
                //        this.lblTOChange.Text = "TO 변경 (" + dt.Rows[0]["TO_CHANGE_CNT"].ToString().Trim() + ")";
                //    }
                //}

                if (!string.IsNullOrEmpty(dt.Rows[0]["TO_ROUND_CHANGE_CNT"].ToString()))
                {
                    if (dt.Rows[0]["TO_ROUND_CHANGE_CNT"].ToString().Trim() != "0")
                    {
                        this.btnRound.Attributes["class"] = "button orange mepm_btn";
                        this.lblRound.Text = "순회/격고 매장 변경 (" + dt.Rows[0]["TO_ROUND_CHANGE_CNT"].ToString().Trim() + ")";
                    }
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["AC_CHANGE_CNT"].ToString()))
                {
                    if (dt.Rows[0]["AC_CHANGE_CNT"].ToString().Trim() != "0")
                    {
                        this.btnACChange.Attributes["class"] = "button orange mepm_btn";
                        this.lblACChange.Text = "사원 정보 변경 (" + dt.Rows[0]["AC_CHANGE_CNT"].ToString().Trim() + ")";
                    }
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["ETC_CHANGE_CNT"].ToString()))
                {
                    if (dt.Rows[0]["ETC_CHANGE_CNT"].ToString().Trim() != "0")
                    {
                        this.btnEtcChange.Attributes["class"] = "button orange mepm_btn";
                        this.lblEtcChange.Text = "기타변경 (" + dt.Rows[0]["ETC_CHANGE_CNT"].ToString().Trim() + ")";
                    }
                }
                //if (!string.IsNullOrEmpty(dt.Rows[0]["TO_SUBMIT_CNT"].ToString()))
                //{
                //    if (dt.Rows[0]["TO_SUBMIT_CNT"].ToString().Trim() != "0")
                //    {
                //        this.btnTOSubmit.Attributes["class"] = "button orange mepm_btn";
                //        this.lblTOSubmit.Text = "TO증원 (" + dt.Rows[0]["TO_SUBMIT_CNT"].ToString().Trim() + ")";
                //    }
                //}
                if (!string.IsNullOrEmpty(dt.Rows[0]["ACCIDENT_REPORT_CNT"].ToString()))
                {
                    if (dt.Rows[0]["ACCIDENT_REPORT_CNT"].ToString().Trim() != "0")
                    {
                        this.btnAccident.Attributes["class"] = "button orange mepm_btn";
                        this.lblAccident.Text = "사고발생보고 (" + dt.Rows[0]["ACCIDENT_REPORT_CNT"].ToString().Trim() + ")";
                    }
                }
                
                if (!string.IsNullOrEmpty(dt.Rows[0]["APPROVAL_CNT"].ToString()))
                {
                    if (dt.Rows[0]["APPROVAL_CNT"].ToString().Trim() != "0")
                    {
                        this.btnApproval.Attributes["class"] = "button orange mepm_btn";
                        this.lblApproval.Text = "고령자근무승인 (" + dt.Rows[0]["APPROVAL_CNT"].ToString().Trim() + ")";
                    }
                }
                
                if (!string.IsNullOrEmpty(dt.Rows[0]["JOIN_CONFIRM_CNT"].ToString()))
                {
                    if (dt.Rows[0]["JOIN_CONFIRM_CNT"].ToString().Trim() != "0")
                    {
                        this.btnJoinConfirm.Attributes["class"] = "button orange mepm_btn";
                        this.lblJoinConfirm.Text = "입사확인요청 (" + dt.Rows[0]["JOIN_CONFIRM_CNT"].ToString().Trim() + ")";
                    }
                }
            }
        }
    }

    private DataTable Select_BUS_CONNECTION_IS_CNT_MOBILE()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_BUS_CONNECTION_IS_CNT_MOBILE", Con);
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

        return ds.Tables[0];
    }
}