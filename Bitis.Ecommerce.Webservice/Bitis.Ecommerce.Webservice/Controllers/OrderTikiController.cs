using Bitis.Ecommerce.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitis.Ecommerce.Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderTikiController : ControllerBase
    {
        protected OrderTikiService Service { get; }
        public OrderTikiController(OrderTikiService orderService)
        {
            Service = orderService;
        }
    }
}
