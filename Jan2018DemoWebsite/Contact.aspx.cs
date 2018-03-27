using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region More Namespaces
using AppSecurity.BLL;
using AppSecurity.DAL;
using AppSecurity.Entities;
using Chinook.Data.POCOs;
using Microsoft.AspNet.Identity.EntityFramework;
#endregion

namespace Jan2018DemoWebsite
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Request.IsAuthenticated)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    if (!User.IsInRole(SecurityRoles.Staff))
                    {
                        Response.Redirect("~/Account/Login.aspx");
                    }
                }
            }
        }

        protected void GetUsername_Click(object sender, EventArgs e)
        {
            // Pull username
            string username = User.Identity.Name;
            Username.Text = username;

            // Pull employee info for this user
            MessageUserControl.TryRun(() =>
            {
                ApplicationUserManager secmgr = 
                new ApplicationUserManager(
                    new UserStore<ApplicationUser>(
                        new ApplicationDbContext()));

                EmployeeInfo info = secmgr.User_GetEmployee(username);
                EmployeeID.Text = info.EmployeeID.ToString();
                EmployeeName.Text = info.FullName;
            });
        }
    }
}