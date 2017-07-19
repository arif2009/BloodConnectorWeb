using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BloodConnector.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/values")]
    [EnableCors("http://localhost:14717,http://localhost:2102", "*", "*")]
    public class OrdersController : ApiController
    {
        // GET api/orders
        public IHttpActionResult Get()
        {
            //return new string[] { "value1", "value2" };

            //ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

            //var Name = ClaimsPrincipal.Current.Identity.Name;
            //var Name1 = User.Identity.Name;

            //var userName = principal.Claims.Where(c => c.Type == "sub").Single().Value;

            return Ok(Order.CreateOrders());
        }

        #region Helpers

        public class Order
        {
            public int OrderID { get; set; }
            public string CustomerName { get; set; }
            public string ShipperCity { get; set; }
            public Boolean IsShipped { get; set; }


            public static List<Order> CreateOrders()
            {
                List<Order> OrderList = new List<Order>
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };

                return OrderList;
            }
        }

        #endregion
    }
}