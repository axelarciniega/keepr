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

        internal Vault CreateVault(Vault vaultData)
        {
            return _repo.CreateVault(vaultData);
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
            // NOTE Bring in this in later
            // if (vault.CreatorId != userInfo) throw new Exception("Unauthorized");
            return vault;
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
            _repo.DeleteVault(vaultId);
            if (foundVault.CreatorId != userInfo) throw new Exception("Unauthorized");
            return $"{foundVault.Name} was removed";

        }






    }
}