using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

public partial class Resource_m_RES_Address : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //SetControl();
            SetPage();
        }
    }

    // 드롭다운 리스트 바인딩
    private void SetControl()
    {
        // 시/도 목록
        this.ddlSIDO.DataSource = Select_SidoList();
        this.ddlSIDO.DataBind();
    }

    #region 시/도 목록 조회
    private DataSet Select_SidoList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["CODY_CommConnectionString1"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_POST_SIDO", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

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

    // 수정 일 경우 페이지 데이터를 바인딩
    private void SetPage()
    {
        if (this.Page.Request["RES_ID"] != null)
        {
            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()), "ADD");

            if (rd.Read())
            {
                this.txtPOST.Text = rd["RES_Post"].ToString();
                this.txtAdd1.Text = rd["RES_Add1"].ToString();
                this.txtAdd2.Text = rd["RES_Add2"].ToString();

                this.hdPOST.Value = rd["RES_Post"].ToString();
                this.hdADD1.Value = rd["RES_Add1"].ToString();
            }

            rd.Close();
        }
    }

    // 조회 버튼 클릭 시
    protected void ibtnSerch_Click(object sender, EventArgs e)
    {
        this.dvPostList.Visible = true;
        SetList();
    }

    // 저장 버튼 클릭 시
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Resource resource = new Resource();

            resource.EPM_RES_DETAIL_UPDATE_ADDRESS_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()),
                                                        this.hdPOST.Value,
                                                        this.hdADD1.Value,
                                                        this.txtAdd2.Text);

            Common.scriptAlert(this.Page, "저장되었습니다.", "/Resource/m_RES_Register.aspx?RES_ID=" + this.Page.Request["RES_ID"].ToString());

            SetPage();
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    /// <summary>
    /// 검색 결과의 우편번호 선택 시
    /// </summary>
    protected void btnAddressAdd_Click(object sender, EventArgs e)
    {
        this.txtPOST.ReadOnly = false;
        this.txtAdd1.ReadOnly = false;

        this.txtPOST.Text = this.hdPOST.Value.ToString();
        this.txtAdd1.Text = this.hdADD1.Value.ToString();

        this.txtPOST.ReadOnly = true;
        this.txtAdd1.ReadOnly = true;

        this.dvPostList.Visible = false;

        this.txtAdd2.Text = "";
        this.txtAdd2.Focus();
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        //Code code = new Code();
        //DataSet ds = code.EPM_POST_SEARCH(this.txtDong.Text.ToString());

        this.gvPostList.DataSource = Select_AddressList();
        this.gvPostList.DataBind();
    }


    #region 주소 목록 조회
    private DataSet Select_AddressList()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["CODY_CommConnectionString1"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_POST_SEARCH_DORO", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        ad.SelectCommand.Parameters.AddWithValue("@SIDO_CD", this.ddlSIDO.SelectedValue.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@SEARCHTEXT", this.txtDong.Text.ToString());

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
    protected void gvPostListt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncAddressAdd('"
                                        + ((DataRowView)e.Row.DataItem)["우편번호"].ToString() + "','"
                                        + ((DataRowView)e.Row.DataItem)["전체주소"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
        }
    }
}