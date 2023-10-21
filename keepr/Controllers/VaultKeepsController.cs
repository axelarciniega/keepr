using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth0;

        public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0)
        {
            _vaultKeepsService = vaultKeepsService;
            _auth0 = auth0;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

                vaultKeepData.CreatorId = userInfo.Id;

                VaultKeep newVaultKeep = _vaultKeepsService.Create(vaultKeepData);
                return Ok(newVaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}