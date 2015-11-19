using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region
using eRestaurantSystem.BLL;
#endregion

public partial class UserControls_DateTimeMocker : System.Web.UI.UserControl
{

    //create two properties that will allow external users of this control to have access 
    //to the data
    //(date and time) on this control.
    public DateTime MockDate
    {
        get
        { 
            //setup a variable to hold the date
            //this variable will be initialised to a default
            DateTime date = DateTime.MinValue;

            //possibly override the default date with the contents of the web control SearchDate
            DateTime.TryParse(SearchDate.Text, out date);

            //return the datevalue
            return date;
        }
        set 
        {
            SearchDate.Text = value.ToString("YYYY-MM-DD");
        }
    }

    public TimeSpan MockTime
    {
        get
        {
            //setup a variable to hold the time
            //this variable will be initialised to a default
            TimeSpan time = TimeSpan.MinValue;

            //possibly override the default time with the contents of the web control SearchDate
            TimeSpan.TryParse(SearchTime.Text, out time);

            //return the datevalue
            return time;
        }
        set
        {
            SearchTime.Text = DateTime.Today.Add(value).ToString("HH:mm:ss");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        AdminController sysmgr = new AdminController();
        DateTime info = sysmgr.GetLastBillDateTime();
        SearchDate.Text = info.ToString("yyyy-MM-dd");
        SearchTime.Text = info.ToString("HH:mm");

    }
}