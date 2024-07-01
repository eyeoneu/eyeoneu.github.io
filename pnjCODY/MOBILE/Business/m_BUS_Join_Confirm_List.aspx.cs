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


public partial class m_BUS_Join_Confirm_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetgvResList();
        }
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Business/m_BUS_Join_Confirm_Report.aspx?APP_ID=" + this.hdAPP_ID.Value.ToString());
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {
            this.txtStartDate.Text = DateTime.Today.ToShortDateString().ToString().Substring(0, 8) + "01";
            this.txtFinishDate.Text = DateTime.Today.ToShortDateString();

            DataTable dt = Select_Market_List();

            this.gvList.DataSource = dt;
            this.gvList.DataBind();
        }
    }

    // 그리드뷰 DataBound 시
    protected void gvResList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["APP_ID"].ToString()
                                       + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }

    private DataTable Select_Market_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_APPROVAL_JOIN_CONFIRM_LIST_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@START_DATE", txtStartDate.Text);
        ad.SelectCommand.Parameters.AddWithValue("@FINISH_DATE", txtFinishDate.Text);

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
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DataTable dt = Select_Market_List();

        this.gvList.DataSource = dt;
        this.gvList.DataBind();
    }
}