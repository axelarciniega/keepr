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
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultsController(VaultsService vaultsService, Auth0Provider auth0, VaultKeepsService vaultKeepsService)
        {
            _vaultsService = vaultsService;
            _auth0 = auth0;
            _vaultKeepsService = vaultKeepsService;
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

        [HttpGet("{vaultId}/keeps")]
        public ActionResult<List<VaultKeep>> GetKeepsInVault(int vaultId)
        {
            try
            {
                List<VaultKeep> foundVaultKeep = _vaultKeepsService.GetKeepsInVault(vaultId);
                return Ok(foundVaultKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{vaultId}")]
        public async Task<ActionResult<Vault>> GetById(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                //TODO bring in userInfo?.Id if needed here
                Vault foundVault = _vaultsService.GetById(vaultId);
                return Ok(foundVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{vaultId}")]
        public async Task<ActionResult<Vault>> Edit([FromBody] Vault updateData, int vaultId)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                updateData.Id = vaultId;
                Vault edited = _vaultsService.Edit(updateData, userInfo.Id);
                return Ok(edited);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{vaultId}")]
        public async Task<ActionResult<Vault>> DeleteVault(int vaultId)
        {
            try
            {
                Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
                string message = _vaultsService.DeleteVault(vaultId, userInfo.Id);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }








    }
}