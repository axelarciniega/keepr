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

        internal Vault GetById(int vaultId)
        {
            string sql = @"
            SELECT
            vaults.*,
            accounts.*
            FROM vaults
            JOIN accounts ON accounts.id = vaults.creatorId
            WHERE vaults.id = @vaultId
            ;";
            Vault foundVault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            }, new { vaultId }).FirstOrDefault();
            return foundVault;
        }

        internal List<Vault> GetVaultsAccount(string userId)
        {
            string sql = @"
            SELECT 
            *
            FROM vaults
            WHERE creatorId = @userId
            ;";
            List<Vault> vault = _db.Query<Vault>(sql, new { userId }).ToList();
            return vault;
        }

        internal List<Vault> GetProfileVaults(string profileId)
        {
            string sql = @"
            SELECT
            vaults.*,
            accounts.*
            FROM vaults
            JOIN accounts ON accounts.id = vaults.creatorId
            WHERE vaults.creatorId = @profileId
            ;";
            List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            }, new { profileId }).ToList();
            return vaults;
        }

        internal Vault Edit(Vault updateData)
        {
            string sql = @"
                UPDATE vaults
                SET
                name = @name,
                description = @description,
                img = @img,
                isPrivate = @isPrivate
                WHERE id = @id;
            ;";
            Vault edit = _db.Query<Vault>(sql, updateData).FirstOrDefault();
            return edit;
        }

        internal void DeleteVault(int vaultId)
        {
            string sql = @"
            DELETE FROM vaults WHERE id = @vaultId
            ;";
            int rowsAffected = _db.Execute(sql, new { vaultId });
            if (rowsAffected > 1) throw new Exception("Deleted everything");
            if (rowsAffected < 1) throw new Exception("Nothing got deleted");
        }

    }
}