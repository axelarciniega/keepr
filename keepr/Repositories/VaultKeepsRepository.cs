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

        internal List<KeepModelView> GetKeepsInVault(int vaultId)
        {
            string sql = @"
                SELECT 
                vaultkeeps.*,
                keeps.*,
                accounts.* 
                FROM vaultkeeps
                JOIN accounts ON accounts.id = vaultkeeps.creatorId
                JOIN keeps ON vaultkeeps.keepId = keeps.Id
                WHERE vaultkeeps.vaultId = @vaultId
            ;";
            List<KeepModelView> foundVault = _db.Query<VaultKeep, KeepModelView, Account, KeepModelView>(sql, (vaultkeep, keepModel, account) =>
            {
                keepModel.vaultKeepId = vaultkeep.Id;
                keepModel.Creator = account;
                return keepModel;
            }, new { vaultId }).ToList();
            return foundVault;
        }

        internal VaultKeep GetVaultKeep(int vaultKeepId)
        {
            string sql = @"
            SELECT
            vaultkeeps.*,
            accounts.*
            FROM vaultkeeps
            JOIN accounts ON accounts.id = vaultkeeps.creatorId
            WHERE vaultkeeps.id = @vaultKeepId
            ;";
            VaultKeep foundVaultKeep = _db.Query<VaultKeep, Account, VaultKeep>(sql, (vaultkeep, account) =>
            {
                vaultkeep.Creator = account;
                return vaultkeep;
            }, new { vaultKeepId }).FirstOrDefault();
            return foundVaultKeep;
        }

        internal void Delete(int vaultKeepId)
        {
            string sql = @"
                DELETE FROM vaultkeeps WHERE id = @vaultKeepId
            ;";
            int rowsAffected = _db.Execute(sql, new { vaultKeepId });
            if (rowsAffected > 1) throw new Exception("Deleted everything");
            if (rowsAffected < 1) throw new Exception("Nothing was deleted");

        }
    }
}