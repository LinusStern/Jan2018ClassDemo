using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region More Namespaces
using Chinook.Data.DTOs;
using Chinook.Data.Entities;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        public List<EmployeeClients> Employee_GetClientList()
        {
            using (var context = new ChinookContext())
            {
                var results =
                    from x in context.Employees
                    where x.Title.Contains("Support")
                    orderby x.LastName, x.FirstName
                    select new EmployeeClients
                    {
                        Name = x.LastName + ", " + x.FirstName,
                        ClientCount = x.Customers.Count(),
                        ClientList =
                            from y in x.Customers
                            orderby y.LastName, y.FirstName
                            select new ClientInfo
                            {
                                Client = y.LastName + ", " + y.FirstName,
                                Phone = y.Phone
                            }
                    };
                return results.ToList();
            }
        }
    }
}
