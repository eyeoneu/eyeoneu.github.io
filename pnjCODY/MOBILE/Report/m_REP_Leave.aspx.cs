﻿using System;
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

public partial class m_REP_Leave : System.Web.UI.Page
{
    int mile = 0;
    DateTime attDateTime;
    double startMile = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetPage();
        }
    }

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            DataTable dt = Select_Att_Is_Saved();

            if (dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["ATT_DATETIME"].ToString()))
                {
                    Common.scriptAlert(this.Page, "출근 기록이 없습니다.", "m_REP_Daily_Mng_Drive.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
                }
                else
                {
                    attDateTime = DateTime.Parse(dt.Rows[0]["ATT_DATETIME"].ToString());
                    //startMile = double.Parse(dt.Rows[0]["START_MILE"].ToString());
                    //Page.Response.Write(startMile);
                    if (dt.Rows[0]["IS_ATT_SAVED"].ToString().Trim().Equals("1"))
                    {
                        this.txtBefore.Text = dt.Rows[0]["START_MILE"].ToString();
                        this.txtMile.Text = dt.Rows[0]["FINISH_MILE"].ToString();
                        this.lblFinish.Text = " : " + dt.Rows[0]["ATT_FINISH_DATETIME"].ToString();
                        this.lblFinish.Visible = true;
                    }
                    else
                        this.lblFinish.Visible = false;
                }
            }
        }
    }

    private DataTable Select_Att_Is_Saved()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ATT_IS_SAVED_MOBILE", Con);
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

    // 저장 버튼 클릭
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool bError = false;
        SqlConnection Con = null;
        SqlTransaction trans = null;

        if (DateTime.Compare(attDateTime, DateTime.Now) > 0)  // 출근시간보다 퇴근시간이 선행일 경우
        {
            Common.scriptAlert(this.Page, "퇴근 시간이 출근시간보다 빠를 수 없습니다.", "m_REP_Daily_Report_Main.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
        //else if (startMile <= double.Parse(txtMile.Text))
        //{
        //    Common.scriptAlert(this.Page, "출근시 입력한 운행 거리보다 적을 수 없습니다.", "m_REP_Daily_Report_Main.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        //}
        else
        {
            try
            {
                Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
                Con.Open();

                SqlCommand CmdRemark = new SqlCommand("EPM_DAILY_LEAVE_INSERT", Con);
                CmdRemark.CommandType = CommandType.StoredProcedure;
                trans = Con.BeginTransaction();
                CmdRemark.Transaction = trans;

                CmdRemark.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());

                if (!string.IsNullOrEmpty(txtMile.Text))
                    CmdRemark.Parameters.AddWithValue("@MILE", txtMile.Text);

                CmdRemark.ExecuteNonQuery();

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
                if (Con != null)
                {
                    Con.Close();
                    Con = null;
                }
            }
        }

        if (!bError)
        {
            Common.scriptAlert(this.Page, "저장되었습니다.", "m_REP_Daily_Report_Main.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());
        }
    }
}