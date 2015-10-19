using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


#region
using eRestaurantSystem.BLL; //controller
using eRestaurantSystem.Entities; //entity
using EatIn.UI; //messageusercontrol
#endregion
public partial class CommandPages_WaiterAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //initialize the date hired to today
        DateHired.Text = DateTime.Today.ToShortDateString();
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }


    protected void FetchWaiter_Click(object sender, EventArgs e)
    {
        if (WaiterList.SelectedIndex == 0)
        {
            MessageUserControl.ShowInfo("Please select a waiter before clicking Fetch Waiter");
        }
        else
        {
            //we will use a TryRun() from the messageUserControl
            //this will capture error messages when/if they happen
            //and properly display in the user control
            //GetWaiterInfo is your method for accessing BLL and query
            MessageUserControl.TryRun((ProcessRequest)GetWaiterInfo);
        }
    }
        public void GetWaiterInfo()
        {
            //a standard look up sequence 
            AdminController sysmgr = new AdminController();
            var waiter = sysmgr.GetWaiterByID(int.Parse(WaiterList.SelectedValue));

            WaiterID.Text = waiter.WaiterID.ToString();
            FirstName.Text = waiter.FirstName;
            LastName.Text = waiter.LastName;
            Phone.Text = waiter.Phone;
            Address.Text = waiter.Address;
            DateHired.Text = waiter.HireDate.ToShortDateString();
            if(waiter.ReleaseDate.HasValue)
            {
                DateReleased.Text = waiter.ReleaseDate.ToString();
            }

        }


        protected void InsertWaiter_Click(object sender, EventArgs e)
        {
            //this example is using the TryRun in line
            MessageUserControl.TryRun(() =>
                {
                    Waiter item = new Waiter();
                    item.FirstName = FirstName.Text;
                    item.LastName = LastName.Text;
                    item.Phone = Phone.Text;
                    item.Address = Address.Text;
                    item.HireDate = DateTime.Parse(DateHired.Text);
                    item.ReleaseDate = null;
                    AdminController sysmgr = new AdminController();
                    WaiterID.Text = sysmgr.Waiters_Add(item).ToString();
                    MessageUserControl.ShowInfo("Waiter Added.");
                }
            );
        }
}