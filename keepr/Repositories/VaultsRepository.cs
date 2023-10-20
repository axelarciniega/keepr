using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
        INSERT INTO vaults
        (name, description, img, isPrivate, creatorId)
        VALUES
        (@name, @description, @img, @isPrivate, @creatorId);

        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID()
        ;";
            Vault newVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            }, vaultData).FirstOrDefault();
            return newVault;
        }

    }
}