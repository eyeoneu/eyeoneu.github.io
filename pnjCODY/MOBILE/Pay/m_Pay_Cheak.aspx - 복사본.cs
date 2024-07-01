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
        if (Session["sRES_ID"] == null)
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
        string lastMonth = DateTime.Now.AddMonths(-2).ToString("yyyy-MM");
        string preMonth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
        string thisMonth = DateTime.Now.ToString("yyyy-MM");

        ListItem firstItem = new ListItem(thisMonth, thisMonth);
        ddlYYYYMM.Items.Insert(0, firstItem);

        ListItem Item = new ListItem(preMonth, preMonth);
        ddlYYYYMM.Items.Insert(1, Item);

        ListItem Item2 = new ListItem(lastMonth, lastMonth);
        ddlYYYYMM.Items.Insert(2, Item2);
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

        if (Session["sRES_ID"] != null)
        {
            DataSet ds = null;
            ds = Select_List();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //사원정보
                this.lbIWORKGROUP2.Text = ds.Tables[0].Rows[0]["WORKGROUP2"].ToString();
                this.lbIAREA.Text = ds.Tables[0].Rows[0]["AREA"].ToString();
                this.lbIBANK_NM.Text = ds.Tables[0].Rows[0]["BANK_NM"].ToString();
                this.lbIACCT_NO.Text = ds.Tables[0].Rows[0]["ACCT_NO"].ToString();
            }

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

        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", Session["sRES_ID"].ToString());
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