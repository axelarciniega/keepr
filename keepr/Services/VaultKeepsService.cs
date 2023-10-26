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
        private readonly KeepsService _keepsService;
        private readonly KeepsRepository _repoKeep;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService, KeepsRepository repoKeep)
        {
            _repo = repo;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
            _repoKeep = repoKeep;
        }

        internal VaultKeep Create(VaultKeep vaultData, bool increaseKepts = false)
        {

            VaultKeep newVaultKeep = _repo.Create(vaultData);

            Vault vault = _vaultsService.GetById(vaultData.VaultId, vaultData.CreatorId);

            if (vault.CreatorId != vaultData.CreatorId) throw new Exception("Unauthorized");

            Keep foundKeep = _keepsService.GetById(vaultData.KeepId, vaultData.CreatorId);
            if (foundKeep.CreatorId == newVaultKeep.CreatorId)
            {
                this.IncreaseKepts(foundKeep);
            };

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

        internal void IncreaseKepts(Keep keep)
        {
            keep.Kept++;
            _repoKeep.Update(keep);
        }

    }
}