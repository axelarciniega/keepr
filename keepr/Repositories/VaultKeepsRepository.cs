using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep vaultData)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@creatorId, @vaultId, @keepId);

            SELECT
            vaultkeeps.*,
            accounts.*
            FROM vaultkeeps
            JOIN accounts ON accounts.id = vaultkeeps.creatorId
            WHERE vaultkeeps.id = LAST_INSERT_ID()
            ;";
            VaultKeep newVaultKeep = _db.Query<VaultKeep, Account, VaultKeep>(sql, (vaultkeep, account) =>
            {
                vaultkeep.Creator = account;
                return vaultkeep;
            }, vaultData).FirstOrDefault();
            return newVaultKeep;
        }
    }
}