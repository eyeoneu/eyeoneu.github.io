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

public partial class MOBILE_Pay_m_Pay_Cheak : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sRES_ID"] == null && this.Page.Request["RES_ID"] != null)
            Response.Redirect("/m_Login_form.aspx");

        if (!IsPostBack)
        {
            SetControl();
            
            SetList();
        }

    }

    // 컨트롤 세팅
    private void SetControl()
    {
        //// 이전 소스
        //string lastMonth = DateTime.Now.AddMonths(-2).ToString("yyyy-MM");
        //string preMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
        //string thisMonth = DateTime.Now.ToString("yyyy-MM");

        //ListItem firstItem = new ListItem(thisMonth, thisMonth);
        //ddlYYYYMM.Items.Insert(0, firstItem);

        //ListItem Item = new ListItem(preMonth, preMonth);
        //ddlYYYYMM.Items.Insert(1, Item);

        //ListItem Item2 = new ListItem(lastMonth, lastMonth);
        //ddlYYYYMM.Items.Insert(2, Item2);

        // 조회년월 DDL 값 변경: 매년 01월, 02월 시 작년도 12개월을 포함하여 나타내고, 나머지 월에는 최근 3개월만 나타낸다. (2013-01-23, 정창화 수정)
        string thisMonth = DateTime.Now.ToString("yyyy-MM").Substring(5, 2);
        int YYYYMMCnt = 0;
        if (thisMonth == "01")
            YYYYMMCnt = 13;
        else if (thisMonth == "02")
            YYYYMMCnt = 14;
        else
            YYYYMMCnt = 4;
        for (int i = 0; i < YYYYMMCnt; i++)
        {
            string YYYYMM = DateTime.Now.AddMonths(-1 * i).ToString("yyyy-MM");
            ListItem Item = new ListItem(YYYYMM, YYYYMM);
            ddlYYYYMM.Items.Insert(i, Item);
        }

        this.ddlYYYYMM.SelectedValue = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
    }

    // 검색 버튼 클릭 시
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SetList();
    }

    /// <summary>
    /// 사원정보 및 그리드 바인딩
    /// </summary>
    private void SetList()
    {
        float sum1 = 0, sum2 = 0, sum3 = 0;

        DataSet ds = null;
        ds = Select_List();

        if (ds.Tables[0].Rows.Count > 0)
        {
            //사원정보
            this.lbIWORKGROUP2.Text = ds.Tables[0].Rows[0]["WORKGROUP2"].ToString();
            this.lbIAREA.Text = ds.Tables[0].Rows[0]["AREA"].ToString();
            this.lbIBANK_NM.Text = ds.Tables[0].Rows[0]["BANK_NM"].ToString();
            this.lbIACCT_NO.Text = ds.Tables[0].Rows[0]["ACCT_NO"].ToString();
            
            this.lbIEMP_CD.Text = ds.Tables[0].Rows[0]["EMP_CD"].ToString();
            this.lbIKOR_NM.Text = ds.Tables[0].Rows[0]["KOR_NM"].ToString();
        }

        if (ds.Tables[0].Rows[0]["WORKTYPE"].ToString() == "002")
        {
            if (ds.Tables[4].Rows.Count > 0)
            {
                //지급금액
                this.gvPayDetailList.DataSource = ds.Tables[4];
                this.gvPayDetailList.DataBind();

                for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    if (float.Parse(ds.Tables[4].Rows[i]["PAY_AM"].ToString()) > 0)// 금액이 있는경우
                        sum1 += float.Parse(ds.Tables[4].Rows[i]["PAY_AM"].ToString());
                }

                //공제금액
                this.gvPayDetailList2.DataSource = ds.Tables[5];
                this.gvPayDetailList2.DataBind();

                for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    //if (float.Parse(ds.Tables[3].Rows[i]["PAY_AM"].ToString()) > 0)// 금액이 있는경우
                    sum2 += float.Parse(ds.Tables[5].Rows[i]["PAY_AM"].ToString());
                }

                sum3 = sum1 - sum2;

                this.lblSum1.Text = String.Format("{0:N0}", sum1);
                this.lblSum2.Text = String.Format("{0:N0}", sum2);
                this.lblSum3.Text = String.Format("{0:N0}", sum3);
            }
            else
            {
                this.dvDetailView.Controls.Clear();
                //this.gvPayDetailList.Controls.Clear();
                //this.gvPayDetailList2.Controls.Clear();

                this.lblSum1.Text = "";
                this.lblSum2.Text = "";
                this.lblSum3.Text = "";
            }
        }
        else
        {

            if (ds.Tables[1].Rows.Count > 0)
            {
                //지급금액
                this.gvPayDetailList.DataSource = ds.Tables[2];
                this.gvPayDetailList.DataBind();

                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    if (float.Parse(ds.Tables[2].Rows[i]["PAY_AM"].ToString()) > 0)// 금액이 있는경우
                        sum1 += float.Parse(ds.Tables[2].Rows[i]["PAY_AM"].ToString());
                }

                //공제금액
                this.gvPayDetailList2.DataSource = ds.Tables[3];
                this.gvPayDetailList2.DataBind();

                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    //if (float.Parse(ds.Tables[3].Rows[i]["PAY_AM"].ToString()) > 0)// 금액이 있는경우
                    sum2 += float.Parse(ds.Tables[3].Rows[i]["PAY_AM"].ToString());
                }

                sum3 = sum1 - sum2;

                this.lblSum1.Text = String.Format("{0:N0}", sum1);
                this.lblSum2.Text = String.Format("{0:N0}", sum2);
                this.lblSum3.Text = String.Format("{0:N0}", sum3);
            }
            else
            {
                this.dvDetailView.Controls.Clear();
                //this.gvPayDetailList.Controls.Clear();
                //this.gvPayDetailList2.Controls.Clear();

                this.lblSum1.Text = "";
                this.lblSum2.Text = "";
                this.lblSum3.Text = "";
            }
        }
    }

    #region 목록 조회
    private DataSet Select_List()
    {
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["SQL_CODY"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_SAL_DEAILS_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;

       // Response.Write(Session["sRES_ID"].ToString() + " , " + this.ddlYYYYMM.SelectedValue.Replace("-", "") + " , " + this.ddlTYPE.SelectedValue);

		if (this.Page.Request["RES_ID"] == null)
		{
			ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
			ad.SelectCommand.Parameters.AddWithValue("@IS_SPT", "1");
		}
		else
			ad.SelectCommand.Parameters.AddWithValue("@RES_ID", this.Page.Request["RES_ID"].ToString());
			
        ad.SelectCommand.Parameters.AddWithValue("@SAL_MONTH", this.ddlYYYYMM.SelectedValue.Replace("-",""));
        ad.SelectCommand.Parameters.AddWithValue("@TYPE", this.ddlTYPE.SelectedValue);

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
    
    /// <summary>
    /// 지급상세 바인딩시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPayDetailList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    /// <summary>
    /// 공제상세 바인딩시
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPayDetailList2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[0].Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void ddlYYYYMM_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetList();
    }

    protected void ddlTYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetList();
    }
}