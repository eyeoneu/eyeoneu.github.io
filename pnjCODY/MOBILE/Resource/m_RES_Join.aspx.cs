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
using System.IO;

public partial class Resource_m_RES_Join : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.hdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

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
            //this.lblRES_Number.Text = rd["RES_Number"].ToString();
            this.lblRES_CP.Text = rd["RES_CP"].ToString();
            this.imgRES_Picture.Visible = true;
            this.imgRES_Picture.ImageUrl = @"/Resource/RES_PIC/" + rd["RES_Picture"].ToString();
        }

        rd.Close();
    }

    // 입사요청 버튼 클릭 시
    protected void btnJoin_Click(Object s, EventArgs e)
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            try
            {
                Resource resource = new Resource();

                resource.EPM_RES_DETAIL_UPDATE_WORKSTATE_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                                                   "001",
                                                                                   this.txtRES_JoinDate.Text.ToString(),
                                                                                   "",
                                                                                   int.Parse(Session["sRES_ID"].ToString()));

                Common.scriptAlert(this.Page, "처리되었습니다.", "/Resource/m_RES_Mng.aspx?RES_ID=" + this.Page.Request["RES_ID"]);
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        else
        {
            Common.scriptAlert(this.Page, "잘못된 접근 입니다.");
            Response.Redirect("/m_Default.aspx");
        }
    }
}