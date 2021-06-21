using Bitis.Ecommerce.Domain.Service;
using Bitis.Ecommerce.Webservice.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitis.Ecommerce.Webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TikiController : ControllerBase
    {
        [HttpPost("[action]")]
        public TikiIdentityResponse Login(TikiIdentityRequest request)
        {
            return (new WebProvider()).PostWebApi<TikiIdentityResponse>(
                "https://api.tiki.vn/sc/oauth2/token", request);
        }
    }
}
