using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep Create(VaultKeep vaultData)
        {
            return _repo.Create(vaultData);
        }

        internal List<VaultKeep> GetKeepsInVault(int vaultId)
        {
            List<VaultKeep> foundVault = _repo.GetKeepsInVault(vaultId);
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
            if (foundVaultKeep.CreatorId != userInfo) throw new Exception("Unauthorized");
            return $"{foundVaultKeep.Id} was removed";
        }
    }
}