﻿using BusinessObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Security.Claims;

namespace CusomterManager.Controllers.Base
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ODataRouteComponent("odata")]
    public class BaseController : ODataController
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public BusinessObject.Models.User GetLoggedInUser()
        {
            var user = this.User;
            string username = user.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value
                ?? throw new CustomerManagementException(4010);
            bool isAdmin = bool.TryParse(user.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value, out isAdmin);

            return new()
            {
                Username = username,
                IsAdmin = isAdmin
            };
        }
    }
}
