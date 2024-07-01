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
using System.Drawing;

public partial class MOBILE_Notice_m_NOT_List : System.Web.UI.Page
{
    // 관리팀,팀장,서포터(A,T,S)로 구분
    string RES_GB = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        btnWrite.Enabled = false;
        if (Session["sRES_ID"] == null)
            Response.Redirect("/m_Login_form.aspx");

        //권한체크
        if (Session["sRES_RBS_CD"].ToString() == "1111" || Session["sRES_RBS_CD"].ToString() == "5000") //관리팀 , 201512 코디지원부문 추가 - 이용현
        {
            RES_GB = "A";             
        }
        else if (Session["sRES_WorkGroup2"].ToString() == "220")//팀장 
        {
            RES_GB = "T";
        }
        else if (Session["sRES_WorkGroup1"].ToString() == "008" || Session["sRES_WorkGroup1"].ToString() == "005")//서포터, 매니저
        {
            RES_GB = "S";
            this.btnWrite.Enabled = false;
        }

        if (!IsPostBack)
        {
            SetControl();
            SetddlCategory();
            SetList();            
        }
    }

    // 컨트롤 세팅
    private void SetControl()
    {
        this.txtFROMDATE.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.txtTODATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
    }

    // 그리드뷰 DataBound
    private void SetList()
    {
        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_List();

            this.gvNoticeList.DataSource = ds;
            this.gvNoticeList.DataBind();
        }
    }

    #region 목록 조회
    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_ETC_NOT_LIST_WORKER_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

        
        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
        ad.SelectCommand.Parameters.AddWithValue("@FROM", this.txtFROMDATE.Text.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@TO", this.txtTODATE.Text.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@SEARCH_GB", this.ddlSearchGB.SelectedValue.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@SEARCH_TEXT", this.txtSearch_Text.Text.ToString());
        ad.SelectCommand.Parameters.AddWithValue("@ETC_CATEGORY", this.ddl_Category.SelectedValue.ToString());
        
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

    // 검색 버튼 클릭 시
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetList();
    }


    // 그리드뷰 DataBound 시
    protected void gvNoticeList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes["onClick"] = "fncDetail('"
                                        + ((DataRowView)e.Row.DataItem)["ETC_NOT_ID"].ToString()
                                        + "');";
            e.Row.Attributes["style"] = "cursor: pointer;";
            
            //신규 공지사항 아이콘
            if (((DataRowView)e.Row.DataItem)["READ_YN"].ToString() == "N")
                e.Row.Cells[1].Text = ((DataRowView)e.Row.DataItem)["ETC_NOT_Title"].ToString();
                e.Row.Cells[1].Text = ((DataRowView)e.Row.DataItem)["ETC_NOT_Title"].ToString();

          
                e.Row.Cells[2].Text = ((DataRowView)e.Row.DataItem)["CREATED_DATE"].ToString();
        
			
			
        }
    }

    /// <summary>
    /// 상세 페이지로 이동
    /// </summary>
    protected void btnDetail_Click(object sender, EventArgs e)
    {
        Response.Redirect("m_NOT_WORKER_Write.aspx?ETC_NOT_ID=" + this.hdETC_NOT_ID.Value.ToString());
    }


    #region EPM 코드리스트 조회
    private DataSet Select_CodeList(string Code_Category)
    {
     
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter adp = new SqlDataAdapter("EPM_CODE", Con);
        adp.SelectCommand.CommandType = CommandType.StoredProcedure;

        adp.SelectCommand.Parameters.AddWithValue("@CODE_CATEGORY", Code_Category);

        DataSet ds = new DataSet();

        try
        {
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

        Con.Close();
        return ds;
    }
    #endregion

    #region 카테고리 DDL 바인딩
    protected void SetddlCategory()
    {
        DataSet ds = null;
        ds = Select_CodeList("24");

        string code_name = "";
        string code_value = "";

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            code_name = dr["COD_NAME"].ToString();
            code_value = dr["COD_CD"].ToString();

            ListItem tempItem = new ListItem(code_name, code_value);
            ddl_Category.Items.Add(tempItem);
        }
    }
    #endregion
}