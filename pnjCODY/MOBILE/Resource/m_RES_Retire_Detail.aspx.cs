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

public partial class m_RES_Retire_Detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetgvResList();
        }
    }

    // 그리드뷰 DataBound
    private void SetgvResList()
    {
        if (Session["sRES_ID"] != null)
        {

            Resource resource = new Resource();

            SqlDataReader rd = resource.EPM_RES_DETAIL_SELECT_MOBILE(int.Parse(this.Page.Request["RES_ID"].ToString()));

            if (rd.Read())
            {
                this.lblRES_Name.Text = rd["RES_Name"].ToString();
                this.lblRES_PersonNumber.Text = rd["RES_PersonNumber"].ToString();
                this.lblRES_Number.Text = rd["RES_Number"].ToString();
                this.lblRES_WorkState.Text = rd["RES_WorkState"].ToString();
            }
            
            rd.Close();


			DataSet ds = Select_List(this.Page.Request["RES_ID"].ToString());

			if(ds != null)
			{

				if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
				{
					this.lblJoin.Text = ds.Tables[0].Rows[0]["RES_JoinDate"].ToString();
					this.lblRetire.Text = ds.Tables[0].Rows[0]["RES_RetireDate"].ToString();
				}

				if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
				{
					DataTable dtMain = new DataTable();
					DataTable dtSub = null;

					dtMain = ds.Tables[1];
					dtMain.Columns.Add("Sub", typeof(DataTable));

					foreach (DataRow dtMainRow in dtMain.Rows)
					{
						DataRow[] dr = ds.Tables[2].Select(string.Format("YYYY='{0}'", dtMainRow["YYYY"].ToString()));

						if (dr.Length > 0)
						{
							dtSub = ds.Tables[2].Clone();

							for (int i = 0; i < dr.Length; i++)
								dtSub.Rows.Add(dr[i].ItemArray);
						}
						dtMainRow["Sub"] = dtSub;
					}


					ListView2.DataSource = dtMain;
					ListView2.DataBind();
				}

                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    lblBank.Text = ds.Tables[3].Rows[0]["BANK_NAME"].ToString();
                    lblAmount.Text = string.Format("\\{0:N0}", decimal.Parse(ds.Tables[3].Rows[0]["REPAY_AMA"].ToString()));
                }
            }

        }
    }

    #region 목록 조회
    private DataSet Select_List(string resID)
    {
        //SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["EPMConnLive"].ToString());
        SqlConnection Con = new SqlConnection(ConfigurationManager.ConnectionStrings["CODY_CommConnectionString1"].ToString());
        
        SqlCommand Cmd = new SqlCommand("EPM_RES_RETIRE_DEAILS_MOBILE");
        Cmd.Connection = Con;
        Cmd.CommandType = CommandType.StoredProcedure;
        Cmd.Parameters.AddWithValue("@RES_ID", int.Parse(resID));
        Cmd.CommandTimeout = 60;


        SqlDataAdapter adp = new SqlDataAdapter(Cmd);
        DataSet ds = new DataSet();

        try
        {
            adp.Fill(ds);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        Con.Close();

        /*
        SqlConnection Con = new SqlConnection(ConfigurationManager.AppSettings["EPMConnLive"].ToString());
        SqlDataAdapter ad = new SqlDataAdapter("EPM_RES_RETIRE_DEAILS_MOBILE", Con);
        ad.SelectCommand.CommandType = CommandType.StoredProcedure;
        ad.SelectCommand.Parameters.AddWithValue("@RES_ID", resID);

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
        */
        
        return ds;
    }
    #endregion

    protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem dataitem = (ListViewDataItem)e.Item;

            int worktime = Convert.ToInt32(DataBinder.Eval(dataitem.DataItem, "WORK_TIME_INT").ToString());
            if (worktime < 60)
            {
                HtmlTableRow cell = (HtmlTableRow)e.Item.FindControl("MainTableRow");
                cell.BgColor = "LightYellow";
            }
        }
    }
}