using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep Create(VaultKeep vaultData)
        {

            VaultKeep newVaultKeep = _repo.Create(vaultData);

            Vault vault = _vaultsService.GetById(vaultData.VaultId, vaultData.CreatorId);

            if (vault.CreatorId != vaultData.CreatorId) throw new Exception("Unauthorized");



            return newVaultKeep;
        }

        internal List<KeepModelView> GetKeepsInVault(int vaultId, string userInfo)
        {
            Vault vault = _vaultsService.GetById(vaultId, userInfo);
            List<KeepModelView> foundVault = _repo.GetKeepsInVault(vaultId);
            return foundVault;
        }

        internal VaultKeep GetVaultKeep(int vaultKeepId)
        {
            VaultKeep foundVaultKeep = _repo.GetVaultKeep(vaultKeepId);
            if (foundVaultKeep == null) throw new Exception($"invalid id at {vaultKeepId}");
            return foundVaultKeep;
        }

        internal string Delete(int vaultKeepId, string userInfo)
        {
            VaultKeep foundVaultKeep = this.GetVaultKeep(vaultKeepId);
            _repo.Delete(vaultKeepId);
            if (foundVaultKeep.CreatorId != userInfo) throw new Exception("Unauthorized");
            return $"{foundVaultKeep.Id} was removed";
        }
    }
}