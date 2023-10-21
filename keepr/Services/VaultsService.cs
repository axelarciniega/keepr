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

        internal Vault GetById(int vaultId)
        {
            Vault foundVault = _repo.GetById(vaultId);
            if (foundVault == null) throw new Exception($"invalid id at {vaultId}");
            return foundVault;
        }

        internal Vault Edit(Vault updateData, string userInfo)
        {
            Vault original = this.GetById(updateData.Id);
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
            Vault foundVault = this.GetById(vaultId);
            _repo.DeleteVault(vaultId);
            if (foundVault.CreatorId != userInfo) throw new Exception("Unauthorized");
            return $"{foundVault.Name} was removed";

        }






    }
}