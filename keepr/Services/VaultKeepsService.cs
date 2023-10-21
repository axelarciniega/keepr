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
    }
}