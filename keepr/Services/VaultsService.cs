using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault CreateVault(Vault vaultData, string userInfo)
        {
            Vault newVault = _repo.CreateVault(vaultData);
            if (newVault.CreatorId != userInfo) throw new Exception("Unauthorized");
            return newVault;
        }

        internal Vault GetById(int vaultId, string userInfo)
        {
            Vault foundVault = _repo.GetById(vaultId);
            if (foundVault == null) throw new Exception($"invalid id at {vaultId}");
            if (foundVault.IsPrivate == true && foundVault.CreatorId != userInfo) throw new Exception("this vault is private");
            return foundVault;
        }

        internal List<Vault> GetVaultsAccount(string userInfo)
        {
            List<Vault> vault = _repo.GetVaultsAccount(userInfo);
            return vault;
        }

        // STUB this one starts from the profile controller
        internal List<Vault> GetProfileVaults(string profileId, string userId)
        {
            List<Vault> vaults = _repo.GetProfileVaults(profileId);
            vaults = vaults.FindAll(vault => vault.IsPrivate == false || vault.CreatorId == userId);
            return vaults;
        }


        internal Vault Edit(Vault updateData, string userInfo)
        {
            Vault original = this.GetById(updateData.Id, userInfo);
            if (original.CreatorId != userInfo) throw new Exception("unauthorized to edit");

            original.Name = updateData.Name ?? original.Name;
            original.Description = updateData.Description ?? original.Description;
            original.Img = updateData.Img ?? original.Img;
            original.IsPrivate = updateData.IsPrivate;

            Vault vault = _repo.Edit(original);
            return original;

        }

        internal string DeleteVault(int vaultId, string userInfo)
        {
            Vault foundVault = this.GetById(vaultId, userInfo);
            if (foundVault.CreatorId != userInfo) throw new Exception("Unauthorized");
            _repo.DeleteVault(vaultId);
            return $"{foundVault.Name} was removed";

        }






    }
}