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

                VaultKeep newVaultKeep = _vaultKeepsService.Create(vaultKeepData, userInfo?.Id);
                return Ok(newVaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultKeepId}")]
        public async Task<ActionResult<VaultKeep>> GetVaultKeep(int vaultKeepId)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                VaultKeep foundVaultKeep = _vaultKeepsService.GetVaultKeep(vaultKeepId);
                return Ok(foundVaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }

        [Authorize]
        [HttpDelete("{vaultKeepId}")]
        public async Task<ActionResult<VaultKeep>> Delete(int vaultKeepId)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultKeepsService.Delete(vaultKeepId, userInfo?.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}