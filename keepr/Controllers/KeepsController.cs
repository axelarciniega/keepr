using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly Auth0Provider _auth0;

        public KeepsController(KeepsService keepsService, Auth0Provider auth)
        {
            _keepsService = keepsService;
            _auth0 = auth;
        }
    }
}