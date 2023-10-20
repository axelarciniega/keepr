using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("api/vaults")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly Auth0Provider _auth0;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth0)
        {
            _vaultsService = vaultsService;
            _auth0 = auth0;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                vaultData.CreatorId = userInfo.Id;
                Vault newVault = _vaultsService.CreateVault(vaultData);
                return Ok(newVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }







    }
}